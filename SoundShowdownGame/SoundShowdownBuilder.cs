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
        private Deck<EventCard>? EventDeck;
        private GameState CurrentGameState;
        private int EnemiesDefeated;
        private Enemy? CurrentEnemy;
        private Shop? GameShop;
        private List<Musician>? Musicians;

        public SoundShowdownBuilder() 
        {
            // Create defaults
            Players = [];
            EnemyDeck = null;
            EventDeck = null;
            CurrentGameState = GameState.Awaiting_Player_Choose_Genre;
            EnemiesDefeated = 0;
            CurrentEnemy = null;
            GameShop = null;
            Musicians = null;
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
            EnemyDeck ??= EnemyDeckFactory.CreateShuffledDeck();
            EventDeck ??= EventDeckFactory.CreateShuffledDeck();

            // Create a shop if one has not been defined
            GameShop ??= new Shop(
                InstrumentDeckFactory.CreatedShuffledExoticInstrumentDeck(),
                InstrumentDeckFactory.CreatedShuffledHighInstrumentDeck(),
                InstrumentDeckFactory.CreatedShuffledGoodInstrumentDeck(),
                InstrumentDeckFactory.CreatedShuffledLowInstrumentDeck(),
                [new Item(ItemName.Food, "Heals you", 10), new Item(ItemName.Antidote, "Gets rid of all poison counters", 10)]
            );

            // Create musicians if not defined
            Musicians =
            [
                new Musician(MusicianName.Dirty_Dan, 10, 5, StatusEffect.Poison, GlobalData.MusicianPowers[MusicianName.Dirty_Dan], 1),
                new Musician(MusicianName.Rex_Rhythm, 20, 10, StatusEffect.Shock, GlobalData.MusicianPowers[MusicianName.Rex_Rhythm], 2),
            ];

            SoundShowdown game = new SoundShowdown(players: Players, enemyDeck: EnemyDeck, eventDeck: EventDeck, currentGameState: CurrentGameState, enemiesDefeated: EnemiesDefeated, currentEnemy: CurrentEnemy, gameShop: GameShop, musicians: Musicians);
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
