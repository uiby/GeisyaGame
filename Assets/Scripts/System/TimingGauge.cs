using UnityEngine;
using System.Collections;

//タイムゲージの処理
//時間につれてだんだん小さくなる
public class TimingGauge : MonoBehaviour {
	private float timer = 0;
	private float interval;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		ContractScale();
	}

  //画像を縮小する
	private void ContractScale() {
		timer -= Time.deltaTime;
		Vector3 scale = this.transform.localScale;

		if (scale.x <= 0) delete(); //もしinterval秒かかったらこのオブジェクトを削除する

		scale.x = timer / interval;
		scale.y = timer / interval;
		scale.z = timer / interval;
		this.transform.localScale = scale;
	}

	//インターバルの設定
	public void SetInterval(float time) {
		timer = time;
		interval = time;
	}

  //削除
	private void delete() {
		Destroy(this.gameObject);
	}
}
