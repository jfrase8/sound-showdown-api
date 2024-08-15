using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoundShowdownGame.Enums;

namespace SoundShowdownGame
{
    public class EndTurnEvent(Player currentPlayer, Player nextPlayer, Action action) : SoundShowdownEventArgs(SoundShowdownEventType.EndTurn)
    {
        public Player CurrentPlayer { get; private set; } = currentPlayer;
        public Player NextPlayer { get; private set; } = nextPlayer;
        public Action Action { get; private set; } = action;
    }
}
