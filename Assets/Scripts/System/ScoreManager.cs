using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//スコアの処理
public class ScoreManager : MonoBehaviour {
	private static int score;
	private static GameObject scoreText;

	// Use this for initialization
	void Start () {
		score = 0;
		scoreText = GameObject.Find("MainCanvas/ScoreNum");
	}
	
	public static void Init() {
		score = 0;
		scoreText.GetComponent<Text>().text ="";
	}

	public static void AddScore(string result) {
		switch (result) {
			case "Early" : score += (int)(50 * ComboSystem.GetRate()); break;
			case "Nice" : score += (int)(100 * ComboSystem.GetRate()); break;
			case "Late" : score += (int)(50 * ComboSystem.GetRate()); break;
		}
		ShowScore();
	}

	private static void ShowScore() {
		scoreText.GetComponent<Text>().text = "" + score;
	}
}
