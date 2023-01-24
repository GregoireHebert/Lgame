using System;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private BaseUnit _occupiedUnit;
    public int Value;
    public int Position;
    public TileClickedEvent MouseDown;

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
        MouseDown?.Invoke(this);
    }

    public BaseUnit getOccupiedUnit()
    {
        return _occupiedUnit;
    }

    public void SetUnit(BaseUnit unit)
    {
        if (unit.OccupiedTile != null) unit.OccupiedTile._occupiedUnit = null;
        unit.transform.position = new Vector3(transform.position.x, transform.position.y, (float)-0.1301);
        unit.SetTilesValue(Position);
        _occupiedUnit = unit;
        unit.OccupiedTile = this;
        SoundManager.Instance.PlaySound(_clip);
    }
}

[Serializable]
public class TileClickedEvent : UnityEvent<Tile> {}