﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
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
        public GameState CurrentGameState { get; private set; }
        private int EnemiesDefeated { get; set; } = 0;
        private Enemy? CurrentEnemy { get; set; }

        // Events
        public event EventHandler<SoundShowdownEventArgs>? SoundShowdownEvent;

        public SoundShowdown(List<string> playerIds, Deck<Enemy> enemyDeck)
        {
            PlayerList = playerIds.Select(playerId => new Player(playerId)).ToList();
            EnemyDeck = enemyDeck;
            CurrentGameState = GameState.Awaiting_Player_Choose_Genre;
        }

        public SoundShowdown(List<Player> players, Deck<Enemy> enemyDeck, GameState currentGameState, int enemiesDefeated, Enemy? currentEnemy)
        {
            PlayerList = players;
            EnemyDeck = enemyDeck;
            CurrentGameState = currentGameState;
            EnemiesDefeated = enemiesDefeated;
            CurrentEnemy = currentEnemy;
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
                //case Action.Train:
                //    Train();
                //    break;
                //case Action.Shop:
                //    Shop();
                //    break;
                //case Action.Scavenge:
                //    Scavenge();
                //    break;
            }
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

        public void PlayerChoseScrapResource(Resource resource, string playerId)
        {
            // Validations
            Player player = ValidatePlayer(playerId);
            ValidateGameState(GameState.Awaiting_Player_Choose_Scrap_Resource);

            player.Inventory += resource;

            CurrentGameState = GameState.Awaiting_Player_Choose_Upgrade;
            SoundShowdownEvent?.Invoke(this, new ScrapResourceChosenEvent(player, resource));
        }

        public void PlayerCancelledReplaceUpgrade(string playerId)
        {
            // Validations
            Player player = ValidatePlayer(playerId);
            ValidateGameState(GameState.Awaiting_Player_Replace_Upgrade);

            CurrentGameState = GameState.Awaiting_Player_Choose_Upgrade;
            SoundShowdownEvent?.Invoke(this, new BackToChooseUpgradeEvent(player));
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
