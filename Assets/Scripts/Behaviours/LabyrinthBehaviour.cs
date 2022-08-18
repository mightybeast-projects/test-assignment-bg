using UnityEngine;

[RequireComponent(typeof(WallSpawner))]
[RequireComponent(typeof(TrajectorySpawner))]
[RequireComponent(typeof(DeathZonesSpawner))]
public class LabyrinthBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerBehaviour _playerBehaviour;

    private WallSpawner _wallSpawner;
    private TrajectorySpawner _trajectorySpawner;
    private DeathZonesSpawner _deathZonesSpawner;
    private Board _board;
    private AStar _algo;

    private void Awake()
    {
        _wallSpawner = GetComponent<WallSpawner>();
        _trajectorySpawner = GetComponent<TrajectorySpawner>();
        _deathZonesSpawner = GetComponent<DeathZonesSpawner>();
    }

    private void Start()
    {
        _board = new Board();
        _algo = new AStar();

        _wallSpawner.Initialize(_board);
        _wallSpawner.GenerateWalls();

        _algo.DoAStar(_board.grid[0, 0], _board.grid[9, 9]);

        if (ThereIsNoValidPath())
            Start();
        else
        {
            _deathZonesSpawner.Initialize(_board, _algo.path);
            _trajectorySpawner.Initialize(_algo.path);

            _trajectorySpawner.GenerateTrajectory();
            _deathZonesSpawner.GenerateDeathZones();

            _playerBehaviour.Initialize(_algo.path);
        }
    }

    private bool ThereIsNoValidPath()
    {
        return _algo.path.Count == 0;
    }
}