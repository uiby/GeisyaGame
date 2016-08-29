using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//ゲームクリア―の処理
public class GameClear : MonoBehaviour {
	private Text result;
	private int frame = 50;
	private int wordNumber = 0;
	private int state; //簡易状態
	public Text maxCombo;
	public Text score;
	public Text[] rankingText;
	public Text[] anotherRankingText; //ランキングのスコア以外の文章
	private int[] rankingScore;
	private bool isNewRecord; //新しいスコアかどうか
	public	GameObject egg; //センターの中のタマゴのイラスト
	public GameObject center; //センター
	private GameObject resultChara;

  //touch情報
  public enum eState {
  	//touchなし
  	None = 99,
  	Result = 0,  //結果の演出
  	//touch開始
  	TitleWord = 1,  //タイトルワードの演出
  }
  private eState playState;

	// Use this for initialization
	void Start () {
		resultChara = GameObject.Find("ResultChara");
		result = this.transform.FindChild("Result").GetComponent<Text>();
		state = 0;
		playState = eState.Result;
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
		switch(playState) {
			case eState.Result: 
			if (FirstProduction()){
				frame = 33;
				playState = eState.TitleWord;
			}
			break;
			case eState.TitleWord: SecoundProduction(); break;
		}
		
	}

	public bool FirstProduction() {
		frame--;
		if (frame > 0) {
			Camera.main.orthographicSize -= 0.08f;
		}
		if (frame <= 0 && frame > -5) center.GetComponent<Egg>().Roll(3);
		if (frame <= -5 && frame > -10) center.GetComponent<Egg>().Roll(-3);
		if (frame <= -10 && frame > -15) center.GetComponent<Egg>().Roll(2);
		if (frame <= -15 && frame > -20) center.GetComponent<Egg>().Roll(-2);
		if (frame == -20) {
  		resultChara.GetComponent<ParticleSystem>().Play();
		}
		if(frame == -25) {
			Sprite[] brokenEgg = Resources.LoadAll<Sprite>("Sprite/brokenEgg");
			egg.GetComponent<SpriteRenderer>().sprite = brokenEgg[0];
		}
		if (frame == -30) {
			Sprite chara = Result.GetSprite(ScoreManager.GetScore())[0];
			Vector2 pos= egg.transform.position;
			pos.y += 0.25f;
			resultChara.transform.position = GameObject.Find("Center/Egg").transform.position;
			resultChara.GetComponent<SpriteRenderer>().sprite = chara;
			resultChara.GetComponent<SpriteRenderer>().enabled = true;
		}
		if (frame == -40) {
			if (!resultChara.GetComponent<ParticleSystem>().isStopped) {
				frame++;
			}
		}
		if(frame == -40) {
			return true;
		}
		/*if (frame < -30 && frame >= -40) {
			Vector2 pos = resultChara.transform.position;
			pos.y += 0.02f;
			resultChara.transform.position = pos;
		}*/
		if (frame == -40) return true;

		return false;
	}

  //gameclearの文字表示　ランキング表示
	public void SecoundProduction() {
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
    playState = eState.Result;
		result.text = "";
		frame = 50;
		wordNumber = 0;
		state = 0;
		resultChara.GetComponent<SpriteRenderer>().enabled = false;
		maxCombo.enabled = false;
		score.enabled = false;
		isNewRecord = false;
		Camera.main.orthographicSize = 5;
		for (int i = 0; i < rankingText.Length; i++)  rankingText[i].enabled = false;
		for (int i = 0; i < anotherRankingText.Length; i++)  anotherRankingText[i].enabled = false;
		egg.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprite/egg")[0];
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
