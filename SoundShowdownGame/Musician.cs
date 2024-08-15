
using SoundShowdownGame.Enums;

namespace SoundShowdownGame
{
    // IBattleEntity implements TakeDamage() function
    public class Musician(MusicianName name, int health, int damage, StatusEffect? effect, string specialPowerDesc, int rank, int coinReward)
    {
        public MusicianName Name { get; init; } = name;
        public int DefaultHealth { get; init; } = health;
        public int Health { get; set; } = health;
        public int Damage { get; set; } = damage;
        public StatusEffect? Effect { get; init; } = effect;
        public string SpecialPowerDesc { get; init;} = specialPowerDesc;
        public int Rank { get; init; } = rank;
        public List<Player> DefeatedBy { get; set; } = [];
        public Player? AttackingPlayer { get; set; }
        public int CoinReward { get; set; } = coinReward;
        public bool IsDefeated => Health <= 0;

        public void TakeDamage(int damage)
        {
            Health -= damage;

            // Check if you ran out of health points
            if (Health <= 0)
            {
                Defeated();
            }
        }

        // Defeated() is called when an enemy has run out of health.
        public void Defeated()
        {
            if (AttackingPlayer == null) throw new SoundShowdownException("The attacking player has not been assigned to this enemy.");

            // Player gets reward
            AttackingPlayer.Inventory.Coins += CoinReward;

            // Player gets body exp equal to musician health
            AttackingPlayer.BodyExp += DefaultHealth;

            // Move player up the musician rank track
            AttackingPlayer.MusicianTrackRank++;

            // Add this player to the list of players who have defeated this musician
            DefeatedBy.Add(AttackingPlayer);
        }
    }
}
