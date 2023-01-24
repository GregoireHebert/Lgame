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
    public bool Selected = false;
    private bool _coroutineAllowed = true;

    public void Update()
    {
        if (_coroutineAllowed && Selected) {
            StartCoroutine("StartPulsing");
        }
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
        // DO NOTHING
    }

    public virtual void RotateLeft()
    {
       // DO NOTHING
    }

    public virtual void ToggleMirror()
    {
        // DO NOTHING
    }

    private IEnumerator StartPulsing()
    {
        _coroutineAllowed = false;

        for (float i = 0f; i <= 2f; i += 0.1f) {
            transform.localScale = new UnityEngine.Vector3(
                (Mathf.Lerp(transform.localScale.x, transform.localScale.x + 0.001f, Mathf.SmoothStep(0f, 1f, i))),
                (Mathf.Lerp(transform.localScale.y, transform.localScale.y + 0.001f, Mathf.SmoothStep(0f, 1f, i))),
                (Mathf.Lerp(transform.localScale.z, transform.localScale.z + 0.001f, Mathf.SmoothStep(0f, 1f, i)))
            );
            yield return new WaitForSeconds(0.015f);
        }

        for (float i = 0f; i <= 2f; i += 0.1f) {
            transform.localScale = new UnityEngine.Vector3(
                (Mathf.Lerp(transform.localScale.x, transform.localScale.x - 0.001f, Mathf.SmoothStep(0f, 1f, i))),
                (Mathf.Lerp(transform.localScale.y, transform.localScale.y - 0.001f, Mathf.SmoothStep(0f, 1f, i))),
                (Mathf.Lerp(transform.localScale.z, transform.localScale.z - 0.001f, Mathf.SmoothStep(0f, 1f, i)))
            );
            yield return new WaitForSeconds(0.015f);
        }

        _coroutineAllowed = true;
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
