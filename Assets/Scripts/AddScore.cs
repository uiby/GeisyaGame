using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//スコア加点の表示
public class AddScore : MonoBehaviour {
	private GameObject egg;
	private Vector2 firstPos;
	private float timer = 10;
	
	// Use this for initialization
	void Start () {
		egg = GameObject.Find("Egg");
		firstPos = egg.transform.position;
		firstPos.y += 1;
		this.GetComponent<RectTransform>().position = RectTransformUtility.WorldToScreenPoint (Camera.main, firstPos);
	}
	
	// Update is called once per frame
	void Update () {
		if (timer < 0) Destroy(this.gameObject);
		this.GetComponent<RectTransform>().position = RectTransformUtility.WorldToScreenPoint (Camera.main, firstPos);
		timer--;
	}

	public void SetValue(int value) {
		int comboNum = ComboSystem.GetCombo();
		if (comboNum <= 1 )	this.GetComponent<Text>().text = "+" + value;
		if (comboNum > 1 && comboNum <= 8)	this.GetComponent<Text>().text = "<color=magenta>+" + value + "</color>";
		if (comboNum > 8)	this.GetComponent<Text>().text = "<color=aqua>+" + value + "</color>";
	}
}
