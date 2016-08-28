using UnityEngine;
using System.Collections;

public class SE : MonoBehaviour {
	private static AudioSource audioSource;
	private static AudioClip earlySE;
	private static AudioClip niceSE;
	private static AudioClip lateSE;
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
  		  float comboNum = ComboSystem.GetCombo();
  		  if (comboNum <= 5)
    		  audioSource.pitch = 1 + comboNum / 10.0f;
    		else audioSource.pitch = 1.5f;

  			audioSource.PlayOneShot( niceSE );
	    break;
  		case "Late" :  
  		  audioSource.pitch = 0.8f;
  			audioSource.PlayOneShot( lateSE );
	    break;
	  }
	}
}
