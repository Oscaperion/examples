using UnityEngine;
using System.Collections;

public class TrashBehavior : MonoBehaviour
{
		
	public GameObject pelaaja;
	public UnityEngine.UI.Text pistelasku;

	private bool maassa;
	private float siirretaanYlos;

	// Use this for initialization
	void Start ()
	{
		maassa = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (maassa && !pelaaja.GetComponent<PlayerBehaviour> ().Kannossa) {
			float apu = gameObject.transform.position.x;
			float vali = (pelaaja.transform.localScale.x / 2) * 2f;
			float pelaajaX = pelaaja.transform.position.x;

			if ((apu < (pelaajaX + vali)) && (apu > (pelaajaX - vali))) {
				maassa = false;
				kantoon ();
				pelaaja.GetComponent<PlayerBehaviour> ().Kannossa = true;
			}

		} else if (!maassa && pelaaja.GetComponent<PlayerBehaviour> ().Kannossa) {
			kantoon ();
		} else if (!maassa && !pelaaja.GetComponent<PlayerBehaviour> ().Kannossa) {
			pistelasku.GetComponent<PisteenLasku>().lisaaPiste();
			Destroy (gameObject);
		}
	}

	void kantoon ()
	{
		float vali = pelaaja.transform.localScale.x / 2;
		float pelaajaX = pelaaja.transform.position.x;

		bool oik = pelaaja.GetComponent<PlayerBehaviour> ().onkoOikea ();
		float posX = pelaajaX + vali;
		if (!oik) {
			posX = pelaajaX - vali;
		}
		
		gameObject.transform.position = new Vector3 (posX, pelaaja.transform.position.y - (pelaaja.transform.localScale.y / 2), gameObject.transform.position.z);
	}
}
