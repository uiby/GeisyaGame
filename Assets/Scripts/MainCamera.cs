using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {
	public GameObject egg; //タマゴ
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = GetPositon();
		pos.y = egg.transform.position.y + 0.5f;
		pos.z = -1;
		this.transform.position = pos;
	}

	private Vector3 GetPositon() {
		return this.transform.position;
	}
}
