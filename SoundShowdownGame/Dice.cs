using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGame
{
    public class Dice
    {
        private static readonly Random R = new();

        public static int RollDie()
        {
            return R.Next(1, 7);
        }
    }
}
