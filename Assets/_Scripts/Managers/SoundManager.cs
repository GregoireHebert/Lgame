using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonPersistent<SoundManager>
{
    [SerializeField] private AudioSource DropPieceSource;

    public void playSound(AudioClip clip)
    {
        DropPieceSource.PlayOneShot(clip);
    }

    // from 0 to 1;
    public void changeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void toggleEffects()
    {
        DropPieceSource.mute = !DropPieceSource.mute;
    }
}
