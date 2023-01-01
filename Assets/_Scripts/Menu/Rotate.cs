using UnityEngine;

public class Rotate : MonoBehaviour
{
    public void Right() {
        if (GameManager.Instance.State == GameState.PlayerOneMoveShape) {
            UnitManager.Instance.PlayerOneUnit.rotateRight();
            return;
        }

        if (GameManager.Instance.State == GameState.PlayerTwoMoveShape) {
            UnitManager.Instance.PlayerTwoUnit.rotateRight();
            return;
        }
    }

    public void Left() {
        if (GameManager.Instance.State == GameState.PlayerOneMoveShape) {
            UnitManager.Instance.PlayerOneUnit.rotateLeft();
            return;
        }

        if (GameManager.Instance.State == GameState.PlayerTwoMoveShape) {
            UnitManager.Instance.PlayerTwoUnit.rotateLeft();
            return;
        }
    }
}

