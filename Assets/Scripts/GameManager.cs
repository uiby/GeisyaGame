using UnityEngine;
using System.Collections;
//ゲーム全体の処理
public class GameManager : MonoBehaviour {
  private CameraPosition cameraPosition;
  private GameObject player;

	// Use this for initialization
	void Start () {
		cameraPosition = GameObject.Find("MainCamera").GetComponent<CameraPosition>();
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
	//	if (isUp()) CreateNextBlack();
	}

	//上昇するかどうか判断
	/*private bool isUp() {
		Vector3 judge = Camera.main.WorldToViewportPoint(player.transform.position);
		if (judge.y > 0.75)  return true;
		return false;
	}*/

  //次のブロック
	public void CreateNextStage() {
		StageManager.CreateStage();
		cameraPosition.SetPos(StageManager.stages[StageManager.stages.Count - 1]);
	}
}
