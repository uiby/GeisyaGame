using UnityEngine;
using System.Collections;

//卵処理
public class Egg : MonoBehaviour {
	private Rigidbody2D rigidbody2D;
	public float jumpSpeed;
	public float jumpDiff;//左右にジャンプする時の垂直jumpからの差分
	public float leftAndRightjumpSpeed;//左右jumpの速さ
	public bool isRad;
	private Vector2 firstPos;
	public GameObject nextPos;  //次の目標点
	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D>();
		Physics2D.gravity = new Vector2(0, -9.81f);
		firstPos = this.transform.position;
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
			transform.position = firstPos;
			StageManager.nowStageCount = 0;
			TimeTest.FinishTime();
		}
	}
	void FixedUpdate() {
		float range = 1.0f;
		Vector2 speed = rigidbody2D.velocity;
		if (speed.y >= -range && speed.y <= range && speed.y != 0) {
		  Debug.Log("頂点:" + transform.position +" 速度:"+ rigidbody2D.velocity);
		}
	}

  //touchアクション
	private void TouchAction() {
		Vector2 speed = rigidbody2D.velocity;
		if (speed.y < -1.0f || speed.y > 1.0f) return;
		if (speed.y >= -0.2f && speed.y <= 0.2f) {
			Debug.Log("Nice!");
			SetVelocity();
			StageManager.AddNextStageCount();
		}
		else if (speed.y >= -0.5f && speed.y <= 0.5f) {
			Debug.Log("Good!");
		  SetVelocity();
		  StageManager.AddNextStageCount();
		}
		else if (speed.y >= -1.0f && speed.y <= 1.0f) {
			Debug.Log("Ok!");
		  SetVelocity();
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
			  SetVelocity (90 - jumpDiff, leftAndRightjumpSpeed);
			break;
			case FlickInfo.LeftUp :  //左
			  SetVelocity (90 + jumpDiff, leftAndRightjumpSpeed);
			  break;
		}
	}

	private void SetVelocity() {
		float rad = CreateStage.stages[StageManager.nowStageCount].nextRad;
		float speed = CreateStage.stages[StageManager.nowStageCount].nextSpeed;
		float gravity = CreateStage.stages[StageManager.nowStageCount].nextGravity;
		if (StageManager.isGravityVersion)  SetVelocity(rad, speed, gravity);
		else  SetVelocity(rad, speed);
	}
	/// 移動量を設定.rad : 角度(radian)
	//x,y平面の移動
  private void SetVelocity(float rad, float speed) {
    Vector2 v;
    v.x = Mathf.Cos(rad) * speed;
    v.y = Mathf.Sin(rad) * speed;
    rigidbody2D.velocity = v;
    //Debug.Log("角度:"+ rad + "  初速度:" + speed);
  }
  private void SetVelocity(float rad, float speed, float gravity) {
    Physics2D.gravity = new Vector2(0, -gravity);
    Vector2 v;
    v.x = Mathf.Cos(rad) * speed;
    v.y = Mathf.Sin(rad) * speed;
    rigidbody2D.velocity = v;
  }
}
