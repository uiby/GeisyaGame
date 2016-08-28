using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour {
  private Text result;
	private int frame = 36;
	private int wordNumber = 0;
	public Image[] image;
	public Text[] text;
	// Use this for initialization
	void Start () {
		result = this.transform.FindChild("GameOver").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!GetShowCanvas()) return;
		if(frame < -5) return ;
		frame--;
		if (frame >= 0) {
	  	if (frame % 3 == 0) {
	  		result.text += GetWord(wordNumber);
	  		wordNumber++;
	  	}
	  } else if (frame == -5) {
			frame--;
			ShowAnother();
		}
	}

	public void ShowCanvas() {
		this.GetComponent<Canvas>().enabled = true;
	}

	public void HideCanvas() {
		this.GetComponent<Canvas>().enabled = false;
		result.text = "";
		for (int i = 0; i < 2; i++) {
			image[i].enabled = false;
			text[i].enabled = false;
		}
		frame = 36;
		wordNumber = 0;	
	}

	private void ShowAnother() {
		for (int i = 0; i < 2; i++) {
			image[i].enabled = true;
			text[i].enabled = true;
		}
	}

	private string GetWord(int num) {
		string word = "";
		switch (num) {
			case 0: word = "G"; break;
			case 1: word = "a"; break;
			case 2: word = "m"; break;
			case 3: word = "e"; break;
			case 4: word = " "; break;
			case 5: word = "O"; break;
			case 6: word = "v"; break;
			case 7: word = "e"; break;
			case 8: word = "r"; break;
			case 9: word = "."; break;
			case 10: word = "."; break;
			case 11: word = "."; break;
		}
  	return word;
	}

	private bool GetShowCanvas() {
		return this.GetComponent<Canvas>().enabled;
	}
}
