using UnityEngine;
using System.Collections;

public class PaluuAlkuun : MonoBehaviour {

	private bool peliLoppui = false;
	private int odota = 400;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (peliLoppui) {
			if (odota > 0) {
				odota--;
			} else {
				Application.LoadLevel ("titleScene");
			}
		}
	}

	public void PeliLoppui() {
		peliLoppui = true;

		AudioSource bgm = GameObject.Find ("Main Camera").GetComponent<AudioSource> ();
		bgm.clip = Resources.Load("spell_it-game_over") as AudioClip;
		bgm.Play ();
		bgm.loop = false;
	}
}
