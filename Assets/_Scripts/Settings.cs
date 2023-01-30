using System;

[System.Serializable]
public class Settings : ICloneable
{
    public float Volume;
    public bool SoundEffects;
    public int Language;
    public string Username;

    public object Clone()
    {
        return (Settings)MemberwiseClone();
    }

    public static string createName()
    {
        String[] first = new String[] {"Gentle", "Purring", "Brilliant", "Flappy", "Bubbling", "Uncanny", "Fast", "Furious", "Massive"};
        String[] last = new String[] {"Dog", "Cat", "Dalmatian", "Bird", "Goldfish", "Turtle", "Sloth", "Ferret", "Elephant"};
        
        Random rnd = new Random();
        
        int x = rnd.Next(0,first.Length);
        int y = rnd.Next(0,last.Length);
        
        return first[x] + " " + last[y];
    }
}
