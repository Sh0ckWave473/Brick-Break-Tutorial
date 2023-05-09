using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenuScript : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        if (PlayerPrefs.GetString("HIGHSCORENAME") != null)
            highScoreText.text = "High score set by " + PlayerPrefs.GetString("HIGHSCORENAME") + ": " + PlayerPrefs.GetInt("HIGHSCORE");
    }
    //needs to be public since it is for a button
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    //Remember to import UnityEngine.SceneManagement in order to run SceneManager
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
