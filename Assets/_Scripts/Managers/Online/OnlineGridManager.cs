using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OnlineGridManager : MonoBehaviour
{
    [SerializeField] private int _width = 4, _height = 4;
    [SerializeField] private Tile _lightTilePrefab;
    [SerializeField] private Tile _darkTilePrefab;
    [SerializeField] private Transform _cam;
    [SerializeField] private OnlineGameManager _gameManager;

    private Dictionary<int, Tile> _tiles;

    public void GenerateGrid()
    {
        int square = 0;
        _tiles = new Dictionary<int, Tile>();

        for (int y = _height; y > 0; y--)
        {
            for (int x = 0; x < _width; x++)
            {
                var isOffset = (x + y) % 2 == 1;
                var prefab = isOffset == true ? _lightTilePrefab : _darkTilePrefab;

                var spawnedTile = Instantiate(prefab, new UnityEngine.Vector3(x, y), UnityEngine.Quaternion.identity);
                spawnedTile.name = $"Tile {1 << square}";
                spawnedTile.Value = 1 << square;
                spawnedTile.Position = square;

                _tiles[square] = spawnedTile;
                
                square++;
            }
        }

        _cam.transform.position = new UnityEngine.Vector3((float)_width / 2 - 0.5f, (float)_width / 2 + 0.5f, -10);
    }

    public Dictionary<int, Tile> GetTiles()
    {
        return _tiles;
    }

    public Tile GetTileAtPosition(int position)
    {
        if (_tiles.TryGetValue(position, out var tile))
        {
            return tile;
        }

        return null;
    }

    public Tile GetCoinOneSpawnTile()
    {
        return _tiles.Where(t => t.Value.Value == 1 << 0).First().Value;
    }

    public Tile GetCoinTwoSpawnTile()
    {
        return _tiles.Where(t => t.Value.Value == 1 << 15).First().Value;
    }

    public Tile GetPlayerOneSpawnTile()
    {
        return _tiles.Where(t => t.Value.Value == 1 << 2).First().Value;
    }

    public Tile GetPlayerTwoSpawnTile()
    {
        return _tiles.Where(t => t.Value.Value == 1 << 13).First().Value;
    }
}
