using SoundShowdownGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = SoundShowdownGame.Action;

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
            catch (SoundShowdownException)
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
                game.PlayerChooseAction("5384043508", SoundShowdownGame.Action.Fight_Enemies);
                Assert.Fail("PlayerChooseAcion should have thrown exception.");
            }
            catch (SoundShowdownException)
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
                game.PlayerChooseAction("sad83908230", SoundShowdownGame.Action.Fight_Enemies);
                Assert.Fail("PlayerChooseAcion should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void FightEnemies_Success()
        {
            SoundShowdown game = new(["1hsdfosdn2", "sad83908230"], EnemyDeckFactory.CreateShuffledDeck());

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            game.PlayerChooseGenre("1hsdfosdn2", GenreName.Pop);
            game.PlayerChooseGenre("sad83908230", GenreName.Rock);

            Assert.AreEqual(GameState.Awaiting_Player_Choose_Action, game.CurrentGameState);

            game.PlayerChooseAction("1hsdfosdn2", Action.Fight_Enemies);

            Assert.AreEqual(GameState.Awaiting_Player_Attack, game.CurrentGameState);

            Assert.AreEqual(3, events.Count);
            Assert.AreEqual(SoundShowdownEventType.ActionChosen, events[2].EventType);
            Assert.IsTrue(events[2] is ActionChosenEvent);
            ActionChosenEvent actionChosenEvent = (ActionChosenEvent)events[2];
            Assert.AreEqual("1hsdfosdn2", actionChosenEvent.Player.Id);
            Assert.AreEqual(Action.Fight_Enemies, actionChosenEvent.ChoseAction);
        }
    }
}
