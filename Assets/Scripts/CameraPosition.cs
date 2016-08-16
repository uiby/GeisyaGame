using UnityEngine;
using System.Collections;

//カメラ位置処理
//自機とY座標のみ共有
public class CameraPosition : MonoBehaviour {
	private GameObject player;
	
	void Start () {
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		//Y = player.transform.position.y + 3;
	}

	

	// Y座標の設定
  public float Y
  {
    get { return this.transform.position.y; }
    set
    {
      Vector3 p = this.transform.position;
      p.y = value;
      this.transform.position = p;
    }
  }
  public void SetPos(GameObject obj) {
  	transform.RotateAround(GameObject.Find("Player").transform.position, Vector3.up, 15);
  }
}
