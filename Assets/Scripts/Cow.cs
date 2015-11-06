using UnityEngine;
using System.Collections;

public class Cow : MonoBehaviour {

	enum CowStates {
		Idle,
		Wandering
	}
	CowStates currentState;

	public float pushSpeed;
	public float maxVelocity;
	public float turnSpeed;

	Vector3 desiredOrientation;

	void Awake() {
		desiredOrientation = transform.forward;
	}

	// Use this for initialization
	void Start () {
		ChangeState (CowStates.Idle);
		StartCoroutine (FSM ());
	}

	IEnumerator FSM() {
		while (true)
			yield return StartCoroutine (currentState.ToString ());
	}

	void ChangeState(CowStates nextState) {
		Debug.Log ("Cow: " + nextState);
		currentState = nextState;
	}

	IEnumerator Idle() {
		Debug.Log ("Idle...");
		while (currentState == CowStates.Idle) {
			Debug.Log ("MUUUUUUU");
			yield return new WaitForSeconds(Random.value * 2.0f);
			if (Random.value > 0.5f) {
				ChangeState (CowStates.Wandering);
			}
		}
		Debug.Log ("gonna walk a bit...");
	}

	IEnumerator Wandering() {
		Debug.Log ("Wandering " + Time.time);
		while (currentState == CowStates.Wandering) {
			desiredOrientation = new Vector3(Random.Range(-1f, 1f),  0f, Random.Range (-1f, 1f)).normalized;

			Debug.Log (transform.position.magnitude);
			if (transform.position.magnitude > 15)
				desiredOrientation = new Vector3 (
				-transform.position.x,
				0,
				-transform.position.z).normalized;

			yield return new WaitForSeconds (Random.value * 3.0f);
			if (Random.value < 0.25f) {
				ChangeState (CowStates.Idle);
			}


			yield return 0;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (currentState == CowStates.Wandering) {
			//transform.rotation = Quaternion.FromToRotation (transform.forward, desiredOrientation);
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (desiredOrientation), Time.deltaTime * turnSpeed);
			//if (rigidbody.velocity.magnitude < maxVelocity) 
			//	rigidbody.AddForce (transform.forward * pushSpeed * Time.deltaTime);
			GetComponent<Rigidbody>().velocity = transform.forward.normalized * maxVelocity;
		}
		Debug.DrawLine (transform.position, transform.position + transform.forward * 5.0f, Color.white);
		Debug.DrawLine (transform.position, transform.position + desiredOrientation * 5.0f, Color.red);
	}

	void OnCollisionEnter(Collision collision) {
		Debug.Log ("Bump on " + collision.transform.name);
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("Triggered " + other.transform.name);
	}
	
	void OnTriggerStay(Collider other) {
		Debug.Log ("Inside " + other.transform.name);
	}
	
	void OnTriggerExit(Collider other) {
		Debug.Log ("Bye " + other.transform.name);
	}

}
