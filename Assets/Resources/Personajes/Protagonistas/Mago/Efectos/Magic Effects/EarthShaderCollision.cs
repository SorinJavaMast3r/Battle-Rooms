using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIBehavior;

public class EarthShaderCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            other.gameObject.GetComponent<AIBehaviors>().Damage(50);
        }
    }
}
