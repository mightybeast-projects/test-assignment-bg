using System.Collections.Generic;
using UnityEngine;

public class TrajectorySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _trajectoryPrefab;
    [SerializeField] private Transform _trajectoryTransform;

    private List<IPathfindingNode> _path;

    public void Initialize(List<IPathfindingNode> path)
    {
        _path = path;
    }

    public void GenerateTrajectory()
    {
        foreach (IPathfindingNode node in _path)
        {
            GameObject tr = Instantiate(_trajectoryPrefab, _trajectoryTransform);
            Vector3 pos = new Vector3(node.position.Y, 0, node.position.X);
            tr.transform.localPosition = pos;
        }
    }
}