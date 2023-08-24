using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleableObject : MonoBehaviour
{
    public string SettingToDetect;
    public bool Default;
    public bool Flip;

    [Space]

    public GameObject ObjectToToggle;


    public void Toggle() {
        bool SettingValue = System.Convert.ToBoolean(Settings.Get(SettingToDetect, Default));
        ObjectToToggle.SetActive(((!Flip) ? SettingValue : !SettingValue));
    }


    private void Awake() {
        Settings.SettingsChanged += Toggle;
        this.Toggle();
    }

    private void OnDestroy() {
        Settings.SettingsChanged -= Toggle;
    }
}
