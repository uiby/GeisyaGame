using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TapResultWord : MonoBehaviour {
	private float timer = 20;
	private GameObject target;
	private Vector2 firstPos;
	private GameObject particle;
	
	// Use this for initialization
	void Start () {
		timer = 20;
		this.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<RectTransform>().position = RectTransformUtility.WorldToScreenPoint (Camera.main, firstPos);
    if (timer < 0) Destroy(this.gameObject);
    if (timer < 0) Destroy(this.gameObject);
		timer -= 1;

		Vector3 scale = this.transform.localScale;
		if (timer >= 13) {
	  	scale.x += 0.1f;
		  scale.y += 0.1f;
	 	  scale.z += 0.1f;
	 	}
		this.transform.localScale = scale;
	}

	//ぶつかった時のエフェクト追加
  public void PlayEffect(float r, float g, float b) {
  	if (ComboSystem.GetCombo() > 8)  particle = (GameObject)Resources.Load("UI/TapResult-nice");
  	else particle = (GameObject)Resources.Load("UI/TapResult");
  	GameObject temp = (GameObject)Instantiate(particle, firstPos, Quaternion.identity);
  	temp.transform.parent = null;

  	if (ComboSystem.GetCombo() <= 8) {
  	  Color color = new Color(r/255, g/255, b/255);
    	temp.GetComponent<ParticleSystem>().startColor = color;
    }
  }

  public void SetString(string result) {
  	switch (result) {
  		case "Early" : SetText("<color=teal>Early</color>");  break;
  		case "Nice" : 
  		  int combo = ComboSystem.GetCombo();
  		  SetText("Nice♪"); 
  		  if (combo <= 8) SetText("<color=orange>Nice♪</color>");
  		  else if (combo > 8) SetText("<color=red>Nice♪</color>");
  		break;
  		case "Late": SetText("<color=teal>Late</color>"); break;
  	}
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

  public void SetFirstPos(Vector2 pos) {
  	firstPos = pos;
  	this.GetComponent<RectTransform>().position = RectTransformUtility.WorldToScreenPoint (Camera.main, firstPos);
  	SetParent();
  }

  private void SetParent() {
  	this.transform.SetParent(GameObject.Find("MainCanvas").transform);
  }

  private void SetText(string sentence) {
  	this.GetComponent<Text>().text = sentence;
  }
}
