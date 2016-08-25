using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//ステージ生成
public class CreateStage : MonoBehaviour {
	//ステージ
	public class Stage {
		public Vector2 nextPos;
		public GameObject bard;
		public float interval = 0; //次のステージにかかる時間
		public float nextRad = 0; //次のステージに対する発射角度
		public float nextSpeed = 0; //次のステージに対する初速度
		public float nextGravity = 0; //次の重力
	}
	public GameObject firstObj; //最初のオブジェクト
	public static List<Stage> stages;
	private GameObject timingPointLeft;
	private GameObject timingPointRight;
	private bool isRight = true;
	public bool isRandom;
	public bool isColorChange;
	public int[] musicInterval; //間隔
	public bool[] musicIsRightPos; //位置 右ならtrue, 左ならfalse
	public float vy; //次のステージにタマゴが通る時のy軸の速度. 0なら頂点

  ///後でちゃんとしたコードに書き直す///
	private int musicIntervalCount = 0;
	private int musiceIsRightPosCount = 0;
	///**********************///

	void Start () {
		stages = new List<Stage>();
		timingPointLeft = (GameObject)Resources.Load("TimingPointLeft");
		timingPointRight = (GameObject)Resources.Load("TimingPointRight");

		int max = 20;
		if (musicInterval.Length == musicIsRightPos.Length && musicInterval.Length != 0) max = musicInterval.Length;
		if (isRandom) max = 20;
		for (int count = 0; count < max; count++) {
			isRight = RandomBool();
			GameObject temp ;
			if (isRight) temp = CreateObj(timingPointRight);
			else temp = CreateObj(timingPointLeft);
			
			//ステージのパラメータ設定
		  SetStage(temp);
		  
		  if (isColorChange)  SetColor();//色指定
		}

		for (int i = 0; i < max; i++) {
			GameObject point = stages[i].bard;
			GameObject bard = point.transform.FindChild("Bard").gameObject;
			GameObject branch = point.transform.FindChild("Branch").gameObject;
			bard.transform.SetParent(branch.transform);
			branch.transform.parent = null;
      point.transform.SetParent(bard.transform);
      branch.transform.SetParent(GameObject.Find("Tree").transform);
      stages[i].bard = bard;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
  
  //ステージ生成
  private void SetStage(GameObject temp) {
		Stage stage = new Stage();
		stage.bard = temp;
		stages.Add(stage); //ステージに追加
		stages[stages.Count - 1].interval = GetNextInterval();//時間の設定

    //次のステージの位置の設定
		Vector2 pos = firstObj.transform.position; //今のステージ位置
  	if (stages.Count != 1)   pos = stages[stages.Count - 2].bard.transform.position;
		
		pos.y += GetNextY();
		pos.x = GetNextX();
		//Debug.Log(pos.y);
		SetParameter(pos, stages[stages.Count - 1].interval);
		stages[stages.Count - 1].bard.transform.position = stages[stages.Count - 1].nextPos; //ステージの位置を画面に反映
		stages[stages.Count - 1].bard.transform.SetParent(GameObject.Find("Tree").transform); //親の設定
	}

	//オブジェクト生成
	private GameObject CreateObj(GameObject bard) {
		return (GameObject)Instantiate(bard, new Vector2(0, 0), Quaternion.identity);
	}

	//RandomBool
	private bool RandomBool() {
		if (musicIsRightPos.Length == 0 || isRandom) return Random.Range(0, 2) == 0;

		return musicIsRightPos[musiceIsRightPosCount++];
	}

	//次のステージのy座標を設定
	///今のステージからどれくらい高いかを返り値として返す
	private float GetNextY() {
		float y = 0;
		if (stages[stages.Count - 1].interval == 1.0f)  y = 4;
		else if (stages[stages.Count - 1].interval == 0.5f)  y = 4;
		else if (stages[stages.Count - 1].interval == 0.25f) y = 4;
		return y;
	}

	//次のステージのx座標を設定
	private float GetNextX() {
		if (isRight) return 3; //左側に配置の際は,x座標3
		return 0;
	}
	//次のステージまでにかかる時間を設定
	private float GetNextInterval() {
		float time = 0;
		if (musicInterval.Length == 0 || isRandom) {
			switch (Random.Range(0, 3)) {
				case 0: time = 1.0f; break;
				case 1: time = 0.5f; break;
				case 2: time = 0.25f; break;
			}
			return time;
		}

		//Debug.Log(musicIntervalCount);
    switch (musicInterval[musicIntervalCount++]) {
    	case 1: time = 1.0f; break;
    	case 2: time = 0.5f; break;
    	case 3: time = 0.25f; break;
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

  //色指定
  private void SetColor() {
  	if (!IsFirstBard()) { 
    	if (stages[stages.Count - 1].interval == 0.5f)  stages[stages.Count - 2].bard.transform.FindChild("Bard").GetComponent<Bard>().SetColor(0, 255, 65);
	  	else if (stages[stages.Count - 1].interval == 0.25f) stages[stages.Count - 2].bard.transform.FindChild("Bard").GetComponent<Bard>().SetColor(252, 24, 255);
	  }
  }

  public static void Init() {
  	for(int i = 0; i < stages.Count; i++) {
      Destroy(stages[i].bard.gameObject);
    }
  }
  private bool IsFirstBard() {
  	if (stages.Count == 1)  return true;

  	return false;
  }

}
