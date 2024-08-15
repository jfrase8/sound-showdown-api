using NuGet.Frameworks;
using SoundShowdownGame;
using SoundShowdownGame.Builders;
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
    public class SoundShowdownGamePlayerAttackEnemyTests
    {
        [TestMethod]
        public void AttackEnemy_InvalidState()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Genre)
                .Build();

            try
            {
                game.AttackEnemy("1hsdfosdn2");
                Assert.Fail("Attack should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void AttackEnemy_InvalidPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Attack)
                .Build();

            try
            {
                game.AttackEnemy("5384043508");
                Assert.Fail("Attack should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void AttackEnemy_NullEnemy()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Attack)
                .Build();
            try
            {
                game.AttackEnemy("1hsdfosdn2");
                Assert.Fail("Attack should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void Attack_SuccessPlayerWon()
        {
            Enemy testEnemy = EnemyDeckFactory.CreateTestEnemy(1, 1, InstrumentType.String, InstrumentType.Percussion, StatusEffect.None);

            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithInstrument(new InstrumentBuilder().WithLevel(3).Build()).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Attack)
                .WithCurrentEnemy(testEnemy)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            testEnemy.AttackingPlayer = game.PlayerList[0];

            game.AttackEnemy("1hsdfosdn2");

            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(SoundShowdownEventType.Attack, events[0].EventType);
            Assert.IsTrue(events[0] is AttackEvent);
            AttackEvent attackEvent = (AttackEvent)events[0];
            Assert.AreEqual("1hsdfosdn2", attackEvent.Player.Id);
            Assert.AreEqual(BattleWinner.Player, attackEvent.Attack.BattleResult);
            Assert.AreEqual(2, attackEvent.Player.Inventory.AccumulatedResources.Count);
            Assert.IsTrue(attackEvent.Player.Inventory.AccumulatedResources.ContainsKey(ResourceName.Leather));
            Assert.IsTrue(attackEvent.Player.Inventory.AccumulatedResources.ContainsKey(ResourceName.Vial_Of_Poison));

            Assert.AreEqual(GameState.Awaiting_Player_Fight_Or_End_Action, game.CurrentGameState);
        }

        [TestMethod]
        public void Attack_SuccessEnemyWon()
        {
            Enemy testEnemy = EnemyDeckFactory.CreateTestEnemy(100, 100, InstrumentType.String, InstrumentType.Percussion, StatusEffect.None);

            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Attack)
                .WithCurrentEnemy(testEnemy)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            testEnemy.AttackingPlayer = game.PlayerList[0];

            game.AttackEnemy("1hsdfosdn2");

            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(SoundShowdownEventType.Attack, events[0].EventType);
            Assert.IsTrue(events[0] is AttackEvent);
            AttackEvent attackEvent = (AttackEvent)events[0];
            Assert.AreEqual("1hsdfosdn2", attackEvent.Player.Id);
            Assert.AreEqual(BattleWinner.Enemy, attackEvent.Attack.BattleResult);
            Assert.AreEqual(0, attackEvent.Player.Inventory.AccumulatedResources.Count);

            Assert.AreEqual(GameState.Awaiting_Player_Choose_Action, game.CurrentGameState);
        }

        [TestMethod]
        public void Attack_SuccessNeitherWon()
        {
            Enemy testEnemy = EnemyDeckFactory.CreateTestEnemy(100, 1, InstrumentType.String, InstrumentType.Percussion, StatusEffect.None);

            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Attack)
                .WithCurrentEnemy(testEnemy)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            testEnemy.AttackingPlayer = game.PlayerList[0];

            game.AttackEnemy("1hsdfosdn2");

            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(SoundShowdownEventType.Attack, events[0].EventType);
            Assert.IsTrue(events[0] is AttackEvent);
            AttackEvent attackEvent = (AttackEvent)events[0];
            Assert.AreEqual("1hsdfosdn2", attackEvent.Player.Id);
            Assert.AreEqual(BattleWinner.None, attackEvent.Attack.BattleResult);
            Assert.AreEqual(0, attackEvent.Player.Inventory.AccumulatedResources.Count);

            Assert.AreEqual(GameState.Awaiting_Player_Attack, game.CurrentGameState);
        }

        [TestMethod]
        public void Attack_SuccessPlayerGainHealth()
        {
            Enemy testEnemy = EnemyDeckFactory.CreateTestEnemy(2, 1, InstrumentType.String, InstrumentType.Percussion, StatusEffect.None);

            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithBodyExperience(9).WithInstrument(new InstrumentBuilder().WithLevel(3).Build()).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Attack)
                .WithCurrentEnemy(testEnemy)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            testEnemy.AttackingPlayer = game.PlayerList[0];

            game.AttackEnemy("1hsdfosdn2");

            AttackEvent attackEvent = (AttackEvent)events[0];
            Assert.AreEqual(BattleWinner.Player, attackEvent.Attack.BattleResult);
            Assert.AreEqual(11, attackEvent.Player.Health);
            Assert.AreEqual(1, attackEvent.Player.BodyExp);

            Assert.AreEqual(GameState.Awaiting_Player_Fight_Or_End_Action, game.CurrentGameState);
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
