using System;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class SettingsManager : SingletonPersistent<SettingsManager>
{
    private Settings _settings;

    public Settings GetSettings()
    {
        return (Settings)_settings.Clone();
    }

    protected override void Awake()
    {
        base.Awake();
        _settings = SaveSystem.LoadSettings();
    }

    public void Start()
    {
        SoundManager.Instance.ToggleEffects(_settings.SoundEffects);
        SoundManager.Instance.ChangeMasterVolume(_settings.Volume);
    }

    public void UpdateSettings(Settings settings)
    {
        _settings = settings;
        SaveSystem.SaveSettings(settings);
    }
}
