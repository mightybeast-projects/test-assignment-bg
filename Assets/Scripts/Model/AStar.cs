using System;
using System.Collections.Generic;

public class AStar
{
    public List<IPathfindingNode> path => _path;

    private List<IPathfindingNode> _openSet;
    private List<IPathfindingNode> _closedSet;
    private List<IPathfindingNode> _path;
    private List<IPathfindingNode> _nodesToReset;
    private IPathfindingNode _goal;
    private IPathfindingNode _currentNode;
    private IPathfindingNode _currentNodeNeighbour;
    
    private float _tempG;
    private bool _newPathDiscovered;
    
    public void DoAStar(IPathfindingNode start, IPathfindingNode goal)
    {
        ResetFields();

        _goal = goal;

        _openSet.Add(start);

        StartAlgorithm();

        ResetNodes();
    }

    private void StartAlgorithm()
    {
        while (OpenSetHasTiles())
        {
            GetNearestToTheGoalNode();

            if (_currentNode == _goal)
            {
                AddNodesToPath();
                break;
            }

            _openSet.Remove(_currentNode);
            _closedSet.Add(_currentNode);

            CheckCurrentNodeNeighbors();
        }
    }

    private void CheckCurrentNodeNeighbors()
    {
        foreach (IPathfindingNode neighbour in _currentNode.GetPathfindingNeighbours())
        {
            _currentNodeNeighbour = neighbour;
            AddNodeToResetList(_currentNodeNeighbour);

            if (!_closedSet.Contains(_currentNodeNeighbour))
                ProcessCurrentNodeNeighbour();
        }
    }

    private void ProcessCurrentNodeNeighbour()
    {
        _tempG = _currentNode.g + 1;
        _newPathDiscovered = false;

        CheckCurrentNodeNeighborG();

        if (_newPathDiscovered)
            CalculateCurrentNodeNeighbourF();
    }

    private void CheckCurrentNodeNeighborG()
    {
        if (_openSet.Contains(_currentNodeNeighbour))
        {
            if (_tempG < _currentNodeNeighbour.g)
                AssignNewCurrentNodeNeighborGAndPath();
        }
        else
        {
            AssignNewCurrentNodeNeighborGAndPath();
            _openSet.Add(_currentNodeNeighbour);
        }
    }

    private void AssignNewCurrentNodeNeighborGAndPath()
    {
        _currentNodeNeighbour.g = _tempG;
        _newPathDiscovered = true;
    }

    private void CalculateCurrentNodeNeighbourF()
    {
        _currentNodeNeighbour.h = CalculateHeuristic(_currentNodeNeighbour, _goal);
        _currentNodeNeighbour.f = _currentNodeNeighbour.g + _currentNodeNeighbour.h;
        _currentNodeNeighbour.parent = _currentNode;
    }

    private void AddNodesToPath()
    {
        IPathfindingNode endNode = _currentNode;
        _path.Add(endNode);
        while (endNode.parent != null)
        {
            _path.Add(endNode.parent);
            endNode = endNode.parent;
        }
        _path.Reverse();
    }

    private void GetNearestToTheGoalNode()
    {
        int lowestIndex = 0;
        for (int i = 0; i < _openSet.Count; i++)
        {
            IPathfindingNode tile = _openSet[i];

            if (tile.f < _openSet[lowestIndex].f)
                lowestIndex = i;
        }
        _currentNode = _openSet[lowestIndex];

        AddNodeToResetList(_currentNode);
    }

    private void AddNodeToResetList(IPathfindingNode node)
    {
        if (!_nodesToReset.Contains(node))
            _nodesToReset.Add(node);
    }

    private bool OpenSetHasTiles()
    {
        return _openSet.Count > 0;
    }

    private void ResetFields()
    {
        _openSet = new List<IPathfindingNode>();
        _closedSet = new List<IPathfindingNode>();
        _path = new List<IPathfindingNode>();
        _nodesToReset = new List<IPathfindingNode>();
    }

    private void ResetNodes()
    {
        foreach (IPathfindingNode node in _nodesToReset)
        {
            node.f = 0;
            node.g = 0;
            node.h = 0;
            node.parent = null;
        }
    }

    private float CalculateHeuristic(IPathfindingNode neighbor, IPathfindingNode goal)
    {
        return Dist((int) neighbor.position.Y, (int) neighbor.position.Y, 
                    (int) goal.position.X, (int) goal.position.Y);
    }

    private float Dist(int aX, int aY, int bX, int bY) {
        return (float) Math.Sqrt(Math.Pow(bX - aX, 2) + Math.Pow(bY - aY, 2));
    }
}