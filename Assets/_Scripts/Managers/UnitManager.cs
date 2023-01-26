using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] private GridManager _gridManager;
    private List<ScriptableUnit> _units;
    public BaseUnit SelectedUnit;
    public BasePlayerOne PlayerOneUnit;
    public BasePlayerTwo PlayerTwoUnit;
    public BaseNeutral CoinOneUnit;
    public BaseNeutral CoinTwoUnit;

    public void Awake()
    {
        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

    public void SpawnUnits()
    {
        SpawnCoins();
        SpawnPlayers();
    }

    private void SpawnCoins()
    {
        var coinPrefab = GetUnit<BaseNeutral>(Side.Neutral);

        var spawnedCoinOne = Instantiate(coinPrefab);
        var spawnedCoinOneTile = _gridManager.GetCoinOneSpawnTile();
        spawnedCoinOneTile.SetUnit(spawnedCoinOne);
        CoinOneUnit = spawnedCoinOne;

        var spawnedCoinTwo = Instantiate(coinPrefab);
        var spawnedCoinTwoTile = _gridManager.GetCoinTwoSpawnTile();
        spawnedCoinTwoTile.SetUnit(spawnedCoinTwo);
        CoinTwoUnit = spawnedCoinTwo;
    }

    private void SpawnPlayers()
    {
        var playerOnePrefab = GetUnit<BasePlayerOne>(Side.PlayerOne);
        var playerTwoPrefab = GetUnit<BasePlayerTwo>(Side.PlayerTwo);

        var spawnedPlayerOneUnit = Instantiate(playerOnePrefab);
        var spawnedPlayerOneTile = _gridManager.GetPlayerOneSpawnTile();
        spawnedPlayerOneTile.SetUnit(spawnedPlayerOneUnit);
        PlayerOneUnit = spawnedPlayerOneUnit;

        var spawnedPlayerTwoUnit = Instantiate(playerTwoPrefab);
        var spawnedPlayerTwoTile = _gridManager.GetPlayerTwoSpawnTile();
        spawnedPlayerTwoTile.SetUnit(spawnedPlayerTwoUnit);
        PlayerTwoUnit = spawnedPlayerTwoUnit;
    }

    private T GetUnit<T>(Side side) where T : BaseUnit
    {
        return (T)_units.Where(u => u.Side == side).First().UnitPrefab;
    }

    public void SelectUnit(BaseUnit unit)
    {
        CoinOneUnit.Selected = false;
        CoinTwoUnit.Selected = false;
        PlayerOneUnit.Selected = false;
        PlayerTwoUnit.Selected = false;
                
        SelectedUnit = unit;
        SelectedUnit.Selected = true;
    }

    public void DeselectedUnit()
    {
        CoinOneUnit.Selected = false;
        CoinTwoUnit.Selected = false;
        PlayerOneUnit.Selected = false;
        PlayerTwoUnit.Selected = false;
                
        SelectedUnit = null;
    }

    public void SelectPlayerOneUnit()
    {
        CoinOneUnit.Selected = false;
        CoinTwoUnit.Selected = false;
        PlayerOneUnit.Selected = true;
        PlayerTwoUnit.Selected = false;
        
        SelectedUnit = PlayerOneUnit;
    }

    public void SelectPlayerTwoUnit()
    {
        CoinOneUnit.Selected = false;
        CoinTwoUnit.Selected = false;
        PlayerOneUnit.Selected = false;
        PlayerTwoUnit.Selected = true;

        SelectedUnit = PlayerTwoUnit;
    }

    public bool UnitWouldOverlap(BaseUnit unit, int position)
    {
        int valueToCheck = unit.CalculateTilesValue(position);

        return 
            (unit != CoinOneUnit && (CoinOneUnit.GetTilesValue() & valueToCheck) != 0) ||
            (unit != CoinTwoUnit && (CoinTwoUnit.GetTilesValue() & valueToCheck) != 0) ||
            (unit != PlayerOneUnit && (PlayerOneUnit.GetTilesValue() & valueToCheck) != 0) ||
            (unit != PlayerTwoUnit && (PlayerTwoUnit.GetTilesValue() & valueToCheck) != 0);
    }

    public bool UnitWouldOverflow(BaseUnit unit, int position)
    {
        int valueToCheck = unit.GetAllowedSquares();

        return ((1 << position) & valueToCheck) == 0;
    }

    public GamePosition GetGamePosition(GameState state)
    {
        // Player two just finished playing, it has to be plugged in as p1
        if (state == GameState.PlayerOneMoveShape)
        {
            return new GamePosition(
                (CoinOneUnit.GetTilesValue() | CoinTwoUnit.GetTilesValue()),
                PlayerTwoUnit.GetTilesValue(),
                PlayerOneUnit.GetTilesValue()
            );
        }

        // Player one just finished playing, it has to be plugged in as p1
        if (state == GameState.PlayerTwoMoveShape)
        {
            return new GamePosition(
                (CoinOneUnit.GetTilesValue() | CoinTwoUnit.GetTilesValue()),
                PlayerOneUnit.GetTilesValue(),
                PlayerTwoUnit.GetTilesValue()
            );
        }

        throw new InvalidStateException();
    }

    public void MirrorSelectedUnit()
    {
        SelectedUnit?.ToggleMirror();
        if (Tutorial.Instance) Tutorial.Instance.NextStep();
    }

    public void RotateSelectedUnitRight()
    {
        SelectedUnit?.RotateRight();
        if (Tutorial.Instance) Tutorial.Instance.NextStep();
    }

    public void RotateSelectedUnitLeft()
    {
        SelectedUnit?.RotateLeft();
        if (Tutorial.Instance) Tutorial.Instance.NextStep();
    }
}

