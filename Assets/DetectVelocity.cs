using UnityEngine;
using System.Collections;

public class DetectVelocity : MonoBehaviour {

	//オブジェクトが衝突したとき
  void OnTriggerStay2D(Collider2D col) {
  	if (col.gameObject.tag == "HitPosition"){
    	Debug.Log("速度:"+this.GetComponent<Rigidbody2D>().velocity + "  位置:" + this.transform.position);
    }
  }
}
