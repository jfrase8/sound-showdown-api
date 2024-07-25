using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGame
{
    public class Shop(Deck<Instrument> exoticInstrumentsDeck, Deck<Instrument> highInstrumentsDeck, Deck<Instrument> goodInstrumentsDeck, Deck<Instrument> lowInstrumentsDeck, List<Item> items)
    {
        public Deck<Instrument> ExoticInstrumentsDeck { get; set; } = exoticInstrumentsDeck;
        public Deck<Instrument> HighInstrumentsDeck { get; set; } = highInstrumentsDeck;
        public Deck<Instrument> GoodInstrumentsDeck { get; set; } = goodInstrumentsDeck;
        public Deck<Instrument> LowInstrumentsDeck { get; set; } = lowInstrumentsDeck;
        public List<Item> Items { get; set; } = items;
        public List<Instrument> ResaleInstruments { get; set; } = [];
    }
}
