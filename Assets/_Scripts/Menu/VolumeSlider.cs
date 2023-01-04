using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _volume;

    public void Start()
    {
        Settings settings = SettingsManager.Instance.GetSettings();
        _slider.value = settings.Volume ;

        SoundManager.Instance.ChangeMasterVolume(_slider.value);
     
        _slider.onValueChanged.AddListener(val => {
            Settings settings = SettingsManager.Instance.GetSettings();
            SoundManager.Instance.ChangeMasterVolume(val);
            
            settings.Volume = val;
            SettingsManager.Instance.UpdateSettings(settings);
        });
    }
}
