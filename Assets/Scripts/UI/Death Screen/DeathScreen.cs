using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public int MenuSceneIndex;
    public TMP_Text WaveText;
    public CanvasGroup DeathUIGroup;
    public float AlphaInterpolationTime;

    // Private / Hidden variables..
    private float AlphaFadeVelocity;


    private void OnEnable() {
        WaveText.text = $"You got to wave <color=lightblue>{((WaveSystem.Instance != null) ? WaveSystem.Instance.Wave : -1)}</color>.";
    }

    private void Update() {
        DeathUIGroup.alpha = Mathf.SmoothDamp(DeathUIGroup.alpha, 1f, ref AlphaFadeVelocity, (1 - Mathf.Exp(-AlphaInterpolationTime * Time.deltaTime)));
    }

    public void Retry()
    {
        Fader.Instance.OnFadeIn += ReloadScene;
        Fader.Instance.FadeIn(AlphaInterpolationTime);
        DeathUIGroup.blocksRaycasts = false;
    }

    public void Quit()
    {
        Fader.Instance.OnFadeIn += QuitToMainMenu;
        Fader.Instance.FadeIn(AlphaInterpolationTime);
        DeathUIGroup.blocksRaycasts = false;
    }

    
    private void ReloadScene() {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Fader.Instance.OnFadeIn -= ReloadScene;
    }

    private void QuitToMainMenu() {
        SceneManager.LoadSceneAsync(MenuSceneIndex);
        Fader.Instance.OnFadeOut -= QuitToMainMenu;
    }
}
