namespace SoundShowdownGame;

public class Item(ItemName name, string description, int price)
{
    public ItemName Name { get; set; } = name;
    public string Description { get; set; } = description;
    public int Price { get; set; } = price;
}