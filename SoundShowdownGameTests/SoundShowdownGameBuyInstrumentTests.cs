using SoundShowdownGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGameTests
{
    [TestClass]
    public class SoundShowdownGameBuyInstrumentTests
    {
        [TestMethod]
        public void BuyInstrument_InvalidState()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Roll_For_Training)
                .Build();

            Instrument testInstrument = game.GameShop.GoodInstrumentsDeck.Draw();

            try
            {
                game.BuyInstrument(testInstrument, "1hsdfosdn2");
                Assert.Fail("BuyInstrument should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void BuyInstrument_InvalidPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Shop)
                .Build();

            Instrument testInstrument = game.GameShop.GoodInstrumentsDeck.Draw();

            try
            {
                game.BuyInstrument(testInstrument, "5384043508");
                Assert.Fail("BuyInstrument should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void BuyInstrument_WrongPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Shop)
                .Build();

            Instrument testInstrument = game.GameShop.GoodInstrumentsDeck.Draw();

            try
            {
                game.BuyInstrument(testInstrument, "sad83908230");
                Assert.Fail("BuyInstrument should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void BuyInstrument_NotEnoughCoins()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).WithInventoryCoins(10).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Shop)
                .Build();

            Instrument testInstrument = game.GameShop.GoodInstrumentsDeck.Draw();

            try
            {
                game.BuyInstrument(testInstrument, "1hsdfosdn2");
                Assert.Fail("BuyInstrument should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void BuyInstrument_SuccessTradeIn()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).WithInventoryCoins(30)
                    .WithInstrument(new InstrumentBuilder().WithCost(20).WithName(InstrumentName.Cymbal).Build())
                    .Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Shop)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            Instrument testInstrument = game.GameShop.GoodInstrumentsDeck.Draw();

            game.BuyInstrument(testInstrument, "1hsdfosdn2");

            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(SoundShowdownEventType.PlayerBoughtInstrument, events[0].EventType);
            Assert.IsTrue(events[0] is PlayerBoughtInstrumentEvent);
            PlayerBoughtInstrumentEvent playerBoughtInstrumentEvent = (PlayerBoughtInstrumentEvent)events[0];
            Assert.AreEqual("1hsdfosdn2", playerBoughtInstrumentEvent.Player.Id);

            Assert.AreEqual(0, playerBoughtInstrumentEvent.Player.Inventory.Coins);
            Assert.AreEqual(testInstrument, playerBoughtInstrumentEvent.Instrument);
            Assert.AreEqual(1, playerBoughtInstrumentEvent.GameShop.ResaleInstruments.Count);
            Assert.AreEqual(InstrumentName.Cymbal, playerBoughtInstrumentEvent.GameShop.ResaleInstruments[0].Name);
        }

        [TestMethod]
        public void BuyInstrument_SuccessNoTradeIn()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).WithInventoryCoins(40).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Shop)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            Instrument testInstrument = game.GameShop.GoodInstrumentsDeck.Draw();

            game.BuyInstrument(testInstrument, "1hsdfosdn2");

            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(SoundShowdownEventType.PlayerBoughtInstrument, events[0].EventType);
            Assert.IsTrue(events[0] is PlayerBoughtInstrumentEvent);
            PlayerBoughtInstrumentEvent playerBoughtInstrumentEvent = (PlayerBoughtInstrumentEvent)events[0];
            Assert.AreEqual("1hsdfosdn2", playerBoughtInstrumentEvent.Player.Id);

            Assert.AreEqual(0, playerBoughtInstrumentEvent.Player.Inventory.Coins);
            Assert.AreEqual(testInstrument, playerBoughtInstrumentEvent.Instrument);
            Assert.AreEqual(0, playerBoughtInstrumentEvent.GameShop.ResaleInstruments.Count);
        }
    }
}
