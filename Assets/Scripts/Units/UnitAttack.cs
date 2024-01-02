using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    public enum _attackState
    {
        InStrike,
        InBlock,
        InDefenceMove,
        InDefenceIdle,
        Other
    }

    public _attackState AttackState;   

    void Start()
    {
        AttackState = _attackState.Other;
    }

    
    void Update()
    {
        
    }
}
