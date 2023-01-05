using System.Runtime.Serialization;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.SmartFormat.Extensions;
using UnityEngine.Localization.SmartFormat.GlobalVariables;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveSettings(Settings settings)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.lgame";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, settings);
        stream.Close();
    }

    public static Settings LoadSettings()
    {
        string path = Application.persistentDataPath + "/settings.lgame";

        if (File.Exists(path)) {
            try {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                Settings settings = formatter.Deserialize(stream) as Settings;
                stream.Close();
                return settings;
            } catch (SerializationException) 
            {

            }
        }

        UnityEngine.Debug.Log($"Save file not found in {path}");
        return new Settings() { Volume = (float)0.7, SoundEffects = true, Language = 0 };
    }

    public static void SaveWinner(Player player)
    {
        var source = LocalizationSettings
            .StringDatabase
            .SmartFormatter
            .GetSourceExtension<PersistentVariablesSource>();

        // Get the specific global variable
        var winner = source["global"]["Winner"] as UnityEngine.Localization.SmartFormat.PersistentVariables.IntVariable;

        // Update the global variable
        winner.Value = (int) player;
    }
}
