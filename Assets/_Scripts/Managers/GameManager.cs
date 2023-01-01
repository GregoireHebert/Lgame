using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState State;

    private EndGameChecker EndGameChecker;

    void Start() {
        EndGameChecker = new EndGameChecker();

        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState) {
        if (State == newState) {
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
                UnityEngine.Debug.Log(EndGameChecker.isGameOver(UnitManager.Instance.getGamePosition()));
                if (EndGameChecker.isGameOver(UnitManager.Instance.getGamePosition())) {
                    LevelManager.Instance.LoadScene("HomeScreen");
                }

                UnitManager.Instance.SelectPlayerOneUnit();
                MenuManager.Instance.toggleShapeButtons();
                break;
            case GameState.PlayerOneMoveCoin:
                UnitManager.Instance.SetSelectedUnit(null);
                MenuManager.Instance.toggleForwardButton();
                break;
            case GameState.PlayerTwoMoveShape:
                UnityEngine.Debug.Log(EndGameChecker.isGameOver(UnitManager.Instance.getGamePosition()));
                if (EndGameChecker.isGameOver(UnitManager.Instance.getGamePosition())) {
                    LevelManager.Instance.LoadScene("HomeScreen");
                }

                UnitManager.Instance.SelectPlayerTwoUnit();
                MenuManager.Instance.toggleShapeButtons();
                break;
            case GameState.PlayerTwoMoveCoin:
                UnitManager.Instance.SetSelectedUnit(null);
                MenuManager.Instance.toggleForwardButton();
                break;
            case GameState.GameEnded:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}

public enum GameState {
    init = -1,
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
    public InvalidStateException() : base("Invalid game state") {  }
}