using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : Unit
{

    private void Start()
    {
        UnitVisionAngle = 10f;
        UnitVisionDistance = 25f;
    }

    private void Update()
    {
        if (isPatrol)
        {
            Patrol();
        }
    }

}
