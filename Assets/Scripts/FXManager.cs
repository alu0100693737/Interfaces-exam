using UnityEngine;
using System.Collections;

public class FXManager : MonoBehaviour {

	public ParticleSystem hitParticles;

	void OnEnable() {
		// Subscribirse al evento.
		Mole.OnMoleIsHit += MoleIsHit;
	}

	void OnDisable() {
		// Desubscribirse al evento.
		Mole.OnMoleIsHit -= MoleIsHit;
	}

	public void MoleIsHit(Transform mole, bool wasRight) {
		hitParticles.transform.position = mole.position;
		hitParticles.Play ();
	}

}
