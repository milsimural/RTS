using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Unit : SelectableObject
{

    public Animator anim;
    public NavMeshAgent Agent;

    public Transform[] movePoint; // Points
    

    public bool circularRoute;
    public int curWaypoint;
    public float distance;
    private Vector3 moveToPoint; // Point where unit have to go
    public float speed;
    public float walkSpeed;
    public float runSpeed;
    public float rotationSpeed;
    public bool fast;

    private LineRenderer lineRenderer;
    public NavMeshPath path;

    //==UnitStats==//
    public float UnitPower;

    //==Unit Vision==//
    public float UnitVisionAngle;
    public float UnitVisionDistance;


    //==Behavior==//
    public bool isPatrol;
    public Transform Utils;

    //==Attack==//
    public enum _attackState
    {
        InStrike,
        InBlock,
        InDefenceMove,
        InDefenceIdle,
        GoToTarget,
        No
    }

    public _attackState AttackState;
    public float AttackRange;

    //==HP==//

    public int MaxHP;
    public int CurrentHP;

    public enum _healthState
    {
        Full, Injured, Low, Critical
    }

    public _healthState HealthState;


    private void Start()
    {
        SelectionIndicator.SetActive(false);
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;

        moveToPoint = transform.position;

        lineRenderer = GetComponent<LineRenderer>();

        path = new NavMeshPath();

        AttackState = _attackState.No;
        HealthState = _healthState.Full;
    }

    // Юнит последовательно двигается по заданным точкам
    public void Patrol()
    {
        if (movePoint.Length >= 1)
        {
            if (movePoint.Length > curWaypoint)
            {
                if (Vector2.Distance(transform.position, movePoint[curWaypoint].position) < 0.2f)
                {
                    curWaypoint++;
                }
                else
                {
                    MoveToPoint(movePoint[curWaypoint].position);
                }
            }
            else if (movePoint.Length == curWaypoint)
            {
                if (circularRoute)
                {
                    if (movePoint.Length > 1)
                        curWaypoint = 0;
                    else circularRoute = false;
                }

                else
                {
                    if (Vector2.Distance(transform.position, movePoint[curWaypoint - 1].position) < 0.2f)
                        Stop();
                    else
                    {
                        MoveToPoint(movePoint[curWaypoint - 1].position);
                    }
                }
            }
        }
        else
        {
            Stop();
        }
    }


    public virtual void MoveToPoint(Vector2 point)
    {
        var position = new Vector3(point.x, point.y, transform.position.z);

        if (moveToPoint != position)
        {
            Agent.SetDestination(position);
            moveToPoint = position;
        }

        if (Agent.hasPath)
        {
            if (fast)
                speed = Mathf.MoveTowards(speed, runSpeed, Time.deltaTime * 3);
            else speed = Mathf.MoveTowards(speed, walkSpeed, Time.deltaTime * 3);

            if(anim)
                anim.SetFloat("Speed", speed);
            
            Agent.speed = speed;

            lineRenderer.positionCount = Agent.path.corners.Length;
            lineRenderer.SetPositions(Agent.path.corners);

            RotationToTarget(Agent.path.corners[1]);
        }
        else RotationToTarget(moveToPoint);
    }

    public bool RotationToTarget(Vector3 point)
    {
        Vector2 targetPoint = new Vector2(point.x, point.y);
        var direction = targetPoint - new Vector2(transform.position.x, transform.position.y);
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime);

        if (transform.rotation == Quaternion.Euler(0, 0, angle))
        {

            return true;
        }
        else
        {

            return false;
        }
    }

    public void Stop()
    {
        if (speed > 0)
        {
            Agent.SetDestination(transform.position);
            speed = Mathf.MoveTowards(speed, 0, Time.deltaTime * 15);
            anim.SetFloat("Speed", speed);
            Agent.speed = speed;
        }
    }

    //==Attack Scripts==//
    public void GoToTarget(Vector2 targetPosition)
    {
        MoveToPoint(targetPosition);
    }

    public Vector2 TrackingTarget(GameObject target)
    {
        if(target != null)
        {
            return new Vector2(target.transform.position.x, target.transform.position.y);
        }
        else
        {
            return Vector2.zero;
        }
        
    }

    public GameObject ChooseNearestTarget(GameObject[] avalibleTargets)
    {
        float distanceToTarget = 0f;
        GameObject nearestTarget = null;
        foreach (GameObject target in avalibleTargets)
        {
            float cheker = Vector2.Distance(transform.position, target.transform.position);
            if(distanceToTarget > cheker)
            {
                distanceToTarget = cheker;
                nearestTarget = target;
            }
        }
        return nearestTarget; 
    }

    public GameObject ChooseMostPowerTarget(GameObject[] avalibleTargets)
    {
        float powerTarget = 0f;
        GameObject mostPowerTarget = null;
        foreach(GameObject target in avalibleTargets)
        {
            float cheker = target.GetComponent<Unit>().UnitPower;
            if(powerTarget < cheker)
            {
                powerTarget = cheker;
                mostPowerTarget = target;
            }
        }
        return mostPowerTarget;
    }

    public GameObject ChooseMostWeakTarget(GameObject[] avalibleTargets)
    {
        float weakTarget = avalibleTargets[0].GetComponent<Unit>().UnitPower;
        GameObject mostWeakTarget = null;
        foreach (GameObject target in avalibleTargets)
        {
            float cheker = target.GetComponent<Unit>().UnitPower;
            if (weakTarget > cheker)
            {
                weakTarget = cheker;
                mostWeakTarget = target;
            }
        }
        return mostWeakTarget;
    }

    public void FollowTargetAndStrike(GameObject target, float AttackRange)
    {
        if(Vector2.Distance(transform.position, target.transform.position) > AttackRange)
        {
            MoveToPoint(target.transform.position);
        }
        else
        {
            Strike(target);
        }
    }

    public virtual void Strike(GameObject target)
    {
        //сюда помещаем функции скиллов атаки которая реализуется в дочерних классах
        //проигрываем тригер анимации атаки
    }

    public virtual void RotateToHitBoxes(GameObject target)
    {

    }

    //==HP Metods==//

    public virtual void MinusHP(int amount)
    {
        CurrentHP -= amount;
        if(CurrentHP < 0)
        {
            Coma();
        }
    }

    public virtual void PlusHP(int amount)
    {
        CurrentHP += amount;
        if(CurrentHP > MaxHP)
        {
            CurrentHP = MaxHP;
        }
    }

    public virtual void Injured(_healthState healthState) // Если Юнит ранен то раз в определенное время запускается этот метод.
    {
        if(healthState == _healthState.Injured)
        {
            MinusHP(1);
        }
        else if(healthState == _healthState.Low)
        {
            MinusHP(2);
        }
        else if(healthState == _healthState.Critical)
        {
            MinusHP(3);
        }
    }

    public virtual void Coma() // Метод отправляющий Юнит в кому.
    {
        anim.SetTrigger("Coma");
        SelectableType = _SelectableType.UnitComa;
        Utils.gameObject.SetActive(false); // Выключаем все коллайдеры, тригерры
        GetComponent<LineRenderer>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        SelectionIndicator.GetComponent<SpriteRenderer>().color = Color.gray;
    }
}
