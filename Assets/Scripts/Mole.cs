using UnityEngine;
using System.Collections;

public class Mole : MonoBehaviour {

	public delegate void _moleIsHit (Transform who, bool wasRight);
	public static event _moleIsHit OnMoleIsHit;

	public float moveSpeed = 3;
	public float hidingTime = 4;
	public float hidingOffset = 2;
	public float showingTime = 2;
	public float showingOffset = 1;
	public float elapsedShowTime = 0;

	bool right;

	Vector3 home;
	TextMesh operationText;

	enum MoleStates {
		Hidden,
		Rising,
		Showing,
		Hiding
	};
	MoleStates currentState;

	void Start () {
		home = transform.position;
		operationText = transform.FindChild ("Operation").GetComponent<TextMesh>();
		Hidden ();
	}

	void ChangeState (MoleStates nextState) {
		currentState = nextState;
	}

	void Hidden() {
		ChangeState (MoleStates.Hidden);
		transform.position = home;
		SetOperation ();
		Invoke ("Rising", hidingTime + Random.Range(-hidingOffset, hidingOffset));
	}

	void SetOperation() {
		right = Random.value < 0.75f; // El 75% de las veces sera correcto.

		int a = Random.Range (2, 10);
		int b = Random.Range (1, 10);

		int result = right ? a * b : FakeAnswer (a, b);

		transform.tag = right ? "RightAnswer" : "WrongAnswer";
		operationText.text = a + "x" + b + "=" + result;
		HideOperation ();
	}

	void HideOperation () {
		operationText.GetComponent<Renderer>().enabled = false;
	}

	void RevealOperation() {
		operationText.GetComponent<Renderer>().enabled = true;
	}

	int FakeAnswer(int a, int b) {
		int offset = Random.value < 0.5f ? 1 : -1;
		return a * b + offset * b;
	}

	void Rising() {
		ChangeState (MoleStates.Rising);
		Invoke ("Showing", 0.5f);
	}

	void Showing() {
		ChangeState (MoleStates.Showing);
		RevealOperation ();
		Invoke ("Hiding", showingTime + Random.Range(-showingOffset, showingOffset));
	}

	void Hiding() {
		ChangeState (MoleStates.Hiding);
		Invoke ("Hidden", 0.5f);
	}

	void Update() {
		if (currentState == MoleStates.Rising)
			Rise();
		if (currentState == MoleStates.Hiding)
			Hide();
	}

	void Rise() {
		transform.position += Vector3.up * Time.deltaTime * moveSpeed;
	}

	void Hide() {
		transform.position -= Vector3.up * Time.deltaTime * moveSpeed;
	}

	void OnMouseDown() {
		if (currentState != MoleStates.Hidden) {
			Hit ();
		}
	}

	void Hit() {
		Debug.Log ("OUCH!");
		if (OnMoleIsHit != null) 
			OnMoleIsHit (transform, right);
		CancelInvoke ();
		Hidden ();
	}
}
 