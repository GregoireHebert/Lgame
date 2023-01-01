using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;
    
    private EndGameChecker EndGameChecker;

    private Animator ForwardAnimator;
    private Animator RotateLeftAnimator;
    private Animator RotateRightAnimator;
    private Animator MirrorAnimator;

    void Awake() {
        Instance = this;
    }

    void Start() {
        EndGameChecker = new EndGameChecker();
        ForwardAnimator = MenuManager.Instance.Forward.GetComponent<Animator>();
        RotateLeftAnimator = MenuManager.Instance.RotateLeft.GetComponent<Animator>();
        RotateRightAnimator = MenuManager.Instance.RotateRight.GetComponent<Animator>();
        MirrorAnimator = MenuManager.Instance.Mirror.GetComponent<Animator>();
        
        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState) {
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

                UnitManager.Instance.SelectPlayerOneUnit();
                toggleShapeButtons();
                break;
            case GameState.PlayerOneMoveCoin:
                toggleForwardButton();
                break;
            case GameState.PlayerTwoMoveShape:
                UnityEngine.Debug.Log(EndGameChecker.isGameOver(UnitManager.Instance.getGamePosition()));

                UnitManager.Instance.SelectPlayerTwoUnit();
                toggleShapeButtons();
                break;
            case GameState.PlayerTwoMoveCoin:
                toggleForwardButton();
                break;
            case GameState.GameEnded:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void toggleShapeButtons() {
        ForwardAnimator.ResetTrigger("show");
        ForwardAnimator.SetTrigger("hide");

        RotateLeftAnimator.SetTrigger("show");
        RotateRightAnimator.SetTrigger("show");
        MirrorAnimator.SetTrigger("show");
    }

    private void toggleForwardButton() {
        ForwardAnimator.ResetTrigger("hide");
        ForwardAnimator.SetTrigger("show");

        RotateLeftAnimator.SetTrigger("hide");
        RotateRightAnimator.SetTrigger("hide");
        MirrorAnimator.SetTrigger("hide");
        UnitManager.Instance.SetSelectedUnit(null);
    }
}

public enum GameState {
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