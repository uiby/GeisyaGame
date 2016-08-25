using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 
//ゲーム全体に関する処理
public class GameManager : MonoBehaviour {
	private Egg egg;//タマゴ
	private bool isTap; //タップターゲット
	public float correctionValue; //左右の補正値

	void Start () {
		isTap = false;
		egg = GameObject.Find("Center").GetComponent<Egg>();
	}
	
	void Update () {
		TouchInfo info = TouchUtil.GetTouch();
		switch(info) {
			case TouchInfo.Began :
			  if (!isTap) {
			  	switch (egg.TouchAction()) {
			    	case "Nice" : 
  			    	BeforeJump();
			      	egg.SetVelocity();
			        AfterJump();
			  	  break;
			  	  case "Early" :
			  	    BeforeJump();
			  	    NearlyAction("Early");
			  	    if (CreateStage.stages[StageManager.nowStageCount].bard.GetComponent<Bard>().IsMirror()) {
			  	      egg.SetVelocity(correctionValue);
			  	      SetMoveBardAction(correctionValue);
			  	    }
			  	    else {
			  	    	  egg.SetVelocity(-correctionValue);
			  	        SetMoveBardAction(-correctionValue);
			  	    }
			  	    DamageEgg(20);
			  	    AfterJump();
			  	  break;
			  	  case "Late" :
			  	    BeforeJump();
			  	    NearlyAction("Late");
			  	    if (CreateStage.stages[StageManager.nowStageCount].bard.GetComponent<Bard>().IsMirror()) {
			  	      egg.SetVelocity(-correctionValue);
			  	      SetMoveBardAction(-correctionValue);
			  	    }
			  	    else {
			  	      egg.SetVelocity(correctionValue);
			  	      SetMoveBardAction(correctionValue);
			  	    }
			  	    DamageEgg(20);
			  	    AfterJump();
			  	  break;
			  	  case "None" : 
			  	    isTap = true;
			    	break;
			    }
			  } else {

			  }

			  /*if (egg.TouchAction() && !isTap) {
			  	isTap = false;
			  	BeforeJump();
			  	egg.SetVelocity();
			    AfterJump();
			  }
			  else {
			  	if (!isTap) 	SetNowBardAction();
			  	isTap = true;
			  }*/
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

	private void DamageEgg(float value) {
		GameObject.Find("MainCanvas/EggBar").GetComponent<EggBar>().Damage(value);
	}
	private void NearlyAction(string result) {
		if (StageManager.IsFirstStageNumber())  return;
		CreateStage.stages[StageManager.PrevStageNumber()].bard.GetComponent<Bard>().SetHeading(result);
	}

	//ジャンプ前(寸前)の処理
	private void BeforeJump() {
		SetNowBardAction();
	}

	//ジャンプの後の処理
	private void AfterJump() {
		//GameObject.Find("se").GetComponent<SE>().SEPlay();
		//SetNextBardAction();
		StageManager.AddNextStageCount();
		//StageManager.CreateTimingGauge();
		TimeTest.FinishTime();
		TimeTest.StartTime();
	}

  //鳥を移動状態に移行
  private void SetMoveBardAction(float correction) {
  	CreateStage.stages[StageManager.nowStageCount].bard.GetComponent<Bard>().SetMove(correction);
  }
  //鳥のアクション（溜める)を設定
	private void SetNextBardAction() {
		//次の鳥を溜める状態に移行
		CreateStage.stages[StageManager.nowStageCount].bard.GetComponent<Bard>().SetCharge(CreateStage.stages[StageManager.nowStageCount].interval);
	}
	//今の鳥のアクションを頭突き状態に移行
	private void SetNowBardAction() {
		if (StageManager.IsFirstStageNumber())  return;
		CreateStage.stages[StageManager.PrevStageNumber()].bard.GetComponent<Bard>().SetHeading();
	}

	public static void Init() {

	}
}
