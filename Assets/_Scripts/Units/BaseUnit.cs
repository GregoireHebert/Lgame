using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour, IEquatable<BaseUnit>
{
    public Tile OccupiedTile;
    public Side Side;
    protected int TilesValue;
    protected Rotation Rotation;
    protected bool Mirror = false;
#nullable enable
    private Tutorial? _tutorial;
#nullable disable  

    void Start()
    {
        _tutorial = Tutorial.Instance ?? null;
    }

    public bool Equals(BaseUnit obj)
    {
        return TilesValue == obj.GetTilesValue();
    }

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
        if (null != _tutorial) {
            _tutorial.NextStep();
        }
    }

    public virtual void RotateLeft()
    {
       if (null != _tutorial) {
            _tutorial.NextStep();
        }
    }

    public virtual void ToggleMirror()
    {
        if (null != _tutorial) {
            _tutorial.NextStep();
        }
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
