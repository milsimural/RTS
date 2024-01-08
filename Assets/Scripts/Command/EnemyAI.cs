using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : Unit
{

    private void Update()
    {
        if (isPatrol)
        {
            Patrol();
        }
    }

}
