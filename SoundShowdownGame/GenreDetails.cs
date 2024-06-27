using System.Collections.Immutable;
using static SoundShowdownGame.Instrument;

namespace SoundShowdownGame
{
    public class GenreDetails(GenreName name, string description, GenreDetails.SpecialPowers special)
    {
        public enum SpecialPowers
        {
            Calming_Noise,
            Improvisation,
            Sampling,

            /* Still needs new powers */
            // NEEDS IMPLEMENTATION
            Special_Power_4,
            Special_Power_5
        }
        // Immutable Dictionary that has instruments specific to a genre
        public static readonly ImmutableDictionary<GenreName, InstrumentName[]> GenreInstruments = new Dictionary<GenreName, InstrumentName[]>()
        {
            {GenreName.Rock, [InstrumentName.Mic, InstrumentName.Electric_Guitar, InstrumentName.Drums, InstrumentName.Bass_Guitar] },
            {GenreName.Classical, [InstrumentName.Violin, InstrumentName.Piano, InstrumentName.Clarinet, InstrumentName.Flute] },
            {GenreName.Hip_Hop, [InstrumentName.Mic, InstrumentName.Sampler, InstrumentName.Drums, InstrumentName.Bass_Guitar] },
        }.ToImmutableDictionary();

        public GenreName Name { get; set; } = name;
        public string Description { get; set; } = description;
        public SpecialPowers Special { get; set; } = special;
    }
}
