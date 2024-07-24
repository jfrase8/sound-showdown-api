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
    public class SoundShowdownGamePlayerReplaceUpgradeTests
    {
        [TestMethod]
        public void PlayerReplaceUpgrade_InvalidState()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Genre)
                .Build();

            Dictionary<ResourceName, int>? buildCost = new Dictionary<ResourceName, int>
            {
                { ResourceName.Metal_Scrap, 1 },
                { ResourceName.Wire, 1 },
                { ResourceName.Batteries, 1 },
                { ResourceName.Crystal, 1 },
                { ResourceName.Plastic, 1 },
            };
            Upgrade testReplacedUpgrade = new Upgrade(UpgradeName.Exo_Suit, UpgradeType.Suit, "", 0, 0, 5, buildCost);
            Upgrade testNewUpgrade = new Upgrade(UpgradeName.Hazmat_Suit, UpgradeType.Suit, "", 0, 0, 0, buildCost);

            try
            {
                game.PlayerReplaceUpgrade(testNewUpgrade, testReplacedUpgrade, "1hsdfosdn2");
                Assert.Fail("PlayerReplaceUpgrade should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerReplaceUpgrade_InvalidPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Replace_Upgrade)
                .Build();

            Dictionary<ResourceName, int>? buildCost = new Dictionary<ResourceName, int>
            {
                { ResourceName.Metal_Scrap, 1 },
                { ResourceName.Wire, 1 },
                { ResourceName.Batteries, 1 },
                { ResourceName.Crystal, 1 },
                { ResourceName.Plastic, 1 }
            };
            Upgrade testReplacedUpgrade = new Upgrade(UpgradeName.Exo_Suit, UpgradeType.Suit, "", 0, 0, 5, buildCost);
            Upgrade testNewUpgrade = new Upgrade(UpgradeName.Hazmat_Suit, UpgradeType.Suit, "", 0, 0, 0, buildCost);

            try
            {
                game.PlayerReplaceUpgrade(testNewUpgrade, testReplacedUpgrade, "5384043508");
                Assert.Fail("PlayerReplaceUpgrade should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerReplaceUpgrade_WrongPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Replace_Upgrade)
                .Build();

            Dictionary<ResourceName, int>? buildCost = new Dictionary<ResourceName, int>
            {
                { ResourceName.Metal_Scrap, 1 },
                { ResourceName.Wire, 1 },
                { ResourceName.Batteries, 1 },
                { ResourceName.Crystal, 1 },
                { ResourceName.Plastic, 1 }
            };
            Upgrade testReplacedUpgrade = new Upgrade(UpgradeName.Exo_Suit, UpgradeType.Suit, "", 0, 0, 5, buildCost);
            Upgrade testNewUpgrade = new Upgrade(UpgradeName.Hazmat_Suit, UpgradeType.Suit, "", 0, 0, 0, buildCost);

            try
            {
                game.PlayerReplaceUpgrade(testNewUpgrade, testReplacedUpgrade, "sad83908230");
                Assert.Fail("PlayerReplacedUpgrade should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerReplaceUpgrade_Success()
        {
            Dictionary<ResourceName, int>? buildCost = new Dictionary<ResourceName, int>
            {
                { ResourceName.Metal_Scrap, 1 },
                { ResourceName.Wire, 1 },
                { ResourceName.Batteries, 1 },
                { ResourceName.Crystal, 1 },
                { ResourceName.Plastic, 1 }
            };
            Upgrade testReplacedUpgrade = new Upgrade(UpgradeName.Exo_Suit, UpgradeType.Suit, "", 0, 0, 5, buildCost);
            Upgrade testNewUpgrade = new Upgrade(UpgradeName.Hazmat_Suit, UpgradeType.Suit, "", 0, 0, 0, buildCost);

            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz)
                    .WithInventoryResource(ResourceName.Metal_Scrap, 1)
                    .WithInventoryResource(ResourceName.Wire, 1)
                    .WithInventoryResource(ResourceName.Batteries, 1)
                    .WithInventoryResource(ResourceName.Crystal, 1)
                    .WithInventoryResource(ResourceName.Plastic, 1)
                    .WithSuitUpgrade(testReplacedUpgrade)
                    .WithInstrument(new InstrumentBuilder().Build())
                    .Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop)
                    .WithInstrument(new InstrumentBuilder().Build())
                    .Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Replace_Upgrade)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            game.PlayerReplaceUpgrade(testNewUpgrade, testReplacedUpgrade, "1hsdfosdn2");

            Assert.AreEqual(GameState.Awaiting_Player_Choose_Scrap_Resource, game.CurrentGameState);

            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(SoundShowdownEventType.UpgradeReplaced, events[0].EventType);
            Assert.IsTrue(events[0] is UpgradeReplacedEvent);
            UpgradeReplacedEvent upgradeReplacedEvent = (UpgradeReplacedEvent)events[0];
            Assert.AreEqual("1hsdfosdn2", upgradeReplacedEvent.Player.Id);
            Assert.AreEqual(testNewUpgrade, upgradeReplacedEvent.NewUpgrade);
            Assert.AreEqual(testReplacedUpgrade, upgradeReplacedEvent.ReplacedUpgrade);

            Assert.AreEqual(testNewUpgrade.Name, game.PlayerList[0].SuitUpgrade.Name);
        }
    }
}
