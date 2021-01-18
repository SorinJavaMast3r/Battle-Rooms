using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIBehavior;

public class SwordCollider : MonoBehaviour
{
    public bool atacar = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy") && atacar)
        {
            other.gameObject.GetComponent<AIBehaviors>().Damage(30);
        }

        atacar = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            atacar = true;
        }
    }
}
