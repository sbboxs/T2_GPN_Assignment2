using System;

[Serializable]
public class Equipment
{
    public string equipmentType;
    public int equipmentArritbute;
    public int equipmentEnchantLvl;
    public int equipmentEnchantCost;

    public Equipment (string type, int attribute, int enchantLvl, int cost)
    {
        equipmentType = type;
        equipmentArritbute = attribute;
        equipmentEnchantLvl = enchantLvl;
        equipmentEnchantCost = cost;
    }
}
    
