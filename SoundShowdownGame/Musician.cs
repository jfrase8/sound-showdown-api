namespace SoundShowdownGame
{
    // IBattleEntity implements TakeDamage() function
    public class Musician(MusicianName name, int health, int attack, StatusEffect? effect, string specialPowerDesc, int rank)
    {
        public MusicianName Name { get; init; } = name;
        public int Health { get; set; } = health;
        public int Attack { get; set; } = attack;
        public StatusEffect? Effect { get; init; } = effect;
        public string SpecialPowerDesc { get; init;} = specialPowerDesc;
        public int Rank { get; init; } = rank;
    }
}
