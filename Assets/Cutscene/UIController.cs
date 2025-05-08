using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public DialogManager dialogManager;
    public TextMeshProUGUI dialogText;
    public Image dialogImage;
    public Button nextButton;
    public Button backButton;
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
            dialogImage.sprite = dialog.image;
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
        nextButton.interactable = dialogManager.CanMoveNext();
    }

    private void OnNextButtonClick()
    {
        if (dialogManager.MoveNext())
        {
            ShowDialog(dialogManager.GetCurrentDialog());
        }
        UpdateButtonState();
    }
}
