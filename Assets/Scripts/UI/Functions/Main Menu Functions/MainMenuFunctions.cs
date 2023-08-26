using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunctions : MonoBehaviour
{
    public float AlphaInterpolationTime;
    public int GameSceneIndex;
    public CanvasGroup MenuGroup;

    public void Play() {
        Fader.Instance.OnFadeIn += LoadGameScene;
        Fader.Instance.FadeIn(AlphaInterpolationTime);
        MenuGroup.blocksRaycasts = false;
    }

    public void Quit() { 
        Application.Quit();
    }


    private void LoadGameScene() {
        SceneManager.LoadScene(GameSceneIndex);
        Fader.Instance.OnFadeIn -= LoadGameScene;
    }
}
