using SoundShowdownGame;
using SoundShowdownGame.Builders;
using SoundShowdownGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGameTests
{
    [TestClass]
    public class SoundShowdownGamePlayerChooseActionBuildUpgrades
    {
        [TestMethod]
        public void BuildUpgrades_InvalidState()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Genre)
                .Build();

            try
            {
                game.PlayerChooseAction("1hsdfosdn2", SoundShowdownGame.Enums.GameAction.Build_Upgrades);
                Assert.Fail("PlayerChooseAction should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void BuildUpgrades_InvalidPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Pop).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Rock).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Action)
                .Build();

            try
            {
                game.PlayerChooseAction("5384043508", SoundShowdownGame.Enums.GameAction.Build_Upgrades);
                Assert.Fail("PlayerChooseAction should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void BuildUpgrades_WrongPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Pop).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Rock).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Action)
                .Build();

            try
            {
                game.PlayerChooseAction("sad83908230", SoundShowdownGame.Enums.GameAction.Build_Upgrades);
                Assert.Fail("PlayerChooseAction should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void BuildUpgrades_Success()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Pop).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Rock).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Action)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            game.PlayerChooseAction("1hsdfosdn2", SoundShowdownGame.Enums.GameAction.Build_Upgrades);

            Assert.AreEqual(GameState.Awaiting_Player_Choose_Upgrade, game.CurrentGameState);

            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(SoundShowdownEventType.ActionChosen, events[0].EventType);
            Assert.IsTrue(events[0] is ActionChosenEvent);
            ActionChosenEvent actionChosenEvent = (ActionChosenEvent)events[0];
            Assert.AreEqual("1hsdfosdn2", actionChosenEvent.Player.Id);
            Assert.AreEqual(SoundShowdownGame.Enums.GameAction.Build_Upgrades, actionChosenEvent.ChoseAction);
        }
    }
}
