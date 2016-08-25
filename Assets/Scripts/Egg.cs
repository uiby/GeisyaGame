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
	public float hp;
	private string test;
	private string result;
	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D>();
		Physics2D.gravity = new Vector2(0, -9.81f);
		firstPos = this.transform.position;
	}
	
	void Update () {
		//test = result;
		result = TouchAction();
		//if (test == "None" && result == "Early" && IdealTime() == 0.25f) Time.timeScale = 0;
	}
	void FixedUpdate() {
		//if (result != "None") {
		//	Debug.Log(result +": now:"+ TimeTest.GetCurrentTime());
		//}
	}

	//初期位置に戻す
	public void initPos() {
		rigidbody2D.velocity = Vector2.zero;
		transform.position = firstPos;
		StageManager.nowStageCount = 0;
		TimeTest.FinishTime();
	}

  //touchアクション 成功だとtrue
	/*public bool TouchAction() {
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

		return true;
	}*/
	//touchアクション
	//返り値 : Nice ... 誤差±0.1
	//        Early(早い) ... 誤差-0.5
	//        Late(遅い) ... 誤差+0.5
	//        None(論外) ... ±0.5より大きい
	public string TouchAction() {
		if (StageManager.IsFirstStageNumber()) return "Nice";
		Vector2 speed = rigidbody2D.velocity;
		float idealTime = IdealTime();
		float currentTime = TimeTest.GetCurrentTime();
		float rate = 0.1f;
		if (idealTime == 1.0f)  rate = 0.1f;
		else if (idealTime == 0.5f)  rate = 0.15f;
		else if (idealTime == 0.25f)  rate = 0.2f;
		//if (currentTime < -0.5f + idealTime || currentTime > 0.5f - idealTime) return "None";
		
		if (currentTime >= idealTime * (1 - rate) && currentTime <= idealTime * (1 + rate)) {
			Debug.Log("Nice:" + currentTime +"  frame:" + Time.deltaTime);
			return "Nice";
		}
		else if (currentTime >= idealTime * (1 - rate * 2.0f) && currentTime < idealTime * (1 - rate)) {
			Debug.Log("Early:" + currentTime +"  frame:" + Time.deltaTime);
			return "Early";
		}
		else if (currentTime >= idealTime * (1 + rate) && currentTime < idealTime * (1 + rate * 2.0f)) {
			Debug.Log("Late:" + currentTime +"  frame:" + Time.deltaTime);
			return "Late";
		}
		else  return "None";


	}
	//フリック操作によるアクション
	private void FlickAction() {
		switch (TouchUtil.GetDirection()) {
			case FlickInfo.RightUp : //右 
			break;
			case FlickInfo.LeftUp :  //左
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
		SetVelocity(pos, interval);
	}
	//移動
	// correction : 補正値
	public void SetVelocity(float correction) {
		Vector2 pos = CreateStage.stages[StageManager.nowStageCount].nextPos;
		pos.x += correction;
		CreateStage.stages[StageManager.nowStageCount].nextPos = pos;
		float interval = CreateStage.stages[StageManager.nowStageCount].interval;
		SetVelocity(pos, interval);
	}
	
	//重力可変移動
  private void SetVelocity(float rad, float speed, float gravity) {
  	//Debug.Log(" g:" + gravity + " rad:"+ rad +" deg:" + rad * Mathf.Rad2Deg + " v0:" + speed);
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
  		pos = CreateStage.stages[StageManager.nowStageCount - 1].bard.transform.position;
  	  Debug.Log(CreateStage.stages[StageManager.nowStageCount - 1].bard.transform.position);
  	}
  	center.transform.position = pos;
  }

  //タマゴがタイミングタップされた後その都度計算して移動
  private void SetVelocity(Vector2 end, float time) {

  	//Vector2 start = new Vector2(0, 0);
  	Vector2 start = this.transform.position;
  	//if (CreateStage.stages.Count == 1) start = GameObject.Find("StageManager").GetComponent<CreateStage>().firstObj.transform.position; //スタート位置
  	//if (CreateStage.stages.Count != 1) start = CreateStage.stages[CreateStage.stages.Count - 1].nextPos; //ひとつ前のステージがスタート位置
  	
  	float gravity = 9.81f;
  	//発射角度
  	float rad = (float)Mathf.Atan((end.y - start.y + (gravity * time * time / 2)) / (end.x - start.x));
  	//初速度
  	float v0 = (end.x - start.x) / (Mathf.Cos(rad) * time);

  	SetVelocity(rad, v0, gravity);
  }

  //ベストタイミング時の経過時間
  private float IdealTime() {
  	if (StageManager.IsFirstStageNumber())  return 1.0f;
  	return CreateStage.stages[StageManager.PrevStageNumber()].interval;
  }
}
