using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour 
{
	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public SimpleTouchPad touchPad;
	public SimpleTouchAreaButton areaButton;

	private Rigidbody rb;
	private AudioSource audioSource;
	private float nextFire;
	private Quaternion calibrationQuaternion;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource> ();
		CalibrateAccelerometer ();
	}

	void Update ()
	{
		// using keyboard/mouse to fire
		// if (Input.GetButton("Fire1") && Time.time > nextFire) {
		// using touch area button
		if ( areaButton.CanFire() && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			// GameObject clone = 
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation); // as GameObject;
			audioSource.Play();
		}
	}

	void FixedUpdate ()
	{
		/*
		// using the keyboard and mouse for input
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		*/

		/*
		// using a device (e.g. iOS) accelerometer for input
		Vector3 accelerationRaw = Input.acceleration;
		Vector3 acceleration = FixAcceleration(accelerationRaw);
		Vector3 movement = new Vector3 (acceleration.x, 0.0f, acceleration.y);
		*/

		// using touchpad
		Vector2 direction = touchPad.GetDirection();
		Vector3 movement = new Vector3 (direction.x, 0.0f, direction.y);

		rb.velocity = movement * speed;

		// prevent ship from leaving play area
		rb.position = new Vector3
		( 
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax), 
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);

		// tilt shape left or right depending on movement
		rb.rotation = Quaternion.Euler( 0.0f, 0.0f, rb.velocity.x * -tilt);
	}

	void CalibrateAccelerometer ()
	{
		// take snapshot of acceleration vector and invert it
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);
	}

	Vector3 FixAcceleration (Vector3 acceleration)
	{
		// reset acceleration vector to 0 from device rotation calculated by CalibrateAccelerometer() 
		Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
		return fixedAcceleration;
	}
}
