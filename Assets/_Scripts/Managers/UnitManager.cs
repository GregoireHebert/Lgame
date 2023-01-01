using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    public List<ScriptableUnit> _units;

    public BaseUnit SelectedUnit;

    public BasePlayerOne PlayerOneUnit;
    public BasePlayerTwo PlayerTwoUnit;

    public BaseNeutral CoinOneUnit;
    public BaseNeutral CoinTwoUnit;

    void Awake() {
        Instance = this;

        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

    public void SpawnUnits() {
        SpawnCoins();
        SpawnPlayers();
        GameManager.Instance.ChangeState(GameState.PlayerOneMoveShape);
    }

    private void SpawnCoins() {
        var coinPrefab = GetUnit<BaseNeutral>(Side.Neutral);
        
        var spawnedCoinOne = Instantiate(coinPrefab);
        var spawnedCoinOneTile = GridManager.Instance.GetCoinOneSpawnTile();
        spawnedCoinOneTile.SetUnit(spawnedCoinOne);
        CoinOneUnit = spawnedCoinOne;
        
        var spawnedCoinTwo = Instantiate(coinPrefab);
        var spawnedCoinTwoTile = GridManager.Instance.GetCoinTwoSpawnTile();
        spawnedCoinTwoTile.SetUnit(spawnedCoinTwo);
        CoinTwoUnit = spawnedCoinTwo;
    }

    private void SpawnPlayers() {
        var playerOnePrefab = GetUnit<BasePlayerOne>(Side.PlayerOne);
        var playerTwoPrefab = GetUnit<BasePlayerTwo>(Side.PlayerTwo);
        
        var spawnedPlayerOneUnit = Instantiate(playerOnePrefab);
        var spawnedPlayerOneTile = GridManager.Instance.GetPlayerOneSpawnTile();
        spawnedPlayerOneTile.SetUnit(spawnedPlayerOneUnit);
        PlayerOneUnit = spawnedPlayerOneUnit;
        
        var spawnedPlayerTwoUnit = Instantiate(playerTwoPrefab);
        var spawnedPlayerTwoTile = GridManager.Instance.GetPlayerTwoSpawnTile();
        spawnedPlayerTwoTile.SetUnit(spawnedPlayerTwoUnit);
        PlayerTwoUnit = spawnedPlayerTwoUnit;
    }

    private T GetUnit<T>(Side side) where T : BaseUnit {
        return (T)_units.Where(u => u.Side == side).First().UnitPrefab;
    }

    public void SetSelectedUnit(BaseUnit unit) {
        SelectedUnit = unit;
    }

    public void SelectPlayerOneUnit() {
        SelectedUnit = PlayerOneUnit;
    }

    public void SelectPlayerTwoUnit() {
        SelectedUnit = PlayerTwoUnit;
    }

    public bool unitWouldOverlap(BaseUnit unit, int position) {
        int valueToCheck = unit.calculateTilesValue(position);
        
        if (unit != CoinOneUnit && (CoinOneUnit.getTilesValue() & valueToCheck) != 0) {
            UnityEngine.Debug.Log("overlap coin one");
            return true;
        }

        if (unit != CoinTwoUnit && (CoinTwoUnit.getTilesValue() & valueToCheck) != 0) {
            UnityEngine.Debug.Log("overlap coin two");
            return true;
        }

        if (unit != PlayerOneUnit && (PlayerOneUnit.getTilesValue() & valueToCheck) != 0) {
            UnityEngine.Debug.Log("overlap coin shape player one");
            return true;
        }

        if (unit != PlayerTwoUnit && (PlayerTwoUnit.getTilesValue() & valueToCheck) != 0) {
            UnityEngine.Debug.Log("overlap coin shape player two");
            return true;
        }

        return false;
    }

    public bool unitWouldOverflow(BaseUnit unit, int position) {
        int valueToCheck = unit.getAllowedSquares();
        
        return ((1 << position) & valueToCheck) == 0;
    }
    
    public GamePosition getGamePosition() {
        // Player two just finished playing, it has to be plugged in as p1
        if (GameManager.Instance.State == GameState.PlayerOneMoveShape) {
            return new GamePosition(
                (CoinOneUnit.getTilesValue() | CoinTwoUnit.getTilesValue()),
                PlayerTwoUnit.getTilesValue(),
                PlayerOneUnit.getTilesValue()
            );
        }

        // Player one just finished playing, it has to be plugged in as p1
        if (GameManager.Instance.State == GameState.PlayerTwoMoveShape) {
            return new GamePosition(
                (CoinOneUnit.getTilesValue() | CoinTwoUnit.getTilesValue()),
                PlayerOneUnit.getTilesValue(),
                PlayerTwoUnit.getTilesValue()
            );
        }

        throw new InvalidStateException();
    }
}

