using UnityEngine;
using System.Collections;

public class SE : MonoBehaviour {
	private AudioSource audioSource;
	public AudioClip bardSE;
	// Use this for initialization
	void Start () {
		audioSource = this.GetComponent<AudioSource>();
	}

	public void SEPlay() {
		audioSource.PlayOneShot( bardSE );
	}
}
