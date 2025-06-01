using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public DialogManager dialogManager;
    public TextMeshProUGUI dialogText;
    public TextMeshProUGUI nameText;
    public Image dialogImage;
    public Button nextButton;
    public float typingSpeed = 0.05f;

    void Start()
    {
        ShowDialog(dialogManager.GetCurrentDialog());
        nextButton.onClick.AddListener(OnNextButtonClick);
        UpdateButtonState();
    }

    private void ShowDialog(Dialog dialog)
    {
        if (dialog != null)
        {
            StopAllCoroutines();
            StartCoroutine(TypeText(dialog.text));
            // StartCoroutine(TypeText(dialog.name));
            dialogImage.sprite = dialog.image;
            nameText.text = dialog.characterName;

        }
        else
        {
            Debug.Log("No found Dialog");
            
        }
    }

    private IEnumerator TypeText(string text)
    {
        dialogText.text = "";

        foreach (char letter in text.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void UpdateButtonState()
    {
        nextButton.interactable = true;
    }

    private void OnNextButtonClick()
    {
        if (dialogManager.CanMoveNext())
        {
            dialogManager.MoveNext();
            ShowDialog(dialogManager.GetCurrentDialog());
        }
        else
        {
            SceneManager.LoadScene("Map1Scene");
        }
    }
    
}
