using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraShake;

public class WindCameraShake : MonoBehaviour
{
    public float WindMagnitude;
    public float WindRoughness;
    public float WindFadeInTime;
    public float WindFadeOutTime;

    private void Update()
    {
        if (PauseMenu.Instance == null || PauseMenu.Instance != null && !PauseMenu.Instance.IsPaused) {
            CameraShaker.Instance?.ShakeOnce((WindMagnitude * (float.Parse((Settings.Get("Screenshake Intensity", 1f).ToString())))), WindRoughness, WindFadeInTime, WindFadeOutTime);
        }
    }
}
