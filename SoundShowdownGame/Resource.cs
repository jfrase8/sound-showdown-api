namespace SoundShowdownGame
{
    public class Resource(string name, string description, int sellValue)
    {
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
        public int SellValue { get; set;} = sellValue; // How much you can sell the resources for
    }
}
