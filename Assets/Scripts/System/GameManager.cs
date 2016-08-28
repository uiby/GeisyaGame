﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 
//ゲーム全体に関する処理
public class GameManager : MonoBehaviour {
	private Egg egg;//タマゴ
	private bool isTap; //タップターゲット
	public float correctionValue; //左右の補正値
	public float presetJumpTimer; //事前のタイミングタイマー
	public static GameState gameState;
	public static int StageNumber = 0; //ステージの種類
	public TitleManager titleManager;
	
	void Start () {
		isTap = false;
		egg = GameObject.Find("Center").GetComponent<Egg>();
		gameState = GameState.Title;
		titleManager = GameObject.Find("TitleCanvas").GetComponent<TitleManager>();
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Q)) {
			CreateStage.Init();
      SceneManager.LoadScene ("Main");
		  //egg.initPos();
		}

		switch (gameState) {
			case GameState.Title: Title(); break;
      case GameState.Play: Play(); break;
			case GameState.GameOver: break;
			case GameState.GameClear: break;
		}
		
	}

	private void Title() {
		titleManager.ChoseStage();
	}

	private void Play() {
		TouchInfo info = TouchUtil.GetTouch();
		switch(info) {
			case TouchInfo.Began :
			  switch (egg.TouchAction()) {
			  	case "Nice" : 
			  	  if (!StageManager.IsFirstStageNumber()) {
			  	    ShowTapResult("Nice", CreateStage.stages[StageManager.PrevStageNumber()].bard);
    			  }
  			  	egg.SetVelocity();
			    	IdealBardAction();
			    	if (IsClear()) {
			    		GameClear();
			    	}
			    break;
			    case "Early" :
			    	ShowTapResult("Early", CreateStage.stages[StageManager.PrevStageNumber()].bard);
  				  EarlyBardAction();
			    	if (IsClear()) {
			    		GameClear();
			    	}
			    break;
			    case "Late" :
			    	ShowTapResult("Late", CreateStage.stages[StageManager.PrevStageNumber()].bard);
			      LateBardAction();
			    	if (IsClear()) {
			    		GameClear();
			    	}
			    break;
			    case "VeryEarly" : 
  			    VeryEarlyAction();
			   	break;
			  	case "VeryLate" :
			  	  VeryLateAction();
			  	break;
			  }
			  break;
			case TouchInfo.Ended :
			  break;
		}
	}

	private bool IsClear() {
		//Debug.Log(StageManager.nowStageCount);
		if (StageManager.nowStageCount == CreateStage.stages.Count)
		  return true;
		return false;
	}
	
	private void GameOver() {
		Debug.Log("GameOver.");
	}

  private void GameClear() {
  	Debug.Log("GameClear.");
  	gameState = GameState.GameClear;
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
		egg.PresetJumpTimer(presetJumpTimer/2 , correctionValue);
		
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
		egg.PresetJumpTimer(presetJumpTimer , -correctionValue);
    
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
		//Debug.Log("late pos:"+ predictPosition +"egg:" + GameObject.Find("Center").transform.position);
		CreateStage.stages[StageManager.PrevStageNumber()].bard.GetComponent<Bard>().LateAction(predictPosition);
    JumpMoment();
	}

	private void VeryEarlyAction() {
		gameState = GameState.GameOver;
		GameObject.Find("GameOver").GetComponent<GameOver>().ShowCanvas();
	  if (StageManager.IsFirstStageNumber())  return;
		CreateStage.stages[StageManager.PrevStageNumber()].bard.GetComponent<Bard>().VeryEarlyAction();
	}

	//何もタップしなかった場合、自動的にgameoverの処理
	public void ChangeVeryLateAction() {
		gameState = GameState.GameOver;
		GameObject.Find("GameOver").GetComponent<GameOver>().ShowCanvas();
		if (StageManager.IsFirstStageNumber())  return;
		CreateStage.stages[StageManager.PrevStageNumber()].bard.GetComponent<Bard>().VeryLateAction();
	}

	private void VeryLateAction() {
		gameState = GameState.GameOver;
		GameObject.Find("GameOver").GetComponent<GameOver>().ShowCanvas();
		if (StageManager.IsFirstStageNumber())  return;
		CreateStage.stages[StageManager.PrevStageNumber()].bard.GetComponent<Bard>().VeryLateAction();
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

	//Game情報
  public enum GameState {
	  None = 99,
	  Title = 0,
	  Play = 1,
	  GameOver = 2,
	  GameClear = 3,
  }

  //タップ結果を表示
  //target : targetを中心に文字が回る
  public static void ShowTapResult(string result, GameObject target) {
  	GameObject word;
  	GameObject temp;
  	Vector2 pos = new Vector2(target.transform.position.x - 2, target.transform.position.y + 1);
  	SE.CryBard(result);
  	switch (result) {
  		case "Early" :  
  		  word = (GameObject)Resources.Load("UI/EarlyWord");
  		  temp = (GameObject)Instantiate(word, pos, Quaternion.identity);
  		  temp.GetComponent<TapResultWord>().PlayEffect(0, 241, 251);
  		  ComboSystem.ResetCombo();
  		break;
  		case "Nice" :
  		  word = (GameObject)Resources.Load("UI/NiceWord");  
  		  temp = (GameObject)Instantiate(word, pos, Quaternion.identity);
  		  temp.GetComponent<TapResultWord>().PlayEffect(0, 255, 20);
  		  ComboSystem.AddCombo();
  		break;
  		case "Late" :  
  		  word = (GameObject)Resources.Load("UI/LateWord");
  		  temp = (GameObject)Instantiate(word, pos, Quaternion.identity);
  		  temp.GetComponent<TapResultWord>().PlayEffect(251, 109, 0);
  		  ComboSystem.ResetCombo();
  		break;
  	}
    ScoreManager.AddScore(result);
  }

  public void Retry() {
  	CreateStage.Init();
  	ComboSystem.Init();
  	ScoreManager.Init();
  	GameObject.Find("GameOver").GetComponent<GameOver>().HideCanvas();
  	GameObject.Find("GameClear").GetComponent<GameClear>().HideCanvas();
  	GameObject.Find("StageManager").GetComponent<CreateStage>().MakeStage();
  	egg.InitPos();
  	gameState = GameState.Play;
    //SceneManager.LoadScene ("Main");
  }
  public void Home() {
  	CreateStage.Init();
    SceneManager.LoadScene ("Main");	
  }
}