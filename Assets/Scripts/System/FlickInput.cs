using UnityEngine;
using System.Collections;

//フリック操作処理
public class FlickInput : MonoBehaviour {
	/*private Player player;
	private Vector3 touchStartPos;//touchした位置
	private Vector3 touchEndPos;//離した位置

	public enum Direct {
		UP,
		RIGHTUP,
		RIGHT,
		RIGHTDOWN,
		DOWN,
		LEFTDOWN,
		LEFT,
		LEFTUP
	}
	public static Direct direct;

	// Use this for initialization
	void Start () {
		player = this.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		GetTouch();
	}

	//touchした位置の取得
	private void GetTouch() {
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			touchStartPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
		}
		if (Input.GetKeyUp(KeyCode.Mouse0)) {
			touchEndPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
			GetDirection();
		}
	}
	//フリックした方向を取得
	private void GetDirection() {
		float dx = touchEndPos.x - touchStartPos.x;
		float dy = touchEndPos.y - touchStartPos.y;
		float angle = Mathf.Atan2(dx, dy) * Mathf.Rad2Deg;
		
		if (angle > -22.5 && angle <= 22.5) direct = Direct.UP;
		else if (angle > 22.5 && angle <= 67.5) direct = Direct.RIGHTUP;
		else if (angle > 67.5 && angle <= 112.5) direct = Direct.RIGHT;
	  else if (angle > 112.5 && angle <= 157.5) direct = Direct.RIGHTDOWN;
		else if ((angle > 157.5 && angle <= 180) || (angle >= -180 && angle < -157.5)) direct = Direct.DOWN;
		else if (angle > -157.5 && angle <= -112.5) direct = Direct.LEFTDOWN;
		else if (angle > -112.5 && angle <= -67.5) direct = Direct.LEFT;
		else if (angle > -67.5 && angle <= -22.5) direct = Direct.LEFTUP;

		Debug.Log(direct);
		player.FlickAction();
	}*/
}
