using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equiparArco : MonoBehaviour
{
    public Transform Pivot;

	public void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Arco")
		{
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
            collision.transform.position = Pivot.position;
            collision.transform.rotation = Pivot.rotation;
            collision.transform.SetParent(Pivot);
		}
	}
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
