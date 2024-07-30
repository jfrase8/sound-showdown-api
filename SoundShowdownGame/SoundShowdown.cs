using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace SoundShowdownGame
{
    public class SoundShowdown
    {
        public List<Player> PlayerList { get; private set; } // List of players in the game
        public Deck<Enemy> EnemyDeck { get; private set; } // Deck of enemies
        public Deck<EventCard> EventDeck { get; private set; } // Deck of events
        public Shop GameShop { get; private set; } // The Shop for the game
        public GameState CurrentGameState { get; private set; }
        private int EnemiesDefeated { get; set; } = 0;
        private Enemy? CurrentEnemy { get; set; }
        private EventCard? CurrentEventCard { get; set; }

        // Events
        public event EventHandler<SoundShowdownEventArgs>? SoundShowdownEvent;

        public SoundShowdown(List<string> playerIds, Deck<Enemy> enemyDeck, Deck<EventCard> eventDeck)
        {
            PlayerList = playerIds.Select(playerId => new Player(playerId)).ToList();
            EnemyDeck = enemyDeck;
            EventDeck = eventDeck;
            CurrentGameState = GameState.Awaiting_Player_Choose_Genre;
            GameShop = new(
                InstrumentDeckFactory.CreatedShuffledExoticInstrumentDeck(),
                InstrumentDeckFactory.CreatedShuffledHighInstrumentDeck(),
                InstrumentDeckFactory.CreatedShuffledGoodInstrumentDeck(),
                InstrumentDeckFactory.CreatedShuffledLowInstrumentDeck(),
                [new Item(ItemName.Food, "Heals you", 10), new Item(ItemName.Antidote, "Gets rid of all poison counters", 10)]
            );
        }

        public SoundShowdown(List<Player> players, Deck<Enemy> enemyDeck, Deck<EventCard> eventDeck, GameState currentGameState, int enemiesDefeated, Enemy? currentEnemy, Shop gameShop)
        {
            PlayerList = players;
            EnemyDeck = enemyDeck;
            EventDeck = eventDeck;
            CurrentGameState = currentGameState;
            EnemiesDefeated = enemiesDefeated;
            CurrentEnemy = currentEnemy;
            GameShop = gameShop;
        }

        public void PlayerChooseGenre(string playerId, GenreName genreName)
        {
            Player player = ValidatePlayer(playerId);
            ValidateGameState(GameState.Awaiting_Player_Choose_Genre);

            // Set player's genre
            player.Genre = genreName;

            // Send event to all players (NOT BEING IMPLEMENTED HERE)
            SoundShowdownEvent?.Invoke(this, new GenreChosenEvent(player, genreName));

            // Check if all players have chose a genre
            if (PlayerList.All(p => p.Genre != null))
            {
                CurrentGameState = GameState.Awaiting_Player_Choose_Action;
            }
            OnEndOfTurn();
        }
        public Player GetTurnPlayer()
        {
            return PlayerList[0];
        }

        public void PlayerChooseAction(string playerId, Action action)
        {
            // Validate player and game state
            Player player = ValidatePlayer(playerId);
            ValidateGameState(GameState.Awaiting_Player_Choose_Action);

            switch (action)
            {
                case Action.Fight_Enemies:
                    // Get the drawn enemy card
                    CurrentEnemy = DrawEnemyCard(player);
                    // Throw event
                    SoundShowdownEvent?.Invoke(this, new ActionChosenEvent(player, action));
                    break;
                case Action.Build_Upgrades:
                    CurrentGameState = GameState.Awaiting_Player_Choose_Upgrade;
                    SoundShowdownEvent?.Invoke(this, new ActionChosenEvent(player, action));
                    break;
                //case Action.ChallengeMusician:
                //    ChallengeMusician();
                //    break;
                case Action.Train:
                    CurrentGameState = GameState.Awaiting_Player_Roll_For_Training;
                    SoundShowdownEvent?.Invoke(this, new ActionChosenEvent(player, action));
                    break;
                case Action.Shop:
                    CurrentGameState = GameState.Awaiting_Player_Shop;
                    SoundShowdownEvent?.Invoke(this, new ActionChosenEvent(player, action));
                    break;
                case Action.Scavenge:
                    CurrentGameState = GameState.Awaiting_Player_Roll_Scavenge_Dice;
                    SoundShowdownEvent?.Invoke(this, new ActionChosenEvent(player, action));
                    break;
            }
        }

        public void RollScavengeDice(string playerId)
        {
            // Validations
            Player player = ValidatePlayer(playerId);
            ValidateGameState(GameState.Awaiting_Player_Roll_Scavenge_Dice);

            // Get the 4 dice rolls
            List<int> rolls = [];
            for (int i = 0; i < 4; i++) rolls.Add(Dice.RollDie());

            // Draw an event card if the player rolled a 1
            if (rolls.Contains(1))
            {
                CurrentEventCard = EventDeck.Draw();
            }
            else // If player didnt roll an event marker, add the resources to their inventory immediately
            {
                foreach (int roll in rolls) player.Inventory += GlobalData.ScavengeDiceRolls[roll];
            }

            // Send results
            SoundShowdownEvent?.Invoke(this, new RolledScavengeDiceEvent(player, rolls, CurrentEventCard));
        }

        public void TradeWithTrader(ResourceName[] fourResources, ResourceName newResource, string playerId)
        {
            // Validations
            Player player = ValidatePlayer(playerId);
            ValidateGameState(GameState.Awaiting_Player_Shop);
            player.Inventory.ValidateHasResources(fourResources);

            foreach (var resource in fourResources)
            {
                player.Inventory -= resource;
            }

            player.Inventory += newResource;

            SoundShowdownEvent?.Invoke(this, new TradeWithTraderEvent(player, fourResources, newResource));
        }

        public void BuyItem(Item item, string playerId)
        {
            // Validations
            Player player = ValidatePlayer(playerId);
            ValidateGameState(GameState.Awaiting_Player_Shop);
            if (player.Inventory.Coins < item.Price) // Validate player has enough coins
                throw new SoundShowdownException("Player does not have enough coins to buy an item");

            if (player.Inventory.GetItemCount() == 5) // Validate player has inventory space
                throw new SoundShowdownException("Player does not have any more inventory space for items.");

            // Buy the item
            player.Inventory.Coins -= item.Price;

            // Gain the item
            if (player.Inventory.Items.TryGetValue(item.Name, out int value))
            {
                player.Inventory.Items[item.Name] = value + 1;
            }
            else player.Inventory.Items[item.Name] = 1;

            // Send event
            SoundShowdownEvent?.Invoke(this, new PlayerBoughtItemEvent(player, item));
        }

        public void BuyInstrument(Instrument instrument, string playerId)
        {
            // Validations
            Player player = ValidatePlayer(playerId);
            ValidateGameState(GameState.Awaiting_Player_Shop);
            player.ValidateInstrumentCost(instrument);

            if (player.Instrument != null) // Do a trade-in
            {
                // Sell current instrument
                player.Inventory.Coins += player.Instrument.Cost / 2 + (player.Instrument.Upgrades.Count * 5);

                // Buy new instrument
                player.Inventory.Coins -= instrument.Cost;

                // Put players current instrument on resale
                GameShop.ResaleInstruments.Add(player.Instrument.CreateResale());

                // Replace player's instrument with new one
                player.Instrument = instrument;
            }
            else // Buy new instrument
            {
                player.Inventory.Coins -= instrument.Cost;
                player.Instrument = instrument;
            }

            SoundShowdownEvent?.Invoke(this, new PlayerBoughtInstrumentEvent(player, instrument, GameShop));
        }

        public void PlayerRolledForTraining(int lastRoll, string playerId)
        {
            // Validations
            Player player = ValidatePlayer(playerId);
            ValidateGameState(GameState.Awaiting_Player_Roll_For_Training);
            if (player.Instrument == null) throw new SoundShowdownException("Player does not have an instrument so they cannot train.");

            int roll = Dice.RollDie();

            // If this is not the players first roll
            if (lastRoll > 0)
            {
                // If the players first and second roll add up to more than 8, they get a damage counter on their instrument
                if (lastRoll + roll > 8 && player.Genre != GenreName.Folk)
                {
                    player.Instrument.DamageCounters++;
                    SoundShowdownEvent?.Invoke(this, new RollForTrainingEvent(player, lastRoll, roll, true));
                }
                else SoundShowdownEvent?.Invoke(this, new RollForTrainingEvent(player, lastRoll, roll, false));

            }
            else SoundShowdownEvent?.Invoke(this, new RollForTrainingEvent(player, roll, 0, false));
        }

        public void AddExperienceToInstrument(int experience, string playerId)
        {
            // Validations
            Player player = ValidatePlayer(playerId);
            if (player.Instrument == null) throw new SoundShowdownException("Player cannot add experience to their instrument because they don't have one.");
            if (player.Instrument.Level == 3) throw new SoundShowdownException("Player instrument is already at level 3.");

            bool leveledUp = false;

            player.Instrument.Experience += experience;
            if (player.Instrument.Experience >= 10)
            {
                player.Instrument.Level++;
                leveledUp = true;
                if (player.Instrument.Level != 3) player.Instrument.Experience -= 10;

                CurrentGameState = GameState.Awaiting_Player_Choose_Technique;
            }

            SoundShowdownEvent?.Invoke(this, new AddedInstrumentExpEvent(player, experience, player.Instrument.Level, player.Instrument.Experience, leveledUp));
        }

        public void PlayerChooseTechnique(Upgrade technique, string playerId)
        {
            // Validations
            Player player = ValidatePlayer(playerId);
            ValidateGameState(GameState.Awaiting_Player_Choose_Technique);
            if (player.Instrument == null) throw new SoundShowdownException("Player cannot choose a technique because they don't have an instrument.");

            player.Instrument.Techniques.Add(technique);
            SoundShowdownEvent?.Invoke(this, new TechniqueLearnedEvent(player, technique));
        }

        public void PlayerChooseUpgrade(Upgrade upgrade, string playerId)
        {
            // Validations
            Player player = ValidatePlayer(playerId);
            ValidateGameState(GameState.Awaiting_Player_Choose_Upgrade);     
            player.Inventory.ValidateInventory(InventoryType.Resource, upgrade); // Validates the player has the necessary resources
            if (upgrade.Type != UpgradeType.Suit && upgrade.Type != UpgradeType.Accessory) // If the player is trying to build an instrument upgrade, validate the player has an instrument
            {
                if (player.Instrument == null) throw new SoundShowdownException("Player does not have an instrument and cannot get an instrument upgrade.");
            }
            if (upgrade.Type ==  UpgradeType.Instrument_Type) // If the upgrade is instrument specific, check the player has the correct instrument type 
            {
                if (player.Instrument.Type != upgrade.InstrumentType) 
                    throw new SoundShowdownException($"Upgrade Instrument Type does not match player instrument type: Upgrade - {upgrade.InstrumentType}, Player - {player.Instrument.Type}");
            }

            bool hasSpace = player.CheckUpgradeSpace(upgrade, this); // Checks if the player has space for the upgrade
            if (hasSpace)
            {
                player.AddUpgrade(upgrade);
                SoundShowdownEvent?.Invoke(this, new UpgradeBuiltEvent(player, upgrade));
            }
            else
            {
                CurrentGameState = GameState.Awaiting_Player_Replace_Upgrade;
                SoundShowdownEvent?.Invoke(this, new ChooseUpgradeToReplaceEvent(player, upgrade));
            }
        }

        public void PlayerReplaceUpgrade(Upgrade newUpgrade, Upgrade replacedUpgrade, string playerId)
        {
            // Validations
            Player player = ValidatePlayer(playerId);
            ValidateGameState(GameState.Awaiting_Player_Replace_Upgrade);
            player.ValidatePlayerHasUpgrade(replacedUpgrade);

            player.ReplaceUpgrade(newUpgrade, replacedUpgrade);

            // Player can now choose a resource to get back from the replaced upgrade
            CurrentGameState = GameState.Awaiting_Player_Choose_Scrap_Resource;
            SoundShowdownEvent?.Invoke(this, new UpgradeReplacedEvent(player, newUpgrade, replacedUpgrade));
        }

        public void PlayerCancelledReplaceUpgrade(string playerId)
        {
            // Validations
            Player player = ValidatePlayer(playerId);
            ValidateGameState(GameState.Awaiting_Player_Replace_Upgrade);

            CurrentGameState = GameState.Awaiting_Player_Choose_Upgrade;
            SoundShowdownEvent?.Invoke(this, new BackToChooseUpgradeEvent(player));
        }

        public void PlayerChoseScrapResource(ResourceName resource, string playerId)
        {
            // Validations
            Player player = ValidatePlayer(playerId);
            ValidateGameState(GameState.Awaiting_Player_Choose_Scrap_Resource);

            player.Inventory += resource;

            CurrentGameState = GameState.Awaiting_Player_Choose_Upgrade;
            SoundShowdownEvent?.Invoke(this, new ScrapResourceChosenEvent(player, resource));
        }

        public void PlayerFixInstrument(InstrumentType type, string playerId)
        {
            // Validations
            Player player = ValidatePlayer(playerId);
            ValidateGameState(GameState.Awaiting_Player_Choose_Upgrade);
            player.Inventory.ValidateInventory(InventoryType.Resource, type); // Validates the player has the necessary resources
            if (player.Instrument == null) throw new SoundShowdownException("Player does not have an instrument.");
            if (player.Instrument.DamageCounters < 1) throw new SoundShowdownException("Player cannot fix an instrument that isnt damaged.");

            player.Inventory.FixInstrument(type);
            player.Instrument.DamageCounters--;

            SoundShowdownEvent?.Invoke(this, new FixedInstrumentEvent(player, type));
        }

        public void Attack(string playerId)
        {
            // Validations
            Player player = ValidatePlayer(playerId);
            ValidateGameState(GameState.Awaiting_Player_Attack);
            if (CurrentEnemy == null) throw new SoundShowdownException("Enemy card was not drawn. There is no current enemy.");

            // Attack info object
            AttackInfo attackInfo = new() { Roll = Dice.RollDie() };
            attackInfo.CalcDamage(CurrentEnemy, player);

            // Check if enemy or player was defeated
            CurrentEnemy.TakeDamage(attackInfo.Damage);
            if (CurrentEnemy.IsDefeated)
            {
                attackInfo.BattleResult = BattleWinner.Player;
                EnemiesDefeated++;
            }
            else
            {
                player.TakeDamage(CurrentEnemy.Damage, CurrentEnemy);

                attackInfo.BattleResult = player.IsDefeated ? BattleWinner.Enemy : BattleWinner.None;
            }

            // Set Game state based on battleResult
            CurrentGameState = attackInfo.BattleResult switch
            {
                BattleWinner.Player => GameState.Awaiting_Player_Fight_Or_End_Action,
                BattleWinner.Enemy => GameState.Awaiting_Player_Choose_Action,
                BattleWinner.None => GameState.Awaiting_Player_Attack,
                BattleWinner.Musician => throw new SoundShowdownException("Musician should not have won the battle."),
                _ => throw new SoundShowdownException("Invalid value for BattleResult.")
            };

            SoundShowdownEvent?.Invoke(this, new AttackEvent(player, attackInfo));
        }

        // Called if the player decides to not fight any more enemies
        public void EndFightAction(string playerId)
        {
            // Validations
            Player player = ValidatePlayer(playerId);
            ValidateGameState(GameState.Awaiting_Player_Fight_Or_End_Action);

            // Player adds accumulated resources to inventory
            player.Inventory.GainResources();

            OnEndOfTurn();
        }

        private void OnEndOfTurn()
        {
            // Set new turn order
            Player currentPlayer = PlayerList[0];
            PlayerList.RemoveAt(0);
            PlayerList.Add(currentPlayer);

            // Reset values
            CurrentEnemy = null;
            EnemiesDefeated = 0;

            StartNewTurn();
        }

        private void StartNewTurn()
        {
            // TODO : Switch turn to new player
        }

        private Player ValidatePlayer(string playerId)
        {
            // Find player. If null, throw SoundShowdownException
            Player player = PlayerList.Find(p => p.Id == playerId) ?? throw new SoundShowdownException($"Player not found: {playerId}.");

            // Check if its players turn
            Player turnPLayer = GetTurnPlayer();
            if (turnPLayer != player) throw new SoundShowdownException($"It is not this players turn: {playerId}.");

            return turnPLayer;
        }
        private void ValidateGameState(GameState validGameState)
        {
            if (validGameState != CurrentGameState) throw new SoundShowdownException($"The game is not in required game state: {validGameState}");
        }

        private Enemy DrawEnemyCard(Player player)
        {
            // Set the game state
            CurrentGameState = GameState.Awaiting_Player_Attack;

            // Draw an enemy from the enemies deck
            Enemy enemy = EnemyDeck.Draw();
            enemy.AttackingPlayer = player;

            return enemy;
        }
    }
}
