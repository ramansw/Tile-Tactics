using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IAI
{
    public Pathfinder pathfinder; 
    public Transform player; 
    
    public float moveSpeed = 2f; 

    public bool isMoving = false; 

    private Animator anim; 
    private float yPos; 

    void Start()
    {
        anim = GetComponent<Animator>(); 
        yPos = transform.position.y; 
    }

    public void TakeTurn()
    {
        // if enemy is Already moving, no need to move again
        if (isMoving || pathfinder == null || player == null)
              return;
      
      
        Vector3 myPos = new Vector3(Mathf.Round(transform.position.x), 0, Mathf.Round(transform.position.z));

        Vector3 playerPos = new Vector3(Mathf.Round(player.position.x), 0, Mathf.Round(player.position.z));

        // if enemy is too close, no move

        if (Vector3.Distance(myPos, playerPos) <= 1.1f)
            return;

   
             List<Vector3> path = pathfinder.FindPath(myPos, playerPos);

     
        if (path == null || path.Count < 2)
                   return;

      
        StartCoroutine(MoveAlongPath(path));
    }



    IEnumerator MoveAlongPath(List<Vector3> path)
    {
        isMoving = true;

        // anim check
        if (anim != null)
            anim.SetBool("isRunning", true);

        // move to each tile one by one
        for (int i = 0; i < path.Count - 1; i++)
        {
            Vector3 point = path[i];


            Vector3 target = new Vector3(point.x, yPos, point.z); // freex y
            Vector3 start = transform.position;



            Vector3 dir = target - transform.position;

            // face the next tile
            if (dir != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(dir);

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * moveSpeed;



                transform.position = Vector3.Lerp(start, target, t); //leop to  move smoothly
                yield return null;
            }

            transform.position = target; 
        }

        
        if (anim != null)
            anim.SetBool("isRunning", false);

        // face the player after reaching
        if (player != null)
        {
            Vector3 lookAt = player.position - transform.position;
            lookAt.y = 0f;
            if (lookAt != Vector3.zero)

            
                transform.rotation = Quaternion.LookRotation(lookAt);
        }

        isMoving = false;
    }
}
