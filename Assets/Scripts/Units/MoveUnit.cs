using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveUnit : MonoBehaviour
{
    private Transform _selfTransform;
    [Range(0f, 10f)]
    public float Speed = 0.2f;
    private Animator _selfAnimator;

    void Start()
    {
        _selfTransform = GetComponent<Transform>();
        _selfAnimator = GetComponent<Animator>();


    }

    void Update()
    {
        
        
        
        
        //if (TargetPoint)
        //{
        //    Vector3 newPosition =  Vector3.MoveTowards(_selfTransform.position, TargetPoint.position, Speed * Time.deltaTime);
        //    Vector3 between = newPosition - _selfTransform.position;
        //    _selfAnimator.SetFloat("Speed", between.magnitude / Time.deltaTime);
        //    _selfTransform.position = newPosition;

        //    var direction = TargetPoint.position - _selfTransform.position;
        //    var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //    _selfTransform.rotation = Quaternion.Euler(0, 0, angle + 90);
        //}    
    }
}
