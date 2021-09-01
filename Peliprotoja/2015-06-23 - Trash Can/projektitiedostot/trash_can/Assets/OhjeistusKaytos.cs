using UnityEngine;
using System.Collections;

public class OhjeistusKaytos : MonoBehaviour {

	public UnityEngine.UI.Text ohje;
	public UnityEngine.UI.Button lopetusnappi;

	private int aika = 200;
	private bool alkuTeksti = true;
	private bool totuusEsiin = false;
	private bool esitetaanTotuutta = false;
	private string totuus1 = "Valitettavasti tällä pelillä ei ole Tasoa 2, koska osa ihmisistä vain jatkavat roskaamista kaikesta siivouksesta ja valistuksesta huolimatta";
	private string totuus2 = "Sinulla on tosin mahdollisuus poistua tästä pelistä, toisin kuin niillä jotka pitävät katumme ja puistomme puhtaina\n";

	// Use this for initialization
	void Start () {
		ohje.text = "Kerää kaikki roskat edetäksesi Tasoon 2";
		lopetusnappi.transform.localPosition = new Vector3 (lopetusnappi.transform.localPosition.x + 20, lopetusnappi.transform.localPosition.y, lopetusnappi.transform.localPosition.z);
	}

	public void EsitaTotuus() {
		if (!esitetaanTotuutta) {
			totuusEsiin = true;
			aika = 600;
			ohje.text = totuus1;
			esitetaanTotuutta = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (alkuTeksti && aika > 0) {
			aika = aika - 1;
		} else if (alkuTeksti) {
			ohje.text = "";
			alkuTeksti = false;
		}

		if (totuusEsiin && aika > 0) {
			aika = aika - 1;
		} else if (totuusEsiin) {
			ohje.text = totuus2;
			totuusEsiin = false;
			lopetusnappi.transform.localPosition = new Vector3 (lopetusnappi.transform.localPosition.x - 20, lopetusnappi.transform.localPosition.y, lopetusnappi.transform.localPosition.z);
		}
	}
}
