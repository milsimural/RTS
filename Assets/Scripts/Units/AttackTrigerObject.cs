using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigerObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("CanHit") == true) 
        {
            Debug.Log(transform.name + "Hit somebody");
        }
        else if(other.gameObject.CompareTag("Shield") == true)
        {
            Debug.Log(transform.name + "We take block!");
        }
    }
}
