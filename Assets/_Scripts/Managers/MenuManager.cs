using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public GameObject Forward;
    public GameObject RotateLeft;
    public GameObject RotateRight;
    public GameObject Mirror;

    void Awake() {
        Instance = this;
    }
}

