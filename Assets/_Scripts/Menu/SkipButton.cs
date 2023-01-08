using UnityEngine;

public class SkipButton : MonoBehaviour
{
    #nullable enable
    private Tutorial? _tutorial;
#nullable disable    

    void Start()
    {
        _tutorial = Tutorial.Instance ?? null;
    }

    public void Skip()
    {
        if (GameManager.Instance.State == GameState.PlayerOneMoveCoin)
        {
            GameManager.Instance.ChangeState(GameState.PlayerTwoMoveShape);
        }

        if (GameManager.Instance.State == GameState.PlayerTwoMoveCoin)
        {
            GameManager.Instance.ChangeState(GameState.PlayerOneMoveShape);
        }

        if (null != _tutorial) {
            _tutorial.NextStep();
        }
    }
}

