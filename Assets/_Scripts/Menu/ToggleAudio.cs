using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAudio : MonoBehaviour
{
    [SerializeField] private bool toggleEffects;

    public void toggle()
    {
        if (toggleEffects)
        {
            SoundManager.Instance.toggleEffects();
        }
    }
}
