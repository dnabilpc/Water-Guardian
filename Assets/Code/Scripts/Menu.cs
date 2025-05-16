using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private TextMeshProUGUI currencyText;

    private void OnGUI()
    {
        // Update the currency text with the current currency value
        currencyText.text = LevelManager.main.currency.ToString();
    }

    public void SetSelected()
    {

    }
}
