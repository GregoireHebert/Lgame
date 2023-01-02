using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private AudioClip clip;

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
        if(GameState.PlayerOneMoveCoin != GameManager.Instance.State && GameState.PlayerTwoMoveCoin != GameManager.Instance.State) {
            return;
        }

        // clicked on a tile occupied by a coin, select it.
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
            SoundManager.Instance.playSound(clip);
            UnitManager.Instance.SetSelectedUnit(null);
            
            // move to next game state
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
        UnityEngine.Debug.Log(GameManager.Instance.State);
        UnityEngine.Debug.Log(GameManager.Instance.State);
        if(GameState.PlayerOneMoveShape != GameManager.Instance.State && GameState.PlayerTwoMoveShape != GameManager.Instance.State) {
            return;
        }

        if (
            // clicked on an empty cell, or if clicked on selected unit tile allow it if the shape changed its position
            (OccupiedUnit == null || (OccupiedUnit == UnitManager.Instance.SelectedUnit && UnitManager.Instance.SelectedUnit.getTilesValue() != UnitManager.Instance.SelectedUnit.calculateTilesValue(position))) && 
            // then check if the selected position and tile is compatible
            false == UnitManager.Instance.unitWouldOverflow(UnitManager.Instance.SelectedUnit, position) &&
            false == UnitManager.Instance.unitWouldOverlap(UnitManager.Instance.SelectedUnit, position)
        ) {
            SetUnit(UnitManager.Instance.SelectedUnit);
            SoundManager.Instance.playSound(clip);
            UnitManager.Instance.SetSelectedUnit(null);
            
            // move to next game state
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
