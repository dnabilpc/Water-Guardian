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


        if (finalCurrencyText != null)
        {
            finalCurrencyText.text = "Final Currency: " + LevelManager.main.currency.ToString();
        }
    }

    private void NextLevel()
    {
        Time.timeScale = 1f;
        // Load next level scene - sesuaikan dengan nama scene level berikutnya
        SceneManager.LoadScene("Level2"); // Atau gunakan build index
    }

    private void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }
}
