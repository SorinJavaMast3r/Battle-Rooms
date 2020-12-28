using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills1/RayCastSkill")]
public class RayCastSkill : Skill
{
    public float range;
    public Rigidbody efecto;

    public override void lanzarHabilidad(GameObject emitter)
    {
        emitter.GetComponent<HabilidadDeCasteo>().Cast(new Ray(), this.range, efecto);
    }
}
