using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public ObstacleData obstacleData;
    public GameObject obstaclePrefab;

    public int width = 10;
    public int height = 10;
    
    void Start()
{
    float tileSize = 1f;
    float spacing = 0.1f;

    for (int x = 0; x < width; x++)
    {
        for (int y = 0; y < height; y++)
        {
            int index = y * width + x;

            if (obstacleData.obstacles[index])
            {
                Vector3 position = new Vector3(x * (tileSize + spacing), 0.9f, y * (tileSize + spacing));
                Instantiate(obstaclePrefab, position, Quaternion.identity);
            }
        }
    }
}
public bool IsObstacle(int x, int y){

    if (x < 0 || x >= width || y >= height)
    return false;

    int index = y * width + x;
    return obstacleData.obstacles[index];
}

 
    
}
