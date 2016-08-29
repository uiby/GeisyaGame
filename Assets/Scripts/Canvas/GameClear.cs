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
	public Text[] rankingText;
	public Text[] anotherRankingText; //ランキングのスコア以外の文章
	private int[] rankingScore;
	private bool isNewRecord; //新しいスコアかどうか
	// Use this for initialization
	void Start () {
		result = this.transform.FindChild("Result").GetComponent<Text>();
		state = 0;
		result.text = "";
		maxCombo.enabled = false;
		score.enabled = false;
		isNewRecord = false;
		for (int i = 0; i < rankingText.Length; i++)  rankingText[i].enabled = false;
		for (int i = 0; i < anotherRankingText.Length; i++)  anotherRankingText[i].enabled = false;
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
			  	frame = 40;
			  	if (isNewRecord) {
			  		Ranking.RenewalRank(rankingScore, ScoreManager.GetScore(), GameManager.StageNumber);
			  		Debug.Log("ok:" + rankingScore);
			  	}
			  	rankingScore = Ranking.GetRanking(GameManager.StageNumber);
			  	SetRank();
			  }
			break;
			case 2:
			  frame--;
			  if (frame == 20) anotherRankingText[0].enabled = true;
			  if (frame == 15) {
			  	anotherRankingText[1].enabled = true;
			  	rankingText[0].enabled = true;
			  }
			  if (frame == 10) {
			  	anotherRankingText[2].enabled = true;
			  	rankingText[1].enabled = true;
			  }
			  if (frame == 5) {
			  	anotherRankingText[3].enabled = true;
			  	rankingText[2].enabled = true;
			  }
			  if (isNewRecord && frame == 0) {
			    state = -1;
			  }
			break;
		}
	}

	public void SetRank() {
		for (int i = 0; i < rankingText.Length; i++)
  		rankingText[i].text = "" + rankingScore[i];
	}

	public void ShowCanvas() {
		maxCombo.text = "Max Combo : "+ ComboSystem.GetMaxCombo();
		int record = ScoreManager.GetScore();
		score.text = "Score : "+ record;
		rankingScore = Ranking.GetRanking(GameManager.StageNumber);
		if (Ranking.CanChangeRank(rankingScore, record))  isNewRecord = true;
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
		isNewRecord = false;
		for (int i = 0; i < rankingText.Length; i++)  rankingText[i].enabled = false;
		for (int i = 0; i < anotherRankingText.Length; i++)  anotherRankingText[i].enabled = false;
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
