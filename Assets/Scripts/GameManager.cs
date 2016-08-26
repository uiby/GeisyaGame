using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 
//ゲーム全体に関する処理
public class GameManager : MonoBehaviour {
	private Egg egg;//タマゴ
	private bool isTap; //タップターゲット
	public float correctionValue; //左右の補正値
	public float presetJumpTimer; //事前のタイミングタイマー

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
  			    	egg.SetVelocity();
			      	IdealBardAction();
			      break;
			  	  case "Early" :
  			  	  EarlyBardAction();
			  	    /*if (CreateStage.stages[StageManager.nowStageCount].bard.GetComponent<Bard>().IsMirror()) {
			  	      egg.SetVelocity(correctionValue);
			  	      SetMoveBardAction(correctionValue);
			  	    }
			  	    else {
			  	    	  egg.SetVelocity(-correctionValue);
			  	        SetMoveBardAction(-correctionValue);
			  	    }
			  	    JumpMoment();*/
			  	  break;
			  	  case "Late" :
			  	    LateBardAction();
			  	    /*if (CreateStage.stages[StageManager.nowStageCount].bard.GetComponent<Bard>().IsMirror()) {
			  	      egg.SetVelocity(-correctionValue);
			  	      SetMoveBardAction(-correctionValue);
			  	    }
			  	    else {
			  	      egg.SetVelocity(correctionValue);
			  	      SetMoveBardAction(correctionValue);
			  	    }
			  	    JumpMoment();*/
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
			    JumpMoment();
			  }
			  else {
			  	if (!isTap) 	SetIdealTimeAction();
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

	
	//earlyまたはlateの時の鳥のアクション
	private void NearlyAction(string result) {
		if (StageManager.IsFirstStageNumber())  return;
		CreateStage.stages[StageManager.PrevStageNumber()].bard.GetComponent<Bard>().SetHeading(result);
	}

	//ジャンプ前(寸前)の処理
	private void BeforeJump() {
		SetIdealTimeAction();
	}

	//ジャンプの瞬間
	private void JumpMoment() {
		//GameObject.Find("se").GetComponent<SE>().SEPlay();
		//SetNextBardAction();
		StageManager.AddNextStageCount();
		//StageManager.CreateTimingGauge();
		TimeTest.FinishTime();
		TimeTest.StartTime();
	}

	//理想のタイミング時のアクション
	private void IdealBardAction() {
		SetIdealTimeAction();
		JumpMoment();
	}

	//早いタイミング時のアクション
	private void EarlyBardAction() {
		if (CreateStage.stages[StageManager.nowStageCount].bard.GetComponent<Bard>().IsMirror()) {
	    egg.PresetJumpTimer(presetJumpTimer/2 , correctionValue);
		  SetMoveBardAction(correctionValue);
		}
		else {
			egg.PresetJumpTimer(presetJumpTimer/2 , -correctionValue);
		  SetMoveBardAction(-correctionValue);
		}
		//presetJumpTimer/2秒後の位置を予測してアクションを行う
		
		//最初のタイミングジャンプかどうか判断
		Vector2 predictPosition;
		if (StageManager.nowStageCount == 1) {
			predictPosition = CreateStage.PredictPosition(
			GameObject.Find("StageManager").GetComponent<CreateStage>().firstStage.nextSpeed,
			egg.transform.root.gameObject.transform.position, 
			GameObject.Find("StageManager").GetComponent<CreateStage>().firstStage.nextRad,
			presetJumpTimer/2);
		} else {
		  predictPosition = CreateStage.PredictPosition(
			CreateStage.stages[StageManager.PrevStageNumber()].nextSpeed,
			egg.transform.root.gameObject.transform.position, 
			CreateStage.stages[StageManager.PrevStageNumber()].nextRad,
			presetJumpTimer/2);
	  }
		Debug.Log("early pos:"+ predictPosition +"egg:" + GameObject.Find("Center").transform.position + "bard:" + CreateStage.stages[StageManager.PrevStageNumber()].bard.transform.position);
		float[] parameter = CreateStage.GetParameter(
			CreateStage.stages[StageManager.PrevStageNumber()].bard.transform.position, 
			predictPosition, 
			presetJumpTimer/2);
		CreateStage.stages[StageManager.PrevStageNumber()].bard.GetComponent<Bard>().EarlyAction(parameter);
		JumpMoment();
	}

	//遅いタイミング時のアクション
	private void LateBardAction() {
		if (CreateStage.stages[StageManager.nowStageCount].bard.GetComponent<Bard>().IsMirror()) {
			egg.PresetJumpTimer(presetJumpTimer , -correctionValue);
      SetMoveBardAction(-correctionValue);
    }
    else {
    	egg.PresetJumpTimer(presetJumpTimer , correctionValue);
		  SetMoveBardAction(correctionValue);
    }
		
		//最初のタイミングジャンプかどうか判断
		Vector2 predictPosition;
		if (StageManager.nowStageCount == 1) {
			predictPosition = CreateStage.PredictPosition(
			GameObject.Find("StageManager").GetComponent<CreateStage>().firstStage.nextSpeed,
			egg.transform.root.gameObject.transform.position, 
			GameObject.Find("StageManager").GetComponent<CreateStage>().firstStage.nextRad,
			presetJumpTimer);
		} else {
			predictPosition = CreateStage.PredictPosition(
			CreateStage.stages[StageManager.PrevStageNumber()].nextSpeed,
			egg.transform.root.gameObject.transform.position, 
			CreateStage.stages[StageManager.PrevStageNumber()].nextRad,
			presetJumpTimer);
		}
		Debug.Log("late pos:"+ predictPosition +"egg:" + GameObject.Find("Center").transform.position);
		CreateStage.stages[StageManager.PrevStageNumber()].bard.GetComponent<Bard>().LateAction(predictPosition);
    JumpMoment();
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
	private void SetIdealTimeAction() {
		if (StageManager.IsFirstStageNumber())  return;
		CreateStage.stages[StageManager.PrevStageNumber()].bard.GetComponent<Bard>().SetHeading();
	}

	public static void Init() {

	}
}
