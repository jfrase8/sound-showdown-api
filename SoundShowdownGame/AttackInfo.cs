using System;
using System.Collections.Generic;
using System.Linq;
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
            // NEEDS IMPLEMENTATION
            Damage = Roll;
        }
    }
}
