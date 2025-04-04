using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class Item : ScriptableObject
{
    public string itemName;
    public ItemRarity rarity;
    public Sprite icon;
    public ItemType itemType;
    public ItemStats stats;
}

public enum ItemRarity
{
    Common,
    Magical,
    Rare,
    Epic,
    Legendary,
    Mythic,
    UltraMythic
}

public enum ItemType
{
    WeaponLeft,
    WeaponRight,
    WeaponTwoHand,
    Armor,
    Necklace,
    Ring,
    Helmet,
    Pants,
    Shoes,
    Wings
}

[System.Serializable]
public class ItemStats
{
    public int strength;
    public int agility;
    public int intelligence;
    public float criticalChance; 
    public float criticalDamage; 
    public float parry; 
    public float counterAttack; 
    public float counterAttackDamage; 
    public float dodge; 
    public int vitality; 
    public int physicalArmor; 
    public int magicalArmor; 
    public float comboChance;
    public float comboDamage;
    public float regen; 
    public float lifeSteal;
    
}
