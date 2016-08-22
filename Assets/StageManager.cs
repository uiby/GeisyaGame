using UnityEngine;
using System.Collections;

//ステージ管理
public class StageManager : MonoBehaviour {
	private GameObject branch;
	private GameObject bard;
	// Use this for initialization
	void Start () {
		branch = (GameObject)Resources.Load("Branch");
		bard = (GameObject)Resources.Load("Bard01");
		for (int y = 1; y < 20; y++) {
			GameObject temp = (GameObject)Instantiate(branch, new Vector2(0, y * 5), Quaternion.identity);
			temp.transform.SetParent(GameObject.Find("Tree").transform);
			GameObject bardObj = (GameObject)Instantiate(bard, new Vector2(0, 0), Quaternion.identity);
			bardObj.transform.SetParent(temp.transform);
			bardObj.transform.localPosition = new Vector2(0, 0.6f);
		}
		for (int y = 1; y < 20; y++) {
			GameObject temp = (GameObject)Instantiate(branch, new Vector2(4.52f, y * 5), Quaternion.identity);
			temp.transform.SetParent(GameObject.Find("Tree").transform);
			GameObject bardObj = (GameObject)Instantiate(bard, new Vector2(0, 0), Quaternion.identity);
			bardObj.transform.SetParent(temp.transform);
			bardObj.transform.localPosition = new Vector2(0, 0.6f);
			bardObj.GetComponent<SpriteRenderer>().flipX = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
