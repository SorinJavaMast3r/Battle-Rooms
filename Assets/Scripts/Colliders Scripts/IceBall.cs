using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : MonoBehaviour
{
    public GameObject iceExplosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                print(collision.gameObject.name);
                //dañar al enemigo
                break;
            default:
                break;
        }

        GameObject explosion = Instantiate(iceExplosion, transform.position, transform.rotation);
        Destroy(this.gameObject);
        Destroy(explosion, 0.5f);
    }
}
