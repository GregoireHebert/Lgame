using System;

public class GameManager : Singleton<GameManager>
{
    public GameState State;

    private EndGameChecker _endGameChecker;

    void Start()
    {
        _endGameChecker = new EndGameChecker();

        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState)
    {
        if (State == newState)
        {
            return;
        }

        State = newState;
        switch (newState)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SpawnPieces:
                UnitManager.Instance.SpawnUnits();
                break;
            case GameState.PlayerOneMoveShape:
                UnityEngine.Debug.Log(_endGameChecker.IsGameOver(UnitManager.Instance.GetGamePosition()));
                if (_endGameChecker.IsGameOver(UnitManager.Instance.GetGamePosition()))
                {
                    LevelManager.Instance.LoadScene("HomeScreen");
                }

                UnitManager.Instance.SelectPlayerOneUnit();
                MenuManager.Instance.ToggleShapeButtons();
                break;
            case GameState.PlayerOneMoveCoin:
                UnitManager.Instance.SetSelectedUnit(null);
                MenuManager.Instance.ToggleForwardButton();
                break;
            case GameState.PlayerTwoMoveShape:
                UnityEngine.Debug.Log(_endGameChecker.IsGameOver(UnitManager.Instance.GetGamePosition()));
                if (_endGameChecker.IsGameOver(UnitManager.Instance.GetGamePosition()))
                {
                    LevelManager.Instance.LoadScene("HomeScreen");
                }

                UnitManager.Instance.SelectPlayerTwoUnit();
                MenuManager.Instance.ToggleShapeButtons();
                break;
            case GameState.PlayerTwoMoveCoin:
                UnitManager.Instance.SetSelectedUnit(null);
                MenuManager.Instance.ToggleForwardButton();
                break;
            case GameState.GameEnded:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}

public enum GameState
{
    Init = -1,
    GenerateGrid = 0,
    SpawnPieces = 1,
    PlayerOneMoveShape = 2,
    PlayerOneMoveCoin = 3,
    PlayerTwoMoveShape = 4,
    PlayerTwoMoveCoin = 5,
    GameEnded = 6
}

[Serializable]
class InvalidStateException : Exception
{
    public InvalidStateException() : base("Invalid game state") { }
}