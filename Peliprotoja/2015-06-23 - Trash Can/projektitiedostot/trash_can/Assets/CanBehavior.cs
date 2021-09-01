using UnityEngine;
using System.Collections;

public class CanBehavior : MonoBehaviour
{

	public GameObject pelaaja;
	private float leveys;
	private float korkeus;
	private bool animationOn;
	private float muuttokerroin;
	private int aika;
	private int kulunutAika;
	private float leveysmuutos;
	private float korkeusmuutos;

	// Use this for initialization
	void Start ()
	{
		muuttokerroin = 0.8f;
		aika = 10;
		kulunutAika = 0;

		leveys = gameObject.transform.localScale.x;
		korkeus = gameObject.transform.localScale.y;
		animationOn = false;

		leveysmuutos = ((leveys / muuttokerroin) - leveys) / aika;
		korkeusmuutos = (korkeus - (korkeus * muuttokerroin)) / aika;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (pelaaja.GetComponent<PlayerBehaviour> ().Kannossa) {
			//if (Input.GetKey (KeyCode.S)) {
			float reuna = pelaaja.transform.position.x - ((pelaaja.transform.localScale.x / 2) * 2f);
			if (pelaaja.GetComponent<PlayerBehaviour> ().oikea) {
				reuna = pelaaja.transform.position.x + ((pelaaja.transform.localScale.x / 2) * 2f);
			}

			bool laitaRoskiin = (reuna < (gameObject.transform.position.x + (gameObject.transform.localScale.x / 2))) && 
				(reuna > (gameObject.transform.position.x - (gameObject.transform.localScale.x / 2)));

			if (laitaRoskiin) {
				gameObject.transform.localScale = new Vector3 (leveys / muuttokerroin, korkeus * muuttokerroin, gameObject.transform.localScale.z);
				animationOn = true;
				kulunutAika = 0;

				pelaaja.GetComponent<PlayerBehaviour> ().Kannossa = false;
			}
		}

		if (animationOn) {
			if (kulunutAika >= aika) {
				gameObject.transform.localScale = new Vector3 (leveys, korkeus, gameObject.transform.localScale.z);
	
				animationOn = false;
			} else {
				kulunutAika++;

				float leveysNyt = gameObject.transform.localScale.x;
				float korkeusNyt = gameObject.transform.localScale.y;

				gameObject.transform.localScale = new Vector3 (leveysNyt - leveysmuutos, korkeusNyt + korkeusmuutos, gameObject.transform.localScale.z);
			}
		}
	}
}
