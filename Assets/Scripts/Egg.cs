using UnityEngine;
using System.Collections;

//卵処理
public class Egg : MonoBehaviour {
	private Rigidbody2D rigidbody2D;
	public float jumpSpeed;
	public float jumpDiff;//左右にジャンプする時の垂直jumpからの差分
	public float LeftAndRightjumpSpeed;//左右jumpの速さ
	public bool isRad;
	private Vector2 fristPos;
	public GameObject nextPos;  //次の目標点
	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D>();
		Physics2D.gravity = new Vector2(0, -9.81f);
		fristPos = this.transform.position;
	}
	
	void Update () {
		TouchInfo info = TouchUtil.GetTouch();
		switch(info) {
			case TouchInfo.Began :
			  TouchAction();
			  //TouchUtil.SetStartPosition();
			  break;
			case TouchInfo.Ended :
			  //TouchUtil.SetEndPosition();
			  //FlickAction();
			  break;
		}
		if (Input.GetKeyDown(KeyCode.Q)) {
			rigidbody2D.velocity = Vector2.zero;
			transform.position = fristPos;
			StageManager.nowStageCount = 0;
			TimeTest.FinishTime();
		}
	}
	void FixedUpdate() {
		Vector2 speed = rigidbody2D.velocity;
		if (speed.y >= -0.3f && speed.y <= 0.3f && speed.y != 0) {
		  Debug.Log("頂点:" + transform.position +" 速度:"+ rigidbody2D.velocity);
		}
	}

  //touchアクション
	private void TouchAction() {
		Vector2 speed = rigidbody2D.velocity;
		if (speed.y < -0.5f || speed.y > 0.5f) return;
		if (speed.y >= -0.2f && speed.y <= 0.2f) {
			Debug.Log("Nice!");
			SetVelocity(CreateStage.stages[StageManager.nowStageCount].nextRad, CreateStage.stages[StageManager.nowStageCount].nextSpeed);
		  StageManager.AddNextStageCount();
		}
		else if (speed.y >= -0.5f && speed.y <= 0.5f) {
			Debug.Log("Good!");
		  SetVelocity(CreateStage.stages[StageManager.nowStageCount].nextRad, CreateStage.stages[StageManager.nowStageCount].nextSpeed);
		  StageManager.AddNextStageCount();
		}
		TimeTest.FinishTime();
		TimeTest.StartTime();
		//SetVelocity(new Vector2(0, 4), jumpSpeed);
	}
	//フリック操作によるアクション
	private void FlickAction() {
		switch (TouchUtil.GetDirection()) {
			case FlickInfo.RightUp : //右 
			  SetVelocity (90 - jumpDiff, LeftAndRightjumpSpeed);
			break;
			case FlickInfo.LeftUp :  //左
			  SetVelocity (90 + jumpDiff, LeftAndRightjumpSpeed);
			  break;
		}
	}

	/// 移動量を設定.rad : 角度(radian)
	//x,y平面の移動
  private void SetVelocity(float rad, float speed) {
    Vector2 v;
    v.x = Mathf.Cos(rad) * speed;
    v.y = Mathf.Sin(rad) * speed;
    rigidbody2D.velocity = v;
    Debug.Log("角度:"+ rad + "  初速度:" + speed);
  }
}
