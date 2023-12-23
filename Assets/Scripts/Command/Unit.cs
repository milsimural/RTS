using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : SelectableObject
{
    public NavMeshAgent Agent;

    public override void WhenClickOnGround(Vector2 point) // Приказ идти в точку клика
    {
        base.WhenClickOnGround(point);

        Agent.SetDestination(new Vector3(point.x, point.y, transform.position.z));

        Debug.Log(Agent.path.corners.Length);
        for (int i = 0; i < Agent.path.corners.Length; i++)
        {
            Debug.Log(Agent.path.corners[i]);
        }
    }

}
