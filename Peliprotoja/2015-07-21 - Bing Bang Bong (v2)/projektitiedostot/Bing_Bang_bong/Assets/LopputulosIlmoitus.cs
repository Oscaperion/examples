using UnityEngine;
using System.Collections;

public class LopputulosIlmoitus : MonoBehaviour {
	public UnityEngine.UI.Text esitys;
	private bool PeliKaynnissa = true;
	private int PelinLoppuAika = 300;
	/*
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	} */

	private string PelaajaVoitto = "Voitit pelin!";
	private string VihollisVoitto = "Hävisit pelin!";
	private string VihollisVoittoPutous = "Putosit kentältä!";

	public void EsitaPelaajaVoitto() {
		esitys.text = PelaajaVoitto;
		VaihdaMusiikki(false);
	}

	public void EsitaVihollisVoitto() {
		esitys.text = VihollisVoitto;
		VaihdaMusiikki(true);
	}

	public void EsitaPutousHavio() {
		esitys.text = VihollisVoittoPutous;
		VaihdaMusiikki(true);
	}

	void VaihdaMusiikki(bool havio) {

		AudioSource bgm = GameObject.Find ("Main Camera").GetComponent<AudioSource> ();
		if (havio) {
			bgm.clip = Resources.Load ("lose") as AudioClip;
		} else {
			bgm.clip = Resources.Load ("won") as AudioClip;
		}
		bgm.Play ();
		bgm.loop = false;

		PeliKaynnissa = false;
	}

	void Update () {
		if (!PeliKaynnissa) {
			PelinLoppuAika--;

			if (PelinLoppuAika <= 0) {
				Application.LoadLevel ("TitleScene");
			}
		}
	}
}
