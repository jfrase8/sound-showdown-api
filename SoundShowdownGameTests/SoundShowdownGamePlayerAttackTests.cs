﻿using NuGet.Frameworks;
using SoundShowdownGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGameTests
{
    [TestClass]
    public class SoundShowdownGamePlayerAttackTests
    {
        [TestMethod]
        public void Attack_InvalidState()
        {
            SoundShowdown game = new(["1hsdfosdn2", "sad83908230"], EnemyDeckFactory.CreateShuffledDeck());

            game.PlayerChooseGenre("1hsdfosdn2", GenreName.Pop);

            try
            {
                game.Attack("1hsdfosdn2");
                Assert.Fail("Attack should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void Attack_InvalidPlayer()
        {
            SoundShowdown game = new(["1hsdfosdn2", "sad83908230"], EnemyDeckFactory.CreateShuffledDeck());

            game.PlayerChooseGenre("1hsdfosdn2", GenreName.Pop);
            game.PlayerChooseGenre("sad83908230", GenreName.Rock);

            try
            {
                game.Attack("5384043508");
                Assert.Fail("Attack should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void Attack_SuccessPlayerWon()
        {
            SoundShowdown game = new(["1hsdfosdn2", "sad83908230"], EnemyDeckFactory.CreateShuffledDeck());

            List<SoundShowdownEventArgs> events = [];

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            game.PlayerChooseGenre("1hsdfosdn2", GenreName.Pop);
            game.PlayerChooseGenre("sad83908230", GenreName.Rock);

            Player player = game.GetTurnPlayer();

            // Create custom enemy
            Dictionary<Resource, int> loot = new Dictionary<Resource, int>
            {
                { new Resource("Leather", "Dry leather", 5), 2 },
                { new Resource("Goo", "Gooey slimly stuff", 2), 3 }
            };
            game.EnemyDeck.Cards.Push(new Enemy("Slime", "Slimey", 1, 1, loot));

            game.PlayerChooseAction("1hsdfosdn2", SoundShowdownGame.Action.Fight_Enemies);

            game.Attack("1hsdfosdn2");

            // Assert that player does not have resources in inventory yet
            Assert.AreEqual(0, player.Inventory.ResourceInventory.Count);
            // Assert player has 2 different resources accumulated (Leather and goo)
            Assert.AreEqual(2, player.Inventory.AccumulatedResources.Count);

            Assert.AreEqual(GameState.Awaiting_Player_Fight_Or_End_Action, game.CurrentGameState);
        }

        [TestMethod]
        public void Attack_SuccessEnemyWon()
        {
            SoundShowdown game = new(["1hsdfosdn2", "sad83908230"], EnemyDeckFactory.CreateShuffledDeck());

            List<SoundShowdownEventArgs> events = [];

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            game.PlayerChooseGenre("1hsdfosdn2", GenreName.Pop);
            game.PlayerChooseGenre("sad83908230", GenreName.Rock);

            Player player = game.GetTurnPlayer();

            // Create custom enemy
            Dictionary<Resource, int> loot = new Dictionary<Resource, int>
            {
                { new Resource("Leather", "Dry leather", 5), 2 },
                { new Resource("Goo", "Gooey slimly stuff", 2), 3 }
            };
            game.EnemyDeck.Cards.Push(new Enemy("Slime", "Slimey", 20, 20, loot));

            game.PlayerChooseAction("1hsdfosdn2", SoundShowdownGame.Action.Fight_Enemies);

            game.Attack("1hsdfosdn2");

            // Assert that player does not have resources in inventory
            Assert.AreEqual(0, player.Inventory.ResourceInventory.Count);
            // Assert player has no accumulated resources
            Assert.AreEqual(0, player.Inventory.AccumulatedResources.Count);

            Assert.AreEqual(GameState.Awaiting_Player_Choose_Action, game.CurrentGameState);
        }

        [TestMethod]
        public void Attack_SuccessNeitherWon()
        {
            SoundShowdown game = new(["1hsdfosdn2", "sad83908230"], EnemyDeckFactory.CreateShuffledDeck());

            List<SoundShowdownEventArgs> events = [];

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            game.PlayerChooseGenre("1hsdfosdn2", GenreName.Pop);
            game.PlayerChooseGenre("sad83908230", GenreName.Rock);

            Player player = game.GetTurnPlayer();

            // Create custom enemy
            Dictionary<Resource, int> loot = new Dictionary<Resource, int>
            {
                { new Resource("Leather", "Dry leather", 5), 2 },
                { new Resource("Goo", "Gooey slimly stuff", 2), 3 }
            };
            game.EnemyDeck.Cards.Push(new Enemy("Slime", "Slimey", 20, 1, loot));

            game.PlayerChooseAction("1hsdfosdn2", SoundShowdownGame.Action.Fight_Enemies);

            game.Attack("1hsdfosdn2");

            // Assert that player does not have resources in inventory
            Assert.AreEqual(0, player.Inventory.ResourceInventory.Count);
            // Assert player has no resources accumulated
            Assert.AreEqual(0, player.Inventory.AccumulatedResources.Count);

            Assert.AreEqual(GameState.Awaiting_Player_Attack, game.CurrentGameState);
        }
    }
}