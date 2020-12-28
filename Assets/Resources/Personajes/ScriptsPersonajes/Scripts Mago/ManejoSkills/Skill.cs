using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public string nombreHabilidad;

    public abstract void lanzarHabilidad(GameObject emisor);
}
