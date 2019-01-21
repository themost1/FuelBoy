#pragma strict

var factor : float;

function Start () {
    factor = 1.0 / transform.localScale.x; 
}

function Update () {
    GetComponent.<Renderer>().material.SetTextureOffset("_MainTex", Vector2(Camera.main.transform.position.x * factor, 0.0));
}