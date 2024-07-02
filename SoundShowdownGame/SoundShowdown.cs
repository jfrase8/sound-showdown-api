using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGame
{
    public class SoundShowdown
    {
        public List<Player> PlayerList { get; private set; } // List of players in the game
        private int ActionsCount { get; set; } = 3; // Current amount of actions left
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
            //EventListeners = [];
        }

        public SoundShowdown(List<Player> players, Deck<Enemy> enemyDeck, int actionsCount, GameState currentGameState, int enemiesDefeated, Enemy? currentEnemy)
        {
            PlayerList = players;
            EnemyDeck = enemyDeck;
            ActionsCount = actionsCount;
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

            // Update the game state
            CurrentGameState = GameState.Awaiting_Player_Choose_Action;

            // Player adds accumulated resources to inventory
            player.Inventory.GainResources();

            // NEEDS IMPLEMENTATION
            //SoundShowdownEvent?.Invoke(this, new EndFightEvent(player))
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
            Enemy enemy = EnemyDeck.Draw();
            enemy.AttackingPlayer = player;

            return enemy;
        }
    }
}
