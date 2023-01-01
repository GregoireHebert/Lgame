using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    public BaseUnit OccupiedUnit;
    public int value;
    public int position;

    void OnMouseEnter() {
        _highlight.SetActive(true);
    }

    void OnMouseExit() {
        _highlight.SetActive(false);
    }

     void OnMouseDown() {
        tryMoveCoin();
        tryMoveShape();
    }

    private void tryMoveCoin() {
        // turn to move a coin
        if(
            GameManager.Instance.State != GameState.PlayerOneMoveCoin &&
            GameManager.Instance.State != GameState.PlayerTwoMoveCoin
        ) {
            return;
        }

        // tile is occupied by a coin, select it.
        if (OccupiedUnit != null && OccupiedUnit.Side == Side.Neutral) {
            UnitManager.Instance.SetSelectedUnit((BaseNeutral)OccupiedUnit);
            return;
        } 

        if (
            OccupiedUnit == null && 
            UnitManager.Instance.SelectedUnit != null && 
            false == UnitManager.Instance.unitWouldOverlap(UnitManager.Instance.SelectedUnit, position)
        ) {
            SetUnit(UnitManager.Instance.SelectedUnit);
            UnitManager.Instance.SetSelectedUnit(null);
            
            if (GameManager.Instance.State == GameState.PlayerOneMoveCoin) {
                GameManager.Instance.ChangeState(GameState.PlayerTwoMoveShape);
            } else {
                GameManager.Instance.ChangeState(GameState.PlayerOneMoveShape);
            }

            return;
        }
    }

    private void tryMoveShape() {
        // turn to move a shape
        if(
            GameManager.Instance.State != GameState.PlayerOneMoveShape &&
            GameManager.Instance.State != GameState.PlayerTwoMoveShape
        ) {
            return;
        }

        if (
            (OccupiedUnit == null || (OccupiedUnit == UnitManager.Instance.SelectedUnit && UnitManager.Instance.SelectedUnit.getTilesValue() != UnitManager.Instance.SelectedUnit.calculateTilesValue(position))) && 
            false == UnitManager.Instance.unitWouldOverflow(UnitManager.Instance.SelectedUnit, position) &&
            false == UnitManager.Instance.unitWouldOverlap(UnitManager.Instance.SelectedUnit, position)
        ) {
            SetUnit(UnitManager.Instance.SelectedUnit);
            UnitManager.Instance.SetSelectedUnit(null);
            
            if (GameManager.Instance.State == GameState.PlayerOneMoveShape) {
                GameManager.Instance.ChangeState(GameState.PlayerOneMoveCoin);
            } else {
                GameManager.Instance.ChangeState(GameState.PlayerTwoMoveCoin);
            }

            return;
        }
    }

    public void SetUnit(BaseUnit unit) {
        if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;        
        unit.transform.position = new Vector3(transform.position.x, transform.position.y, (float)-0.1301);
        unit.setTilesValue(position);
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }
}