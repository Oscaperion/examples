using UnityEngine;
using System.Collections;

public class PisteenLasku : MonoBehaviour {

	public GameObject pelaaja;
	public UnityEngine.UI.Text pistenakyma;
	public UnityEngine.UI.Text ohjeita;

	private string pisteteksti = "Pisteet: ";
	private float pisteet = 0f;
	private float edelPisteet;
	private int palikanArvo = 100;
	private float pisteraja = 4000f;
	private bool lasketaan;

	// Use this for initialization
	void Start () {
		lasketaan = true;
		edelPisteet = pisteet;
		pistenakyma.text = pisteteksti + pisteet;
	}

	public void lisaaPiste() {
		pisteet = pisteet + palikanArvo;
	}
	
	// Update is called once per frame
	void Update () {
		if (edelPisteet != pisteet) {
			pistenakyma.text = pisteteksti + pisteet;
			edelPisteet = pisteet;
		}

		if (pisteet >= pisteraja && lasketaan) {
			lasketaan = false;
			ohjeita.GetComponent<OhjeistusKaytos>().EsitaTotuus();
		}

		/*
		if (pelaaja.GetComponent<PlayerBehaviour> ().Kannossa) {
			lasketaan = true;
		} else if (!pelaaja.GetComponent<PlayerBehaviour> ().Kannossa && lasketaan) {
			lasketaan = false;
			pisteet = pisteet + palikanArvo;
			pistenakyma.text = pisteteksti + pisteet;
		}*/

	}
}
