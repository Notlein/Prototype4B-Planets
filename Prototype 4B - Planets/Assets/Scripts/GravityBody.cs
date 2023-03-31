using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour {
	
	public GravityAttractor planet;
	public Rigidbody rigidbody;
	public GameObject lastplanet;
	public GameObject variable_score;
	public int score;
	
	void Awake () {
		planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
		score = 0;
		variable_score.GetComponent<TextMeshProUGUI>().text = score + "";
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
    private void OnTriggerEnter(Collider other)
    {
		if (other.CompareTag("alien") && gameObject.CompareTag("Player"))
		{
			other.gameObject.GetComponentInChildren<TextMeshPro>().enabled = true;
			
		}
		if (other.CompareTag("pickup") && gameObject.CompareTag("Player"))
		{
			Destroy(other.gameObject);
			score++;
			variable_score.GetComponent<TextMeshProUGUI>().text = score + "";

		}
	}

    private void OnCollisionEnter(Collision collision)
    {
		/*if (collision.collider.CompareTag("alien"))
		{
			Debug.Log("OK");
			collision.collider.gameObject.GetComponentInChildren<TextMesh>().gameObject.SetActive(true);
		}
		*/
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
		if (collision.collider.CompareTag("spaceship"))
		{
			if (score >= 4)
            {
				SceneManager.LoadScene("End");
            }
		};
	}
}