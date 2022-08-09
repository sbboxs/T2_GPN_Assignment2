using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SetUpQuest : MonoBehaviour
{
    public TextMeshProUGUI questTitle;
    public TextMeshProUGUI questObjective;
    public TextMeshProUGUI objectiveAmount;
    public TextMeshProUGUI rewardEXP;
    public TextMeshProUGUI rewardGold;

    public GameObject acceptButton;
    public GameObject refreshButton;
    public GameObject onProgress;
    public GameObject completeButton;

    public static Quest currentQuest;
    private void OnEnable()
    {
        List<Quest> questList = DataHandler.ReadListFromJSON<Quest>("Quest");
        bool ifQuestAccept = false;
        foreach (Quest quest in questList)
        {
            if (quest.questStatus == "Accepted")
            {
                currentQuest = quest;
                updateQuest(false);
                ifQuestAccept = true;
                break;
            }
            else if(quest.questStatus == "Completed")
            {
                currentQuest = quest;
                updateQuest(true);
                ifQuestAccept = true;
                break;
            }
        }
        if (ifQuestAccept == false)
        {
            int randomNo = Random.Range(0, questList.Count);
            Debug.Log("RandomNo: " + randomNo);
            currentQuest = questList[randomNo];
       
            questTitle.text = currentQuest.questTitle;
            questObjective.text = currentQuest.questObjective;
            objectiveAmount.text = currentQuest.archiveAmount.ToString() + " / " + currentQuest.objectiveAmount;
            rewardEXP.text = currentQuest.rewardExp.ToString() +" EXP";
            rewardGold.text = currentQuest.rewardGold.ToString() + " G";
            acceptButton.SetActive(true);
            refreshButton.SetActive(true);
            onProgress.SetActive(false);
            completeButton.SetActive(false);

        }
    }
    private void updateQuest(bool ifCompleted)
    {
        questTitle.text = currentQuest.questTitle;
        questObjective.text = currentQuest.questObjective;
        objectiveAmount.text = currentQuest.archiveAmount.ToString() + " / " + currentQuest.objectiveAmount;
        rewardEXP.text = currentQuest.rewardExp.ToString() + " EXP";
        rewardGold.text = currentQuest.rewardGold.ToString() + " G";
        acceptButton.SetActive(false);
        refreshButton.SetActive(false);
        if (ifCompleted)
        {
            onProgress.SetActive(false);
            completeButton.SetActive(true);
        }
        else
        {
            onProgress.SetActive(true);
            completeButton.SetActive(false);
        }

    }

    public static Quest getCurrentQuest()
    {
        return currentQuest;
    }
}
