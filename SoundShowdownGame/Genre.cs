namespace SoundShowdownGame
{
    public class Genre(Genre.GenreName name, string description, string bonus, Genre.SpecialPowers special)
    {
        public enum GenreName
        {
            Classical,
            Jazz,
            HipHop,
            Rock,
            Pop
        }
        public enum SpecialPowers
        {
            CalmingNoise,
            Improvisation,
            Sampling,

            /* Still needs new powers */
            // NEEDS IMPLEMENTATION
            SpecialPower4,
            SpecialPower5
        }

        public GenreName Name { get; set; } = name;
        public string Description { get; set; } =description;
        public string Bonus { get; set; } = bonus;
        public SpecialPowers Special { get; set; } = special;
    }
}
