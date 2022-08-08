using System;

[Serializable]
public class Quest 
{
    public string questTitle;
    public string questObjective;
    public int objectiveAmount;
    public int archiveAmount;
    public int rewardExp;
    public int rewardGold;
    public string questStatus;
    public Quest()
    {
    }

    public Quest(string title, string objective, int amount, int archive, int exp, int gold, string status)
    {
        questTitle = title;
        questObjective = objective;
        objectiveAmount = amount;
        archiveAmount = archive;
        rewardExp = exp;
        rewardGold = gold;
        questStatus = status;
    }
}
