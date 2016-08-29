using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//メインキャンバスの処理
public class MainCanvas : MonoBehaviour {
	private static GameObject timingResultText; //次のタイミングまでのゲージ
	public Text[] hideText;
	void Start () {
		timingResultText = (GameObject)Resources.Load("UI/TimingResult");
		HideCanvas();
	}
	
	public static void ShowTimingResult(string resultWord) {
		Vector2 pos ;
		if (StageManager.nowStageCount == 0)  pos = GameObject.Find("StageManager").GetComponent<CreateStage>().firstObj.transform.position;
		else pos = CreateStage.stages[StageManager.PrevStageNumber()].nextPos;
		GameObject temp = (GameObject)Instantiate(timingResultText, pos, Quaternion.identity);
		temp.transform.localPosition = pos;
		SetString(temp, resultWord);
		temp.transform.SetParent(GameObject.Find("MainCanvas").transform); //親の設定
	}

	private static void SetString(GameObject obj, string word) {
		obj.GetComponent<Text>().text = word;
	}

	public void ShowCanvas() {
		for (int i=0; i< hideText.Length;i ++) {
			hideText[i].enabled = true;
		}
		//this.GetComponent<Canvas>().enabled = true;
	}

	public void HideCanvas() {
		for (int i=0; i< hideText.Length;i ++) {
			hideText[i].enabled = false;
		}
		//this.GetComponent<Canvas>().enabled = false;
	}

}
