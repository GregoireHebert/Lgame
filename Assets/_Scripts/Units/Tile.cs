using System;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private AudioClip _clip;
    [SerializeField] public BaseUnit OccupiedUnit;
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

    public void SetUnit(BaseUnit unit)
    {
        if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
        unit.transform.position = new Vector3(transform.position.x, transform.position.y, (float)-0.1301);
        unit.SetTilesValue(Position);
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
        SoundManager.Instance.PlaySound(_clip);
    }
}

[Serializable]
public class TileClickedEvent : UnityEvent<Tile> {}