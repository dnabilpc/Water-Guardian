using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [Header("Game Over Panel")]
    public GameObject gameOverPanel;
    public Button restartButton;
    public Button mainMenuButton;
    public TextMeshProUGUI enemiesLeftText; // Perbaiki dari TextMeshProGUI ke TextMeshProUGUI
    public TextMeshProUGUI waveText; // Perbaiki dari TextMeshProGUI ke TextMeshProUGUI


    [Header("Audio Settings")]
    public bool playGameOverMusic = true;
    public bool playGameOverSFX = true;

    private void Start()
    {
        gameOverPanel.SetActive(false);

        /* restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(GoToMainMenu); */
        // Debug untuk memastikan button reference ada
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
            Debug.Log("Restart button found and listener added");
        }
        else
        {
            Debug.LogError("Restart button is NULL! Please assign in Inspector");
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(GoToMainMenu);
            Debug.Log("Main menu button found and listener added");
        }
        else
        {
            Debug.LogError("Main menu button is NULL! Please assign in Inspector");
        }
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;

        // Play Game Over Audio
        PlayGameOverAudio();

        // Tampilkan enemies left dan wave info
        if (enemiesLeftText != null)
        {
            int enemiesLeft = LevelManager.main.EnemiesLeft;
            enemiesLeftText.text = "Enemies Left: " + enemiesLeft.ToString();
        }

        if (waveText != null && EnemySpawner.main != null)
        {
            int currentWave = EnemySpawner.main.GetCurrentWave();
            waveText.text = "Wave Reached: " + currentWave.ToString();
        }
    }

    private void PlayGameOverAudio()
    {
        if (AudioManager.Instance != null)
        {
            // Play game over sound effect
            if (playGameOverSFX)
            {
                AudioManager.Instance.PlayGameOverAudio();
            }

        }
        else
        {
            Debug.LogWarning("AudioManager instance not found!");
        }
    }

    public void RestartGame()
    {
        // log
        Debug.Log("Restarting game...");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        // log
        Debug.Log("Going to main menu...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }
}
