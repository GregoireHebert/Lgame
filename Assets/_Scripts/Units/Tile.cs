using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private BaseUnit _occupiedUnit;
    public int Value;
    public int Position;
#nullable enable
    private Tutorial? _tutorial;
#nullable disable    

    void Start()
    {
        _tutorial = Tutorial.Instance ?? null;
    }

    void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    void OnMouseDown()
    {
        _tryMoveCoin();
        _tryMoveShape();
    }

    private void _tryMoveCoin()
    {
        // turn to move a coin
        if (GameState.PlayerOneMoveCoin != GameManager.Instance.State && GameState.PlayerTwoMoveCoin != GameManager.Instance.State)
        {
            return;
        }

        // clicked on a tile occupied by a coin, select it.
        if (_occupiedUnit != null && _occupiedUnit.Side == Side.Neutral)
        {
            UnitManager.Instance.SetSelectedUnit((BaseNeutral)_occupiedUnit);

            if (null != _tutorial) {
                _tutorial.NextStep();
            }
            
            return;
        }

        if (
            _occupiedUnit == null &&
            UnitManager.Instance.SelectedUnit != null &&
            false == UnitManager.Instance.UnitWouldOverlap(UnitManager.Instance.SelectedUnit, Position)
        )
        {
            SetUnit(UnitManager.Instance.SelectedUnit);
            SoundManager.Instance.PlaySound(_clip);
            UnitManager.Instance.SetSelectedUnit(null);

            // move to next game state
            if (GameManager.Instance.State == GameState.PlayerOneMoveCoin)
            {
                GameManager.Instance.ChangeState(GameState.PlayerTwoMoveShape);
            }
            else
            {
                GameManager.Instance.ChangeState(GameState.PlayerOneMoveShape);
            }

            if (null != _tutorial) {
                _tutorial.NextStep();
            }

            return;
        }
    }

    private void _tryMoveShape()
    {
        // turn to move a shape
        UnityEngine.Debug.Log(GameManager.Instance.State);
        UnityEngine.Debug.Log(GameManager.Instance.State);
        if (GameState.PlayerOneMoveShape != GameManager.Instance.State && GameState.PlayerTwoMoveShape != GameManager.Instance.State)
        {
            return;
        }

        if (
            // clicked on an empty cell, or if clicked on selected unit tile allow it if the shape changed its position
            (_occupiedUnit == null || (_occupiedUnit == UnitManager.Instance.SelectedUnit && UnitManager.Instance.SelectedUnit.GetTilesValue() != UnitManager.Instance.SelectedUnit.CalculateTilesValue(Position))) &&
            // then check if the selected position and tile is compatible
            false == UnitManager.Instance.UnitWouldOverflow(UnitManager.Instance.SelectedUnit, Position) &&
            false == UnitManager.Instance.UnitWouldOverlap(UnitManager.Instance.SelectedUnit, Position)
        )
        {
            SetUnit(UnitManager.Instance.SelectedUnit);
            SoundManager.Instance.PlaySound(_clip);
            UnitManager.Instance.SetSelectedUnit(null);

            // move to next game state
            if (GameManager.Instance.State == GameState.PlayerOneMoveShape)
            {
                GameManager.Instance.ChangeState(GameState.PlayerOneMoveCoin);
            }
            else
            {
                GameManager.Instance.ChangeState(GameState.PlayerTwoMoveCoin);
            }

            if (null != _tutorial) {
                _tutorial.NextStep();
            }

            return;
        }
    }

    public void SetUnit(BaseUnit unit)
    {
        if (unit.OccupiedTile != null) unit.OccupiedTile._occupiedUnit = null;
        unit.transform.position = new Vector3(transform.position.x, transform.position.y, (float)-0.1301);
        unit.SetTilesValue(Position);
        _occupiedUnit = unit;
        unit.OccupiedTile = this;
    }
}
