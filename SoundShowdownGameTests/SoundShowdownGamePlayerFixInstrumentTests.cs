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
    public class SoundShowdownGamePlayerFixInstrumentTests
    {
        [TestMethod]
        public void PlayerFixInstrument_InvalidState()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Genre)
                .Build();

            try
            {
                game.PlayerFixInstrument(InstrumentType.Vocal, "1hsdfosdn2");
                Assert.Fail("PlayerFixInstrument should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerFixInstrument_InvalidPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Upgrade)
                .Build();

            try
            {
                game.PlayerFixInstrument(InstrumentType.Vocal, "5384043508");
                Assert.Fail("PlayerFixInstrument should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerFixInstrument_WrongPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Upgrade)
                .Build();

            try
            {
                game.PlayerFixInstrument(InstrumentType.Vocal, "sad83908230");
                Assert.Fail("PlayerFixInstrument should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerFixInstrument_InvalidResources()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz)
                    .WithInstrument(new InstrumentBuilder().WithType(InstrumentType.Vocal).Build())
                    .WithInventoryResource(ResourceName.Wire, 3)
                    .Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Upgrade)
                .Build();

            try
            {
                game.PlayerFixInstrument(InstrumentType.Vocal, "1hsdfosdn2");
                Assert.Fail("PlayerFixInstrument should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerFixInstrument_NoInstrument()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz)
                    .WithInventoryResource(ResourceName.Wire, 3)
                    .WithInventoryResource(ResourceName.Adhesive, 1)
                    .Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Upgrade)
                .Build();

            try
            {
                game.PlayerFixInstrument(InstrumentType.Vocal, "1hsdfosdn2");
                Assert.Fail("PlayerFixInstrument should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerFixInstrument_InstrumentNotDamaged()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz)
                    .WithInventoryResource(ResourceName.Wire, 3)
                    .WithInventoryResource(ResourceName.Adhesive, 1)
                    .WithInstrument(new InstrumentBuilder().WithType(InstrumentType.Vocal).Build())
                    .Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Upgrade)
                .Build();
            try
            {
                game.PlayerFixInstrument(InstrumentType.Vocal, "1hsdfosdn2");
                Assert.Fail("PlayerFixInstrument should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerFixInstrument_Success()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz)
                    .WithInventoryResource(ResourceName.Wire, 2)
                    .WithInventoryResource(ResourceName.Adhesive, 1)
                    .WithInstrument(new InstrumentBuilder().WithType(InstrumentType.Vocal).WithDamageCounters(1).Build())
                    .Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Upgrade)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            game.PlayerFixInstrument(InstrumentType.Vocal, "1hsdfosdn2");

            Assert.AreEqual(GameState.Awaiting_Player_Choose_Upgrade, game.CurrentGameState);

            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(SoundShowdownEventType.FixedInstrument, events[0].EventType);
            Assert.IsTrue(events[0] is FixedInstrumentEvent);
            FixedInstrumentEvent fixedInstrumentEvent = (FixedInstrumentEvent)events[0];
            Assert.AreEqual("1hsdfosdn2", fixedInstrumentEvent.Player.Id);

            Assert.AreEqual(0, game.PlayerList[0].Inventory.ResourceInventory.Count);
            Assert.AreEqual(0, game.PlayerList[0].Instrument?.DamageCounters);
        }
    }
}

