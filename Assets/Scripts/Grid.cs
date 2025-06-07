using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject tilePrefab;
    public int width = 10;
    public int height = 10;
    float spacing = 0.1f;

    public ObstacleManager obstacleData; // <-- Add this

    void Start()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(x * (1 + spacing), 0, y * (1 + spacing));
                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity);
                tile.name = $"Tile_{x},{y}";
                Tile tileScript = tile.GetComponent<Tile>();
                tileScript.gridX = x;
                tileScript.gridY = y;
            }
        }
    }

    public Tile GetTileFromWorldPosition(Vector3 worldPos)
    {
        int x = Mathf.RoundToInt(worldPos.x / (1.1f));
        int y = Mathf.RoundToInt(worldPos.z / (1.1f));
        string tileName = $"Tile_{x},{y}";
        GameObject tileObj = GameObject.Find(tileName);
        if (tileObj != null)
        {
            return tileObj.GetComponent<Tile>();
        }
        return null;
    }

    public List<Tile> GetNeighbours(Tile tile)
    {
        List<Tile> neighbours = new List<Tile>();

        int[,] directions = new int[,]
        {
            { 0, 1 },
            { 0, -1 },
            { -1, 0 },
            { 1, 0 }
        };

        for (int i = 0; i < 4; i++)
        {
            int checkX = tile.gridX + directions[i, 0];
            int checkY = tile.gridY + directions[i, 1];

            string tileName = $"Tile_{checkX},{checkY}";
            GameObject neighborObj = GameObject.Find(tileName);
            if (neighborObj != null)
            {
                neighbours.Add(neighborObj.GetComponent<Tile>());
            }
        }

        return neighbours;
    }

    public bool IsObstacle(int x, int y)
    {
        if (obstacleData == null)
        {
            Debug.LogWarning("ObstacleManager reference is missing!");
            return false;
        }

        return obstacleData.IsObstacle(x,y);
    }

    void Update() { }
}
