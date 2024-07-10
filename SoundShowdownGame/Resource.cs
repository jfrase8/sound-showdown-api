namespace SoundShowdownGame
{
    public class Resource(string name, int sellValue)
    {
        public string Name { get; set; } = name;
        public int SellValue { get; set;} = sellValue; // How much you can sell the resources for
    }
}
