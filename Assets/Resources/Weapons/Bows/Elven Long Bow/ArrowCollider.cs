using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIBehavior;

public class ArrowCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            collision.gameObject.GetComponent<AIBehaviors>().Damage(25);
        }

        Destroy(this.gameObject);
    }
}
