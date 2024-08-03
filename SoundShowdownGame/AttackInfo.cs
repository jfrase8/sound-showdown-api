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

        public readonly Dictionary<int, int> DamageForFists = new()
        {
            { 1, 0 },
            { 2, 0 },
            { 3, 1 },
            { 4, 2 },
            { 5, 3 },
            { 6, 4 }
        };

        // Calculates the damage that a player does to an enemy
        public void CalcDamage<T>(T opponent, Player player)
        {
            
            // TODO : Add upgrade effect testing and implementation

            
            if (player.Instrument == null)
            {
                Damage = DamageForFists[Roll];
            }
            else // Player has an instrument
            {
                if (opponent is Enemy enemy)
                {
                    // Weakness
                    if (player.Instrument.Type == enemy.Weakness) Damage += 2;
                    // Resistance
                    if (player.Instrument.Type == enemy.Resistance) Damage -= 2;
                }
                
                // Instrument Upgrades
                if (player.Instrument.Upgrades.Count != 0)
                {
                    // Roll
                    Damage = GlobalData.RollToDamage(Roll, player.Instrument.Level, player.Instrument.Quality);

                    // Genre Bonus
                    foreach (GenreName genre in player.Instrument.GenreBonuses)
                    {
                        if (genre == player.Genre) Damage++; break;
                    }
                }
            }
        }
    }
}
