using SoundShowdownGame.Enums;

namespace SoundShowdownGame;

public delegate void ItemEffect(Player player);
public class Item(ItemName name, string description, int price)
{
    public ItemName Name { get; set; } = name;
    public string Description { get; set; } = description;
    public int Price { get; set; } = price;
    public ItemEffect? Effect { get; set; } // TODO : Add implementation of item effects
}