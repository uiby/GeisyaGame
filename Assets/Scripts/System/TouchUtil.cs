using UnityEngine;
using System.Collections;
//画面のタップ動作を認識するスクリプト
public static class TouchUtil {
	private static Vector3 TouchPosition = Vector3.zero;
	private static Vector3 TouchStartPositon = Vector3.zero;
	private static Vector3 TouchEndPositon = Vector3.zero;
	
	//touch情報の取得.
	//<returns>タッチ情報。タッチされていない場合は null</returns>
	public static TouchInfo GetTouch() {
	  //Editor上のプレイかどうか
    if (Application.isEditor) {
			if (Input.GetMouseButtonDown(0)) { return TouchInfo.Began; }
			if (Input.GetMouseButton(0)) { return TouchInfo.Moved; }
			if (Input.GetMouseButtonUp(0)) { return TouchInfo.Ended; }
		} else {
			if (Input.touchCount > 0) {
				return (TouchInfo)((int)Input.GetTouch(0).phase);
			}
		}
		return TouchInfo.None;
	}

	// タッチポジションの取得
  // <returns>タッチポジション。タッチされていない場合は (0, 0, 0)</returns>
  public static Vector3 GetTouchPosition() {
    if (Application.isEditor) {
      TouchInfo touch = TouchUtil.GetTouch();
      if (touch != TouchInfo.None) { return Input.mousePosition; }
    } else {
      if (Input.touchCount > 0) {
        Touch touch = Input.GetTouch(0);
        TouchPosition.x = touch.position.x;
        TouchPosition.y = touch.position.y;
        return TouchPosition;
      }
    }
    return Vector3.zero;
  }

  //touchはじめの位置を代入
  public static void SetStartPosition() {
  	TouchStartPositon = TouchUtil.GetTouchPosition();
  }

  //touch終わりの位置を代入
  public static void SetEndPosition() {
  	TouchEndPositon = TouchUtil.GetTouchPosition();
  }

  //フリックの向きの取得
  public static FlickInfo GetDirection() {
  	FlickInfo flickInfo = FlickInfo.None;
		float dx = TouchEndPositon.x - TouchStartPositon.x;
		float dy = TouchEndPositon.y - TouchStartPositon.y;
		if (Vector2.Distance(TouchEndPositon, TouchStartPositon) < 1.0f) {
			flickInfo = FlickInfo.None;
			return flickInfo;
		}
		float angle = Mathf.Atan2(dx, dy) * Mathf.Rad2Deg;
		
		if (angle > -22.5 && angle <= 22.5) flickInfo = FlickInfo.Up;
		else if (angle > 22.5 && angle <= 67.5) flickInfo = FlickInfo.RightUp;
		else if (angle > 67.5 && angle <= 112.5) flickInfo = FlickInfo.Right;
	  else if (angle > 112.5 && angle <= 157.5) flickInfo = FlickInfo.RightDown;
		else if ((angle > 157.5 && angle <= 180) || (angle >= -180 && angle < -157.5)) flickInfo = FlickInfo.Down;
		else if (angle > -157.5 && angle <= -112.5) flickInfo = FlickInfo.LeftDown;
		else if (angle > -112.5 && angle <= -67.5) flickInfo = FlickInfo.Left;
		else if (angle > -67.5 && angle <= -22.5) flickInfo = FlickInfo.LeftUp;
	  //Debug.Log(flickInfo);
  
		return flickInfo;
	}
}

//touch情報
public enum TouchInfo {
	//touchなし
	None = 99,
	//touch開始
	Began = 0,
	//touch移動
	Moved = 1,
	//touch静止
	Stationary = 2,
	//touch終了
	Ended = 3,
}

//フリック操作情報
public enum FlickInfo {
	None = 0,
	Up = 1,
  RightUp,
	Right,
	RightDown,
	Down,
	LeftDown,
	Left,
	LeftUp
}
