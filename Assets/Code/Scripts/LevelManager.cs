using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;

    public int currency;
    [SerializeField] private int maxLives = 3;  // Jumlah maksimal enemy yang boleh lolos
    private int currentLives;  // Jumlah nyawa yang tersisa

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
        // Tambahkan logika game over di sini
        // Misalnya memunculkan UI game over, menghentikan spawn enemy, dll
        Time.timeScale = 0f;  // Menghentikan game
    }
}
