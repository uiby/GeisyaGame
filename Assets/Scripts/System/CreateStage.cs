using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//ステージ生成
public class CreateStage : MonoBehaviour {
	//ステージ
	public class Stage {
		public Vector2 nextPos;
		public GameObject bard;  //鳥
		public GameObject branch; //枝
		public float interval = 0; //次のステージにかかる時間
		public float nextRad = 0; //次のステージに対する発射角度
		public float nextSpeed = 0; //次のステージに対する初速度
		public float nextGravity = 0; //次の重力
		public bool isGoal = false; //goalかどうか
	}
	public GameObject firstObj; //最初のオブジェクト
	public GameObject goal; //ゴール地点
	public static List<Stage> stages;
	public Stage firstStage;
	private GameObject timingPointLeft;
	private GameObject timingPointRight;
	public bool isRandom;
	public bool isColorChange;
	public float vy; //次のステージにタマゴが通る時のy軸の速度. 0なら頂点
	public Sprite blueBard;

	private List<int> x; //stage : x軸座標の集まり
	private List<float> y; //stage : y軸座標の集まり
	private int maxBardCount; //最大バード数

	void Start () {
		firstStage = new Stage();
		firstObj = GameObject.Find("Tree/FirstTimingPoint");
		timingPointLeft = (GameObject)Resources.Load("TimingPointLeft");
		timingPointRight = (GameObject)Resources.Load("TimingPointRight");
	}
	
	public void MakeStage() {
		stages = new List<Stage>();
		x = new List<int>();
		y = new List<float>();
		switch (GameManager.StageNumber) {
			case 0:
			  x.AddRange(Stage01.GetX());
			  y.AddRange(Stage01.GetY());
			  maxBardCount = Stage01.GetMaxBard();
			break;
			case 1:
			  x.AddRange(Stage02.GetX());
			  y.AddRange(Stage02.GetY());
			  maxBardCount = Stage02.GetMaxBard();
			break;
		}

    SetFirstBard();
		for (int i = 1; i < maxBardCount; i++) {
			SetBard(i);

		  if (isColorChange)  SetColor();//色指定

		}

		for (int i = 0; i < maxBardCount; i++) {
			GameObject point = stages[i].bard;
			GameObject bard = point.transform.FindChild("Bard").gameObject;
			GameObject branch = point.transform.FindChild("Branch").gameObject;
			bard.transform.SetParent(branch.transform);
			branch.transform.parent = null;
      point.transform.SetParent(bard.transform);
      branch.transform.SetParent(GameObject.Find("Tree").transform);
      stages[i].bard = bard;
      stages[i].branch = branch;
		}

		SetGoalStage();
	}

  //鳥の設置
  private void SetFirstBard() {
  	Stage stage = new Stage();
		stage.bard = CreateObj(timingPointRight);
		stages.Add(stage);
		stages[stages.Count - 1].interval = GetNextInterval(0);//時間の設定

		Vector2 pos = firstObj.transform.position; //今のステージ位置
  	pos.x += x[0];
  	pos.y += y[0];
  	SetParameter(pos, stages[stages.Count - 1].interval);
  	stages[stages.Count - 1].bard.transform.position = stages[stages.Count - 1].nextPos; //ステージの位置を画面に反映
		stages[stages.Count - 1].bard.transform.SetParent(GameObject.Find("Tree").transform); //親の設定

		SetColor();
  }
	private void SetBard(int count) {
		Stage stage = new Stage();
		stage.bard = CreateObj(timingPointRight);
		stages.Add(stage);
		stages[stages.Count - 1].interval = GetNextInterval(count);//時間の設定

		Vector2 pos = stages[stages.Count - 2].bard.transform.position;
  	pos.x += x[count];
  	pos.y += y[count];
  	SetParameter(pos, stages[stages.Count - 1].interval);
  	stages[stages.Count - 1].bard.transform.position = stages[stages.Count - 1].nextPos; //ステージの位置を画面に反映
		stages[stages.Count - 1].bard.transform.SetParent(GameObject.Find("Tree").transform); //親の設定
  }
  
	//オブジェクト生成
	private GameObject CreateObj(GameObject bard) {
		return (GameObject)Instantiate(bard, new Vector2(0, 0), Quaternion.identity);
	}

	//ゴール地点の生成
	private void SetGoalStage() {
		Stage stage = new Stage();
		stage.bard = goal;
		stages.Add(stage);
		//Destroy(stages[stages.Count - 1].bard.gameObject);
		//stages[stages.Count - 1].bard = goal;
		//stages.Add(stage); //ステージに追加
		stages[stages.Count - 1].interval = 1.5f;
		Vector2 pos = stages[stages.Count - 2].bard.transform.position;
  	pos.x += 5.0f;
  	pos.y += 0.0f;
    SetParameter(pos, stages[stages.Count - 1].interval);
  	stages[stages.Count - 1].bard.transform.position = stages[stages.Count - 1].nextPos; //ステージの位置を画面に反映
    //goal.transform.position = stages[stages.Count - 1].nextPos;
    stages[stages.Count - 1].isGoal = true;
	}

	//次のステージまでにかかる時間を設定
	private float GetNextInterval(int count) {
		float time = 0;
		switch (x[count]) {
			case 4: time = 1.0f; break;
			case 2: time = 0.5f; break;
			case 1: time = 0.25f; break;
		}

		return time;
	}

	//パラメータ設定
  //既知パラメータ : 頂点のx,y座標, 頂点までにかかる時間
  //未知パラメータ : 重力, 発射角度, 初速度
  private void SetParameter(Vector2 end, float time) {
  	Vector2 start = firstObj.transform.position; //スタート位置
  	if (stages.Count != 1) start = stages[stages.Count - 2].nextPos; //ひとつ前のステージがスタート位置
  	
  	//float gravity = 2 * (end.y - start.y - vy * time) / (time * time);  //重力
  	float gravity = 9.81f;
  	//発射角度
  	//float rad = (float)Mathf.Atan((vy * time + gravity * time * time) / (end.x - start.x));
  	float rad = (float)Mathf.Atan((end.y - start.y + (gravity * time * time / 2)) / (end.x - start.x));
  	//初速度
  	//float v0 = (vy + gravity * time) / Mathf.Sin(rad);
   	float v0 = (end.x - start.x) / (Mathf.Cos(rad) * time);

    Debug.Log((stages.Count - 1) + " x:"+ end.x +" y:"+ end.y +" g:" + gravity + " rad:"+ rad +" deg:" + rad * Mathf.Rad2Deg + " v0:" + v0);
    
  	//値の格納
  	stages[stages.Count - 1].nextRad = rad;
  	stages[stages.Count - 1].nextSpeed = v0;
  	stages[stages.Count - 1].nextPos = end;
  	stages[stages.Count - 1].nextGravity = gravity;
  }
  public void SetFirstBardParameter(float rad, float v0, float interval) {
  	firstStage.nextSpeed = v0;
  	firstStage.nextRad = rad;
  	firstStage.interval = interval;
  }

  //最初の位置と頂点の位置とかかる時間から発射角度と初速度を計算
  //返り値 : Vector2(発射角度(rad), 初速度)
  public static float[] GetParameter(Vector2 start, Vector2 end, float time) {
  	//float gravity = 9.81f;
  	//float rad = (float)Mathf.Atan((end.y - start.y + (gravity * time * time / 2)) / (end.x - start.x));
  	//float v0 = (end.x - start.x) / (Mathf.Cos(rad) * time);
  	float gravity = 2 * (end.y - start.y) / (time * time);  //重力
  	float rad = (float)Mathf.Atan(gravity * time * time / (end.x - start.x));
  	float v0 = gravity * time / Mathf.Sin(rad);

  	return new float[]{gravity, rad, v0};
  }

  //time秒後の位置を予測して返す
  public static Vector2 PredictPosition(float vo, Vector2 nowPos, float rad, float time) {
  	Vector2 predictPosition = new Vector2(0, 0);
  	float gravity = 9.81f;
  	predictPosition.x = nowPos.x + vo * Mathf.Cos(rad) * time;
  	predictPosition.y = nowPos.y + vo * Mathf.Sin(rad) * time - gravity * time * time / 2;
  	return predictPosition;
  }

  //色指定
  private void SetColor() {
  	if (!IsFirstBard()) { 
    	if (stages[stages.Count - 1].interval == 0.5f)	stages[stages.Count - 2].bard.transform.FindChild("Bard").GetComponent<SpriteRenderer>().sprite = blueBard;
	  	else if (stages[stages.Count - 1].interval == 0.25f) stages[stages.Count - 2].bard.transform.FindChild("Bard").GetComponent<Bard>().SetColor(252, 24, 255);
	  }
	  else {
    	if (stages[stages.Count - 1].interval == 0.5f)	firstObj.transform.FindChild("Bard").GetComponent<SpriteRenderer>().sprite = blueBard;
	  	else if (stages[stages.Count - 1].interval == 0.25f) firstObj.transform.FindChild("Bard").GetComponent<Bard>().SetColor(252, 24, 255);
	  }
  }

  public static void Init() {
  	for(int i = 0; i < stages.Count - 1; i++) {
      Destroy(stages[i].branch.gameObject);
    }
  }
  private bool IsFirstBard() {
  	if (stages.Count == 1)  return true;

  	return false;
  }
}
