using SoundShowdownGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGameTests
{
    [TestClass]
    public class SoundShowdownGamePlayerChooseGenreTests
    {
        [TestMethod]
        public void PlayerChooseGenre_InvalidState()
        {
            SoundShowdown game = new(["1hsdfosdn2", "sad83908230"], EnemyDeckFactory.CreateShuffledDeck());

            game.PlayerChooseGenre("1hsdfosdn2", GenreName.Pop);
            game.PlayerChooseGenre("sad83908230", GenreName.Rock);

            try
            {
                game.PlayerChooseGenre("1hsdfosdn2", GenreName.Pop);
                Assert.Fail("PlayerChooseGenre should have thrown an exception");
            }
            catch (SoundShowdownException e)
            {

            }
        }

        [TestMethod]
        public void PlayerChooseGenre_InvalidPlayer()
        {
            SoundShowdown game = new(["1hsdfosdn2", "sad83908230"], EnemyDeckFactory.CreateShuffledDeck());

            try
            {
                game.PlayerChooseGenre("11111111", GenreName.Pop);
                Assert.Fail("PlayerChooseGenre should have thrown an exception because of invalid player ID");
            }
            catch (SoundShowdownException e)
            {
            }
        }

        [TestMethod]
        public void PlayerChooseGenre_WrongPlayer()
        {
            SoundShowdown game = new(["1hsdfosdn2", "sad83908230"], EnemyDeckFactory.CreateShuffledDeck());

            try
            {
                game.PlayerChooseGenre("sad83908230", GenreName.Pop);
                Assert.Fail("PlayerChooseGenre should have thrown an exception because of wrong player");
            }
            catch (SoundShowdownException e)
            {
            }
        }

        [TestMethod]
        public void PlayerChooseGenre_Success()
        {
            SoundShowdown game = new(["1hsdfosdn2", "sad83908230"], EnemyDeckFactory.CreateShuffledDeck());

            game.PlayerChooseGenre("1hsdfosdn2", GenreName.Pop);

            Assert.AreEqual("sad83908230", game.GetTurnPlayer().ID);
            Assert.AreEqual(GameState.Awaiting_Player_Choose_Genre, game.CurrentGameState);
            Assert.AreEqual(GenreName.Pop, game.PlayerList.Find(player => player.ID == "1hsdfosdn2").Genre);
            Assert.AreEqual("sad83908230", game.PlayerList[0].ID);
        }

        [TestMethod]
        public void PlayerChooseGenre_SuccessLastPlayer()
        {
            SoundShowdown game = new(["1hsdfosdn2", "sad83908230"], EnemyDeckFactory.CreateShuffledDeck());

            game.PlayerChooseGenre("1hsdfosdn2", GenreName.Pop);
            game.PlayerChooseGenre("sad83908230", GenreName.Rock);

            Assert.AreEqual("1hsdfosdn2", game.GetTurnPlayer().ID);
            Assert.AreEqual(GameState.Awaiting_Player_Choose_Action, game.CurrentGameState);
            Assert.AreEqual(GenreName.Pop, game.PlayerList.Find(player => player.ID == "1hsdfosdn2").Genre);
        }
    }
}
