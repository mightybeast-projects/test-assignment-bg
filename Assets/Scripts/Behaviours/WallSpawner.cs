using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [SerializeField] [Range(0, 50)] private int _wallCount;
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private Transform _wallsTransform;

    private Board _board;
    private int _wallX, _wallY;

    public void Initialize(Board board)
    {
        _board = board;
    }

    public void GenerateWalls()
    {
        foreach (Transform child in _wallsTransform)
            Destroy(child.gameObject);

        for (int i = 0; i < _wallCount; i++)
        {
            ChooseRandomPosition();

            if (!_board.grid[_wallX, _wallY].isEmpty)
            {
                i--;
                continue;
            }
            
            _board.grid[_wallX, _wallY].isEmpty = false;
            GameObject wallGO = Instantiate(_wallPrefab, _wallsTransform);
            wallGO.transform.localPosition = new Vector3(_wallY, 0, _wallX);
        }
    }

    private void ChooseRandomPosition()
    {
        _wallX = Random.Range(0, _board.grid.GetLength(0));
        _wallY = Random.Range(0, _board.grid.GetLength(1));
        if ((_wallX == 0 && _wallY == 0) || (_wallX == 9 && _wallY == 9))
            ChooseRandomPosition();
    }
}