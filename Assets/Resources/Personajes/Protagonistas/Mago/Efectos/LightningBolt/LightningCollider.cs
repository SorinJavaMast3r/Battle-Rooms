using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIBehavior;

public class LightningCollider : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            other.gameObject.GetComponent<AIBehaviors>().Damage(1);
        }
    }
}
