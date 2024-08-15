using SoundShowdownGame;
using SoundShowdownGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGameTests
{
    [TestClass]
    public class SoundShowdownGamePlayerRolledForTrainingTests
    {
        [TestMethod]
        public void PlayerRolledForTraining_InvalidState()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithInstrument(new InstrumentBuilder().Build()).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Genre)
                .Build();

            try
            {
                game.PlayerRolledForTraining(0, "1hsdfosdn2");
                Assert.Fail("PlayerRolledForTraining should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerRolledForTraining_InvalidPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).WithInstrument(new InstrumentBuilder().Build()).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Roll_For_Training)
                .Build();

            try
            {
                game.PlayerRolledForTraining(0, "5384043508");
                Assert.Fail("PlayerRolledForTraining should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerRolledForTraining_WrongPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).WithInstrument(new InstrumentBuilder().Build()).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Roll_For_Training)
                .Build();

            try
            {
                game.PlayerRolledForTraining(0, "sad83908230");
                Assert.Fail("PlayerRolledForTraining should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerRolledForTraining_NoInstrument()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Roll_For_Training)
                .Build();

            try
            {
                game.PlayerRolledForTraining(0, "1hsdfosdn2");
                Assert.Fail("PlayerRolledForTraining should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerRolledForTraining_SuccessFirstRoll()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).WithInstrument(new InstrumentBuilder().Build()).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Roll_For_Training)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            game.PlayerRolledForTraining(0, "1hsdfosdn2");

            Assert.AreEqual(GameState.Awaiting_Player_Roll_For_Training, game.CurrentGameState);

            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(SoundShowdownEventType.RollForTraining, events[0].EventType);
            Assert.IsTrue(events[0] is RollForTrainingEvent);
            RollForTrainingEvent rollForTrainingEvent = (RollForTrainingEvent)events[0];
            Assert.AreEqual("1hsdfosdn2", rollForTrainingEvent.Player.Id);

            Assert.AreNotEqual(0, rollForTrainingEvent.FirstRoll);
            Assert.AreEqual(0, rollForTrainingEvent.SecondRoll);
            Assert.IsFalse(rollForTrainingEvent.OverPracticed);
        }

        [TestMethod]
        public void PlayerRolledForTraining_SuccessOverPracticed()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).WithInstrument(new InstrumentBuilder().Build()).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Roll_For_Training)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            game.PlayerRolledForTraining(8, "1hsdfosdn2");

            Assert.AreEqual(GameState.Awaiting_Player_Roll_For_Training, game.CurrentGameState);

            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(SoundShowdownEventType.RollForTraining, events[0].EventType);
            Assert.IsTrue(events[0] is RollForTrainingEvent);
            RollForTrainingEvent rollForTrainingEvent = (RollForTrainingEvent)events[0];
            Assert.AreEqual("1hsdfosdn2", rollForTrainingEvent.Player.Id);

            Assert.AreNotEqual(0, rollForTrainingEvent.FirstRoll);
            Assert.AreNotEqual(0, rollForTrainingEvent.SecondRoll);
            Assert.IsFalse(rollForTrainingEvent.FirstRoll + rollForTrainingEvent.SecondRoll < 9);
            Assert.IsTrue(rollForTrainingEvent.OverPracticed);
            Assert.AreEqual(1, game.PlayerList[0].Instrument.DamageCounters);
        }

        [TestMethod]
        public void PlayerRolledForTraining_SuccessNotOverPracticed()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).WithInstrument(new InstrumentBuilder().Build()).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Roll_For_Training)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            game.PlayerRolledForTraining(1, "1hsdfosdn2");

            Assert.AreEqual(GameState.Awaiting_Player_Roll_For_Training, game.CurrentGameState);

            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(SoundShowdownEventType.RollForTraining, events[0].EventType);
            Assert.IsTrue(events[0] is RollForTrainingEvent);
            RollForTrainingEvent rollForTrainingEvent = (RollForTrainingEvent)events[0];
            Assert.AreEqual("1hsdfosdn2", rollForTrainingEvent.Player.Id);

            Assert.AreNotEqual(0, rollForTrainingEvent.FirstRoll);
            Assert.AreNotEqual(0, rollForTrainingEvent.SecondRoll);
            Assert.IsFalse(rollForTrainingEvent.FirstRoll + rollForTrainingEvent.SecondRoll > 8);
            Assert.IsFalse(rollForTrainingEvent.OverPracticed);
            Assert.AreEqual(0, game.PlayerList[0].Instrument.DamageCounters);
        }
    }
}
