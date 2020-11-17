using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteraccionItems : MonoBehaviour
{
	public int vida = 10;
	public int mana = 10;

	public int PocionVida()
    {
		vida++;
		return vida;
    }
	public int PocionMana()
	{
		vida--;
		return vida;
	}



}
