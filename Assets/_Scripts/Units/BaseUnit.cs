using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public Tile OccupiedTile;
    public Side Side;
    protected int TilesValue;
    protected Rotation Rotation;
    protected bool Mirror = false;

    public int GetTilesValue()
    {
        return TilesValue;
    }

    public virtual int CalculateTilesValue(int position)
    {
        return 1 << position;
    }

    public void SetTilesValue(int position)
    {
        TilesValue = CalculateTilesValue(position);
    }

    public virtual int GetAllowedSquares()
    {
        return 0; // actually not needed here;
    }

    public virtual void RotateRight()
    {
        // do nothing
    }

    public virtual void RotateLeft()
    {
        // do nothing
    }

    public virtual void ToggleMirror()
    {
        Mirror = !Mirror;
    }
}

// A rotation represent a number of degrees to the right
public enum Rotation
{
    Zero = 0,
    Quarter = 90,
    Half = 180,
    ThreeQuarter = 270,
}
