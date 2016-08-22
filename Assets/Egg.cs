using UnityEngine;
using System.Collections;

//卵処理
public class Egg : MonoBehaviour {
	private Rigidbody2D rigidbody2D;
	public float jumpSpeed;
	public float jumpDiff;//左右にジャンプする時の垂直jumpからの差分
	public float LeftAndRightjumpSpeed;//左右jumpの速さ
	public bool isRad;
	private bool isRightJump = false;
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
		if (speed.y >= -1.0f && speed.y <= 1.0f) {
		  Debug.Log("OK 速度:"+ speed + "  位置:" + this.transform.position);
		  //SetVelocity(90, jumpSpeed);

		  //左右jump
		  Debug.Log(nextPos.transform.position);
		  if (isRightJump) SetPos(nextPos.transform.position.x, 1.0f);
		  else SetPos(nextPos.transform.position.x, 1.0f);
		  isRightJump = !isRightJump;
		}
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

	/// 移動量を設定.direction : 角度(degree)
	//x,y平面の移動
  private void SetVelocity(float direction, float speed) {
    Vector2 v;
    v.x = Mathf.Cos(Mathf.Deg2Rad * direction) * speed;
    v.y = Mathf.Sin(Mathf.Deg2Rad * direction) * speed;
    rigidbody2D.velocity = v;
  }

  //移動量を設定
  //次に進む方向の場所とspeedから角度を算出して発射
  //既知パラメータ : 頂点のx座標, 頂点までにかかる時間
  //未知パラメータ : y, 発射角度, 初速度
  private void SetPos(float end_x, float time) {
  	Vector2 start = transform.position; //スタート位置
  	float gravity = Mathf.Abs(Physics2D.gravity.y);  //重力

  	//頂点のy座標
  	float y = start.y + gravity * time * time / 2.0f;
  	//発射角度
  	float rad = (float)Mathf.Atan((gravity * time * time / (end_x - start.x)));
  	//float rad = (float)Mathf.Asin(Mathf.Deg2Rad * ((2 * gravity * (end_x - start.x)) / (speed * speed))) / 2.0f;
  	//初速度
  	float v0 = gravity * time / Mathf.Sin(rad);
  	Debug.Log("y:" + y + "rad:"+ rad +"deg:" + rad * Mathf.Rad2Deg + " v0:" + v0);
  	SetVelocity(rad * Mathf.Rad2Deg, v0);
  }
  /*private void SetVelocity(Vector2 aimPos, float time , float direction) {
  	Vector2 start = transform.position; //スタート位置
  	Vector2 end = new Vector2(aimPos.x + (aimPos.x - start.x), start.y); //着地地点
  	float gravity = Mathf.Abs(Physics2D.gravity.y);  //重力
  	float vo = 0; //初速度

  	//ターゲットとの単位ベクトルを計測
		Vector2 distance = end - start;
		vo = Mathf.Sqrt(distance.x * gravity / Mathf.Sin(Mathf.Deg2Rad * 2 * direction)); //距離と角度から初速度を計算
		float vo2 = (time * gravity)/(2 * Mathf.Sin(Mathf.Deg2Rad * 2 * direction)); //時間と角度から初速度を計算
		Debug.Log("vo_x:" + vo +" vo_t:" + vo2);
		SetVelocity(direction, vo);
  }*/
  /*private void SetVelocity(Vector2 end, float speed) {
  	Vector2 start = transform.position;
  	float time = 1.5f;  //時間
  	float gravity = Physics2D.gravity.y;  //重力
  	float direction = 90; //角度
  	float Vo_t = (float)(time * gravity)/Mathf.sin(Mathf.Deg2Rad * direction);
  	Debug.Log("Vo_t : " + Vo_t);
  	float rad = (float)Mathf.Asin(Mathf.Deg2Rad * ((gravity * time) / speed));
  	float rad2 = (float)Mathf.Asin(Mathf.Deg2Rad * ((gravity * (end.x - start.x)) / (speed * speed))) / 2.0f;

  	Debug.Log("rad[t]:" + rad +" rad[x]:" + rad2);
  	Debug.Log("deg[t]:" + rad * Mathf.Rad2Deg +" deg[x]:" + rad2 * Mathf.Rad2Deg);
  	Debug.Log(((gravity * time) / speed) +":"+ (gravity * (end.x - start.x)) / (speed * speed));
  	if (isRad) SetVelocity(rad2 * Mathf.Rad2Deg, speed);
  	else SetVelocity(rad * Mathf.Rad2Deg, speed);
  }*/
}
