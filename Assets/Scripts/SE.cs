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
  		  audioSource.pitch = 1.2f;
  			audioSource.PlayOneShot( earlySE );
	    break;
  		case "Nice" :  
  		  audioSource.pitch = 1;
  			audioSource.PlayOneShot( niceSE );
	    break;
  		case "Late" :  
  		  audioSource.pitch = 1.2f;
  			audioSource.PlayOneShot( lateSE );
	    break;
	  }
	}
}
