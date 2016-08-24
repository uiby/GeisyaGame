using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 
//ゲーム全体に関する処理
public class GameManager : MonoBehaviour {
	private Egg egg;//タマゴ
	private bool isTap; //タップターゲット

	void Start () {
		isTap = false;
		egg = GameObject.Find("Center").GetComponent<Egg>();
	}
	
	void Update () {
		TouchInfo info = TouchUtil.GetTouch();
		switch(info) {
			case TouchInfo.Began :
			  if (egg.TouchAction() && !isTap) {
			  	isTap = false;
			  	BeforeJump();
			  	egg.SetVelocity();
			    AfterJump();
			  }
			  else {
			  	if (!isTap) 	SetNowBardAction();
			  	isTap = true;
			  }
			  //TouchUtil.SetStartPosition();
			  break;
			case TouchInfo.Ended :
			  //TouchUtil.SetEndPosition();
			  //FlickAction();
			  break;
		}
		if (Input.GetKeyDown(KeyCode.Q)) {
			CreateStage.Init();
      SceneManager.LoadScene ("Main");
		  //egg.initPos();
		}
	}

	//ジャンプ前(寸前)の処理
	private void BeforeJump() {
		SetNowBardAction();
	}

	//ジャンプの後の処理
	private void AfterJump() {
		//GameObject.Find("se").GetComponent<SE>().SEPlay();
		SetNextBardAction();
		StageManager.AddNextStageCount();
		StageManager.CreateTimingGauge();
		TimeTest.FinishTime();
		TimeTest.StartTime();
	}

  //鳥のアクション（溜める)を設定
	private void SetNextBardAction() {
		//次の鳥を溜める状態に移行
		CreateStage.stages[StageManager.nowStageCount].obj.transform.FindChild("Bard").GetComponent<Bard>().SetCharge(CreateStage.stages[StageManager.nowStageCount].interval);
	}
	//今の鳥のアクションを頭突き状態に移行
	private void SetNowBardAction() {
		if (StageManager.isFirstStageNumber())  return;
		CreateStage.stages[StageManager.PrevStageNumber()].obj.transform.FindChild("Bard").GetComponent<Bard>().SetHeading();
	}

	public static void Init() {

	}
}
