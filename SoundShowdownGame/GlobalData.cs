namespace SoundShowdownGame
{
    public static class GlobalData
    {
        public static readonly Dictionary<ResourceName, int> Resources = new()
        {
            { ResourceName.Plastic, 3 },
            { ResourceName.Wire, 3 },
            { ResourceName.Glass, 3 },
            { ResourceName.Adhesive, 3 },
            { ResourceName.Leather, 3 },
            { ResourceName.Tin_Can, 3 },
            { ResourceName.String, 3 },
            { ResourceName.Fur, 5 },
            { ResourceName.Bone, 5 },
            { ResourceName.Metal_Scrap, 5 },
            { ResourceName.Vial_Of_Poison, 5 },
            { ResourceName.Slime, 5 },
            { ResourceName.Batteries, 7 },
            { ResourceName.Crystal, 10 }
        };

        public static readonly Dictionary<int, ResourceName> ScavengeDiceRolls = new()
        {
            { 2, ResourceName.Glass},
            { 3, ResourceName.Tin_Can },
            { 4, ResourceName.String },
            { 5, ResourceName.Plastic },
            { 6, ResourceName.Wire },
        };

        public static readonly Dictionary<MusicianName, string> MusicianPowers = new()
        {
            { MusicianName.Dirty_Dan, "Powers up after every attack, gaining 2 damage and 2 health." },
            { MusicianName.Rex_Rhythm, "Cannot be afflicted with any status effects." }
        };

        public static int RollToDamage(int roll, int level, Quality quality) // TODO : Add different values based on quality in addition to level
        {
            int damage;
            if (level == 1)
            {
                damage = roll switch
                {
                    1 => 0,
                    2 => 2,
                    3 => 3,
                    4 => 4,
                    5 => 5,
                    6 => 6,
                    _ => throw new SoundShowdownException($"Invalid roll value: {roll}")
                };
            }
            else if (level == 2)
            {
                damage = roll switch
                {
                    1 => 0,
                    2 => 3,
                    3 => 4,
                    4 => 5,
                    5 => 6,
                    6 => 7,
                    _ => throw new SoundShowdownException($"Invalid roll value: {roll}")
                };
            }
            else
            {
                damage = roll switch
                {
                    1 => 2,
                    2 => 4,
                    3 => 5,
                    4 => 6,
                    5 => 7,
                    6 => 8,
                    _ => throw new SoundShowdownException($"Invalid roll value: {roll}")
                };
            }
            return damage;
        }
    }
}
