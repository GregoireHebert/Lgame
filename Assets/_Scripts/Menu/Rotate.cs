using UnityEngine;

public class Rotate : MonoBehaviour
{
    public void Right()
    {
        if (GameManager.Instance.State == GameState.PlayerOneMoveShape)
        {
            UnitManager.Instance.PlayerOneUnit.RotateRight();
            return;
        }

        if (GameManager.Instance.State == GameState.PlayerTwoMoveShape)
        {
            UnitManager.Instance.PlayerTwoUnit.RotateRight();
            return;
        }
    }

    public void Left()
    {
        if (GameManager.Instance.State == GameState.PlayerOneMoveShape)
        {
            UnitManager.Instance.PlayerOneUnit.RotateLeft();
            return;
        }

        if (GameManager.Instance.State == GameState.PlayerTwoMoveShape)
        {
            UnitManager.Instance.PlayerTwoUnit.RotateLeft();
            return;
        }
    }
}

