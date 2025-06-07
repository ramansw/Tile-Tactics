using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TileSelector : MonoBehaviour
{
    public Camera mainCamera;
    public TextMeshProUGUI tileInfo;

    // Start is called before the first frame update
    void Start()
    {
        


        
    }

    // Update is called once per frame
    void Update()
    {




        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit)){




            Tile tile = hit.collider.GetComponent<Tile>();
            if(tile != null){
                tileInfo.text = $"Tile: ({tile.gridX}, {tile.gridY})";

            }


        }
        else{
            tileInfo.text = "No Tile";
        }
        
    }
}
