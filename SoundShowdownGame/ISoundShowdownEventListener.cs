using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGame
{
    public interface ISoundShowdownEventListener
    {
        void OnGenreChosen(object sender, GenreChosenEventArgs e);
        void OnActionChosen(object sender, ActionChosenEventArgs e);
    }
}
