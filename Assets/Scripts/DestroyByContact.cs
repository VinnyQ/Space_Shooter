using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour 
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}

		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter(Collider other) {
		// Debug.Log ("from DestroyByContact.OnTriggerEnter():  " + other.name);

		// ignore boundary collider
		if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
		{
			return;
		}

		// asteroid explosion vfx
		if (explosion != null) 
		{
			Instantiate (explosion, transform.position, transform.rotation);
		}

		// player explosion vfx
		if (other.CompareTag("Player"))
		{	
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();
		}

		// add score
		gameController.AddScore(scoreValue);

		// destroy
		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}
