using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;

    public int currency;
    [SerializeField] private int maxLives = 3;
    private int currentLives;

    [Header("UI References")]
    public GameOverUI gameOverUI;
    public GameWonUI gameWonUI; // Tambahkan reference untuk GameWonUI

    // Tambahkan property untuk tracking enemies
    public int EnemiesLeft
    {
        get
        {
            if (EnemySpawner.main != null)
                return EnemySpawner.main.GetEnemiesLeft();
            return 0;
        }
    }

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        currency = 100;
        currentLives = maxLives;
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (currency >= amount)
        {
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("Not enough currency");
            return false;
        }
    }

    public void ReduceLives()
    {
        currentLives--;
        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over! Too many enemies reached the end!");

        if (gameOverUI != null)
        {
            gameOverUI.ShowGameOverPanel();
        }
        else
        {
            Time.timeScale = 0f;
        }
    }

    // Tambahkan method untuk Game Won
    public void GameWon()
    {
        Debug.Log("Game Won! All waves completed!");

        if (gameWonUI != null)
        {
            gameWonUI.ShowGameWonPanel();
        }
        else
        {
            Time.timeScale = 0f;
        }
    }
}
