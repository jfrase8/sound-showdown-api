namespace SoundShowdownGame
{
    public class Resource(string Name, string Description, int SellValue)
    {
        public string Name { get; set; } = Name;
        public string Description { get; set; } = Description;
        public int SellValue { get; set;} = SellValue; // How much you can sell the resources for
    }
}
