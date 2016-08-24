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
	public GameObject center;
	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D>();
		Physics2D.gravity = new Vector2(0, -9.81f);
		firstPos = this.transform.position;
	}
	
	void Update () {
		
	}
	void FixedUpdate() {
		/*float range = 1.0f;
		Vector2 speed = rigidbody2D.velocity;
		if (speed.y >= -range && speed.y <= range && speed.y != 0) {
		  Debug.Log("頂点:" + transform.position +" 速度:"+ rigidbody2D.velocity);
		}*/
	}

	//初期位置に戻す
	public void initPos() {
		rigidbody2D.velocity = Vector2.zero;
		transform.position = firstPos;
		StageManager.nowStageCount = 0;
		TimeTest.FinishTime();
	}

  //touchアクション 成功だとtrue
	public bool TouchAction() {
		Vector2 speed = rigidbody2D.velocity;
		float timingVy = TimingVy();
		if (speed.y < -1.0f + timingVy || speed.y > 1.0f - timingVy) return false;
		
		if (speed.y >= -0.2f + timingVy && speed.y <= 0.2f - timingVy) {
			Debug.Log("Nice!");
		}
		else if (speed.y >= -0.5f + timingVy && speed.y <= 0.5f - timingVy) {
			Debug.Log("Good!");
		}
		else if (speed.y >= -1.0f + timingVy && speed.y <= 1.0f - timingVy) {
			Debug.Log("Ok!");
		}
		
		if (!StageManager.isGravityVersion)  CompensatePosition();

		return true;
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

	public void SetVelocity() {
		float rad = CreateStage.stages[StageManager.nowStageCount].nextRad;
		float speed = CreateStage.stages[StageManager.nowStageCount].nextSpeed;
		float gravity = CreateStage.stages[StageManager.nowStageCount].nextGravity;
		Vector2 pos = CreateStage.stages[StageManager.nowStageCount].nextPos;
		float interval = CreateStage.stages[StageManager.nowStageCount].interval;
		//Debug.Log("pos:" + pos);
		if (StageManager.isGravityVersion)  SetVelocity(pos, interval);
		else  SetVelocity(rad, speed);
	}
	/// 移動量を設定.rad : 角度(radian)
	//y軸可変移動
  private void SetVelocity(float rad, float speed) {
    Vector2 v;
    v.x = Mathf.Cos(rad) * speed;
    v.y = Mathf.Sin(rad) * speed;
    rigidbody2D.velocity = v;
    //Debug.Log("角度:"+ rad + "  初速度:" + speed);
  }
  //重力可変移動
  private void SetVelocity(float rad, float speed, float gravity) {
    Physics2D.gravity = new Vector2(0, -gravity);
    Vector2 v;
    v.x = Mathf.Cos(rad) * speed;
    v.y = Mathf.Sin(rad) * speed;
    rigidbody2D.velocity = v;
  }

  //タップにより位置がずれた時の補正
  private void CompensatePosition() {
  	Vector2 pos = new Vector2(0, 0);
  	if (StageManager.nowStageCount == 0)  pos = GameObject.Find("StageManager").GetComponent<CreateStage>().firstObj.transform.position;
  	else  {
  		pos = CreateStage.stages[StageManager.nowStageCount - 1].obj.transform.position;
  	  Debug.Log(CreateStage.stages[StageManager.nowStageCount - 1].obj.transform.position);
  	}
  	center.transform.position = pos;
  }

  //タマゴがタイミングタップされた後その都度計算して移動
  private void SetVelocity(Vector2 end, float time) {

  	//Vector2 start = new Vector2(0, 0);
  	Vector2 start = this.transform.position;
  	//if (CreateStage.stages.Count == 1) start = GameObject.Find("StageManager").GetComponent<CreateStage>().firstObj.transform.position; //スタート位置
  	//if (CreateStage.stages.Count != 1) start = CreateStage.stages[CreateStage.stages.Count - 1].nextPos; //ひとつ前のステージがスタート位置
  	
  	float gravity = 2 * (end.y - start.y - TimingVy() * time) / (time * time);  //重力
  	//発射角度
  	float rad = (float)Mathf.Atan((TimingVy() * time + gravity * time * time) / (end.x - start.x));
  	//初速度
  	float v0 = (TimingVy() + gravity * time) / Mathf.Sin(rad);

  	SetVelocity(rad, v0, gravity);
  }

  //ベストタイミング時のy軸の速度
  private float TimingVy() {
  	return GameObject.Find("StageManager").GetComponent<CreateStage>().vy;
  }
}
