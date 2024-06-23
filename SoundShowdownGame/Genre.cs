﻿using System.Collections.Immutable;
using static SoundShowdownGame.Instrument;

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
        // Immutable Dictionary that has instruments specific to a genre
        public static readonly ImmutableDictionary<GenreName, InstrumentName[]> GenreInstruments = new Dictionary<GenreName, InstrumentName[]>()
        {
            {GenreName.Rock, [InstrumentName.Mic, InstrumentName.ElectricGuitar, InstrumentName.Drums, InstrumentName.BassGuitar] },
            {GenreName.Classical, [InstrumentName.Violin, InstrumentName.Piano, InstrumentName.Clarinet, InstrumentName.Flute] },
            {GenreName.HipHop, [InstrumentName.Mic, InstrumentName.Sampler, InstrumentName.Drums, InstrumentName.BassGuitar] },
        }.ToImmutableDictionary();

        public GenreName Name { get; set; } = name;
        public string Description { get; set; } =description;
        public string Bonus { get; set; } = bonus;
        public SpecialPowers Special { get; set; } = special;
    }
}
