namespace SoundShowdownGame
{
    public interface IBattleEntity
    {
        int Health { get; set; } // Health points

        // Taking damage reduces your health points
        void TakeDamage(int damage)
        {
            Health -= damage;

            // Check if you ran out of health points
            if (Health <= 0) Defeated();
        }

        // Default method so battle entities can do something specific when defeated. Parameter specifies who got defeated (Player, Enemy, Musician)
        void Defeated();
        void Attack();
    }
}
