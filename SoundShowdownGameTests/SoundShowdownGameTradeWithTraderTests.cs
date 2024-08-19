using SoundShowdownGame;
using SoundShowdownGame.Builders;
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
    public class SoundShowdownGameTradeWithTraderTests
    {

        [TestMethod]
        public void TradeWithTrader_InvalidPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Shop)
                .Build();

            ResourceName[] testResources = [ResourceName.Glass, ResourceName.String, ResourceName.Fur, ResourceName.Fur];
            
            try
            {
                game.TradeWithTrader(testResources, ResourceName.Crystal, "5384043508");
                Assert.Fail("TradeWithTrader should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void TradeWithTrader_WrongPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Shop)
                .Build();

            ResourceName[] testResources = [ResourceName.Glass, ResourceName.String, ResourceName.Fur, ResourceName.Fur];

            try
            {
                game.TradeWithTrader(testResources, ResourceName.Crystal, "sad83908230");
                Assert.Fail("TradeWithTrader should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void TradeWithTrader_InvalidState()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Replace_Upgrade)
                .Build();

            ResourceName[] testResources = [ResourceName.Glass, ResourceName.String, ResourceName.Fur, ResourceName.Fur];

            try
            {
                game.TradeWithTrader(testResources, ResourceName.Crystal, "1hsdfosdn2");
                Assert.Fail("TradeWithTrader should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void TradeWithTrader_InvalidResources()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Shop)
                .Build();

            ResourceName[] testResources = [ResourceName.Glass, ResourceName.String, ResourceName.Fur, ResourceName.Fur];
            

            try
            {
                game.TradeWithTrader(testResources, ResourceName.Crystal, "1hsdfosdn2");
                Assert.Fail("TradeWithTrader should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void TradeWithTrader_Success()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).WithInventoryResource(ResourceName.Glass, 1).WithInventoryResource(ResourceName.String, 1).WithInventoryResource(ResourceName.Fur, 2).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Shop)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            ResourceName[] testResources = [ResourceName.Glass, ResourceName.String, ResourceName.Fur, ResourceName.Fur];

            game.TradeWithTrader(testResources, ResourceName.Crystal, "1hsdfosdn2");

            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(SoundShowdownEventType.TradeWithTrader, events[0].EventType);
            Assert.IsTrue(events[0] is TradeWithTraderEvent);
            TradeWithTraderEvent tradeWithTraderEvent = (TradeWithTraderEvent)events[0];
            Assert.AreEqual("1hsdfosdn2", tradeWithTraderEvent.Player.Id);

            Assert.AreEqual(1, tradeWithTraderEvent.Player.Inventory.ResourceInventory.Count);
            Assert.IsTrue(tradeWithTraderEvent.Player.Inventory.ResourceInventory.ContainsKey(ResourceName.Crystal));
        }
    }
}
