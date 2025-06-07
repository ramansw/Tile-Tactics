using UnityEngine;
using System;
using System.Collections.Generic;


[CreateAssetMenu(menuName = "Grid/Obstacle Data")]



public class ObstacleData : ScriptableObject
{
    public bool[] obstacles = new bool[100]; // 10x10 = 100 tiles
}