using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//スコアの処理
public class ScoreManager : MonoBehaviour {
	private static int score;
	private static GameObject scoreText;
	private static GameObject addScore;
	private static int niceCount;
	
	// Use this for initialization
	void Start () {
		niceCount = 0;
		scoreText = GameObject.Find("MainCanvas/ScoreNum");
		addScore = (GameObject)Resources.Load("UI/AddScore");
		Init();
	}
	
	public static void Init() {
		score = 0;
		niceCount = 0;
		scoreText.GetComponent<Text>().text ="";
	}

	public static void AddScore(string result) {
		int value = 0;
		switch (result) {
			case "Early" : value = 50; break;
			case "Nice" : 
			value = (int)(100 + ComboSystem.GetRate());
			niceCount++;
			break;
			case "Late" : value = 50; break;
		}
		score += value;
		ShowScore();

		GameObject temp = (GameObject)Instantiate(addScore, new Vector2(0, 0), Quaternion.identity);
		temp.transform.SetParent(GameObject.Find("MainCanvas").transform);
		temp.GetComponent<AddScore>().SetValue(value);
	}

	private static void ShowScore() {
		scoreText.GetComponent<Text>().text = "" + score;
	}

	public static int GetScore() {
		return score;
	}
	public static int GetNiceCount() {
		return niceCount;
	}
}
