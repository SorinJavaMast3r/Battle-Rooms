using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills1/EarthCastSkill")]
public class EarthCastSkill : Skill
{
    public Rigidbody efecto;

    public override void lanzarHabilidad(GameObject emisor)
    {
        emisor.GetComponent<HabilidadDeTierra>().Cast(this.efecto);
    }
}
