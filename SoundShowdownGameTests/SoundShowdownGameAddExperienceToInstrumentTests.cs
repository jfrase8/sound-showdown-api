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
    public class SoundShowdownGameAddExperienceToInstrumentTests
    {

        [TestMethod]
        public void AddExperienceToInstrument_InvalidPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Roll_For_Training)
                .Build();

            try
            {
                game.AddExperienceToInstrument(5, "5384043508");
                Assert.Fail("AddExperienceToInstrument should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void AddExperienceToInstrument_WrongPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Replace_Upgrade)
                .Build();

            try
            {
                game.AddExperienceToInstrument(5, "sad83908230");
                Assert.Fail("AddExperienceToInstrument should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void AddExperienceToInstrument_NoInstrument()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Replace_Upgrade)
                .Build();

            try
            {
                game.AddExperienceToInstrument(5, "1hsdfosdn2");
                Assert.Fail("AddExperienceToInstrument should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void AddExperienceToInstrument_MaxLevel()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).WithInstrument(new InstrumentBuilder().WithLevel(3).Build()).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Replace_Upgrade)
                .Build();

            try
            {
                game.AddExperienceToInstrument(5, "1hsdfosdn2");
                Assert.Fail("AddExperienceToInstrument should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void AddExperienceToInstrument_SuccessNoLvlUp()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).WithInstrument(new InstrumentBuilder().WithExperience(3).Build()).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Replace_Upgrade)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            game.AddExperienceToInstrument(1, "1hsdfosdn2");

            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(SoundShowdownEventType.AddedInstrumentExp, events[0].EventType);
            Assert.IsTrue(events[0] is AddedInstrumentExpEvent);
            AddedInstrumentExpEvent addedInstrumentExpEvent = (AddedInstrumentExpEvent)events[0];
            Assert.AreEqual("1hsdfosdn2", addedInstrumentExpEvent.Player.Id);

            Assert.AreEqual(1, addedInstrumentExpEvent.GainedExp);
            Assert.AreEqual(1, addedInstrumentExpEvent.InstrumentLevel);
            Assert.AreEqual(4, addedInstrumentExpEvent.LeftOverExp);
            Assert.IsFalse(addedInstrumentExpEvent.LeveledUp);
        }

        [TestMethod]
        public void AddExperienceToInstrument_SuccessLvlUp()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).WithInstrument(new InstrumentBuilder().WithExperience(7).Build()).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Replace_Upgrade)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            game.AddExperienceToInstrument(4, "1hsdfosdn2");

            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(SoundShowdownEventType.AddedInstrumentExp, events[0].EventType);
            Assert.IsTrue(events[0] is AddedInstrumentExpEvent);
            AddedInstrumentExpEvent addedInstrumentExpEvent = (AddedInstrumentExpEvent)events[0];
            Assert.AreEqual("1hsdfosdn2", addedInstrumentExpEvent.Player.Id);

            Assert.AreEqual(4, addedInstrumentExpEvent.GainedExp);
            Assert.AreEqual(2, addedInstrumentExpEvent.InstrumentLevel);
            Assert.AreEqual(1, addedInstrumentExpEvent.LeftOverExp);
            Assert.IsTrue(addedInstrumentExpEvent.LeveledUp);
        }
    }
}
