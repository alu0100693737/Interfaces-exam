using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour {

	public int pointsPerMole;
	public TextMesh scoreUI;

	public Transform[] lifesUI;

	int score;
	int lifes;

	void Awake() {
		lifes = 2;
		score = 0;
	}

	void OnEnable() {
		// Subscribirse al evento.
		Mole.OnMoleIsHit += HandleMoleIsHit;
	}
	
	void OnDisable() {
		// Desubscribirse al evento.
		Mole.OnMoleIsHit -= HandleMoleIsHit;
	}

	public void HandleMoleIsHit(Transform mole, bool wasRight) {
		if (wasRight)
			ScoreUp ();
		else
			Miss ();
						;
		//score += pointsPerMole;
		//Debug.Log ("well done! " + score);
		//scoreUI.text = score.ToString ("D3"); // D3 formatea el texto a 3 digitos-
	}
	void ScoreUp() {
		score += pointsPerMole;
		scoreUI.text = score.ToString ("D3");
	}

	void Miss() {
		lifes--;
		if (lifes >= 0)
			lifesUI [lifes].GetComponent<Renderer>().enabled = false;
		if (lifes < 0)
			Application.LoadLevel ("GameHub");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
