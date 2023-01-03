using UnityEngine;

public class Forward : MonoBehaviour
{
    public void forward()
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

