﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//ステージ生成
public class CreateStage : MonoBehaviour {
	//ステージ
	public class Stage {
		public Vector2 timingPos;
		public GameObject obj;
		public float interval; //次のステージにかかる時間
		public float nextRad; //次のステージに対する発射角度
		public float nextSpeed; //次のステージに対する初速度
	}
	public GameObject firstObj; //最初のオブジェクト
	public static List<Stage> stages = new List<Stage>();
	private GameObject timingPointRight;
	private GameObject timingPointLeft;
	private bool isRight = true;
	public bool isRandom;
	public int[] musicInterval; //間隔
	public bool[] musicIsRightPos; //位置 右ならtrue, 左ならfalse

  ///後でちゃんとしたコードに書き直す///
	private int musicIntervalCount = 0;
	private int musiceIsRightPosCount = 0;
	///**********************///

	void Start () {
		timingPointRight = (GameObject)Resources.Load("TimingPointRight");
		timingPointLeft = (GameObject)Resources.Load("TimingPointLeft");

		int max = 20;
		if (musicInterval.Length == musicIsRightPos.Length && musicInterval.Length != 0) max = musicInterval.Length;
		if (isRandom) max = 20;
		for (int count = 0; count < max; count++) {
			isRight = RandomBool();
			GameObject temp ;
			if (isRight) temp = CreateObj(timingPointRight);
			else temp = CreateObj(timingPointLeft);
			Stage stage = new Stage();
			stage.obj = temp;
			stages.Add(stage); //ステージに追加
			stages[stages.Count - 1].interval = GetNextInterval();//時間の設定
			SetParameter(GetNextStageX(), stages[stages.Count - 1].interval);
			stages[stages.Count - 1].obj.transform.position = stages[stages.Count - 1].timingPos; //ステージの位置を画面に反映
			stages[stages.Count - 1].obj.transform.SetParent(GameObject.Find("Tree").transform); //親の設定
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//オブジェクト生成
	private GameObject CreateObj(GameObject obj) {
		return (GameObject)Instantiate(obj, new Vector2(0, 0), Quaternion.identity);
	}

	//RandomBool
	private bool RandomBool() {
		if (musicIsRightPos.Length == 0 || isRandom) return Random.Range(0, 2) == 0;

		return musicIsRightPos[musiceIsRightPosCount++];
	}

	//次のステージのx座標を設定
	private float GetNextStageX() {
		if (isRight) return 3; //左側に配置の際は,x座標3
		return 0;
	}
	//次のステージまでにかかる時間を設定
	private float GetNextInterval() {
		if (musicInterval.Length == 0 || isRandom) return 1.0f;

		float time = 0;
		Debug.Log(musicIntervalCount);
    switch (musicInterval[musicIntervalCount++]) {
    	case 1: time = 1.0f; break;
    	case 2: time = 0.5f; break;
    }

    return time;
	}

	//パラメータを設定
  //次に進む方向の場所とspeedから角度を算出して発射
  //既知パラメータ : 頂点のx座標, 頂点までにかかる時間
  //未知パラメータ : y, 発射角度, 初速度
  private void SetParameter(float end_x, float time) {
  	Vector2 start = firstObj.transform.position; //スタート位置
  	if (stages.Count != 1) start = stages[stages.Count - 2].timingPos; //ひとつ前のステージがスタート位置
  	float gravity = Mathf.Abs(Physics2D.gravity.y);  //重力

  	//頂点のy座標
  	float y = start.y + gravity * time * time / 2.0f;
  	//発射角度
  	float rad = (float)Mathf.Atan((gravity * time * time / (end_x - start.x)));
  	//初速度
  	float v0 = gravity * time / Mathf.Sin(rad);
  	Debug.Log((stages.Count - 1) +"y:" + y + "rad:"+ rad +"deg:" + rad * Mathf.Rad2Deg + " v0:" + v0);

  	//値の格納
  	stages[stages.Count - 1].nextRad = rad;
  	stages[stages.Count - 1].nextSpeed = v0;
  	stages[stages.Count - 1].timingPos = new Vector2(end_x, y);
  }
}
