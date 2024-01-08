using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visiblity : MonoBehaviour
{
    public CircleCollider2D visiblityCollider;
    public List<SelectableObject> EnemiesInSight = new List<SelectableObject>();
    public List<SelectableObject> AlliesInSight = new List<SelectableObject>();
    public SelectableCollaider self;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ally"))
        {
            AlliesInSight.Add(collision.GetComponent<SelectableObject>());
        }
        else if (collision.CompareTag("Enemy"))
        {
            EnemiesInSight.Add(collision.GetComponent<SelectableObject>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ally"))
        {
            AlliesInSight.Remove(collision.GetComponent<SelectableObject>());
        }
        else if (collision.CompareTag("Enemy"))
        {
            EnemiesInSight.Remove(collision.GetComponent<SelectableObject>());
        }
    }
}
