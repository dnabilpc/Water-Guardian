using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameWonUI : MonoBehaviour
{
    [Header("Game Won Panel")]
    public GameObject gameWonPanel;
    public Button nextLevelButton;
    public Button mainMenuButton;
    public TextMeshProUGUI finalCurrencyText;

    [Header("Custom Next Level Settings")]
    public string nextLevelSceneName = "Level2"; // Custom scene name
    public bool useSceneIndex = false;
    public int nextLevelSceneIndex = 2;

    [Header("Audio Settings")]
    public bool playVictoryMusic = true;
    public bool playSoundEffect = true;

    private void Start()
    {
        gameWonPanel.SetActive(false);

        if (nextLevelButton != null)
            nextLevelButton.onClick.AddListener(NextLevel);

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    public void ShowGameWonPanel()
    {
        gameWonPanel.SetActive(true);
        Time.timeScale = 0f;

        // Play Game Won Audio
        PlayGameWonAudio();

        if (finalCurrencyText != null)
        {
            finalCurrencyText.text = "Final Currency: " + LevelManager.main.currency.ToString();
        }
    }

    private void PlayGameWonAudio()
    {
        if (AudioManager.Instance != null)
        {
            // Play sound effect
            if (playSoundEffect)
            {
                AudioManager.Instance.PlayGameWonAudio();
            }

        }
        else
        {
            Debug.LogWarning("AudioManager instance not found!");
        }
    }

    private void NextLevel()
    {
        Time.timeScale = 1f;

        // Stop victory music and return to gameplay BGM
        if (AudioManager.Instance != null && playVictoryMusic)
        {
            AudioManager.Instance.PlayBGM(AudioManager.Instance.bgmGamePlay);
        }

        if (useSceneIndex)
        {
            SceneManager.LoadScene(nextLevelSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(nextLevelSceneName);
        }
    }
    private void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }
}
