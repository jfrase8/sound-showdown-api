using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGame
{
    public class SoundShowdownBuilder
    {
        private List<Player> Players;
        private Deck<Enemy>? EnemyDeck;
        private int ActionsCount;
        private GameState CurrentGameState;
        private int EnemiesDefeated;
        private Enemy? CurrentEnemy;

        public SoundShowdownBuilder() 
        {
            // Create defaults
            Players = new List<Player>();
            EnemyDeck = null;
            ActionsCount = 3;
            CurrentGameState = GameState.Awaiting_Player_Choose_Genre;
            EnemiesDefeated = 0;
            CurrentEnemy = null;
        }

        public SoundShowdown Build()
        {
            // Validate
            if(Players.Count < 2)
            {
                throw new Exception("Must have at least 2 players to create an instance of SoundShowdown.");
            }

            // TODO: Finish validation

            // Create a deck if one has not been defined
            if (EnemyDeck == null)
            {
                EnemyDeck = EnemyDeckFactory.CreateShuffledDeck();
            }

            SoundShowdown game = new SoundShowdown(players: Players, enemyDeck: EnemyDeck, currentGameState: CurrentGameState, enemiesDefeated: EnemiesDefeated, currentEnemy: CurrentEnemy);
            return game;
        }

        public SoundShowdownBuilder WithPlayer(Player player)
        {
            Players.Add(player);
            return this;
        }

        public SoundShowdownBuilder WithEnemyDeck(Deck<Enemy> enemyDeck)
        {
            EnemyDeck = enemyDeck;
            return this;
        }

        public SoundShowdownBuilder WithActionsCount(int actionsCount)
        {
            ActionsCount = actionsCount;
            return this;
        }

        public SoundShowdownBuilder WithCurrentGameState(GameState currentGameState)
        {
            CurrentGameState = currentGameState;
            return this;
        }

        public SoundShowdownBuilder WithEnemiesDefeated(int enemiesDefeated)
        {
            EnemiesDefeated = enemiesDefeated;
            return this;
        }

        public SoundShowdownBuilder WithCurrentEnemy(Enemy? currentEnemy)
        {
            CurrentEnemy = currentEnemy;
            return this;
        }

    }
}
