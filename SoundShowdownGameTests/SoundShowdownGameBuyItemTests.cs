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
    public class SoundShowdownGameBuyItemTests
    {
        [TestMethod]
        public void BuyItem_InvalidState()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Roll_For_Training)
                .Build();

            Item testItem = game.GameShop.Items.Find(item => item.Name == ItemName.Food) 
                            ?? throw new SoundShowdownException("Shop does not have a Food Item.");

            try
            {
                game.BuyItem(testItem, "1hsdfosdn2");
                Assert.Fail("BuyItem should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void BuyItem_InvalidPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Shop)
                .Build();

            Item testItem = game.GameShop.Items.Find(item => item.Name == ItemName.Food)
                            ?? throw new SoundShowdownException("Shop does not have a Food Item.");

            try
            {
                game.BuyItem(testItem, "5384043508");
                Assert.Fail("BuyItem should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void BuyItem_WrongPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Shop)
                .Build();

            Item testItem = game.GameShop.Items.Find(item => item.Name == ItemName.Food)
                            ?? throw new SoundShowdownException("Shop does not have a Food Item.");
            
            try
            {
                game.BuyItem(testItem, "sad83908230");
                Assert.Fail("BuyItem should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void BuyItem_NotEnoughCoins()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).WithInventoryCoins(5).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Shop)
                .Build();

            Item testItem = game.GameShop.Items.Find(item => item.Name == ItemName.Food)
                            ?? throw new SoundShowdownException("Shop does not have a Food Item.");
            
            try
            {
                game.BuyItem(testItem, "1hsdfosdn2");
                Assert.Fail("BuyItem should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void BuyItem_NotEnoughSpace()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).WithInventoryCoins(10).WithInventoryItem(ItemName.Antidote, 5).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Shop)
                .Build();

            Item testItem = game.GameShop.Items.Find(item => item.Name == ItemName.Food)
                            ?? throw new SoundShowdownException("Shop does not have a Food Item.");
            
            try
            {
                game.BuyItem(testItem, "1hsdfosdn2");
                Assert.Fail("BuyItem should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void BuyItem_Success()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).WithInventoryCoins(30).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Shop)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            Item testItem = game.GameShop.Items.Find(item => item.Name == ItemName.Food)
                            ?? throw new SoundShowdownException("Shop does not have a Food Item.");

            game.BuyItem(testItem, "1hsdfosdn2");

            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(SoundShowdownEventType.PlayerBoughtItem, events[0].EventType);
            Assert.IsTrue(events[0] is PlayerBoughtItemEvent);
            PlayerBoughtItemEvent playerBoughtItemEvent = (PlayerBoughtItemEvent)events[0];
            Assert.AreEqual("1hsdfosdn2", playerBoughtItemEvent.Player.Id);

            Assert.AreEqual(20, playerBoughtItemEvent.Player.Inventory.Coins);
            Assert.AreEqual(testItem, playerBoughtItemEvent.Item);
            Assert.AreEqual(1, playerBoughtItemEvent.Player.Inventory.Items.Count);
        }
    }
}
