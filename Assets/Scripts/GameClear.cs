using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//ゲームクリア―の処理
public class GameClear : MonoBehaviour {
	private Text result;
	private int frame = 40;
	private int wordNumber = 0;
	// Use this for initialization
	void Start () {
		result = this.transform.FindChild("Result").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!GetShowCanvas()) return;
		if(frame < 0) return ;
		frame--;
		if (frame % 2 == 0) {
			result.text += GetWord(wordNumber);
			wordNumber++;
		}
	}

	public void ShowGameClear() {
		this.GetComponent<Canvas>().enabled = true;
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
		}
  	return word;
	}

	private bool GetShowCanvas() {
		return this.GetComponent<Canvas>().enabled;
	}
}
