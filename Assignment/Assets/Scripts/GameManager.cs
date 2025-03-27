using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float gameTime = 120f;
    private float currentTime;

    public Text timerText;
    public GameObject gameOverPanel;
    public Text gameOverText;

    public int correctPlacements = 0;
    public int totalObjects = 5;

    void Start()
    {
        InitializeGame();
    }

    void Update()
    {
        UpdateTimer();
    }

    void InitializeGame()
    {
        currentTime = gameTime;
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    void UpdateTimer()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            EndGame();
        }

        timerText.text = "Time: " + Mathf.Round(currentTime) + "s";
    }

    public void CheckGameCompletion()
    {
        if (correctPlacements >= totalObjects)
        {
            EndGame(true);
        }
    }

    void EndGame(bool playerWon = false)
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);

        gameOverText.text = playerWon
            ? "Congratulations! You Win!"
            : "Time's Up! Game Over.";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}