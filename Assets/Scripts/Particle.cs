using UnityEngine;
using System.Collections;

//パーティクル
public class Particle : MonoBehaviour {
	private ParticleSystem particle;
	private Vector2 pos = Vector2.zero;
	
	void Start () {
		particle = this.GetComponent<ParticleSystem>();	
	}
	
	void Update () {
		if (particle.isStopped)		Destroy(this.gameObject);
	}
}
