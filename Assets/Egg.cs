using UnityEngine;
using System.Collections;

//卵処理
public class Egg : MonoBehaviour {
	private Rigidbody2D rigidbody2D;
	public float jumpSpeed;
	public float jumpDiff;//左右にジャンプする時の垂直jumpからの差分
	public float LeftAndRightjumpSpeed;//左右jumpの速さ
	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D>();
		Physics2D.gravity = new Vector2(0, -9.81f * 3);
	}
	
	void Update () {
		TouchInfo info = TouchUtil.GetTouch();
		switch(info) {
			case TouchInfo.Began :
			  TouchUtil.SetStartPosition();
			  break;
			case TouchInfo.Ended :
			  TouchUtil.SetEndPosition();
			  FlickAction();
			  break;
		}
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

	/// 移動量を設定.direction : 角度
	//x,y平面の移動
  private void SetVelocity(float direction, float speed) {
    Vector2 v;
    v.x = Mathf.Cos(Mathf.Deg2Rad * direction) * speed;
    v.y = Mathf.Sin(Mathf.Deg2Rad * direction) * speed;
    rigidbody2D.velocity = v;
  }
}
