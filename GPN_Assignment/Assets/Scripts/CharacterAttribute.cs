using System;

[Serializable]
public class CharacterAttribute
{
    //Attribute Stats
    public int strength;
    public int defense;
    public int health;
    public int level;

    //Resources
    public int gold;

    //Stats Pt
    public int strengthStatsPt;
    public int defenseStatsPt;
    public int healthStatsPt;
    public int remainingStatsPt;
    public CharacterAttribute(int strength, int defense, int health, int level, int gold, int strengthPt, int defensePt, int healthPt, int remainingPt)
    {
        this.strength = strength;
        this.defense = defense;
        this.health = health;
        this.level = level;
        this.gold = gold;
        strengthStatsPt = strengthPt;
        defenseStatsPt = defensePt;
        healthStatsPt = healthPt;
        remainingStatsPt = remainingPt;
    }
}
