using System.Numerics;
using System;
using System.Reflection.Metadata;
using System.Resources;
using System.Diagnostics.CodeAnalysis;

namespace SoundShowdownGame
{

    public class Player
    {
        public const int DefaultHealth = 10; // Starting Health Points
        public const int DefaultActions = 3; // Starting amount of actions

        public string Id { get; init; } // Unique player id
        public GenreName? Genre { get; set; } // Chosen class (Genre) of the player
        public int Health { get; set; } = DefaultHealth; // Total current health points
        public Instrument? Instrument { get; set; } // List of instruments that the player owns
        public Inventory Inventory { get; set; } = new();
        public List<Upgrade> Accessories = [];
        public Upgrade? SuitUpgrade { get; set; }
        public Enemy? Enemy { get; set; } // The opponent that the player is facing

        public bool IsDefeated => Health <= 0; // True if the player runs out of health

        public Player(string id)
        {
            Id = id;
        }

        public Player(string id, GenreName? genre, int health, Instrument? instrument, Inventory inventory, Enemy? enemy, Upgrade? suitUpgrade, List<Upgrade> accessories)
        {
            Id = id;
            Genre = genre;
            Health = health;
            Instrument = instrument;
            Inventory = inventory;
            Enemy = enemy;
            SuitUpgrade = suitUpgrade;
            Accessories = accessories;
        }

        public void TakeDamage<T>(int damage, T opponent)
        {
            Health -= damage;

            // Check if you ran out of health points
            if (IsDefeated)
            {
                Defeated(opponent);
            }
        }

        // If a player is defeated, check whether they were defeated by a musician or an enemy
        public void Defeated<T>(T opponent)
        {
            switch (opponent)
            {
                case Enemy enemy:
                    // NEEDS IMPLEMENTATION
                    break;
                case Musician musician:
                    // NEEDS IMPLEMENTATION
                    break;
                case null:
                    throw new SoundShowdownException("Opponent is null. Opponent must be an Enemy or Musician.");
                default:
                    throw new SoundShowdownException($"Opponent is of type {opponent.GetType()}. Must be of type Enemy or Musician");
            }
        }

        // Checks if player has enough space to add a new upgrade
        public bool CheckUpgradeSpace(Upgrade upgrade, SoundShowdown game)
        {
            switch (upgrade.Type)
            {
                case UpgradeType.Suit:
                    if (SuitUpgrade != null) return false; 
                    break;
                case UpgradeType.Accessory:
                    if (Accessories.Count == 2) return false; 
                    break;
                case UpgradeType.Instrument_General:
                case UpgradeType.Instrument_Unique:
                case UpgradeType.Instrument_Type:
                    if (Instrument?.Upgrades.Count == 2) return false; 
                    break;
            }
            return true;
        }

        public void AddUpgrade(Upgrade upgrade)
        {
            // Build the upgrade then add it to player's body or instrument
            Inventory.BuildUpgrade(upgrade);

            switch (upgrade.Type)
            {
                case UpgradeType.Suit:
                    SuitUpgrade = upgrade;  break;
                case UpgradeType.Accessory:
                    Accessories.Add(upgrade); break;
                case UpgradeType.Instrument_General:
                case UpgradeType.Instrument_Unique:
                case UpgradeType.Instrument_Type:
                    Instrument?.Upgrades.Add(upgrade); break;
            }
        }

        public void ValidatePlayerHasUpgrade(Upgrade replacedUpgrade)
        {
            foreach (var upgrade in Accessories)
            {
                if (replacedUpgrade.Name == upgrade.Name) return;
            }
            if (SuitUpgrade?.Name == replacedUpgrade.Name) return;
            
            foreach (var upgrade in Instrument.Upgrades)
            {
                if (upgrade.Name == replacedUpgrade.Name) return;
            }
            throw new SoundShowdownException("Player does not have the upgrade that is getting replaced.");
        }

        // Validates the player can afford this instrument
        public void ValidateInstrumentCost(Instrument instrument)
        {
            int tradeInValue = 0;
            // Check if the player has an instrument
            if (Instrument != null)
            {
                // Get number of upgrades on instrument
                int upgradeCount = Instrument.Upgrades.Count;

                tradeInValue = Instrument.Cost / 2 + (upgradeCount*5); // Instruments are traded in for half price plus 5 coins per upgrade
            }

            int leftOverAmount = instrument.Cost - tradeInValue; // Amount of player's own coins that they must spend

            // Check if player has enough coins
            if (Inventory.Coins < leftOverAmount) throw new SoundShowdownException("Player does not have enough coins to buy this instrument.");
        }

        public void ReplaceUpgrade(Upgrade newUpgrade, Upgrade replacedUpgrade)
        {
            // Validate upgrades are the same type, then replace
            switch (newUpgrade.Type)
            {
                case UpgradeType.Suit:
                    if (replacedUpgrade.Type == UpgradeType.Suit)
                    {
                        AddUpgrade(newUpgrade);
                    }
                    else throw new SoundShowdownException($"Upgrades do not go in the same slot: New Upgrade Type: {newUpgrade.Type}, Old Upgrade Type: {replacedUpgrade.Type}");
                    break;
                case UpgradeType.Accessory:
                    if (replacedUpgrade.Type == UpgradeType.Accessory)
                    {
                        Accessories.Remove(replacedUpgrade);
                        AddUpgrade(newUpgrade);
                    }
                    else throw new SoundShowdownException($"Upgrades do not go in the same slot: New Upgrade Type: {newUpgrade.Type}, Old Upgrade Type: {replacedUpgrade.Type}");
                    break;
                case UpgradeType.Instrument_General:
                case UpgradeType.Instrument_Unique:
                case UpgradeType.Instrument_Type:
                    if (replacedUpgrade.Type == UpgradeType.Instrument_Unique || replacedUpgrade.Type == UpgradeType.Instrument_Type ||
                        replacedUpgrade.Type == UpgradeType.Instrument_General)
                    {
                        Instrument.Upgrades.Remove(replacedUpgrade);
                        AddUpgrade(newUpgrade);
                    }
                    else throw new SoundShowdownException($"Upgrades do not go in the same slot: New Upgrade Type: {newUpgrade.Type}, Old Upgrade Type: {replacedUpgrade.Type}");
                    break;
            }
        }
    }
}
