using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//コンボ処理
//niceを2連続以上でコンボ発動
public class ComboSystem : MonoBehaviour {
	private static int comboNum;
	private static GameObject comboText;
	private static int maxCombo;

	// Use this for initialization
	void Start () {
		maxCombo = 0;
		comboNum = 0;
		comboText = GameObject.Find("MainCanvas/ComboCount");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void Init() {
		maxCombo = 0;
		comboNum = 0;
		comboText.GetComponent<Text>().text = "";
	}

  //コンボ数を1上げる
	public static void AddCombo() {
		comboNum++;
		if (maxCombo < comboNum)  maxCombo = comboNum;
		if (comboNum == 1)  return ;
		ShowStr();
		//Debug.Log("combo:" + comboNum);
	}

	//コンボの設定
	public static void ResetCombo() {
		comboNum = 0;
		//Debug.Log("combo:" + comboNum);
		comboText.GetComponent<Text>().text = "";
	}

	private static void ShowStr() {
		comboText.GetComponent<Text>().text = "" + comboNum;
	}

	public static float GetRate() {
		float rate = 1.0f;
		if (comboNum >= 2 && comboNum < 5)	rate = 1.3f;
		if (comboNum >=5 && comboNum < 10)  rate = 2.0f;
		if (comboNum >=10) rate = 3.0f;
		/*if (comboNum >= 2 && comboNum < 10) {
			rate += comboNum / 10.0f - 0.1f;
		} else {
			rate = 2.0f;
		}*/

		return rate;
	}

	public static int GetCombo() {
		return comboNum;
	}

	public static int GetMaxCombo() {
		return maxCombo;
	}
}
