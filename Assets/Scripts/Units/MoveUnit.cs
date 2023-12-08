using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnit : MonoBehaviour
{
    private Transform _selfTransform;
    [Range(0f, 10f)]
    public float Speed;
    public Transform TargetPoint;
    public GestureClick Gesture;
    
    void Start()
    {
        _selfTransform = GetComponent<Transform>();

        Gesture.OnClick += (pos) =>
        {
            TargetPoint.position = pos;
        };
    }

    void Update()
    {
        _selfTransform.position = Vector3.MoveTowards(_selfTransform.position, TargetPoint.position, Speed * Time.deltaTime);
    }
}
