using UnityEngine;
using System.Collections;

public class TiledBackground : MonoBehaviour {

    public float scrollSpeed = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 offset = new Vector2(Time.time * scrollSpeed, 0);

        GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
}
