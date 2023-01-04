using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : BaseUnit
{
    public override int GetAllowedSquares()
    {
        switch (Rotation)
        {
            case Rotation.Zero:
                return Mirror == false ?
                    (1 << 1) | (1 << 2) | (1 << 3) | (1 << 5) | (1 << 6) | (1 << 7) :
                    (1 << 0) | (1 << 1) | (1 << 2) | (1 << 4) | (1 << 5) | (1 << 6);
            case Rotation.Quarter:
                return Mirror == false ?
                    (1 << 6) | (1 << 7) | (1 << 10) | (1 << 11) | (1 << 14) | (1 << 15) :
                    (1 << 2) | (1 << 3) | (1 << 6) | (1 << 7) | (1 << 10) | (1 << 11);
            case Rotation.Half:
                return Mirror == false ?
                    (1 << 8) | (1 << 9) | (1 << 10) | (1 << 12) | (1 << 13) | (1 << 14) :
                    (1 << 9) | (1 << 10) | (1 << 11) | (1 << 13) | (1 << 14) | (1 << 15);
            case Rotation.ThreeQuarter:
                return Mirror == false ?
                    (1 << 0) | (1 << 1) | (1 << 4) | (1 << 5) | (1 << 8) | (1 << 9) :
                    (1 << 4) | (1 << 5) | (1 << 8) | (1 << 9) | (1 << 12) | (1 << 13);
            default: return 0;
        }
    }

    public override int CalculateTilesValue(int position)
    {
        switch (Rotation)
        {
            case Rotation.Zero:
                return Mirror == false ?
                        (1 << position) | (1 << (position - 1)) | (1 << (position + 4)) | (1 << (position + 8)) :
                        (1 << position) | (1 << (position + 1)) | (1 << (position + 4)) | (1 << (position + 8));
            case Rotation.Quarter:
                return Mirror == false ?
                        (1 << position) | (1 << (position - 1)) | (1 << (position - 2)) | (1 << (position - 4)) :
                        (1 << position) | (1 << (position - 1)) | (1 << (position - 2)) | (1 << (position + 4));
            case Rotation.Half:
                return Mirror == false ?
                        (1 << position) | (1 << (position + 1)) | (1 << (position - 4)) | (1 << (position - 8)) :
                        (1 << position) | (1 << (position - 1)) | (1 << (position - 4)) | (1 << (position - 8));
            case Rotation.ThreeQuarter:
                return Mirror == false ?
                        (1 << position) | (1 << (position + 1)) | (1 << (position + 2)) | (1 << (position + 4)) :
                        (1 << position) | (1 << (position + 1)) | (1 << (position + 2)) | (1 << (position - 4));
            default:
                return 1 << position;
        }
    }

    public override void RotateRight()
    {
        switch (Rotation)
        {
            case Rotation.Zero:
                Rotation = Rotation.Quarter;
                break;
            case Rotation.Quarter:
                Rotation = Rotation.Half;
                break;
            case Rotation.Half:
                Rotation = Rotation.ThreeQuarter;
                break;
            case Rotation.ThreeQuarter:
                Rotation = Rotation.Zero;
                break;
            default:
                Rotation = Rotation.Zero;
                break;
        }

        Vector3 rotationToAdd = new Vector3(0, 0, -90);
        transform.Rotate(rotationToAdd);
    }

    public override void RotateLeft()
    {
        switch (Rotation)
        {
            case Rotation.Zero:
                Rotation = Rotation.ThreeQuarter;
                break;
            case Rotation.ThreeQuarter:
                Rotation = Rotation.Half;
                break;
            case Rotation.Half:
                Rotation = Rotation.Quarter;
                break;
            case Rotation.Quarter:
                Rotation = Rotation.Zero;
                break;
            default:
                Rotation = Rotation.Zero;
                break;
        }

        Vector3 rotationToAdd = new Vector3(0, 0, 90);
        transform.Rotate(rotationToAdd);
    }

    public override void ToggleMirror()
    {
        Mirror = !Mirror;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}