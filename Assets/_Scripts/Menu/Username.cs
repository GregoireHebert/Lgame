using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Username : MonoBehaviour
{
    [SerializeField] private string _username;
    private TMP_InputField _input;

    void Start() {
        _input = this.GetComponent<TMP_InputField>();
        Settings settings = SettingsManager.Instance.GetSettings();

        _input.text = settings.Username ?? Settings.createName();
    }

    public void OnDisable()
    {
        Settings settings = SettingsManager.Instance.GetSettings();

        settings.Username = _input.text;
        SettingsManager.Instance.UpdateSettings(settings);
    }
}
