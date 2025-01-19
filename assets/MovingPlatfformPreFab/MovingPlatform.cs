using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform platform;
    public Transform startPoint;
    public Transform endPoint;

    public float speed;
    private float distance;
    private int direction = 1;
    void Update()
    {
        Vector2 target = CurrentMovementTarget();

        platform.position = Vector2.MoveTowards(platform.position, target, speed * Time.deltaTime);

        distance = (target - (Vector2)platform.position).magnitude;

        if (distance <= 0.1f)
        {
            direction *= -1;
        }
    }

    private Vector2 CurrentMovementTarget()
    {
        if (direction == 1)
        {
            // move to startPoint
            return startPoint.position;
        }
        else
        {
            return endPoint.position;
        }
    }

    private void OnDrawGizmos()
    {
        if (platform!=null && startPoint!=null && endPoint!=null)
        {
            Gizmos.DrawLine(platform.transform.position, startPoint.position);
            Gizmos.DrawLine(platform.transform.position, endPoint.position);
        }
    }

}