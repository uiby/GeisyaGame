using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//スコア加点の表示
public class AddScore : MonoBehaviour {
	private GameObject egg;
	private Vector2 firstPos;
	private float timer = 20;
	
	// Use this for initialization
	void Start () {
		egg = GameObject.Find("Egg");
		firstPos = egg.transform.position;
		firstPos.y += 1.5f;
		this.GetComponent<RectTransform>().position = RectTransformUtility.WorldToScreenPoint (Camera.main, firstPos);
		this.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
		if (timer < 0) Destroy(this.gameObject);
		this.GetComponent<RectTransform>().position = RectTransformUtility.WorldToScreenPoint (Camera.main, firstPos);
		timer--;
	}

	public void SetValue(int value) {
		int comboNum = ComboSystem.GetCombo();
		if (comboNum <= 0 )	this.GetComponent<Text>().text = "<color=teal>+" + value +"</color>";
		if (comboNum > 0 && comboNum <= 8)	this.GetComponent<Text>().text = "<color=orange>+" + value + "</color>";
		if (comboNum > 8)	this.GetComponent<Text>().text = "<color=red>+" + value + "</color>";
		SetEffectColor();
  }

  private void SetEffectColor() {
  	/*int combo = ComboSystem.GetCombo();
  	Color color = new Color(1, 1, 1);
  	if (combo >= 8) {
  		color = new Color(193/255.0f, 193/255.0f, 193/255.0f);
  	}
  	this.GetComponent<Outline>().effectColor = color;*/
  }
}
