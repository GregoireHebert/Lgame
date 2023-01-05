using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState State;

    private EndGameChecker _endGameChecker;

    [SerializeField] private Player _player;

    void Start()
    {
        _endGameChecker = new EndGameChecker();

        ChangeState(GameState.GenerateGrid);
    }

    public Player GetPlayer() 
    {
        return _player;
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
                    ChangeState(GameState.GameEnded);
                    break;
                }

                _player = Player.PlayerOne;
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
                    ChangeState(GameState.GameEnded);
                    break;
                }

                _player = Player.PlayerTwo;
                UnitManager.Instance.SelectPlayerTwoUnit();
                MenuManager.Instance.ToggleShapeButtons();
                break;
            case GameState.PlayerTwoMoveCoin:
                UnitManager.Instance.SetSelectedUnit(null);
                MenuManager.Instance.ToggleForwardButton();
                break;
            case GameState.GameEnded:
                SaveSystem.SaveWinner(_player);
                LevelManager.Instance.LoadScene("EndGame");
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

public enum Player
{
    None = 0,
    PlayerOne = 1,
    PlayerTwo = 2,
}

[Serializable]
class InvalidStateException : Exception
{
    public InvalidStateException() : base("Invalid game state") { }
}