using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills1/SkillProyectil1")]
public class SkillProyectil : Skill
{
    public Rigidbody proyectil;
    public float velocidad;

    public override void lanzarHabilidad(GameObject emisor)
    {
        emisor.GetComponent<LanzarProyectil>().Lanzar(this.proyectil, this.velocidad);
    }
}
