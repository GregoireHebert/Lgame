using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nouvelle unité", menuName = "Scriptable Unit", order = 0)]
public class ScriptableUnit : ScriptableObject {
    public Side Side;
    public BaseUnit UnitPrefab;
}

public enum Side {
    Neutral = 0,
    PlayerOne = 1,
    PlayerTwo = 2,
}