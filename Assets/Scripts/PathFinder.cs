using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public Grid gridManager; 
    public ObstacleManager obstacleManager; 
    public Transform enemyTransform; 

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
                break; // reached player
            }

            foreach (Tile neighbor in gridManager.GetNeighbours(current))
            {
                if (visited.Contains(neighbor))
                    continue;

                // skip obstacle
                if (obstacleManager.IsObstacle(neighbor.gridX, neighbor.gridY))
                    continue;

                // if enemy is occuping the tile 
                if (enemyTransform != null)
                {
                    Vector3 enemyPos = enemyTransform.position;

                    Vector3 tilePos = neighbor.transform.position;

                    enemyPos.y = tilePos.y;

                    // Skip

                    if (Vector3.Distance(tilePos, enemyPos) < 0.5f)
                        continue;
                }

                visited.Add(neighbor);


                cameFrom[neighbor] = current;
                queue.Enqueue(neighbor);
            }
        }

        // backtrack path
        Tile tile = endTile;
        while (tile != startTile)
        {
            if (!cameFrom.ContainsKey(tile))
            
                return new List<Vector3>(); 

            path.Add(tile.transform.position);
            tile = cameFrom[tile];
        }

        path.Reverse(); 
        return path;
    }
}
