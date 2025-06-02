using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneWithDelay(sceneName, 1.5f));
    }

    private IEnumerator LoadSceneWithDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(sceneName);

        if (sceneName == "MainMenuScene")
        {
            AudioManager.Instance?.PlayBGM(AudioManager.Instance.bgmMainMenu);
        }
        else if (sceneName == "CutsceneDialogue")
        {
            AudioManager.Instance?.PlayBGM(AudioManager.Instance.bgmCutscene);
        }
        else
        {
            Debug.LogWarning("No BGM set for scene: " + sceneName);
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
