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
    }
}
