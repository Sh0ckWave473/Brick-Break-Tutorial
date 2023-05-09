using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int lives, score, numberOfBricks;

    //New update does not use Text but instead TextMeshProUGUI
    public TextMeshProUGUI livesText, scoreText, highScoreText;

    public TMP_InputField highScoreInput;

    public bool gameOver;

    public GameObject gameOverPanel, loadLevelPanel;

    public Transform[] levels;

    public int currentLevelIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;

        //FindGameObjectsWithTag is an array which is why we need .Length
        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // public will make it available in the ball script
    public void UpdateLives(int changeInLives)
    {
        lives += changeInLives;

        //need to check for no lives left and trigger the end of the game
        if (lives <= 0)
        {
            lives = 0;
            GameOver();
        }

        livesText.text = "Lives: " + lives;
    }

    //public will make it available in the ball script
    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    public void UpdateNumberOfBricks()
    {
        numberOfBricks--;
        if (numberOfBricks <= 0)
        {
            if (currentLevelIndex >= levels.Length - 1)
                GameOver();
            else
            {
                loadLevelPanel.SetActive(true);
                //The text is its own object so you need to access the children of the panel
                loadLevelPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Loading Level " + (currentLevelIndex + 2);
                gameOver = true;
                //Allows you to call the function in the future
                Invoke("LoadLevel", 3f);
            }
        }
    }

    void LoadLevel()
    {
        currentLevelIndex++;
        //Quaternion.identity is zero rotation
        Instantiate(levels[currentLevelIndex], Vector2.zero, Quaternion.identity);
        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
        gameOver = false;
        loadLevelPanel.SetActive(false);
    }

    void GameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
        //Retrieves the score saved with the HIGHSCORE key
        int highScore = PlayerPrefs.GetInt("HIGHSCORE");
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HIGHSCORE", score);

            highScoreText.text = "NEW High Score!\nEnter your name below.";
            highScoreInput.gameObject.SetActive(true);
        }
        else
        {
            highScoreText.text = PlayerPrefs.GetString("HIGHSCORENAME") + "'s High Score: " + highScore + "\nTry again?";
        }
    }

    public void NewHighScore()
    {
        string highScoreName = highScoreInput.text;
        PlayerPrefs.SetString("HIGHSCORENAME", highScoreName);
        highScoreInput.gameObject.SetActive(false);
        highScoreText.text = "Congratulations " + highScoreName + "!\nYour new high score is " + score;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        SceneManager.LoadScene("StartMenu");
        Debug.Log("Quit");
    }
}
