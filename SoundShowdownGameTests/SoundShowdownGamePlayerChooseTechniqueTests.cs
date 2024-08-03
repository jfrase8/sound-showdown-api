using SoundShowdownGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGameTests
{
    [TestClass]
    public class SoundShowdownGamePlayerChooseTechniqueTests
    {
        [TestMethod]
        public void PlayerChooseTechnique_InvalidState()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Attack)
                .Build();

            Upgrade technique = new Upgrade(UpgradeName.Trill, UpgradeType.Technique, "", null, null, UpgradeEffectType.Passive);

            try
            {
                game.PlayerChooseTechnique(technique, "1hsdfosdn2");
                Assert.Fail("PlayerChooseTechnique should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerChooseTechnique_InvalidPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Technique)
                .Build();

            Upgrade technique = new Upgrade(UpgradeName.Trill, UpgradeType.Technique, "", null, null, UpgradeEffectType.Passive);

            try
            {
                game.PlayerChooseTechnique(technique, "5384043508");
                Assert.Fail("PlayerChooseTechnique should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerChooseTechnique_WrongPlayer()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Technique)
                .Build();

            Upgrade technique = new Upgrade(UpgradeName.Trill, UpgradeType.Technique, "", null, null, UpgradeEffectType.Passive);

            try
            {
                game.PlayerChooseTechnique(technique, "sad83908230");
                Assert.Fail("PlayerChooseTechnique should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerChooseTechnique_NoInstrument()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Technique)
                .Build();

            Upgrade technique = new Upgrade(UpgradeName.Trill, UpgradeType.Technique, "", null, null, UpgradeEffectType.Passive);

            try
            {
                game.PlayerChooseTechnique(technique, "1hsdfosdn2");
                Assert.Fail("PlayerChooseTechnique should have thrown exception.");
            }
            catch (SoundShowdownException)
            {

            }
        }

        [TestMethod]
        public void PlayerChooseTechnique_Success()
        {
            SoundShowdown game = new SoundShowdownBuilder()
                .WithPlayer(new PlayerBuilder().WithId("1hsdfosdn2").WithGenre(GenreName.Jazz).WithInstrument(new InstrumentBuilder().Build()).Build())
                .WithPlayer(new PlayerBuilder().WithId("sad83908230").WithGenre(GenreName.Pop).Build())
                .WithCurrentGameState(GameState.Awaiting_Player_Choose_Technique)
                .Build();

            List<SoundShowdownEventArgs> events = new List<SoundShowdownEventArgs>();

            game.SoundShowdownEvent += delegate (object? sender, SoundShowdownEventArgs args)
            {
                events.Add(args);
            };

            Upgrade technique = new Upgrade(UpgradeName.Trill, UpgradeType.Technique, "", null, null, UpgradeEffectType.Passive);

            game.PlayerChooseTechnique(technique, "1hsdfosdn2");

            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(SoundShowdownEventType.TechniqueLearned, events[0].EventType);
            Assert.IsTrue(events[0] is TechniqueLearnedEvent);
            TechniqueLearnedEvent techniqueLearnedEvent = (TechniqueLearnedEvent)events[0];
            Assert.AreEqual("1hsdfosdn2", techniqueLearnedEvent.Player.Id);

            Assert.AreEqual(technique, techniqueLearnedEvent.Technique);
            Assert.AreEqual(1, techniqueLearnedEvent.Player.Instrument.Techniques.Count);
        }
    }
}
