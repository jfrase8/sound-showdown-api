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
    public class SoundShowdownGameRollScavengeDiceTests
    {
        [TestMethod]
        public void RollScavengeDice_InvalidState()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Roll_For_Training)
                .Build();

            try
            {
                game.RollScavengeDice("1hsdfosdn2");
                Assert.Fail("RollScavengeDice should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void RollScavengeDice_InvalidPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Roll_Scavenge_Dice)
                .Build();

            Item testItem = game.GameShop.Items.Find(item => item.Name == ItemName.Food)
                            ?? throw new SoundShowdownException("Shop does not have a Food Item.");

            try
            {
                game.RollScavengeDice("5384043508");
                Assert.Fail("RollScavengeDice should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void RollScavengeDice_WrongPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Roll_Scavenge_Dice)
                .Build();

            try
            {
                game.RollScavengeDice("sad83908230");
                Assert.Fail("RollScavengeDice should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void RollScavengeDice_Success()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Roll_Scavenge_Dice)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            game.RollScavengeDice("1hsdfosdn2");

            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(SoundShowdownEventType.RolledScavengeDice, events[0].EventType);
            Assert.IsTrue(events[0] is RolledScavengeDiceEvent);
            RolledScavengeDiceEvent rolledScavengeDiceEvent = (RolledScavengeDiceEvent)events[0];
            Assert.AreEqual("1hsdfosdn2", rolledScavengeDiceEvent.Player.Id);

            Assert.AreEqual(4, rolledScavengeDiceEvent.Rolls.Count);
            
            if (rolledScavengeDiceEvent.Rolls.Contains(1))
            {
                Assert.IsNotNull(rolledScavengeDiceEvent.EventCard);
            }
            else
            {
                Assert.IsTrue(rolledScavengeDiceEvent.Player.Inventory.ResourceInventory.ContainsKey(GlobalData.ScavengeDiceRolls[rolledScavengeDiceEvent.Rolls[0]]));
                Assert.IsTrue(rolledScavengeDiceEvent.Player.Inventory.ResourceInventory.ContainsKey(GlobalData.ScavengeDiceRolls[rolledScavengeDiceEvent.Rolls[1]]));
                Assert.IsTrue(rolledScavengeDiceEvent.Player.Inventory.ResourceInventory.ContainsKey(GlobalData.ScavengeDiceRolls[rolledScavengeDiceEvent.Rolls[2]]));
                Assert.IsTrue(rolledScavengeDiceEvent.Player.Inventory.ResourceInventory.ContainsKey(GlobalData.ScavengeDiceRolls[rolledScavengeDiceEvent.Rolls[3]]));
            }
        }
    }
}
