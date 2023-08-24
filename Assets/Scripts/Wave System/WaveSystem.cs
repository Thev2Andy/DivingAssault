using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    // Public / Accessible variables..
    [Header("UI.")]
    public CanvasGroup WaveUI;
    public TMP_Text WaveText;
    public float WaveUIInterpolationTime;
    public float WaveUIDuration;

    // Private / Hidden variables..
    private float WaveUILifetime;
    private float WaveUIVelocity;

    // Properties..
    public int Wave { get; private set; }

    // Singletons..
    public static WaveSystem Instance;


    private void Update()
    {
        WaveUILifetime = Mathf.Max((WaveUILifetime - Time.deltaTime), 0f);
        WaveUI.alpha = Mathf.SmoothDamp(WaveUI.alpha, ((WaveUILifetime > 0f) ? 1f : 0f), ref WaveUIVelocity, (1 - Mathf.Exp(-WaveUIInterpolationTime * Time.deltaTime)));

        if (Input.GetKeyDown(KeyCode.J)) {
            ShowWaveUI(WaveUIDuration);
        }
    }

    public void ShowWaveUI(float Duration) {
        WaveUILifetime = Duration;
    }



    private void Awake()
    {
        if (Instance != this && Instance != null) Destroy(this);
        Instance = this;
    }
}
