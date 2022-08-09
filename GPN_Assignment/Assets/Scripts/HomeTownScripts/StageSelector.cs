using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class StageSelector : MonoBehaviour
{
    public TextMeshProUGUI stage;
    public void PlayGame()
    {
        string stageNo = stage.text.ToString();
        int number = int.Parse(stageNo) + 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + number);
    }
}
