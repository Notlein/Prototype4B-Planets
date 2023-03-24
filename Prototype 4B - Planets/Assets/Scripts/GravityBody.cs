using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour {
	
	public GravityAttractor planet;
	public Rigidbody rigidbody;
	public GameObject lastplanet;
	
	void Awake () {
		planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
		
		rigidbody = GetComponent<Rigidbody>();

		// Disable rigidbody gravity and rotation as this is simulated in GravityAttractor script
		rigidbody.useGravity = false;
		rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
	}

    private void Update()
        {
		if (Input.GetKeyDown(KeyCode.R)) {
			var death = new death();
			death.Die();
		}
		
		
    }


    void FixedUpdate () {
		// Allow this body to be influenced by planet's gravity
		planet.Attract(rigidbody);
	}
	
    private void OnCollisionEnter(Collision collision)
    {
		if (collision.collider.CompareTag("alien"))
		{
			Debug.Log("Contact alien");
		}

			if (collision.collider.CompareTag("Friendly Planet"))
		{
			lastplanet.GetComponent<AudioSource>().Pause();
			planet = collision.collider.gameObject.GetComponentInParent<GravityAttractor>();
			collision.collider.gameObject.GetComponent<AudioSource>().Play();
			lastplanet = collision.collider.gameObject;
		};

		if (collision.collider.CompareTag("death"))
		{
			var death = new death();
			death.Die();
		};
	}
}