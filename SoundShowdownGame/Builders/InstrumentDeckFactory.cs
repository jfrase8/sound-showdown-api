using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoundShowdownGame.Enums;

namespace SoundShowdownGame.Builders
{
    public class InstrumentDeckFactory
    {
        public static Deck<Instrument> CreatedShuffledExoticInstrumentDeck()
        {

            Instrument[] exoticInstruments =
            [
                new Instrument(InstrumentName.Electric_Guitar, InstrumentType.String, "Electric Guitar", 0, 100, 50,
                    Quality.Exotic, [GenreName.Pop, GenreName.Rock],
                    [
                        new Upgrade(UpgradeName.Reverberating_Body, UpgradeType.Instrument_Unique, "blah blah blah", null, null, UpgradeEffectType.Passive)
                    ]),

            ];
            return new Deck<Instrument>("Exotic Quality Instruments", "", new Stack<Instrument>(exoticInstruments));
        }

        public static Deck<Instrument> CreatedShuffledHighInstrumentDeck()
        {
            Instrument[] highInstruments =
            [
                new Instrument(InstrumentName.Piano, InstrumentType.String, "", 0, 60, 30, Quality.High,
                    [GenreName.Pop, GenreName.Rock], []),
                new Instrument(InstrumentName.Clarinet, InstrumentType.Wind, "", 0, 60, 30, Quality.High,
                    [GenreName.Pop, GenreName.Rock], []),
                new Instrument(InstrumentName.Drums, InstrumentType.Percussion, "", 0, 60, 30, Quality.High,
                    [GenreName.Pop, GenreName.Rock], []),
            ];
            return new Deck<Instrument>("High Quality Instruments", "", new Stack<Instrument>(highInstruments));
        }

        public static Deck<Instrument> CreatedShuffledGoodInstrumentDeck()
        {
            Instrument[] goodInstruments =
            [
                new Instrument(InstrumentName.Trumpet, InstrumentType.Brass, "", 0, 40, 20, Quality.Good,
                    [GenreName.Pop, GenreName.Rock], []),
                new Instrument(InstrumentName.Bass_Guitar, InstrumentType.String, "", 0, 40, 20, Quality.Good,
                    [GenreName.Pop, GenreName.Rock], []),
                new Instrument(InstrumentName.Trombone, InstrumentType.Brass, "", 0, 40, 20, Quality.Good,
                    [GenreName.Pop, GenreName.Rock], []),
                new Instrument(InstrumentName.Violin, InstrumentType.String, "", 0, 40, 20, Quality.Good,
                    [GenreName.Pop, GenreName.Rock], []),
            ];
            return new Deck<Instrument>("Good Quality Instruments", "", new Stack<Instrument>(goodInstruments));
        }

        public static Deck<Instrument> CreatedShuffledLowInstrumentDeck()
        {
            Instrument[] lowInstruments =
            [
                new Instrument(InstrumentName.Flute, InstrumentType.Wind, "", 0, 20, 10, Quality.Low,
                    [GenreName.Pop, GenreName.Rock], []),
                new Instrument(InstrumentName.Guitar, InstrumentType.String, "", 0, 20, 10, Quality.Low,
                    [GenreName.Pop, GenreName.Rock], []),
                new Instrument(InstrumentName.Mic, InstrumentType.Vocal, "", 0, 20, 10, Quality.Low,
                    [GenreName.Pop, GenreName.Rock], []),
                new Instrument(InstrumentName.Erhu, InstrumentType.String, "", 0, 20, 10, Quality.Low,
                    [GenreName.Pop, GenreName.Rock], []),
                new Instrument(InstrumentName.Cymbal, InstrumentType.Percussion, "", 0, 20, 10, Quality.Low,
                    [GenreName.Pop, GenreName.Rock], []),

            ];
            return new Deck<Instrument>("Low Quality Instruments", "", new Stack<Instrument>(lowInstruments));
        }
    }
}
