using UnityEngine;
using System.Collections;

public class SE : MonoBehaviour {
	private static AudioSource audioSource;
	private static AudioClip earlySE;
	private static AudioClip niceSE;
	private static AudioClip lateSE;

	private static float[] musicleScale = new float[]{0, 2, 4, 5, 7, 9, 11, 12};
	// Use this for initialization
	void Start () {
		audioSource = this.GetComponent<AudioSource>();
		earlySE = (AudioClip)Resources.Load("Sound/cry");
		niceSE = (AudioClip)Resources.Load("Sound/cry");
		lateSE = (AudioClip)Resources.Load("Sound/cry");
	}

	public static void CryBard(string result) {
		switch (result) {
  		case "Early" : 
  		  audioSource.pitch = 0.8f;
  			audioSource.PlayOneShot( earlySE );
	    break;
  		case "Nice" :
  		  int comboNum = ComboSystem.GetCombo();
  		  if (comboNum < 8)    audioSource.pitch = Mathf.Pow(2, musicleScale[comboNum]/12.0f);
  		  else audioSource.pitch = 2.0f;
     		//if (comboNum <= 5)
    		//  audioSource.pitch = 1 + comboNum / 10.0f;
    		//else audioSource.pitch = 1.5f;

  			audioSource.PlayOneShot( niceSE );
	    break;
  		case "Late" :  
  		  audioSource.pitch = 0.8f;
  			audioSource.PlayOneShot( lateSE );
	    break;
	  }
	}
}
