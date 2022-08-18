using System;
using System.Collections.Generic;
using System.Numerics;

public class Board
{
    public Tile[,] grid => _grid;
    
    private Tile[,] _grid;
    private Tile _currentTile;

    public Board()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        _grid = new Tile[10, 10];

        for (int i = 0; i < _grid.GetLength(0); i++)
            for (int j = 0; j < _grid.GetLength(1); j++)
                GenerateNewTile(i, j);

        for (int i = 0; i < _grid.GetLength(0); i++)
            for (int j = 0; j < _grid.GetLength(1); j++)
                InitializeNeighboursForTileWithIndexes(i, j);
    }

    private void GenerateNewTile(int i, int j)
    {
        Tile newTile = new Tile(new Vector2(i, j));
        _grid[i, j] = newTile;
    }

    private void InitializeNeighboursForTileWithIndexes(int i, int j)
    {
        _currentTile = _grid[i, j];

        AddAdjacentTileToList(_currentTile.neighbors, i, j + 1);
        AddAdjacentTileToList(_currentTile.neighbors, i + 1, j);
        AddAdjacentTileToList(_currentTile.neighbors, i, j - 1);
        AddAdjacentTileToList(_currentTile.neighbors, i - 1, j);
    }

    private void AddAdjacentTileToList(List<Tile> list, int tileX, int tileY)
    {
        try { list.Add(_grid[tileX, tileY]); }
        catch (IndexOutOfRangeException) { }
    }
}