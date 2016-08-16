using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//ステージ処理
//自機が移動するとステージが出現
public class StageManager : MonoBehaviour {
	public static List<GameObject> stages = new List<GameObject>();
	public int stageCount;
	void Start () {
		stageCount = 0;
		stages.Add(GameObject.Find("Stages/Stage"));
		CreateStage();
		CreateStage();
		CreateStage();
	}
	void Update () {
	}
  
  //Stage生成
	public static void CreateStage() {
		GameObject stage = (GameObject)Resources.Load("Stages/Stage");
		Vector3 pos = stages[stages.Count - 1].transform.position;
		pos.y = stages.Count;
		GameObject obj = (GameObject)Instantiate(stage, pos, Quaternion.identity);
		obj.transform.SetParent(GameObject.Find("Stages").transform);
		obj.transform.rotation = stages[stages.Count -1].transform.rotation;
		obj.transform.Rotate(0, 15, 0);
		obj.name = "stage_"+ stages.Count;
		stages.Add(obj);
	}
}
