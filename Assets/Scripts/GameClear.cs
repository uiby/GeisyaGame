using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//ゲームクリア―の処理
public class GameClear : MonoBehaviour {
	private Text result;
	private int frame = 33;
	private int wordNumber = 0;
	private int state; //簡易状態
	public Text maxCombo;
	public Text score;
	// Use this for initialization
	void Start () {
		result = this.transform.FindChild("Result").GetComponent<Text>();
		state = 0;
		result.text = "";
		maxCombo.enabled = false;
		score.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!GetShowCanvas()) return;
		switch (state) {
			case 0:
			  frame--;
		    if (frame % 3 == 0) {
			    result.text += GetWord(wordNumber);
			    wordNumber++;
		    }
		    if(frame < 0) {
		    	frame = 20;
		    	state = 1;
		    }
			break;
			case 1: 
			  frame--;
			  if (frame == 10) maxCombo.enabled = true;
			  if (frame == 0) score.enabled = true;
			  if (frame < 0) {
			  	state = 2;
			  }
			break;
			case 2: 

			break;
		}
	}

	public void ShowCanvas() {
		maxCombo.text = "Max Combo : "+ ComboSystem.GetMaxCombo();
		score.text = "Score : "+ ScoreManager.GetScore();
		this.GetComponent<Canvas>().enabled = true;
	}
	public void HideCanvas() {
		this.GetComponent<Canvas>().enabled = false;
		result.text = "";
		frame = 40;
		wordNumber = 0;
		state = 0;
		maxCombo.enabled = false;
		score.enabled = false;
	}

	private string GetWord(int num) {
		string word = "";
		switch (num) {
			case 0: word = "G"; break;
			case 1: word = "a"; break;
			case 2: word = "m"; break;
			case 3: word = "e"; break;
			case 4: word = " "; break;
			case 5: word = "C"; break;
			case 6: word = "l"; break;
			case 7: word = "e"; break;
			case 8: word = "a"; break;
			case 9: word = "r"; break;
			case 10: word = "!"; break;
		}
  	return word;
	}

	private bool GetShowCanvas() {
		return this.GetComponent<Canvas>().enabled;
	}
}
