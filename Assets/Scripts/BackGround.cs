using UnityEngine;
using System.Collections;

//背景
public class BackGround : MonoBehaviour {
	private MainCamera mainCamera;
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.Find("MainCamera").GetComponent<MainCamera>();
	}
	
	// Update is called once per frame
	void Update () {
		SetPosition();
	}

	private void SetPosition() {
		Vector2 pos = mainCamera.GetMovePosition();
		float add = pos.x / 6.0f;
		if (add <= 0) return;
		Debug.Log(add);
		Vector2 nextPos = this.transform.position;
		nextPos.x += add;
		this.transform.position = nextPos;
	}
}
