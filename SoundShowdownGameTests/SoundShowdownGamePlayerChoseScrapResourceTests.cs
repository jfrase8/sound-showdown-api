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
    public class SoundShowdownGamePlayerChoseScrapResourceTests
    {
        [TestMethod]
        public void PlayerChooseScrapResource_InvalidState()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Genre)
                .Build();

            try
            {
                game.PlayerChoseScrapResource(ResourceName.Plastic, "1hsdfosdn2");
                Assert.Fail("PlayerChoseScrapResource should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerChooseScrapResource_InvalidPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Scrap_Resource)
                .Build();

            try
            {
                game.PlayerChoseScrapResource(ResourceName.Plastic, "5384043508");
                Assert.Fail("PlayerChoseScrapResource should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerChooseScrapResource_WrongPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Scrap_Resource)
                .Build();

            try
            {
                game.PlayerChoseScrapResource(ResourceName.Plastic, "sad83908230");
                Assert.Fail("PlayerChoseScrapResource should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerChooseScrapResource_Success()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Scrap_Resource)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            game.PlayerChoseScrapResource(ResourceName.Plastic, "1hsdfosdn2");

            Assert.AreEqual(GameState.Awaiting_Player_Choose_Upgrade, game.CurrentGameState);

            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(SoundShowdownEventType.ScrapResourceChosen, events[0].EventType);
            Assert.IsTrue(events[0] is ScrapResourceChosenEvent);
            ScrapResourceChosenEvent scrapResourceChosenEvent = (ScrapResourceChosenEvent)events[0];
            Assert.AreEqual("1hsdfosdn2", scrapResourceChosenEvent.Player.Id);

            Assert.AreEqual(1, game.PlayerList[0].Inventory.ResourceInventory[ResourceName.Plastic]);
        }
    }
}

