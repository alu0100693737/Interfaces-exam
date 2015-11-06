using UnityEngine;
using System.Collections;

public class CowBoy : MonoBehaviour {

	public float maxVelocity;
	public float turnSpeed;

	float turn;
	float push;

	Vector3 desiredOrientation;

	void Awake() {
		desiredOrientation = transform.forward;
	}

	void Update () {

		// (Compilacion condicional).
		//#if UNITY_STANDALONE_WIN
		//	WinKeyboardControl ();
		//#endif

		KeyboardControl ();
		Debug.DrawLine (transform.position, transform.position + transform.forward * 5.0f, Color.white);
		//Debug.DrawLine (transform.position, transform.position + desiredOrientation * 5.0f, Color.red);
	}

	void KeyboardControl() {
		//Debug.Log (Input.GetAxis ("Vertical"));
		turn = Input.GetAxis ("Horizontal");
		push = Input.GetAxis ("Vertical");
	}

	void FixedUpdate() {
		//transform.Rotate
		transform.Rotate (new Vector3 (0, turn * turnSpeed, 0));
		GetComponent<Rigidbody>().velocity = transform.forward * push * maxVelocity;
	
	}
}
