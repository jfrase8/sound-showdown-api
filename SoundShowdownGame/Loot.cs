namespace SoundShowdownGame
{
    public class Loot(string Name, string Description, int SellValue)
    {
        public string Name { get; set; } = Name;
        public string Description { get; set; } = Description;
        public int SellValue { get; set;} = SellValue;
    }
}
