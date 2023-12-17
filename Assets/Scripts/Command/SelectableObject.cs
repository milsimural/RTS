using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SelectableObject : MonoBehaviour
{
    // ����� ��������
    // ����� ����� ���������
    // ��������� �� ��������� �������
    // ��������� �� ��������� �������

    public GameObject SelectionIndicator; // ��������� ���� ��� ������ �������

    public void Start()
    {
        SelectionIndicator.SetActive(false);

        var agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public virtual void OnHover()
    {
        transform.localScale = Vector3.one * 1.1f;
    }

    public virtual void OnUnHover()
    {
        transform.localScale = Vector3.one;
    }

    public virtual void Select()
    {
        SelectionIndicator.SetActive(true);
    }

    public virtual void UnSelect()
    {
        SelectionIndicator.SetActive(false);
    }

    public virtual void WhenClickOnGround(Vector2 point)
    {

    }
}
