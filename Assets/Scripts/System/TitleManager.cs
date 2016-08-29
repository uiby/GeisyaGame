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
	void Start () {
		canMove = false;
		nextX = 0;

		Vector2 pos = content.GetComponent<RectTransform>().localPosition;
		pos.x = -400 * nowStagenumber;
		content.GetComponent<RectTransform>().localPosition = pos;
		SetColor(221, 66, 255); //色を付ける
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
		HideTitle();
		GameManager.StageNumber = nowStagenumber;
		GameObject.Find("MainCanvas").GetComponent<MainCanvas>().ShowCanvas();
		GameObject.Find("StageManager").GetComponent<CreateStage>().MakeStage();
		GameManager.gameState = GameManager.GameState.Play;
	}

	/// 色設定.
  private void SetColor (float r, float g, float b)
  {
    Color c = icon[nowStagenumber].GetComponent<Image>().color;
    c.r = r / 255;
    c.g = g / 255;
    c.b = b / 255;
    icon[nowStagenumber].GetComponent<Image>().color = c;
  }
}
