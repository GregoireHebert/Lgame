using System;
using UnityEngine;
using Unity.Netcode;

public class OnlineGameManager : NetworkBehaviour
{
    [SerializeField] private NetworkVariable<GameState> _state = new NetworkVariable<GameState>();
    [SerializeField] private OnlineGridManager _gridManager;
    [SerializeField] private OnlineUnitManager _unitManager;
    [SerializeField] private OnlineMenuManager _menuManager;
    [SerializeField] private EndGameChecker _endGameChecker;
    [SerializeField] private NetworkVariable<Player> _player = new NetworkVariable<Player>();
    
    public void Start()
    {
        _state.OnValueChanged += OnStateChanged;

        _endGameChecker = new EndGameChecker();
        _gridManager.GenerateGrid();
        _unitManager.SpawnUnits();

        foreach (Tile tile in _gridManager.GetTiles().Values) {
            tile.MouseDown.AddListener(TryMoveSelectedPiece);
        }

        if (!IsOwner) {
            ChangeState(GameState.PlayerOneMoveShape);
        }
    }

    public void ChangeState(GameState newState)
    {
        if (_state.Value == newState)
        {
            return;
        }

        ChangeStateServerRpc(newState);
    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeStateServerRpc(GameState newState)
    {
        _state.Value = newState;
    }

    public void OnStateChanged(GameState oldState, GameState newState) 
    {
        switch (newState)
        {
            case GameState.PlayerOneMoveShape:
                if (_endGameChecker.IsGameOver(_unitManager.GetGamePosition(GameState.PlayerOneMoveShape)))
                {
                    ChangeState(GameState.GameEnded);
                    break;
                }

                _player.Value = Player.PlayerOne;
                _unitManager.SelectPlayerOneUnit();
                _menuManager.ToggleShapeButtons();
                break;
            case GameState.PlayerOneMoveCoin:
                _unitManager.DeselectedUnit();
                _menuManager.ToggleForwardButton();
                break;
            case GameState.PlayerTwoMoveShape:
                if (_endGameChecker.IsGameOver(_unitManager.GetGamePosition(GameState.PlayerTwoMoveShape)))
                {
                    ChangeState(GameState.GameEnded);
                    break;
                }

                _player.Value = Player.PlayerTwo;
                _unitManager.SelectPlayerTwoUnit();
                _menuManager.ToggleShapeButtons();
                break;
            case GameState.PlayerTwoMoveCoin:
                _unitManager.DeselectedUnit();
                _menuManager.ToggleForwardButton();
                break;
            case GameState.GameEnded:
                SaveSystem.SaveWinner(_player.Value);
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
        if (GameState.PlayerOneMoveShape != _state.Value && GameState.PlayerTwoMoveShape != _state.Value)
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
            moveShapeServerRpc(tile.Position);

            // move to next game state
            GameState nextState = _state.Value == GameState.PlayerOneMoveShape ? GameState.PlayerOneMoveCoin : GameState.PlayerTwoMoveCoin;
            ChangeState(nextState);
        }
    }
    
    [ServerRpc(RequireOwnership = false)]
    private void moveShapeServerRpc(int position) 
    {
        moveShapeClientRpc(position);
    }

    [ServerRpc(RequireOwnership = false)]
    public void MirrorSelectedUnitServerRpc()
    {
        MirrorSelectedUnitClientRpc();
    }
    
    [ServerRpc(RequireOwnership = false)]
    public void RotateSelectedUnitRightServerRpc()
    {
        RotateSelectedUnitRightClientRpc();
    }
    
    [ServerRpc(RequireOwnership = false)]
    public void RotateSelectedUnitLeftServerRpc()
    {
        RotateSelectedUnitLeftClientRpc();
    }

    [ClientRpc]
    private void moveShapeClientRpc(int position) 
    {
        Tile tile = _gridManager.GetTileAtPosition(position);

        tile.SetUnit(_unitManager.SelectedUnit);
        _unitManager.DeselectedUnit();
    }
    
    [ClientRpc]
    public void MirrorSelectedUnitClientRpc()
    {
        _unitManager.MirrorSelectedUnit();
    }
    
    [ClientRpc]
    public void RotateSelectedUnitRightClientRpc()
    {
        _unitManager.RotateSelectedUnitRight();
    }
    
    [ClientRpc]
    public void RotateSelectedUnitLeftClientRpc()
    {
        _unitManager.RotateSelectedUnitLeft();
    }

    [ServerRpc(RequireOwnership = false)]
    public void SelectUnitAtPositionServerRpc(int position)
    {
        SelectUnitAtPositionClientRpc(position);
    }

    [ClientRpc]
    public void SelectUnitAtPositionClientRpc(int position)
    {
        Tile tile = _gridManager.GetTileAtPosition(position);
        _unitManager.SelectUnit((BaseNeutral)tile.OccupiedUnit);
    }

    private void _tryMoveCoin(Tile tile)
    {
        // turn to move a coin
        if (GameState.PlayerOneMoveCoin != _state.Value && GameState.PlayerTwoMoveCoin != _state.Value)
        {
            return;
        }

        BaseUnit occupiedUnit = tile.OccupiedUnit;

        // clicked on a tile occupied by a coin, select it.
        if (occupiedUnit != null && occupiedUnit.Side == Side.Neutral)
        {
            SelectUnitAtPositionServerRpc(tile.Position);
            
            return;
        }

        if (
            occupiedUnit == null &&
            _unitManager.SelectedUnit != null &&
            false == _unitManager.UnitWouldOverlap(_unitManager.SelectedUnit, tile.Position)
        )
        {
            moveShapeServerRpc(tile.Position);

            // move to next game state
            GameState nextState = _state.Value == GameState.PlayerOneMoveCoin ? GameState.PlayerTwoMoveShape : GameState.PlayerOneMoveShape;
            ChangeState(nextState);

            return;
        }
    }

    public void SkipCoinMove()
    {
        if (_state.Value == GameState.PlayerOneMoveCoin)
        {
            ChangeState(GameState.PlayerTwoMoveShape);
            return;
        }

        if (_state.Value == GameState.PlayerTwoMoveCoin)
        {
            ChangeState(GameState.PlayerOneMoveShape);
            return;
        }
    }
}