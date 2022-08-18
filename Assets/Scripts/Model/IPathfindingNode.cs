using System.Collections.Generic;
using System.Numerics;

public interface IPathfindingNode
{
    Vector2 position { get; }
    float f { get; set; }
    float g { get; set; }
    float h { get; set; }
    IPathfindingNode parent { get; set; }
    List<IPathfindingNode> GetPathfindingNeighbours();
}