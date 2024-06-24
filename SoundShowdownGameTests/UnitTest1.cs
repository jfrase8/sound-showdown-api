using SoundShowdownGame;

namespace SoundShowdownGameTests
{
    [TestClass]
    public class SoundShowdownGameConstructorTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            SoundShowdown game = new(["1hsdfosdn2", "sad83908230"], EnemyDeckFactory.CreateShuffledDeck());


        }
    }
}