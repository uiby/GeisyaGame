using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//時間を図る処理
public class TimeTest : MonoBehaviour {
	private Text text;
	private static float time = 0;
	private static bool isBegin = false;
	// Use this for initialization
	void Start () {
		time = 0;
		isBegin = false;
		text = this.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isBegin) {
			time += Time.deltaTime;
			text.text = "Time:"+time;
		}
		else {

		}
	}

  public static void StartTime() {
  	isBegin = true;
  }
  public static void FinishTime() {
  	isBegin = false;
  	time = 0;
  }

}
