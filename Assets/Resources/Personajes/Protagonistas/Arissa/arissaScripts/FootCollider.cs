using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIBehavior;

public class FootCollider : MonoBehaviour
{
    private bool atacar = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            other.gameObject.GetComponent<AIBehaviors>().Damage(20);
        }

        atacar = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            atacar = true;
        }
    }
}
