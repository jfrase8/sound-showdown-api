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
    public class SoundShowdownGamePlayerCancelledReplaceUpgradeTests
    {
        [TestMethod]
        public void PlayerCancelledReplaceUpgrade_InvalidState()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Genre)
                .Build();

            try
            {
                game.PlayerCancelledReplaceUpgrade("1hsdfosdn2");
                Assert.Fail("PlayerCancelledReplaceUpgrade should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerCancelledReplaceUpgrade_InvalidPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Replace_Upgrade)
                .Build();

            try
            {
                game.PlayerCancelledReplaceUpgrade("5384043508");
                Assert.Fail("PlayerCancelledReplaceUpgrade should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerCancelledReplaceUpgrade_WrongPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Replace_Upgrade)
                .Build();

            try
            {
                game.PlayerCancelledReplaceUpgrade("sad83908230");
                Assert.Fail("PlayerCancelledReplaceUpgrade should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerCancelledReplaceUpgrade_Success()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Replace_Upgrade)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            game.PlayerCancelledReplaceUpgrade("1hsdfosdn2");

            Assert.AreEqual(GameState.Awaiting_Player_Choose_Upgrade, game.CurrentGameState);

            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(SoundShowdownEventType.BackToChooseUpgrade, events[0].EventType);
            Assert.IsTrue(events[0] is BackToChooseUpgradeEvent);
            BackToChooseUpgradeEvent backToChooseUpgradeEvent = (BackToChooseUpgradeEvent)events[0];
            Assert.AreEqual("1hsdfosdn2", backToChooseUpgradeEvent.Player.Id);
        }
    }
}
