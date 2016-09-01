using UnityEngine;
using System.Collections;

public class SE : MonoBehaviour {
	private static AudioSource audioSource;
	private static AudioClip bardCry;
	private static AudioClip decision;
	private static AudioClip choise;
	
	private static float[] musicleScale = new float[]{0, 2, 4, 5, 7, 9, 11, 12};
	// Use this for initialization
	void Start () {
		audioSource = this.GetComponent<AudioSource>();
		bardCry = (AudioClip)Resources.Load("Sound/cry");
		decision = (AudioClip)Resources.Load("Sound/decision01");
		choise = (AudioClip)Resources.Load("Sound/choise01");
	}

	public static void CryBard(string result) {
		switch (result) {
  		case "Early" : 
  		  audioSource.pitch = 0.8f;
  			audioSource.PlayOneShot( bardCry );
	    break;
  		case "Nice" :
  		  int comboNum = ComboSystem.GetCombo();
  		  if (comboNum < 8)    audioSource.pitch = Mathf.Pow(2, musicleScale[comboNum]/12.0f);
  		  else audioSource.pitch = 2.0f;
  			audioSource.PlayOneShot( bardCry );
	    break;
  		case "Late" :  
  		  audioSource.pitch = 0.8f;
  			audioSource.PlayOneShot( bardCry );
	    break;
	  }
	}

	public static void DesicionPlay() {
		audioSource.PlayOneShot( decision );
	}
	public static void ChoisePlay() {
		audioSource.PlayOneShot( choise );
	}
}
