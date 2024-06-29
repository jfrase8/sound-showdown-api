﻿using SoundShowdownGame;
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
            SoundShowdown game = new(["1hsdfosdn2", "sad83908230"], EnemyDeckFactory.CreateShuffledDeck());

            game.PlayerChooseGenre("1hsdfosdn2", GenreName.Pop);
            game.PlayerChooseGenre("sad83908230", GenreName.Rock);

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
            SoundShowdown game = new(["1hsdfosdn2", "sad83908230"], EnemyDeckFactory.CreateShuffledDeck());

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
            SoundShowdown game = new(["1hsdfosdn2", "sad83908230"], EnemyDeckFactory.CreateShuffledDeck());

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
            SoundShowdown game = new(["1hsdfosdn2", "sad83908230"], EnemyDeckFactory.CreateShuffledDeck());

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            game.PlayerChooseGenre("1hsdfosdn2", GenreName.Pop);

            Assert.AreEqual("sad83908230", game.GetTurnPlayer().Id);
            Assert.AreEqual(GameState.Awaiting_Player_Choose_Genre, game.CurrentGameState);
            Assert.AreEqual(GenreName.Pop, game.PlayerList.Find(player => player.Id == "1hsdfosdn2").Genre);
            Assert.AreEqual("sad83908230", game.PlayerList[0].Id);

            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(SoundShowdownEventType.GenreChosen, events[0].EventType);
            Assert.IsTrue(events[0] is GenreChosenEvent);
            GenreChosenEvent genreChosenEvent = (GenreChosenEvent)events[0];
            Assert.AreEqual("1hsdfosdn2", genreChosenEvent.Player.Id);
            Assert.AreEqual(GenreName.Pop, genreChosenEvent.Genre);
        }

        [TestMethod]
        public void PlayerChooseGenre_SuccessLastPlayer()
        {
            SoundShowdown game = new(["1hsdfosdn2", "sad83908230"], EnemyDeckFactory.CreateShuffledDeck());

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            game.PlayerChooseGenre("1hsdfosdn2", GenreName.Pop);
            game.PlayerChooseGenre("sad83908230", GenreName.Rock);

            Assert.AreEqual("1hsdfosdn2", game.GetTurnPlayer().Id);
            Assert.AreEqual(GameState.Awaiting_Player_Choose_Action, game.CurrentGameState);
            Assert.AreEqual(GenreName.Pop, game.PlayerList.Find(player => player.Id == "1hsdfosdn2").Genre);

            Assert.AreEqual(2, events.Count);
            Assert.AreEqual(SoundShowdownEventType.GenreChosen, events[1].EventType);
            Assert.IsTrue(events[1] is GenreChosenEvent);
            GenreChosenEvent genreChosenEvent = (GenreChosenEvent)events[1];
            Assert.AreEqual("sad83908230", genreChosenEvent.Player.Id);
            Assert.AreEqual(GenreName.Rock, genreChosenEvent.Genre);
        }        
    }
}