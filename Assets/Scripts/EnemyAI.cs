using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IAI
{
    public Pathfinder pathfinder;
    public Transform player;
    public float moveSpeed = 2f;
    public float distanceThreshold = 0.1f;

    bool isMoving = false;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>(); // grab anim from self lol
    }

    public void TakeTurn()
    {
        if (isMoving)
        {
            return; // already walkin so chill
        }

        if (pathfinder == null)
        {
            Debug.Log("uhh no pathfinder here");
            return;
        }

        if (player == null)
        {
            Debug.Log("where did the player go lol");
            return;
        }

        Vector3 enemyPos = transform.position;
        Vector3 playerPos = player.position;

        List<Vector3> path = pathfinder.FindPath(enemyPos, playerPos);

        if (path == null || path.Count < 2)
        {
            return; // nowhere to go
        }

        Vector3 targetPos = path[path.Count - 2]; // go near player
        targetPos.y = transform.position.y;

        float dist = Vector3.Distance(transform.position, targetPos);
        if (dist < distanceThreshold)
        {
            return;
        }

        StartCoroutine(MoveToTile(targetPos));
    }

    IEnumerator MoveToTile(Vector3 targetPos)
    {
        isMoving = true;

        if (anim != null)
        {
            anim.SetBool("isRunning", true); // start run
        }

        Vector3 start = transform.position;

        Vector3 faceDir = targetPos - transform.position;
        if (faceDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(faceDir); // look that way
        }

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(start, targetPos, t);
            yield return null;
        }

        transform.position = targetPos;

        // turn off run anim
        if (anim != null)
        {
            anim.SetBool("isRunning", false);
        }

        // always face player even after walking
        if (player != null)
        {
            Vector3 lookAtPlayer = player.position - transform.position;
            lookAtPlayer.y = 0f; // keep it flat
            if (lookAtPlayer != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(lookAtPlayer);
            }
        }

        isMoving = false;
    }
}
