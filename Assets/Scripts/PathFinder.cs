using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public Grid gridManager;
    public ObstacleManager obstacleManager;

    public List<Vector3> FindPath(Vector3 startPos, Vector3 endPos)
    {
        List<Vector3> path = new List<Vector3>();

        Tile startTile = gridManager.GetTileFromWorldPosition(startPos);
        Tile endTile = gridManager.GetTileFromWorldPosition(endPos);

        if (startTile == null || endTile == null)
            return path;

        Queue<Tile> queue = new Queue<Tile>();
        Dictionary<Tile, Tile> cameFrom = new Dictionary<Tile, Tile>();
        HashSet<Tile> visited = new HashSet<Tile>();

        queue.Enqueue(startTile);
        visited.Add(startTile);

        while (queue.Count > 0)
        {
            Tile current = queue.Dequeue();

            if (current == endTile)
            {
                break;
            }

            foreach (Tile neighbor in gridManager.GetNeighbours(current))
            {
                if (visited.Contains(neighbor) || obstacleManager.IsObstacle(neighbor.gridX, neighbor.gridY))

                    continue;

                visited.Add(neighbor);
                cameFrom[neighbor] = current;
                queue.Enqueue(neighbor);
            }
        }

        // Trace back path
        Tile tile = endTile;
        while (tile != startTile)
        {
            if (!cameFrom.ContainsKey(tile))
            {
                return new List<Vector3>(); // No path
            }

            path.Add(tile.transform.position);
            tile = cameFrom[tile];
        }

        path.Reverse();
        return path;
    }
}
