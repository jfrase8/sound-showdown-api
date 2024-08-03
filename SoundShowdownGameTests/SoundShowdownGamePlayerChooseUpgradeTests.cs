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
    public class SoundShowdownGamePlayerChooseUpgradeTests
    {
        [TestMethod]
        public void PlayerChooseUpgrade_InvalidState()
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
            Upgrade testUpgrade = new(UpgradeName.Exo_Suit, UpgradeType.Suit, "", buildCost, null, UpgradeEffectType.Multi);

            try
            {
                game.PlayerChooseUpgrade(testUpgrade, "1hsdfosdn2");
                Assert.Fail("PlayerChooseUpgrade should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerChooseUpgrade_InvalidPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Upgrade)
                .Build();

            Dictionary<ResourceName, int>? buildCost = new Dictionary<ResourceName, int>
            {
                { ResourceName.Metal_Scrap, 1 },
                { ResourceName.Wire, 1 },
                { ResourceName.Batteries, 1 },
                { ResourceName.Crystal, 1 },
                { ResourceName.Plastic, 1 }
            };
            Upgrade testUpgrade = new(UpgradeName.Exo_Suit, UpgradeType.Suit, "", buildCost, null, UpgradeEffectType.Multi);

            try
            {
                game.PlayerChooseUpgrade(testUpgrade, "5384043508");
                Assert.Fail("PlayerChooseUpgrade should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerChooseUpgrade_WrongPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Upgrade)
                .Build();

            Dictionary<ResourceName, int>? buildCost = new Dictionary<ResourceName, int>
            {
                { ResourceName.Metal_Scrap, 1 },
                { ResourceName.Wire, 1 },
                { ResourceName.Batteries, 1 },
                { ResourceName.Crystal, 1 },
                { ResourceName.Plastic, 1 }
            };
            Upgrade testUpgrade = new(UpgradeName.Exo_Suit, UpgradeType.Suit, "", buildCost, null, UpgradeEffectType.Multi);

            try
            {
                game.PlayerChooseUpgrade(testUpgrade, "sad83908230");
                Assert.Fail("PlayerChooseUpgrade should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerChooseUpgrade_InvalidResource()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz)
                    .WithInventoryResource(ResourceName.Metal_Scrap, 1)
                    .WithInventoryResource(ResourceName.Wire, 1)
                    .WithInstrument(new InstrumentBuilder().Build())
                    .Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop)
                    .WithInstrument(new InstrumentBuilder().Build())
                    .Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Upgrade)
                .Build();

            Dictionary<ResourceName, int>? testResources = new Dictionary<ResourceName, int>
            {
                { ResourceName.Metal_Scrap, 1 },
                { ResourceName.Wire, 1 },
                { ResourceName.Batteries, 1 },
                { ResourceName.Crystal, 1 },
                { ResourceName.Plastic, 1 }
            };
            Upgrade testUpgrade = new(UpgradeName.Exo_Suit, UpgradeType.Suit, "", testResources, null, UpgradeEffectType.Multi);

            try
            {
                game.PlayerChooseUpgrade(testUpgrade, "1hsdfosdn2");
                Assert.Fail("PlayerChooseUpgrade should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerChooseUpgrade_NoInstrument()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz)
                    .WithInventoryResource(ResourceName.Metal_Scrap, 1)
                    .WithInventoryResource(ResourceName.Wire, 1)
                    .WithInventoryResource(ResourceName.Batteries, 1)
                    .WithInventoryResource(ResourceName.Crystal, 1)
                    .WithInventoryResource(ResourceName.Plastic, 1)
                    .Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop)
                    .Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Upgrade)
                .Build();

            Dictionary<ResourceName, int>? buildCost = new Dictionary<ResourceName, int>
            {
                { ResourceName.Metal_Scrap, 1 },
                { ResourceName.Wire, 1 },
                { ResourceName.Batteries, 1 },
                { ResourceName.Crystal, 1 },
                { ResourceName.Plastic, 1 }
            };
            Upgrade testUpgrade = new(UpgradeName.Auto_Tune, UpgradeType.Instrument_Type, "", buildCost, InstrumentType.Vocal, UpgradeEffectType.Passive);

            try
            {
                game.PlayerChooseUpgrade(testUpgrade, "1hsdfosdn2");
                Assert.Fail($"PlayerChooseUpgrade should have thrown exception");
            }
            catch (SoundShowdownException)
            {
                
            }
        }

        [TestMethod]
        public void PlayerChooseUpgrade_InvalidInstrumentType()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz)
                    .WithInventoryResource(ResourceName.Metal_Scrap, 1)
                    .WithInventoryResource(ResourceName.Wire, 1)
                    .WithInventoryResource(ResourceName.Batteries, 1)
                    .WithInventoryResource(ResourceName.Crystal, 1)
                    .WithInventoryResource(ResourceName.Plastic, 1)
                    .WithInstrument(new InstrumentBuilder().WithType(InstrumentType.Brass).Build())
                    .Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop)
                    .Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Upgrade)
                .Build();

            Dictionary<ResourceName, int> buildCost = new Dictionary<ResourceName, int>
            {
                { ResourceName.Metal_Scrap, 1 },
                { ResourceName.Wire, 1 },
                { ResourceName.Batteries, 1 },
                { ResourceName.Crystal, 1 },
                { ResourceName.Plastic, 1 }
            };
            Upgrade testUpgrade = new(UpgradeName.Auto_Tune, UpgradeType.Instrument_Type, "", buildCost, InstrumentType.Vocal, UpgradeEffectType.Passive);

            try
            {
                game.PlayerChooseUpgrade(testUpgrade, "1hsdfosdn2");
                Assert.Fail($"PlayerChooseUpgrade should have thrown exception");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerChooseUpgrade_SuccessWithSpace()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz)
                    .WithInventoryResource(ResourceName.Metal_Scrap, 1)
                    .WithInventoryResource(ResourceName.Wire, 1)
                    .WithInventoryResource(ResourceName.Batteries, 1)
                    .WithInventoryResource(ResourceName.Crystal, 1)
                    .WithInventoryResource(ResourceName.Plastic, 1)
                    .WithInstrument(new InstrumentBuilder().Build())
                    .Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop)
                    .WithInstrument(new InstrumentBuilder().Build())
                    .Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Upgrade)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            Dictionary<ResourceName, int>? buildCost = new Dictionary<ResourceName, int>
            {
                { ResourceName.Metal_Scrap, 1 },
                { ResourceName.Wire, 1 },
                { ResourceName.Batteries, 1 },
                { ResourceName.Crystal, 1 },
                { ResourceName.Plastic, 1 }
            };
            Upgrade testUpgrade = new Upgrade(UpgradeName.Exo_Suit, UpgradeType.Suit, "", buildCost, null, UpgradeEffectType.Multi);

            Assert.IsTrue(game.PlayerList[0].Inventory.ResourceInventory.ContainsKey(ResourceName.Metal_Scrap));

            game.PlayerChooseUpgrade(testUpgrade, "1hsdfosdn2");

            Assert.AreEqual(0, game.PlayerList[0].Inventory.ResourceInventory.Count);

            Assert.AreEqual(GameState.Awaiting_Player_Choose_Upgrade, game.CurrentGameState);

            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(SoundShowdownEventType.UpgradeBuilt, events[0].EventType);
            Assert.IsTrue(events[0] is UpgradeBuiltEvent);
            UpgradeBuiltEvent upgradeBuiltEvent = (UpgradeBuiltEvent)events[0];
            Assert.AreEqual("1hsdfosdn2", upgradeBuiltEvent.Player.Id);
            Assert.AreEqual(testUpgrade, upgradeBuiltEvent.Upgrade);

            Assert.AreEqual(testUpgrade.Name, game.PlayerList[0].SuitUpgrade.Name);
        }

        [TestMethod]
        public void PlayerChooseUpgrade_SuccessWithoutSpace()
        {
            Dictionary<ResourceName, int>? buildCost = new Dictionary<ResourceName, int>
            {
                { ResourceName.Metal_Scrap, 1 },
                { ResourceName.Wire, 1 },
                { ResourceName.Batteries, 1 },
                { ResourceName.Crystal, 1 },
                { ResourceName.Plastic, 1 }
            };
            Upgrade testReplacedUpgrade = new Upgrade(UpgradeName.Exo_Suit, UpgradeType.Suit, "", buildCost, null, UpgradeEffectType.Multi);
            Upgrade testNewUpgrade = new Upgrade(UpgradeName.Hazmat_Suit, UpgradeType.Suit, "", buildCost, null, UpgradeEffectType.Passive);

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
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Upgrade)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            Assert.IsTrue(game.PlayerList[0].Inventory.ResourceInventory.ContainsKey(ResourceName.Metal_Scrap));

            game.PlayerChooseUpgrade(testNewUpgrade, "1hsdfosdn2");

            Assert.AreNotEqual(0, game.PlayerList[0].Inventory.ResourceInventory.Count);

            Assert.AreEqual(GameState.Awaiting_Player_Replace_Upgrade, game.CurrentGameState);

            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(SoundShowdownEventType.ChooseUpgradeToReplace, events[0].EventType);
            Assert.IsTrue(events[0] is ChooseUpgradeToReplaceEvent);
            ChooseUpgradeToReplaceEvent upgradeToReplaceEvent = (ChooseUpgradeToReplaceEvent)events[0];
            Assert.AreEqual("1hsdfosdn2", upgradeToReplaceEvent.Player.Id);
            Assert.AreEqual(testNewUpgrade, upgradeToReplaceEvent.Upgrade);

            Assert.AreEqual(testReplacedUpgrade.Name, game.PlayerList[0].SuitUpgrade.Name);
        }
    }
}
