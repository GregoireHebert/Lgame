using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonPersistent<SoundManager>
{
    [SerializeField] private AudioSource _dropPieceSource;

    public void PlaySound(AudioClip clip)
    {
        _dropPieceSource.PlayOneShot(clip);
    }

    // from 0 to 1;
    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void ToggleEffects(bool toggleEffects)
    {
        _dropPieceSource.mute = !toggleEffects;
    }

    public bool IsSoundEffectsMuted()
    {
        return _dropPieceSource.mute;
    }
}
