using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirate : MonoBehaviour {

    public GameObject Player;
	public int health;
	public float speed;
	public float rotSpeed;
	public float ignoreRadius;
	public float shootRadius;
	public float swerveRadius;
	public float angle;

    public float missileSpeed;
    public GameObject missilePrefab;
    public Transform missileSpawn;

    float deltaFire = 0;
    float baseFireTime = 0.3f;

    // Use this for initialization
    void Start () {

	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Missile")
        {
            Destroy(this.gameObject);
            Destroy(col.gameObject);
        }
    }

    // Update is called once per frame
    void Update(){
//		float step = (float)(speed * Time.deltaTime);
		if (Vector3.Distance(this.transform.position, Player.transform.position) < ignoreRadius){
			if (health>20){
				Attack();
			}
			else{
				Escape(); 
			}
		}
	}

	private  void Escape(){
		Vector3 vectorToTarget = Player.transform.position - this.transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(-angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotSpeed);
		moveUp();
	}

	private void Attack(){
        if (Vector3.Distance(this.transform.position, Player.transform.position) > shootRadius) {
            moveUp();
        }
        else
        {
            if (deltaFire > 0)
                deltaFire -= Time.deltaTime;
            else {
                fire();
                deltaFire = baseFireTime;
            }
        }
		Vector3 vectorToTarget = Player.transform.position - this.transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotSpeed);

		object[] obj = GameObject.FindObjectsOfType(typeof (GameObject));
		foreach (object o in obj){
			GameObject g = (GameObject) o;
			if (g.CompareTag ("Missile") == true) {
				if (Vector3.Distance (this.transform.position, Player.transform.position) < swerveRadius && Vector3.Angle(Player.transform.forward, transform.position - Player.transform.position) < angle) {
					swerve ();
				}
			}
		}
	}
		
	private void swerve(){
		this.transform.eulerAngles += new Vector3(4 , 0, 0);
	}

	private void moveUp(){
		Vector3 fwrd = this.transform.right;
		fwrd.Scale(new Vector3(speed, speed, speed));
		this.transform.position += fwrd;
	}

    private void fire()
    {
        Vector3 perp = transform.right;
        perp.x = perp.y;
        perp.y = -transform.right.x;

        //create the missile from the missile Prefab
        var missile1 = (GameObject)Instantiate(
            missilePrefab,
            transform.position+0.45f*perp,
            transform.rotation);

        var missile2 = (GameObject)Instantiate(
            missilePrefab,
            transform.position-0.45f*perp,
            transform.rotation);

        Physics2D.IgnoreCollision(missile1.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(missile2.GetComponent<Collider2D>(), GetComponent<Collider2D>());


        // Add velocity to the missile
        Vector2 missileRight = new Vector2(missile1.transform.right.x, missile1.transform.right.y);
        missile1.GetComponent<Rigidbody2D>().velocity = missileRight * missileSpeed;
        missile2.GetComponent<Rigidbody2D>().velocity = missileRight * missileSpeed;

        // Destroy the missile after 2 seconds
        Destroy(missile1, 5.0f);
        Destroy(missile2, 5.0f);
    }
}