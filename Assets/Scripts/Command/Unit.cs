using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : SelectableObject
{
    public NavMeshAgent Agent;

    public float UnitBaseSpeed;
    public float UnitRotationSpeed;

    public override void WhenClickOnGround(Vector2 point) // Приказ идти в точку клика
    {
        base.WhenClickOnGround(point);

        Agent.SetDestination(new Vector3(point.x, point.y, transform.position.z));

    }



}
