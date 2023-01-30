using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAudio : MonoBehaviour
{
    [SerializeField] private bool _toggleEffects;
    private Toggle _toggle;

    void Start() {
        _toggle = this.GetComponent<Toggle>();
        Settings settings = SettingsManager.Instance.GetSettings();

        _toggle.SetIsOnWithoutNotify(settings.SoundEffects);
    }

    public void Toggle()
    {
        Settings settings = SettingsManager.Instance.GetSettings();

        if (_toggleEffects)
        {
            settings.SoundEffects = !settings.SoundEffects;
            SoundManager.Instance.ToggleEffects(settings.SoundEffects);
            
            SettingsManager.Instance.UpdateSettings(settings);
        }
    }
}
