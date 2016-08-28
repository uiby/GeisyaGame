using UnityEngine;
using System.Collections;

//goalの演出処理
public class Goal : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayGoal() {
  	GameObject.Find("GameClear").GetComponent<GameClear>().ShowCanvas();
		Debug.Log("Goal");
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
  		PlayGoal();
  	}
  }
}
