using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//タイトル処理
public class TitleManager : MonoBehaviour {
	private static int nowStagenumber = 0;
	public Image[] icon;
	public GameObject content;
	private float nextX;
	private bool canMove;
	public bool isAllDataClean;
	public Text difficultyText;
	void Start () {
		if (isAllDataClean) PlayerPrefs.DeleteAll();
		canMove = false;
		nextX = 0;

		Vector2 pos = content.GetComponent<RectTransform>().localPosition;
		pos.x = -400 * nowStagenumber;
		content.GetComponent<RectTransform>().localPosition = pos;
		SetColor(221, 66, 255); //色を付ける
		SetDifficulty();
	}
	
	void Update () {
		if (!canMove) return;
		Vector2 pos = content.GetComponent<RectTransform>().localPosition;
		pos.x += nextX - nextX * 0.9f;
		nextX *= 0.9f;
		content.GetComponent<RectTransform>().localPosition = pos;
		//Debug.Log("pos:"+ pos + "nextX:"+ nextX);
		if (Mathf.Abs(nextX) < 0.01) canMove = false;
	}

	public void ShowTitle() {
		this.GetComponent<Canvas>().enabled = true;
	}
	public void HideTitle() {
		this.GetComponent<Canvas>().enabled = false;
	}

	public void ChoseStage() {
		TouchInfo info = TouchUtil.GetTouch();
		switch(info) {
			case TouchInfo.Began :
			TouchUtil.SetStartPosition();
			break;
			case TouchInfo.Ended :
			TouchUtil.SetEndPosition();
			if (TouchUtil.GetDirection() == FlickInfo.Right) {
				MoveLeft();
			}
			else if (TouchUtil.GetDirection() == FlickInfo.Left) {
				MoveRight();
			}
			/*else if (TouchUtil.GetDirection() == FlickInfo.None) {
				DecideStage();
			}*/
			break;
		}
	}

  //ステージ選択が右から左に流れる
	private void MoveRight() {
		if (nowStagenumber+1 == icon.Length) return;
		SetColor(255, 255, 255); //白色に戻す
		nowStagenumber++;
		nextX -= 400;
		canMove = true;
		SetColor(221, 66, 255); //色を付ける
	}

	private void MoveLeft() {
		if (nowStagenumber <= 0) return;
		SetColor(255, 255, 255); //白色に戻す
		nowStagenumber--;
		nextX += 400;
		canMove = true;
		SetColor(221, 66, 255); //色を付ける
	}

	public void DecideStage() {
		StartGame();
		//SE.DesicionPlay();
	}

	/// 色設定.
  private void SetColor (float r, float g, float b) {
    Color c = icon[nowStagenumber].GetComponent<Image>().color;
    c.r = r / 255;
    c.g = g / 255;
    c.b = b / 255;
    icon[nowStagenumber].GetComponent<Image>().color = c;
  }

  public void ChoiseIcon(int stageCount) {
  	if (stageCount == nowStagenumber) return ;
  	SetColor(255, 255, 255); //白色に戻す
  	nextX += (nowStagenumber - stageCount) * 400;
  	nowStagenumber = stageCount;
  	SetColor(221, 66, 255); //色を付ける
  	canMove = true;
  }

  private void StartGame() {
  	ScreenFadeManager fadeManager = ScreenFadeManager.Instance;
  	GameManager.gameState = GameManager.GameState.None; //フェードインフェードアウトするため、一旦操作不能状態にする
  	SE.DesicionPlay();
		fadeManager.FadeOut(1.0f, Color.white, ()=> {// フェードイン
			HideTitle();
		  GameManager.StageNumber = nowStagenumber;
		  GameObject.Find("MainCanvas").GetComponent<MainCanvas>().ShowCanvas();
	  	GameObject.Find("StageManager").GetComponent<CreateStage>().MakeStage();
    	GameManager.gameState = GameManager.GameState.Play;
			fadeManager.FadeIn(1.0f, Color.white, ()=> {
    	});
			//Application.LoadLevel("Main");
		});
	}

	private void SetDifficulty() {
		if (GameManager.difficulty == 1) {
			difficultyText.text = "Normal";
		} else {
			difficultyText.text = "Hard";
		}
	}

	public void ChangeDifficulty() {
		if (GameManager.difficulty == 1) {
			GameManager.difficulty = 1.2f;
			difficultyText.text = "Hard";
		} else {
			GameManager.difficulty = 1;
			difficultyText.text = "Normal";
		}
		Time.timeScale = GameManager.difficulty;
  }
}
