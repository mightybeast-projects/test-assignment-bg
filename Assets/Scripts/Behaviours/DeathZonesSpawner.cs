using System.Collections.Generic;
using UnityEngine;

public class DeathZonesSpawner : MonoBehaviour
{
    [SerializeField] [Range(0, 20)] private int _deathZonesCount;
    [SerializeField] private GameObject _deathZonePrefab;
    [SerializeField] private Transform _deathZonesTransform;

    private Board _board;
    private List<IPathfindingNode> _path;
    private IPathfindingNode _randomPathNode;
    private int _nodeX => (int) _randomPathNode.position.Y;
    private int _nodeY => (int) _randomPathNode.position.X;

    public void Initialize(Board board, List<IPathfindingNode> path)
    {
        _board = board;
        _path = path;
    }

    public void GenerateDeathZones()
    {
        for (int i = 0; i < _deathZonesCount; i++)
        {
            _randomPathNode = _path[Random.Range(1, _path.Count - 1)];
            
            if (!_board.grid[_nodeX, _nodeY].isEmpty)
            {
                i--;
                continue;
            }
            
            _board.grid[_nodeX, _nodeY].isEmpty = false;
            Vector3 nodePosition = new Vector3(_nodeX, 0, _nodeY);
            GameObject _deathZone = Instantiate(_deathZonePrefab, _deathZonesTransform);
            _deathZone.transform.localPosition = nodePosition;
        }
    }
}