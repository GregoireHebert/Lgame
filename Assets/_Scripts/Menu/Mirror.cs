using UnityEngine;

public class Mirror : MonoBehaviour
{
    public void ToggleMirror()
    {
        if (GameManager.Instance.State == GameState.PlayerOneMoveShape)
        {
            UnitManager.Instance.PlayerOneUnit.ToggleMirror();
            return;
        }

        if (GameManager.Instance.State == GameState.PlayerTwoMoveShape)
        {
            UnitManager.Instance.PlayerTwoUnit.ToggleMirror();
            return;
        }
    }
}

