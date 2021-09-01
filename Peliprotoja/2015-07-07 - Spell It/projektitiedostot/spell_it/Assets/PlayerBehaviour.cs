using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour
{
	public GameObject ammus;
	private Vector3 ammuksenSiirtyma;
	public int nykyinenRivi;
	public float tekstienEtaisyys;
	public int virheidenMaara = 0;
	public int virheidenMaxMaara;
	private float naputtelynMaara = 0;
	private float naputteluaOikein = 0;
	private int alas = 0;
	private int ylos = 0;
	private int ammu = 0;
	private bool peliLoppu = false;
	private KeyCode[] nappainLista = {
		KeyCode.A,
		KeyCode.B,
		KeyCode.C,
		KeyCode.D,
		KeyCode.E,
		KeyCode.F,
		KeyCode.G,
		KeyCode.H,
		KeyCode.I,
		KeyCode.J,
		KeyCode.K,
		KeyCode.L,
		KeyCode.M, 
		KeyCode.N,
		KeyCode.O,
		KeyCode.P,
		KeyCode.Q,
		KeyCode.R,
		KeyCode.S,
		KeyCode.T,
		KeyCode.U,
		KeyCode.V,
		KeyCode.W,
		KeyCode.X,
		KeyCode.Y,
		KeyCode.Z
	};

	public int AnnaMaxMaara ()
	{
		return virheidenMaxMaara;
	}

	// Use this for initialization
	void Start ()
	{
		//gameObject.GetComponent<Renderer> ().material.color = Color.green;

		ammuksenSiirtyma = ammus.GetComponent<UnityStandardAssets.Utility.AutoMoveAndRotate> ().moveUnitsPerSecond.value;

		ammus.GetComponent<UnityStandardAssets.Utility.AutoMoveAndRotate> ().moveUnitsPerSecond.value = new Vector3 (0f, 0f, 0f);

		SiirraHahmoa (nykyinenRivi);

		ArvoNappaimet ();

		MuutaTeksteja ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (!peliLoppu) {
			TarkastaNapinPainallus ();
		}
	}

	private void TarkastaNapinPainallus ()
	{
		if (Input.GetKeyDown (nappainLista [alas]) || Input.GetKeyDown (nappainLista [ylos]) || Input.GetKeyDown (nappainLista [ammu])) {
			if (Input.GetKeyDown (nappainLista [alas])) {
				nykyinenRivi = nykyinenRivi + 1;
				ArvoAlas ();
				MuutaTeksteja ();
				
				SiirraHahmoa (nykyinenRivi);
			} else if (Input.GetKeyDown (nappainLista [ylos])) {
				nykyinenRivi = nykyinenRivi - 1;
				ArvoYlos ();
				MuutaTeksteja ();
				
				SiirraHahmoa (nykyinenRivi);
			} else if (Input.GetKeyDown (nappainLista [ammu])) {
				//nykyinenRivi = nykyinenRivi
				ArvoAmpuma ();
				MuutaTeksteja ();
				
				Ammu ();
			}
			naputtelynMaara++;
			naputteluaOikein++;
			PaivitaTarkkuutta ();
			
			if (GameObject.Find ("Vinkki") != null) {
				GameObject.Find ("Vinkki").SetActive (false);
			}
		} else {
			bool lisaaVirhe = false;
			int tarkistus = 0;
			
			while (!lisaaVirhe && tarkistus < nappainLista.Length) {
				if (tarkistus != alas && tarkistus != ylos && tarkistus != ammu && Input.GetKeyDown (nappainLista [tarkistus])) {
					lisaaVirhe = true;
				} else {
					tarkistus++;
				}
			}
			
			if (lisaaVirhe) {
				virheidenMaara++;
				naputtelynMaara++;
				PaivitaTarkkuutta ();
				GameObject.Find ("FailIndicators").GetComponent<FailIndicatorUpdater> ().MuutaIndikaattori ();
				LisataankoVaikeutta ();
			}
		}
		
		if (odotusAikaaJaljella >= 1) {
			odotusAikaaJaljella--;
			if (odotusAikaaJaljella == 0) {
				GameObject.Find ("FailIndicators").GetComponent<FailIndicatorUpdater> ().MuutaTakaisin ();
			}
		}
	}

	public void PeliLoppui ()
	{
		peliLoppu = true;
	}

	private int odotusAika = 20;
	private int odotusAikaaJaljella = 0;

	private void LisataankoVaikeutta ()
	{
		if (virheidenMaara >= virheidenMaxMaara) {
			GameObject.Find ("Wall").GetComponent<SpawnEnemy> ().LisaaSpawnTiheytta ();
			virheidenMaara = 0;
			odotusAikaaJaljella = odotusAika;
		}
	}

	private void PaivitaTarkkuutta ()
	{
		GameObject.Find ("Tarkkuusosoitin").GetComponent<TarkkuusMittari> ().PaivitaTekstia (NappailyTarkkuus ());
	}

	public float NappailyTarkkuus ()
	{
		return (naputteluaOikein / naputtelynMaara) * 100;
	}

	private void Ammu ()
	{
		GameObject newAmmo = GameObject.Instantiate (ammus, gameObject.transform.localPosition, Quaternion.identity) as GameObject;
		newAmmo.GetComponent<Renderer> ().material.color = Color.cyan;
		newAmmo.GetComponent<UnityStandardAssets.Utility.AutoMoveAndRotate> ().moveUnitsPerSecond.value = ammuksenSiirtyma;
	}

	// Vaihtaa pelaajahahmon riviä rivinumeron mukaan, 1 on ylin ja 4 on alin.
	// Siirtää pelaajan 1. riville, jos numero on isompi kuin 4, tai 4. riville, jos numero on pienempi kuin 1.
	private void SiirraHahmoa (int riviN)
	{
		if (riviN < 1) {
			riviN = 4;
			nykyinenRivi = riviN;
		} else if (riviN > 4) {
			riviN = 1;
			nykyinenRivi = riviN;
		}

		string rivi = "Line" + riviN;
		gameObject.transform.localPosition = new Vector3 (gameObject.transform.localPosition.x, GameObject.Find (rivi).transform.localPosition.y, gameObject.transform.localPosition.z);


		SiirraTeksteja ();
	}

	private void SiirraTeksteja ()
	{
		GameObject.Find ("MoveUpText").transform.localPosition = new Vector3 (gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + tekstienEtaisyys, gameObject.transform.localPosition.z);
		GameObject.Find ("MoveDownText").transform.localPosition = new Vector3 (gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - tekstienEtaisyys, gameObject.transform.localPosition.z);
		GameObject.Find ("ShootEnemyText").transform.localPosition = new Vector3 (gameObject.transform.localPosition.x + tekstienEtaisyys, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
	}

	private void MuutaTeksteja ()
	{
		GameObject.Find ("MoveUpText").GetComponent<TextMesh> ().text = nappainLista [ylos].ToString ().ToUpper ();
		GameObject.Find ("MoveDownText").GetComponent<TextMesh> ().text = nappainLista [alas].ToString ().ToUpper ();
		GameObject.Find ("ShootEnemyText").GetComponent<TextMesh> ().text = nappainLista [ammu].ToString ().ToUpper ();
	}

	private void ArvoNappaimet ()
	{
		ArvoAlas ();
		ArvoYlos ();
		ArvoAmpuma ();
	}

	private void ArvoAlas ()
	{
		System.Random arpomista = new System.Random ();
		
		alas = arpomista.Next (0, nappainLista.Length);

		while (alas == ylos || alas == ammu) {
			alas = arpomista.Next (0, nappainLista.Length);
		}
	}

	private void ArvoYlos ()
	{
		System.Random arpomista = new System.Random ();
		
		ylos = arpomista.Next (0, nappainLista.Length);

		while (ylos == alas || ylos == ammu) {
			ylos = arpomista.Next (0, nappainLista.Length);
		}
	}

	private void ArvoAmpuma ()
	{
		System.Random arpomista = new System.Random ();
		
		ammu = arpomista.Next (0, nappainLista.Length);

		while (ammu == alas || ammu == ylos) {
			ammu = arpomista.Next (0, nappainLista.Length);
		}
	}
}
