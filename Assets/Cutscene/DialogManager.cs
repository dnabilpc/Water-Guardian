using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog {
    public string text;
    public Sprite image;
}
public class DialogManager : MonoBehaviour
{
    public Dialog[] dialogs;
    private int currentIndex = 0;

    public Dialog GetCurrentDialog() {
        if (currentIndex >= 0 && currentIndex < dialogs.Length) {
            return dialogs[currentIndex];
        }
        return null;
    }

    public bool MoveNext() {
        if (currentIndex + 1 < dialogs.Length) {
            currentIndex++;
            return true;
        }
        return false;
    }
    public bool CanMoveNext() {
        return currentIndex < dialogs.Length - 1;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
