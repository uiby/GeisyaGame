using UnityEngine;
using System.Collections;

//プレイヤー処理
public class Player : MonoBehaviour {
	private Rigidbody rigidbody;
	public int playerSpeed;
	public int diff;
	public int planeSpeed;//平面移動速度
	public float coefficient;   // 空気抵抗係数
	private GameObject nowTouchedObj; //今触れているオブジェクト

	// Use this for initialization
	void Start () {
		Physics.gravity = new Vector3(0, -9.81f * 3, 0);
	  rigidbody = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

	}

  //フリック操作によるアクション
	public void FlickAction() {
		switch (FlickInput.direct) {
			case FlickInput.Direct.UP : 
			  SetVelocityYZ (90 - diff, playerSpeed);
			break;
			case FlickInput.Direct.RIGHTUP : 
			  SetVelocityXY (90 - diff, playerSpeed);
			break;
			case FlickInput.Direct.RIGHT : 
			  SetVelocityXY (0, planeSpeed);
			break;
			case FlickInput.Direct.RIGHTDOWN : 
			break;
			case FlickInput.Direct.DOWN : 
  			SetVelocityYZ (180, planeSpeed);
			break;
			case FlickInput.Direct.LEFTDOWN : 
			break;
			case FlickInput.Direct.LEFT : 
			  SetVelocityXY (180, planeSpeed);
			break;
			case FlickInput.Direct.LEFTUP : 
			  SetVelocityXY (90 + diff, playerSpeed);
			break;
		}
	}

	/// 移動量を設定.direction : 角度
	//x,y平面の移動
  private void SetVelocityXY(float direction, float speed)
  {
    Vector3 v;
    v.x = Mathf.Cos(Mathf.Deg2Rad * direction) * speed;
    v.y = Mathf.Sin(Mathf.Deg2Rad * direction) * speed;
    v.z = 0;
    rigidbody.velocity = nowTouchedObj.transform.TransformDirection(v);
  }
  //y,z平面の移動
  private void SetVelocityYZ(float direction, float speed)
  {
    Vector3 v;
    v.x = 0;
    v.y = Mathf.Sin(Mathf.Deg2Rad * direction) * speed;
    v.z = Mathf.Cos(Mathf.Deg2Rad * direction) * speed;
    rigidbody.velocity = nowTouchedObj.transform.TransformDirection(v);
  }

  void FixedUpdate() {
	  // 空気抵抗を与える
    //rigidbody.AddForce(-coefficient * rigidbody.velocity);
	}

	void OnCollisionEnter(Collision col) {
		Debug.Log(col.gameObject.name);
		nowTouchedObj = col.gameObject;
		if (nowTouchedObj.name == StageManager.stages[StageManager.stages.Count - 1].name)
		  GameObject.Find("GameManager").GetComponent<GameManager>().CreateNextStage();
	}
}
