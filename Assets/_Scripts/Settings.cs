using System;

[System.Serializable]
public class Settings : ICloneable
{
    public float Volume;
    public bool SoundEffects;
    public int Language;

    public object Clone()
    {
        return (Settings)MemberwiseClone();
    }
}
