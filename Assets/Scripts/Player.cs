using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 2f;

    public bool isMoving = false;


    private float yPos;

    public EnemyAI enemyAI;

    private Animator anim;

    void Start()
    {
        yPos = transform.position.y;
        anim = GetComponent<Animator>();
    }

    public void MoveAlongPath(List<Vector3> path)
    {
        if (isMoving || path == null || path.Count == 0)
            return;

        StartCoroutine(MoveOnPath(path));


    }

    IEnumerator MoveOnPath(List<Vector3> path)
    {
        //animation check
        isMoving = true;
        if (anim != null)
        
            anim.SetBool("isRunning", true);

        foreach (Vector3 point in path)
        {
            Vector3 targetPos = new Vector3(point.x, yPos, point.z);
            Vector3 start = transform.position;




            // Rotate once before moving so it can face towards it is moving
            Vector3 dir = targetPos - transform.position;
            if (dir != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(dir);
            }

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * moveSpeed;
                transform.position = Vector3.Lerp(start, targetPos, t);
                yield return null;
            }

            transform.position = targetPos;
            yield return null;
        }




        isMoving = false;

        if (anim != null)
        
            anim.SetBool("isRunning", false);

        if (enemyAI != null)
            enemyAI.TakeTurn();
    }
}