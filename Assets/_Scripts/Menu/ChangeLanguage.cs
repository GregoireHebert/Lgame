using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using TMPro;

public class ChangeLanguage : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;

    public void Start()
    {
        Settings settings = SettingsManager.Instance.GetSettings();

        _dropdown.value = settings.Language;
    }

    public void SetLanguage(int identifier)
    { 
        Settings settings = SettingsManager.Instance.GetSettings();
        settings.Language = identifier;
        SettingsManager.Instance.UpdateSettings(settings);

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[identifier];
    }
}
