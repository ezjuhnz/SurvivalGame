using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBowScript : MonoBehaviour {
    private Rigidbody rb;
    public float speed = 30f;
    public float deacvite_time = 3f;
    public float damage = 15f;
	// Use this for initialization
	void Awake () {
        rb = this.GetComponent<Rigidbody>();
	}
	
    void Start()
    {
        Invoke("DeactiveGameObject", deacvite_time);
    }
	// Update is called once per frame
	void Update () {
		
	}

    void DeactiveGameObject()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider target)
    {
        if(target.tag == Tags.ENEMY_TAG)
        {
            target.GetComponent<HealthScript>().ApplyDamage(damage);
        }
    }

    public void Launch(Camera mainCamera)
    {
        rb.velocity = mainCamera.transform.forward * speed;
        transform.LookAt(transform.position + rb.velocity);
    }
}
