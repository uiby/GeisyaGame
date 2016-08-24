using UnityEngine;
using System.Collections;

//鳥の処理
public class Bard : MonoBehaviour {
	private float timer = 0;
	private float maxTimer;
  private float nextBardTimer;
  private Vector3 rotate;
  private float maxAngle; //最大回転角度(z軸に対する)
  private float interval;//フレーム間の時間
  private float angleAmount; //フレーム間の角度
  public float maxHeadUpAngle;
  //鳥動作情報
  public enum eState {
  	None = 99, //通常状態
  	//力を溜める
  	Charge = 1,
  	//頭突き中
  	Heading = 2,
  }
  public eState state = eState.None;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
			case eState.Charge :
			  interval = Time.deltaTime;
			  timer -= interval;
   			//Debug.Log("ok:" + interval/maxAngle);
   			angleAmount = (interval/maxTimer) * maxAngle;
			  if (timer >= 0)  Roll(-angleAmount);
			break;
			case eState.Heading : 
			  interval = Time.deltaTime;
			  timer -= interval;
   			//Debug.Log("ok:" + interval/maxAngle);
   			angleAmount = (interval/maxTimer) * maxAngle;
			  if (timer >= 0)  Roll(angleAmount);
			  else state = eState.None;
			break;
		}
	}

	//チャージ状態に移行 : time...インターバル
	public void SetCharge(float time) {
		SetInterval(time);
		nextBardTimer = time;
		maxAngle = 30;
		state = eState.Charge;
	}
	//頭突き状態に移行
	public void SetHeading() {
		SetInterval(0.075f);
		if (IsMirror()) {
			maxAngle = Mathf.Abs(-NowRotationZ() - maxHeadUpAngle);
		}
		else {
			maxAngle = Mathf.Abs(-(360 - NowRotationZ()) - maxHeadUpAngle);
		}
		//Debug.Log("maxAngle:" + maxAngle + "z:" + NowRotationZ());
    state = eState.Heading;
	}

	//インターバルの設定
	private void SetInterval(float time) {
		timer = time;
		maxTimer = time;
	}

	private void Roll(float amount) {
		if (IsMirror())
			this.transform.Rotate(0, 0, -amount);
		else this.transform.Rotate(0, 0, amount);
	}
	private void Roll(Vector3 roll) {
		this.transform.rotation = Quaternion.Euler(roll);
	}
	private float NowRotationZ() {
		return this.transform.localEulerAngles.z;
	}
	private bool IsMirror() {
		if (this.GetComponent<SpriteRenderer>().flipX) return true;
		return false;
	}
	/// 色設定.
  public void SetColor (float r, float g, float b)
  {
    Color c = this.GetComponent<SpriteRenderer>().color;
    c.r = r / 255;
    c.g = g / 255;
    c.b = b / 255;
    this.GetComponent<SpriteRenderer>().color = c;
  }
}
