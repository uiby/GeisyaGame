using UnityEngine;
using System.Collections;

public class TapResultWord : MonoBehaviour {
	private float timer = 15;
	private GameObject target;
	private ParticleSystem particle;
	// Use this for initialization
	void Start () {
		timer = 10;
		this.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
		particle = this.transform.FindChild("ParticleSystem").GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if (timer < 0 && !particle.isPlaying) Destroy(this.gameObject);
		timer -= 1;
		//transform.RotateAround(target.transform.position, Vector3.forward, 45 / 10);

		Vector3 scale = this.transform.localScale;
		if (timer >= 3) {
	  	scale.x += 0.1f;
		  scale.y += 0.1f;
	 	  scale.z += 0.1f;
	 	}
		this.transform.localScale = scale;
		//Debug.Log("大きさ:"+ scale);
	}

	//ぶつかった時のエフェクト追加
  public void PlayEffect(float r, float g, float b) {
  	Color color = new Color(r/255, g/255, b/255);
  	if (particle == null) 	particle = this.transform.FindChild("ParticleSystem").GetComponent<ParticleSystem>();
  	particle.startColor = color;
  	particle.Play();
  }
}
