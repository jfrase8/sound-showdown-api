namespace SoundShowdownGame
{
    public interface IBattleEntity
    {
        int Health { get; set; } // Health points
        bool IsDefeated { get; set; } // True if battle entity runs out of health

        // Taking damage reduces your health points
        void TakeDamage(int damage);

        // Default method so battle entities can do something specific when defeated
        void Defeated();
    }
}
