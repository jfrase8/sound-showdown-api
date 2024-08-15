using SoundShowdownGame;
using SoundShowdownGame.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGameTests
{
    [TestClass]
    public class SoundShowdownGamePlayerChooseGenreTests
    {
        [TestMethod]
        public void PlayerChooseGenre_InvalidState()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Action)
                .Build();

            try
            {
                game.PlayerChooseGenre("1hsdfosdn2", GenreName.Pop);
                Assert.Fail("PlayerChooseGenre should have thrown an exception");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerChooseGenre_InvalidPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Genre)
                .Build();

            try
            {
                game.PlayerChooseGenre("11111111", GenreName.Pop);
                Assert.Fail("PlayerChooseGenre should have thrown an exception because of invalid player ID");
            }
            catch (SoundShowdownException)
            {
            }
        }

        [TestMethod]
        public void PlayerChooseGenre_WrongPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Genre)
                .Build();

            try
            {
                game.PlayerChooseGenre("sad83908230", GenreName.Pop);
                Assert.Fail("PlayerChooseGenre should have thrown an exception because of wrong player");
            }
            catch (SoundShowdownException)
            {
            }
        }

        [TestMethod]
        public void PlayerChooseGenre_Success()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Genre)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            game.PlayerChooseGenre("1hsdfosdn2", GenreName.Pop);

            Assert.AreEqual("sad83908230", game.GetTurnPlayer().Id);
            Assert.AreEqual(GameState.Awaiting_Player_Choose_Genre, game.CurrentGameState);
            Assert.AreEqual(GenreName.Pop, game.PlayerList?.Find(player => player.Id == "1hsdfosdn2")?.Genre);
            Assert.AreEqual("sad83908230", game.PlayerList?[0].Id);

            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(SoundShowdownEventType.EndTurn, events[0].EventType);
            Assert.IsTrue(events[0] is EndTurnEvent);
            EndTurnEvent endTurnEvent = (EndTurnEvent)events[0];
            Assert.AreEqual("1hsdfosdn2", endTurnEvent.CurrentPlayer.Id);
            Assert.AreEqual("sad83908230", endTurnEvent.NextPlayer.Id);
            Assert.AreEqual(GenreName.Pop, endTurnEvent.CurrentPlayer.Genre);
            Assert.AreEqual(SoundShowdownGame.Enums.Action.ChooseGenre, endTurnEvent.Action);
        }

        [TestMethod]
        public void PlayerChooseGenre_SuccessLastPlayer()
        {
            // Set up a game where the first player has already chosen a Genre
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").Build())
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Genre)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            game.PlayerChooseGenre("sad83908230", GenreName.Rock);

            Assert.AreEqual("1hsdfosdn2", game.GetTurnPlayer().Id);
            Assert.AreEqual(GameState.Awaiting_Player_Choose_Action, game.CurrentGameState);
            Assert.AreEqual(GenreName.Pop, game.PlayerList?.Find(player => player.Id == "1hsdfosdn2")?.Genre);

            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(SoundShowdownEventType.EndTurn, events[0].EventType);
            Assert.IsTrue(events[0] is EndTurnEvent);
            EndTurnEvent endTurnEvent = (EndTurnEvent)events[0];
            Assert.AreEqual("sad83908230", endTurnEvent.CurrentPlayer.Id);
            Assert.AreEqual("1hsdfosdn2", endTurnEvent.NextPlayer.Id);
            Assert.AreEqual(GenreName.Rock, endTurnEvent.CurrentPlayer.Genre);
            Assert.AreEqual(SoundShowdownGame.Enums.Action.ChooseGenre, endTurnEvent.Action);
        }        
    }
}