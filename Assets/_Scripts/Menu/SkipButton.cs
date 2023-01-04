using UnityEngine;

public class SkipButton : MonoBehaviour
{
    public void Skip()
    {
        if (GameManager.Instance.State == GameState.PlayerOneMoveCoin)
        {
            GameManager.Instance.ChangeState(GameState.PlayerTwoMoveShape);
            return;
        }

        if (GameManager.Instance.State == GameState.PlayerTwoMoveCoin)
        {
            GameManager.Instance.ChangeState(GameState.PlayerOneMoveShape);
            return;
        }
    }
}

