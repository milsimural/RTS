using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class UserUnit : Unit
{
    private Vector2 Target;

    private void Update()
    {
        if (Target != Vector2.zero && Vector2.Distance(transform.position, Target) > 0.2f)
        {
            MoveToPoint(Target);
        }
        else
        {
            Stop();
        }
    }

    public override void WhenClickOnGround(Vector2 point) // Приказ идти в точку клика
    {
        //base.WhenClickOnGround(point);
        Target = point;
    }
}
