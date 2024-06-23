namespace SoundShowdownGame
{
    public interface IBattleEntity
    {
        int Health { get; set; } // Health points
        bool IsDefeated { get; set; } // True if battle entity runs out of health
        Type EntityType { get; set; } // What class this battle entity is

        // Taking damage reduces your health points
        void TakeDamage(int damage)
        {
            Health -= damage;

            // Check if you ran out of health points
            if (Health <= 0) IsDefeated = true;
        }

        // Default method so battle entities can do something specific when defeated. Parameter specifies who got defeated (Player, Enemy, Musician)
        void Defeated();
        void Attack();
    }
}
