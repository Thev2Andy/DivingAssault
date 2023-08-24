using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GraphicsFunctions : MonoBehaviour
{
    [Header("Button References")]
    public TMP_Dropdown ResolutionDropdown;
    public TMP_Text FullscreenModeButton;
    public TMP_Text MotionBlurButton;
    public TMP_Text VSyncButton;


    public void SetResolution(int ResolutionIndex)
    {
        Resolution[] Resolutions = Screen.resolutions;
        Resolution[] UniqueResolutions = Resolutions.GroupBy(IndividualResolution => new { IndividualResolution.width, IndividualResolution.height })
                                       .Select(Grouping => Grouping.OrderByDescending(ResolutionRefreshRate => ResolutionRefreshRate.refreshRate).First())
                                       .ToArray();

        string ResolutionText = $"{UniqueResolutions[ResolutionIndex].width} x {UniqueResolutions[ResolutionIndex].height}";
        Settings.Set("Resolution", ResolutionText);
    }

    public void ToggleFullscreen()
    {
        bool Fullscreen = System.Convert.ToBoolean((Settings.Get("Fullscreen", Screen.fullScreen).ToString()));
        Settings.Set("Fullscreen", !Fullscreen);
    }

    public void ToggleMotionBlur()
    {
        bool MotionBlur = System.Convert.ToBoolean((Settings.Get("Enable Motion Blur", false).ToString()));
        Settings.Set("Enable Motion Blur", !MotionBlur);
    }

    public void ToggleVSync()
    {
        bool VSync = System.Convert.ToBoolean((Settings.Get("VSync", false).ToString()));
        Settings.Set("VSync", !VSync);
    }


    public void RefreshUI()
    {
        Resolution[] Resolutions = Screen.resolutions;
        Resolution[] UniqueResolutions = Resolutions.GroupBy(IndividualResolution => new { IndividualResolution.width, IndividualResolution.height })
                                       .Select(Grouping => Grouping.OrderByDescending(ResolutionRefreshRate => ResolutionRefreshRate.refreshRate).First())
                                       .ToArray();

        ResolutionDropdown.ClearOptions();
        List<string> Options = new List<string>();

        string Resolution = Settings.Get("Resolution", $"{Screen.width} x {Screen.height}").ToString();
        string[] Tokens = Resolution.Split(new string[] { " x " }, System.StringSplitOptions.RemoveEmptyEntries);
        int Width = int.Parse(Tokens[0]);
        int Height = int.Parse(Tokens[1]);

        int CurrentResolutionIndex = -1;
        for (int I = 0; I < UniqueResolutions.Length; I++)
        {
            Options.Add($"{UniqueResolutions[I].width} x {UniqueResolutions[I].height}");
            
            if (UniqueResolutions[I].width == Width && UniqueResolutions[I].height == Height) {
                CurrentResolutionIndex = I;
            }
        }

        ResolutionDropdown.AddOptions(Options);
        ResolutionDropdown.SetValueWithoutNotify(CurrentResolutionIndex);
        ResolutionDropdown.RefreshShownValue();


        bool Fullscreen = System.Convert.ToBoolean((Settings.Get("Fullscreen", Screen.fullScreen).ToString()));
        FullscreenModeButton.text = $"Fullscreen: {((Fullscreen) ? "On" : "Off")}";

        bool MotionBlur = System.Convert.ToBoolean((Settings.Get("Enable Motion Blur", Screen.fullScreen).ToString()));
        MotionBlurButton.text = $"Motion Blur: {((MotionBlur) ? "On" : "Off")}";

        bool VSync = System.Convert.ToBoolean((Settings.Get("VSync", Screen.fullScreen).ToString()));
        VSyncButton.text = $"VSync: {((VSync) ? "On" : "Off")}";
    }


    private void Apply()
    {
        string Resolution = Settings.Get("Resolution", $"{Screen.width} x {Screen.height}").ToString();
        string[] Tokens = Resolution.Split(new string[] { " x " }, System.StringSplitOptions.RemoveEmptyEntries);
        int Width = int.Parse(Tokens[0]);
        int Height = int.Parse(Tokens[1]);
        bool Fullscreen = System.Convert.ToBoolean((Settings.Get("Fullscreen", Screen.fullScreen).ToString()));

        Screen.SetResolution(Width, Height, Fullscreen);


        bool VSync = System.Convert.ToBoolean((Settings.Get("VSync", false).ToString()));
        QualitySettings.vSyncCount = ((VSync) ? 1 : 0);
    }

    private void Awake()
    {
        Settings.SettingsChanged += RefreshUI;
        Settings.SettingsChanged += Apply;

        this.RefreshUI();
        this.Apply();
    }

    private void OnDestroy()
    {
        Settings.SettingsChanged -= RefreshUI;
        Settings.SettingsChanged -= Apply;
    }
}
