using NuGet.Frameworks;
using SoundShowdownGame;
using SoundShowdownGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGameTests
{
    [TestClass]
    public class SoundShowdownGamePlayerAttackMusicianTests
    {
        [TestMethod]
        public void AttackMusician_InvalidState()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Genre)
                .Build();
            try
            {
                game.AttackMusician("1hsdfosdn2");
                Assert.Fail("Attack should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }

        }

        [TestMethod]
        public void AttackMusician_InvalidPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Attack)
                .Build();

            try
            {
                game.AttackMusician("5384043508");
                Assert.Fail("Attack should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void AttackMusician_MusiciansIndexOutOfBounds()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithMusicianTrackRank(4).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Attack)
                .Build();
            try
            {
                game.AttackMusician("1hsdfosdn2");
                Assert.Fail("Attack should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }

        }

        [TestMethod]
        public void Attack_SuccessPlayerWon()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithInstrument(new InstrumentBuilder().WithLevel(3).Build()).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Attack)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            Musician testMusician = new Musician(MusicianName.Johnny_Speed, 1, 1, StatusEffect.None, "", 1, 10);
            game.Musicians.Insert(0, testMusician);

            game.AttackMusician("1hsdfosdn2");

            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(SoundShowdownEventType.Attack, events[0].EventType);
            Assert.IsTrue(events[0] is AttackEvent);
            AttackEvent attackEvent = (AttackEvent)events[0];
            Assert.AreEqual("1hsdfosdn2", attackEvent.Player.Id);
            Assert.AreEqual(BattleWinner.Player, attackEvent.Attack.BattleResult);
            Assert.AreEqual(10, attackEvent.Player.Inventory.Coins);
            Assert.AreEqual(1, attackEvent.Player.BodyExp);
            Assert.AreEqual(1, attackEvent.Player.MusicianTrackRank);
            Assert.AreEqual(attackEvent.Player, game.Musicians[0].DefeatedBy[0]);

            Assert.AreEqual(GameState.Awaiting_Player_Choose_Action, game.CurrentGameState);
        }

        [TestMethod]
        public void Attack_SuccessMusicianWon()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Attack)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            Musician testMusician = new Musician(MusicianName.Johnny_Speed, 20, 10, StatusEffect.None, "", 1, 10);
            game.Musicians.Insert(0, testMusician);

            game.AttackMusician("1hsdfosdn2");

            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(SoundShowdownEventType.Attack, events[0].EventType);
            Assert.IsTrue(events[0] is AttackEvent);
            AttackEvent attackEvent = (AttackEvent)events[0];
            Assert.AreEqual("1hsdfosdn2", attackEvent.Player.Id);
            Assert.AreEqual(BattleWinner.Musician, attackEvent.Attack.BattleResult);
            Assert.AreEqual(0, attackEvent.Player.Inventory.Coins);
            Assert.AreEqual(0, attackEvent.Player.MusicianTrackRank);

            Assert.AreEqual(GameState.Awaiting_Player_Choose_Action, game.CurrentGameState);
        }

        [TestMethod]
        public void Attack_SuccessNeitherWon()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Attack)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            Musician testMusician = new Musician(MusicianName.Johnny_Speed, 20, 1, StatusEffect.None, "", 1, 10);
            game.Musicians.Insert(0, testMusician);

            game.AttackMusician("1hsdfosdn2");

            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(SoundShowdownEventType.Attack, events[0].EventType);
            Assert.IsTrue(events[0] is AttackEvent);
            AttackEvent attackEvent = (AttackEvent)events[0];
            Assert.AreEqual("1hsdfosdn2", attackEvent.Player.Id);
            Assert.AreEqual(BattleWinner.None, attackEvent.Attack.BattleResult);
            Assert.AreEqual(0, attackEvent.Player.Inventory.Coins);
            Assert.AreEqual(0, attackEvent.Player.BodyExp);

            Assert.AreEqual(GameState.Awaiting_Player_Attack, game.CurrentGameState);
        }

        [TestMethod]
        public void Attack_SuccessPlayerGainHealth()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithBodyExperience(9).WithInstrument(new InstrumentBuilder().WithLevel(3).Build()).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Attack)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            Musician testMusician = new Musician(MusicianName.Johnny_Speed, 2, 1, StatusEffect.None, "", 1, 10);
            game.Musicians.Insert(0, testMusician);

            game.AttackMusician("1hsdfosdn2");

            AttackEvent attackEvent = (AttackEvent)events[0];
            Assert.AreEqual(BattleWinner.Player, attackEvent.Attack.BattleResult);
            Assert.AreEqual(11, attackEvent.Player.Health);
            Assert.AreEqual(1, attackEvent.Player.BodyExp);

            Assert.AreEqual(GameState.Awaiting_Player_Choose_Action, game.CurrentGameState);
        }

        [TestMethod]
        public void Attack_SuccessDrain()
        {

        }
        [TestMethod]
        public void Attack_SuccessShock()
        {

        }
        [TestMethod]
        public void Attack_SuccessSleep()
        {

        }
        [TestMethod]
        public void Attack_SuccessPoison()
        {

        }
    }
}
