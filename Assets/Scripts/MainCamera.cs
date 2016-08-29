using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {
	public GameObject egg; //タマゴ
	private float margin = 0.1f; //マージン(画面外に出てどれくらい離れたら消えるか)を指定
  private static float negativeMargin;
  private static float positiveMargin;
  private static Camera _setCamera;
  private Vector2 movePos;
	// Use this for initialization
	void Start () {
		if (_setCamera == null) {
      _setCamera = Camera.main;
    }
 
    negativeMargin = 0 - margin;
    positiveMargin = 1 + margin;

    movePos = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.gameState == GameManager.GameState.GameOver || GameManager.gameState == GameManager.GameState.Title)  return;
    Vector3 pos = GetPositon();
		pos.y = egg.transform.position.y + 0.5f;
		pos.z = -1;
		pos.x = egg.transform.position.x;
    movePos = pos - this.transform.position;
		this.transform.position = pos;
  }

	private Vector3 GetPositon() {
		return this.transform.position;
	}

  public static bool IsOutOfScreen(GameObject obj) {
    Vector3 positionInScreen = _setCamera.WorldToViewportPoint(obj.transform.position);
    positionInScreen.z = obj.transform.position.z;
 
    if (positionInScreen.x <= negativeMargin ||
      positionInScreen.x >= positiveMargin ||
      positionInScreen.y <= negativeMargin ||
      positionInScreen.y >= positiveMargin)
    {
      return true;
    } else {
      return false;
    }
  }

  public Vector2 GetMovePosition() {
    return movePos;
  }
}
