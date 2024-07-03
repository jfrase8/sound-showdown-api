using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGame
{
    public class AttackInfo
    {
        public int Roll {  get; set; }
        public int Damage { get; set; }
        public BattleWinner BattleResult { get; set; }

        // Calculates the damage that a player does to an enemy
        public void CalcDamage(Enemy enemy, Player player)
        {
            if (player.Instrument == null) throw new SoundShowdownException("Player has no instrument. Cannot calculate damage.");
            // Components that factor into damage: Roll, upgrade effects, enemy weakness/resistance

            // Weakness
            if (player.Instrument.Type == enemy.Weakness) Damage += 2;
            // Resistance
            if (player.Instrument.Type == enemy.Resistance) Damage -= 2;

            // Upgrades
            if (player.Instrument.Upgrades.Count != 0)
            {
                Damage += player.Instrument.GetDamageFromUpgrades();

                // Check for upgrades that effect roll
                Roll += player.Instrument.GetRollIncreaseFromUpgrades();
                if (Roll > 6) Roll = 6; // make sure roll does not go over 6

                // TODO : Add more checks because of special effects
            }

            // Roll
            Damage = RollToDamage(player.Instrument.Level);
        }

        public int RollToDamage(int level)
        {
            int damage;
            if (level == 1)
            {
                damage = Roll switch
                {
                    1 => 0,
                    2 => 2,
                    3 => 3,
                    4 => 4,
                    5 => 5,
                    6 => 6,
                    _ => throw new SoundShowdownException($"Invalid roll value: {Roll}")
                };
            }
            else if (level == 2)
            {
                damage = Roll switch
                {
                    1 => 0,
                    2 => 3,
                    3 => 4,
                    4 => 5,
                    5 => 6,
                    6 => 7,
                    _ => throw new SoundShowdownException($"Invalid roll value: {Roll}")
                };
            }
            else
            {
                damage = Roll switch
                {
                    1 => 2,
                    2 => 4,
                    3 => 5,
                    4 => 6,
                    5 => 7,
                    6 => 8,
                    _ => throw new SoundShowdownException($"Invalid roll value: {Roll}")
                };
            }
            return damage;
        }
    }
}
