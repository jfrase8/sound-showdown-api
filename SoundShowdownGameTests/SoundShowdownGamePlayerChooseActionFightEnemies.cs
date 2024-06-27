using SoundShowdownGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGameTests
{
    [TestClass]
    public class SoundShowdownGamePlayerChooseActionFightEnemies
    {
        [TestMethod]
        public void FightEnemies_InvalidState()
        {
            SoundShowdown game = new(["1hsdfosdn2", "sad83908230"], EnemyDeckFactory.CreateShuffledDeck());

            game.PlayerChooseGenre("1hsdfosdn2", GenreName.Pop);

            try
            {
                game.PlayerChooseAction("1hsdfosdn2", SoundShowdownGame.Action.Fight_Enemies);
                Assert.Fail("PlayerChooseAcion should have thrown exception.");
            }
            catch (SoundShowdownException e)
            {

            }
        }

        [TestMethod]
        public void FightEnemies_InvalidPlayer()
        {
            SoundShowdown game = new(["1hsdfosdn2", "sad83908230"], EnemyDeckFactory.CreateShuffledDeck());

            game.PlayerChooseGenre("1hsdfosdn2", GenreName.Pop);
            game.PlayerChooseGenre("sad83908230", GenreName.Rock);

            try
            {
                game.PlayerChooseAction("5384043508", SoundShowdownGame.Action.FightEnemies);
                Assert.Fail("PlayerChooseAcion should have thrown exception.");
            }
            catch (SoundShowdownException e)
            {

            }
        }

        [TestMethod]
        public void FightEnemies_WrongPlayer()
        {
            SoundShowdown game = new(["1hsdfosdn2", "sad83908230"], EnemyDeckFactory.CreateShuffledDeck());

            game.PlayerChooseGenre("1hsdfosdn2", GenreName.Pop);
            game.PlayerChooseGenre("sad83908230", GenreName.Rock);

            try
            {
                game.PlayerChooseAction("sad83908230", SoundShowdownGame.Action.FightEnemies);
                Assert.Fail("PlayerChooseAcion should have thrown exception.");
            }
            catch (SoundShowdownException e)
            {

            }
        }

        [TestMethod]
        public void FightEnemies_Success()
        {
            SoundShowdown game = new(["1hsdfosdn2", "sad83908230"], EnemyDeckFactory.CreateShuffledDeck());

            //game.AddEventListener();
            game.PlayerChooseGenre("1hsdfosdn2", GenreName.Pop);
            game.PlayerChooseGenre("sad83908230", GenreName.Rock);

            Assert.AreEqual(GameState.Awaiting_Player_Choose_Action, game.CurrentGameState);

            game.PlayerChooseAction("1hsdfosdn2", SoundShowdownGame.Action.FightEnemies);

            Assert.AreEqual(GameState.Awaiting_Player_Attack, game.CurrentGameState);
        }

        [TestMethod]
        public void FightEnemies_SuccessLastPlayer()
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
