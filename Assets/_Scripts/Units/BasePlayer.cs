using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : BaseUnit
{
    public override int GetAllowedSquares()
    {
        switch (rotation)
        {
            case Rotation.zero:
                return mirror == false ?
                    (1 << 1) | (1 << 2) | (1 << 3) | (1 << 5) | (1 << 6) | (1 << 7) :
                    (1 << 0) | (1 << 1) | (1 << 2) | (1 << 4) | (1 << 5) | (1 << 6);
            case Rotation.quarter:
                return mirror == false ?
                    (1 << 6) | (1 << 7) | (1 << 10) | (1 << 11) | (1 << 14) | (1 << 15) :
                    (1 << 2) | (1 << 3) | (1 << 6) | (1 << 7) | (1 << 10) | (1 << 11);
            case Rotation.half:
                return mirror == false ?
                    (1 << 8) | (1 << 9) | (1 << 10) | (1 << 12) | (1 << 13) | (1 << 14) :
                    (1 << 9) | (1 << 10) | (1 << 11) | (1 << 13) | (1 << 14) | (1 << 15);
            case Rotation.threeQuarter:
                return mirror == false ?
                    (1 << 0) | (1 << 1) | (1 << 4) | (1 << 5) | (1 << 8) | (1 << 9) :
                    (1 << 4) | (1 << 5) | (1 << 8) | (1 << 9) | (1 << 12) | (1 << 13);
            default: return 0;
        }
    }

    public override int calculateTilesValue(int position)
    {
        switch (rotation)
        {
            case Rotation.zero:
                return mirror == false ?
                        (1 << position) | (1 << (position - 1)) | (1 << (position + 4)) | (1 << (position + 8)) :
                        (1 << position) | (1 << (position + 1)) | (1 << (position + 4)) | (1 << (position + 8));
            case Rotation.quarter:
                return mirror == false ?
                        (1 << position) | (1 << (position - 1)) | (1 << (position - 2)) | (1 << (position - 4)) :
                        (1 << position) | (1 << (position - 1)) | (1 << (position - 2)) | (1 << (position + 4));
            case Rotation.half:
                return mirror == false ?
                        (1 << position) | (1 << (position + 1)) | (1 << (position - 4)) | (1 << (position - 8)) :
                        (1 << position) | (1 << (position - 1)) | (1 << (position - 4)) | (1 << (position - 8));
            case Rotation.threeQuarter:
                return mirror == false ?
                        (1 << position) | (1 << (position + 1)) | (1 << (position + 2)) | (1 << (position + 4)) :
                        (1 << position) | (1 << (position + 1)) | (1 << (position + 2)) | (1 << (position - 4));
            default:
                return 1 << position;
        }
    }

    public override void rotateRight()
    {
        switch (rotation)
        {
            case Rotation.zero:
                rotation = Rotation.quarter;
                break;
            case Rotation.quarter:
                rotation = Rotation.half;
                break;
            case Rotation.half:
                rotation = Rotation.threeQuarter;
                break;
            case Rotation.threeQuarter:
                rotation = Rotation.zero;
                break;
            default:
                rotation = Rotation.zero;
                break;
        }

        Vector3 rotationToAdd = new Vector3(0, 0, -90);
        transform.Rotate(rotationToAdd);
    }

    public override void rotateLeft()
    {
        switch (rotation)
        {
            case Rotation.zero:
                rotation = Rotation.threeQuarter;
                break;
            case Rotation.threeQuarter:
                rotation = Rotation.half;
                break;
            case Rotation.half:
                rotation = Rotation.quarter;
                break;
            case Rotation.quarter:
                rotation = Rotation.zero;
                break;
            default:
                rotation = Rotation.zero;
                break;
        }

        Vector3 rotationToAdd = new Vector3(0, 0, 90);
        transform.Rotate(rotationToAdd);
    }

    public override void toggleMirror()
    {
        mirror = !mirror;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}