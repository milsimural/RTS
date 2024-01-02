using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class Unit : SelectableObject
{
    public NavMeshAgent Agent;

    public Transform[] movePoint;
    public Animator anim;

    public bool circularRoute;
    public int curWaypoint;

    public float distance;

    private Vector3 moveToPoint;

    public float speed;
    public float walkSpeed;
    public float runSpeed;
    public float rotationSpeed;

    private LineRenderer lineRenderer;

    public bool fast;

    public NavMeshPath path;

    private void Start()
    {
        SelectionIndicator.SetActive(false);
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;

        moveToPoint = transform.position;

        lineRenderer = GetComponent<LineRenderer>();

        path = new NavMeshPath();
    }

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

    public float SkillSwordMiddleAttack(Sword swordItem)
    {
        float damage = swordItem.Damage;
        return damage;
    }
}
