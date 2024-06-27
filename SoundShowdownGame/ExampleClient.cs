using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGame
{
    public class ExampleClient
    {
        public void OnGenreChosen(object sender, GenreChosenEventArgs e)
        {
            // Do Front-End stuff
        }

        public void OnActionChosen(object sender, ActionChosenEventArgs e)
        {
            // Do Front-End stuff
        }

        // Called when this user has chosen a genre
        public void PickGenre(GenreName genre, Player player)
        {
            // Later change to reference the game instance that has already been created
            SoundShowdown game = new(["awlksdj212", "dsjlk58o71"], EnemyDeckFactory.CreateShuffledDeck());

            game.GenreChosenEvent += OnGenreChosen;

            game.PlayerChooseGenre("awlksdj212", GenreName.Classical);
        }

        // Called when this user has chosen a action
        public void PickAction(Action action, Player player)
        {
            // Later change to reference the game instance that has already been created
            SoundShowdown game = new(["awlksdj212", "dsjlk58o71"], EnemyDeckFactory.CreateShuffledDeck());

            game.ActionChosenEvent += OnActionChosen;

            game.PlayerChooseAction("awlksdj212", Action.Fight_Enemies);
        }
    }
}
