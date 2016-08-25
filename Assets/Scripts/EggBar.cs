using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//タマゴバー
//乱暴に扱う(ジャストタイミング以外のタップ)とバーが減る
public class EggBar : MonoBehaviour {
	private float hp;
	private float diff;
	// Use this for initialization
	void Start () {
		hp = this.GetComponent<Slider>().value;
		diff = 0;
		SetValue(100);
	}
	
	void Update () {
		if (diff < 0.01) return;
		float nowHp = GetValue();
		nowHp -= diff - diff * 0.9f;
		diff *= 0.9f;
		SetValue(nowHp);  
	}

	public void Damage(float value) {
		hp -= value;
		if (hp < 0) hp = 0;
		diff = GetValue() - hp;
	}

	private float GetValue() {
		return this.GetComponent<Slider>().value;
	}
	private void SetValue(float value) {
		this.GetComponent<Slider>().value = value;
	}
}
