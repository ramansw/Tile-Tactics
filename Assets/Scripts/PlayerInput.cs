using UnityEngine;
using System;
using System.Collections.Generic;


public class PlayerInput : MonoBehaviour
{
    public Camera cam;
    public Pathfinder pathfinder;
    public Player player;




    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !player.isMoving)
        {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Tile tile = hit.collider.GetComponent<Tile>();
                if (tile != null)


                {
                    List<Vector3> path = pathfinder.FindPath(player.transform.position, tile.transform.position);
                    if (path.Count > 0)


                    {
                        player.MoveAlongPath(path);
                    }

                    
                }
            }


        }
    }
}
