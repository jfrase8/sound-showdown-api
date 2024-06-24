﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGame
{
    public class SoundShowdown
    {
        private List<Player> PlayerList { get; set; } = []; // List of players in the game
        private int ActionsCount { get; set; } = 3; // Current amount of actions left
        private Deck<Enemy> EnemyDeck { get; set; } // Deck of enemies
        private GameState CurrentGameState { get; set; }
        private int EnemiesDefeated { get; set; } = 0;
        private Enemy? CurrentEnemy { get; set; }


        public SoundShowdown(List<string> playerIDs, Deck<Enemy> enemyDeck) 
        {
            PlayerList = playerIDs.Select(playerID => new Player(playerID)).ToList();
            EnemyDeck = enemyDeck;
            CurrentGameState = GameState.Awaiting_Player_Choose_Genre;
        }

        public void PlayerChooseGenre(string playerID, GenreName genreName)
        {
            Player player = ValidatePlayer(playerID);
            ValidateGameState(GameState.Awaiting_Player_Choose_Genre);

            // Set player's genre
            player.Genre = genreName;

            // Check if all players have chose a genre
            if (PlayerList.All(p => p.Genre != null))
            {
                CurrentGameState = GameState.Awaiting_Player_Choose_Action;
            }
            OnEndOfTurn();
        }
        private Player GetTurnPlayer()
        {
            return PlayerList[0];
        } 

        private void OnEndOfTurn()
        {
            // Set new turn order
            Player currentPlayer = PlayerList[0];
            PlayerList.RemoveAt(0);
            PlayerList.Add(currentPlayer);

            // Reset values
            CurrentEnemy = null;
            EnemiesDefeated = 0;

            StartNewTurn();
        }

        private void StartNewTurn()
        {
            ActionsCount = 3;
        }

        private Player ValidatePlayer(string playerID)
        {
            // Find player. If null, throw exception
            Player player = PlayerList.Find(p => p.ID == playerID) ?? throw new Exception($"Player not found: {playerID}.");

            // Check if its players turn
            Player turnPLayer = GetTurnPlayer();
            if (turnPLayer != player) throw new Exception($"It is not this players turn: {playerID}.");

            return turnPLayer;
        }
        private void ValidateGameState(GameState validGameState)
        {
            if (validGameState != CurrentGameState) throw new Exception($"The game is not in required game state: {validGameState}");
        }

        public void PlayerChooseAction(string playerID, Action action)
        {
            // Validate player and game state
            Player player = ValidatePlayer(playerID);
            ValidateGameState(GameState.Awaiting_Player_Choose_Action);

            // Use up one action
            ActionsCount--;

            switch (action)
            {
                case Action.FightEnemies:
                    // Add listeners to show the card drawn (NEEDS IMPLEMENTATION)
                    CurrentEnemy = DrawEnemyCard(player);
                    break;
                //case Action.ChallengeMusician:
                //    ChallengeMusician();
                //    break;
                //case Action.Train:
                //    Train();
                //    break;
                //case Action.Shop:
                //    Shop();
                //    break;
                //case Action.Scavenge:
                //    Scavenge();
                //    break;
                //case Action.UpgradeInstruments:
                //    UpgradeInstruments();
                //    break;
            }
        }


        private Enemy DrawEnemyCard(Player player)
        {
            // Set the game state
            CurrentGameState = GameState.Awaiting_Player_Attack;

            // Draw an enemy from the enemies deck
             return EnemyDeck.Draw();
        }

        public AttackInfo Attack(string playerID)
        {
            // Validations
            IBattleEntity player = ValidatePlayer(playerID);
            ValidateGameState(GameState.Awaiting_Player_Attack);
            if (CurrentEnemy == null) throw new Exception("Enemy card was not drawn. There is no current enemy.");

            // Attack info object
            AttackInfo attackInfo = new() { Roll = Dice.RollDie() };
            attackInfo.CalcDamage((Enemy)CurrentEnemy, (Player)player);

            // Check if enemy or player was defeated
            BattleWinner battleResult = BattleWinner.None;
            CurrentEnemy.TakeDamage(attackInfo.Damage);
            if (CurrentEnemy.IsDefeated)
            {
                battleResult = BattleWinner.Player;
                EnemiesDefeated++;
            }
            else
            {
                player.TakeDamage(CurrentEnemy.Damage);
                if (player.IsDefeated) battleResult = BattleWinner.Enemy;
                else battleResult = BattleWinner.None;
            }

            // Set Game state based on battleResult
            switch(battleResult)
            {
                case BattleWinner.Player:
                    CurrentGameState = GameState.Awaiting_Player_Fight_Or_End_Action;
                    break;
                case BattleWinner.Enemy:
                    CurrentGameState = GameState.Awaiting_Player_Choose_Action;
                    break;
                case BattleWinner.None:
                    CurrentGameState = GameState.Awaiting_Player_Attack;
                    break;
            }
            
            return attackInfo;
        }
    }
}
