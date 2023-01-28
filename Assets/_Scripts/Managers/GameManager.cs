using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameState _state;
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private UnitManager _unitManager;
    [SerializeField] private MenuManager _menuManager;
    [SerializeField] private EndGameChecker _endGameChecker;
    [SerializeField] private Player _player;

    public void Start()
    {
        _endGameChecker = new EndGameChecker();
        _gridManager.GenerateGrid();
        _unitManager.SpawnUnits();

        foreach (Tile tile in _gridManager.GetTiles().Values) {
            tile.MouseDown.AddListener(TryMoveSelectedPiece);
        }

        ChangeState(GameState.PlayerOneMoveShape);
    }

    public void ChangeState(GameState newState)
    {
        if (_state == newState)
        {
            return;
        }

        _state = newState;

        switch (newState)
        {
            case GameState.PlayerOneMoveShape:
                if (_endGameChecker.IsGameOver(_unitManager.GetGamePosition(GameState.PlayerOneMoveShape)))
                {
                    ChangeState(GameState.GameEnded);
                    break;
                }

                _player = Player.PlayerOne;
                _unitManager.SelectPlayerOneUnit();
                _menuManager.ToggleShapeButtons();
                if (Tutorial.Instance) Tutorial.Instance.NextStep();

                break;
            case GameState.PlayerOneMoveCoin:
                _unitManager.DeselectedUnit();
                _menuManager.ToggleForwardButton();
                if (Tutorial.Instance) Tutorial.Instance.NextStep();
                break;
            case GameState.PlayerTwoMoveShape:
                if (_endGameChecker.IsGameOver(_unitManager.GetGamePosition(GameState.PlayerTwoMoveShape)))
                {
                    ChangeState(GameState.GameEnded);
                    break;
                }

                _player = Player.PlayerTwo;
                _unitManager.SelectPlayerTwoUnit();
                _menuManager.ToggleShapeButtons();
                if (Tutorial.Instance) Tutorial.Instance.NextStep();
                break;
            case GameState.PlayerTwoMoveCoin:

                _unitManager.DeselectedUnit();
                _menuManager.ToggleForwardButton();
                if (Tutorial.Instance) Tutorial.Instance.NextStep();
                break;
            case GameState.GameEnded:
                SaveSystem.SaveWinner(_player);
                LevelManager.Instance.LoadScene("EndGame");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }  
    }
    
    public void TryMoveSelectedPiece(Tile tile)
    {
        _tryMoveCoin(tile);
        _tryMoveShape(tile);
    }

    private void _tryMoveShape(Tile tile)
    {
        // turn to move a shape
        if (GameState.PlayerOneMoveShape != _state && GameState.PlayerTwoMoveShape != _state)
        {
            return;
        }

        if (
            // clicked on an empty cell, or if clicked on selected unit tile allow it if the shape changed its position
            (tile.OccupiedUnit == null || (tile.OccupiedUnit == _unitManager.SelectedUnit && _unitManager.SelectedUnit.GetTilesValue() != _unitManager.SelectedUnit.CalculateTilesValue(tile.Position))) &&
            // then check if the selected position and tile is compatible
            false == _unitManager.UnitWouldOverflow(_unitManager.SelectedUnit, tile.Position) &&
            false == _unitManager.UnitWouldOverlap(_unitManager.SelectedUnit, tile.Position)
        )
        {
            tile.SetUnit(_unitManager.SelectedUnit);
            _unitManager.DeselectedUnit();

            // move to next game state
            GameState nextState = _state == GameState.PlayerOneMoveShape ? GameState.PlayerOneMoveCoin : GameState.PlayerTwoMoveCoin;
            ChangeState(nextState);
        }
    }

    private void _tryMoveCoin(Tile tile)
    {
        // turn to move a coin
        if (GameState.PlayerOneMoveCoin != _state && GameState.PlayerTwoMoveCoin != _state)
        {
            return;
        }

        BaseUnit occupiedUnit = tile.OccupiedUnit;

        // clicked on a tile occupied by a coin, select it.
        if (occupiedUnit != null && occupiedUnit.Side == Side.Neutral)
        {
            _unitManager.SelectUnit((BaseNeutral)occupiedUnit);
            if (Tutorial.Instance) Tutorial.Instance.NextStep();
            
            return;
        }

        if (
            occupiedUnit == null &&
            _unitManager.SelectedUnit != null &&
            false == _unitManager.UnitWouldOverlap(_unitManager.SelectedUnit, tile.Position)
        )
        {
            tile.SetUnit(_unitManager.SelectedUnit);
            _unitManager.DeselectedUnit();

            // move to next game state
            GameState nextState = _state == GameState.PlayerOneMoveCoin ? GameState.PlayerTwoMoveShape : GameState.PlayerOneMoveShape;
            ChangeState(nextState);

            return;
        }
    }

    public void SkipCoinMove()
    {
        if (_state == GameState.PlayerOneMoveCoin)
        {
            ChangeState(GameState.PlayerTwoMoveShape);
            return;
        }

        if (_state == GameState.PlayerTwoMoveCoin)
        {
            ChangeState(GameState.PlayerOneMoveShape);
            return;
        }
    }
}


[Serializable]
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