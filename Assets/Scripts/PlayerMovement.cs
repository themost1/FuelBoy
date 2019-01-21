using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    Animator animator;

    public int rotSpeed = -100;
    public int moveSpeed = 200;

    public float shootingDelay = 0;
    public float baseShootingDelay = 0.2f;

    public int missileSpeed = 6;

    public GameObject missilePrefab;
    public Transform missileSpawn;

    public RuntimeAnimatorController baseAnim;
    public RuntimeAnimatorController right;
    public RuntimeAnimatorController left;
    public RuntimeAnimatorController rightThrust;
    public RuntimeAnimatorController leftThrust;
    public RuntimeAnimatorController thrust;

    public Slider fuel;

	// Use this for initialization
	void Start () {
        animator = this.GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update() {
        if (shootingDelay > 0)
            shootingDelay -= Time.deltaTime;

        float rot = Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.Rotate(0, 0, rot);

        float trans = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        GetComponent<Rigidbody2D>().AddForce(transform.right * trans);
        fuel.value -= 0.01f * Mathf.Abs(trans);

        if (trans != 0)
        {
            if (rot > 0)
            {
                animator.runtimeAnimatorController = rightThrust;
            }
            else if (rot < 0)
            {
                animator.runtimeAnimatorController = leftThrust;
            }
            else
            {
                animator.runtimeAnimatorController = thrust;
            }
        }
        else
        {
            if (rot > 0)
            {
                animator.runtimeAnimatorController = right;
            }
            else if (rot < 0)
            {
                animator.runtimeAnimatorController = left;
            }
            else
            {
                animator.runtimeAnimatorController = baseAnim;
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (shootingDelay <= 0)
            {
                Fire();
                shootingDelay = baseShootingDelay;
            }

        }
    }

    void Fire()
    {
        fuel.value -= 20;

        // Create the missile from the missile Prefab
        var missile = (GameObject)Instantiate(
            missilePrefab,
            missileSpawn.position,
            missileSpawn.rotation);

        Physics2D.IgnoreCollision(missile.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        // Add velocity to the missile
        Vector2 missileRight = new Vector2(missile.transform.right.x, missile.transform.right.y);
        missile.GetComponent<Rigidbody2D>().velocity = (missileRight+0.1f*GetComponent<Rigidbody2D>().velocity) * missileSpeed;

        // Destroy the missile after 2 seconds
        Destroy(missile, 2.0f);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "EnemyMissile")
        {
            fuel.value = fuel.value -= 50;
            Destroy(col.gameObject);
        }
    }
}
