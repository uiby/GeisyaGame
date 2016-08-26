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
  public float correctionValue; //左右の補正値

  private float moveX;
  private int lateState;
  private float moveAmount; //移動量
  private Vector2 latePos; //遅れた時の接触予定位置
  private float lateRad;

  //鳥動作情報
  public enum eState {
  	None = 99, //通常状態
  	//力を溜める
  	Charge = 1,
  	//頭突き中
  	Heading = 2,
  	//移動中
  	Moved = 3,
  	//Early : 早い場合の動作
  	Early = 4,
  	//Late : 遅い場合の動作
  	Late = 5,
  }
  public eState state = eState.None;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
			case eState.Moved :
			  Vector2 pos = this.transform.position;
			  float add = moveX - moveX * 0.9f;
			  moveX *= 0.9f;
			  pos.x += add;
			  this.transform.position = pos;
			  if (Mathf.Abs(moveX) <= 0.01f)  state = eState.None;
			break;
			case eState.Heading :
			  interval -= 1;
			  RollZ(maxAngle/5);
			  if (interval <= 0)  state = eState.None;
			  /*angleAmount = maxAngle - maxAngle * 0.9f;
			  maxAngle *= 0.9f;
			  RollZ(angleAmount);
			  if (Mathf.Abs(maxAngle) < 0.01f) state = eState.None;*/
			  break;
			case eState.Late :
			  Vector2 pos1 = this.transform.position;
			  switch (lateState) {
			  	case 0: //離れる
			  	  pos1.x -= Mathf.Cos(Mathf.Deg2Rad * 60) * 0.2f;
	          pos1.y -= Mathf.Sin(Mathf.Deg2Rad * 60) * 0.2f;
	          this.transform.position = pos1;
	          
	          moveAmount -= 1;
	          if (moveAmount < 0) {
	            moveAmount = 6;
	            lateState = 1;
	          }
			  	break;
			  	case 1: //振り返る
  			  	moveAmount -= 1;
  			  	RollY(180/6);
  			  	if (moveAmount <= 0) {
  			  		interval = 6;
  			  		moveAmount = Vector2.Distance(this.transform.position, latePos);
  			  		lateRad = GetAim(this.transform.position, latePos);
  			  		lateState = 2;
  			  	}
			  	break;
			  	case 2: //近づく
			  	  pos1.x += Mathf.Cos(lateRad) * (moveAmount / 6);
	          pos1.y += Mathf.Sin(lateRad) * (moveAmount / 6);
	          this.transform.position = pos1;
	          
	          interval -= 1;
	          if (interval <= 0) {
	          	lateState = 0;
	            state = eState.None;
	          }
			  	  /*pos1.x += Mathf.Cos(lateRad) * (moveAmount - moveAmount * 0.9f);
	          pos1.y += Mathf.Sin(lateRad) * (moveAmount - moveAmount * 0.9f);
	          this.transform.position = pos1;
	          moveAmount *= 0.9f;
	          if (moveAmount < 0.01f) {
	            lateState = 0;
	            state = eState.None;
	          }*/
			  	break;
			  }
			break;
		}
		/*switch (state) {
			case eState.Charge :
			  interval = Time.deltaTime;
			  timer -= interval;
   			//Debug.Log("ok:" + interval/maxAngle);
   			angleAmount = (interval/maxTimer) * maxAngle;
			  if (timer >= 0)  RollZ(-angleAmount);
			break;
		}*/
	}

	//誤差があった場合誤差に合わせて移動
	public void SetMove(float addX) {
		moveX = addX;
		state = eState.Moved;
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
		/*	switch (state) {
				case "" : break;
			}*/
		interval = 5;
		if (IsMirror()) {
			maxAngle = - maxHeadUpAngle;
		}
		else {
			maxAngle = maxHeadUpAngle;
		}
		//Debug.Log("maxAngle:" + maxAngle + "z:" + NowRotationZ());
    state = eState.Heading;
	}
	public void SetHeading(string result) {
		bool isMirror = IsMirror();
		switch (result) {
				case "Early" : 
				if (isMirror)	SetAnimatorTrigger("LeftCatch");
				else  SetAnimatorTrigger("RightCatch");
				break;
				case "Late" : 
				if (isMirror)	SetAnimatorTrigger("RightCatch");
				else  SetAnimatorTrigger("LeftCatch");
				break;
		}
	}

	public bool IsMirror() {
		if (this.GetComponent<SpriteRenderer>().flipX) return true;
		return false;
	}

	private void SetAnimatorTrigger(string name) {
		this.GetComponent<Animator>().SetTrigger(name);
	}

	//インターバルの設定
	private void SetInterval(float time) {
		timer = time;
		maxTimer = time;
	}

	private void RollY(float amount) {
		this.transform.Rotate(0, amount, 0);
	}
	private void RollZ(float amount) {
		//if (IsMirror())
			this.transform.Rotate(0, 0, amount);
		//else this.transform.Rotate(0, 0, amount);
	}
	private void Roll(Vector3 roll) {
		this.transform.rotation = Quaternion.Euler(roll);
	}
	private float NowRotationZ() {
		return this.transform.localEulerAngles.z;
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

  public void SetVelocity(float rad, float speed) {
  	Debug.Log(" rad:"+ rad +" deg:" + rad * Mathf.Rad2Deg + " v0:" + speed);
    Vector2 v;
    v.x = Mathf.Cos(rad) * speed;
    v.y = Mathf.Sin(rad) * speed;
    this.GetComponent<Rigidbody2D>().velocity = v;
  }
  //重力可変移動
  private void SetVelocity(float rad, float speed, float gravity) {
  	//Debug.Log(" g:" + gravity + " rad:"+ rad +" deg:" + rad * Mathf.Rad2Deg + " v0:" + speed);
  	float gravityScale = gravity / 9.81f;
    Vector2 v;
    v.x = Mathf.Cos(rad) * speed;
    v.y = Mathf.Sin(rad) * speed;
    this.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
    this.GetComponent<Rigidbody2D>().velocity = v;
  }

  //早かった時のアクション
  public void EarlyAction(float[] parameter) {
  	float gravity = parameter[0];
  	float rad = parameter[1];
  	float v0 = parameter[2];
  	this.gameObject.AddComponent<Rigidbody2D>();
  	SetVelocity(rad, v0, gravity);
  }

  //遅かった時のアクション
  public void LateAction(Vector2 pos) {
  	state = eState.Late;
  	lateState = 0;
  	moveAmount = 5;
  	latePos = pos;
  }

  //2点間の角度をradで返す
  private float GetAim(Vector2 start, Vector2 end) {
    float dx = end.x - start.x;
    float dy = end.y - start.y;
    float rad = Mathf.Atan2(dy, dx);
    return rad;
  }
}
