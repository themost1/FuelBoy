using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Missile")
        {
            Destroy(this.gameObject);
            Destroy(col.gameObject);
        }
    }
}
