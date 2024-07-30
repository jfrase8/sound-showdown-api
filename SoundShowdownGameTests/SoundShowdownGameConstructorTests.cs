using SoundShowdownGame;

namespace SoundShowdownGameTests
{
    [TestClass]
    public class SoundShowdownGameConstructorTests
    {
        [TestMethod]
        public void TestConstructor()
        {
            SoundShowdown game = new(["1hsdfosdn2", "sad83908230"], EnemyDeckFactory.CreateShuffledDeck(), EventDeckFactory.CreateShuffledDeck());

            Assert.IsNotNull(game);
            Assert.AreEqual("1hsdfosdn2", game.GetTurnPlayer().Id);
            Assert.AreEqual(GameState.Awaiting_Player_Choose_Genre, game.CurrentGameState);
        }
    }
}