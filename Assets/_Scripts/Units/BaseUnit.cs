using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public Tile OccupiedTile;
    public Side Side;
    protected int tilesValue;
    public Rotation rotation;
    public bool mirror = false;

    public int getTilesValue()
    {
        return tilesValue;
    }

    public virtual int calculateTilesValue(int position)
    {
        return 1 << position;
    }

    public void setTilesValue(int position)
    {
        tilesValue = calculateTilesValue(position);
    }

    public virtual int getAllowedSquares() {
        return 0; // actually not needed here;
    }

    public virtual void rotateRight() {
        // do nothing
    }

    public virtual void rotateLeft() {
        // do nothing
    }

    public virtual void toggleMirror() {
        mirror = !mirror;
    }
}

// A rotation represent a number of degrees to the right
public enum Rotation {
    zero = 0,
    quarter = 90,
    half = 180,
    threeQuarter = 270,
}
