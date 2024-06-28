using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGame
{
    public class SoundShowdown
    {
        public List<Player> PlayerList { get; private set; } // List of players in the game
        private int ActionsCount { get; set; } = 3; // Current amount of actions left
        private Deck<Enemy> EnemyDeck { get; set; } // Deck of enemies
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
            //EventListeners = [];
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

            // Use up one action
            ActionsCount--;

            switch (action)
            {
                case Action.Fight_Enemies:
                    // Get the drawn enemy card
                    CurrentEnemy = DrawEnemyCard(player);
                    // Throw event
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
                    //case Action.UpgradeInstruments:
                    //    UpgradeInstruments();
                    //    break;
            }
        }

        public AttackInfo Attack(string playerId)
        {
            // Validations
            IBattleEntity player = ValidatePlayer(playerId);
            ValidateGameState(GameState.Awaiting_Player_Attack);
            if (CurrentEnemy == null) throw new SoundShowdownException("Enemy card was not drawn. There is no current enemy.");

            // Attack info object
            AttackInfo attackInfo = new() { Roll = Dice.RollDie() };
            attackInfo.CalcDamage((Enemy)CurrentEnemy, (Player)player);

            // Check if enemy or player was defeated
            BattleWinner battleResult = BattleWinner.None;
            CurrentEnemy.TakeDamage(attackInfo.Damage);
            if (CurrentEnemy.IsDefeated)
            {
                battleResult = BattleWinner.Player;
                EnemiesDefeated++;
            }
            else
            {
                player.TakeDamage(CurrentEnemy.Damage);

                battleResult = player.IsDefeated ? BattleWinner.Enemy : BattleWinner.None;
            }

            // Set Game state based on battleResult
            switch (battleResult)
            {
                case BattleWinner.Player:
                    CurrentGameState = GameState.Awaiting_Player_Fight_Or_End_Action;
                    break;
                case BattleWinner.Enemy:
                    CurrentGameState = GameState.Awaiting_Player_Choose_Action;
                    break;
                case BattleWinner.None:
                    CurrentGameState = GameState.Awaiting_Player_Attack;
                    break;
            }

            return attackInfo;
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
            ActionsCount = 3;
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
             return EnemyDeck.Draw();
        }
    }
}
