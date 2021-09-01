using UnityEngine;
using System.Collections;


public class MoveEnemy : MonoBehaviour {

	public GameObject alusta;
	public GameObject ammus;
	private float alustaLeveys;
	private float alustaKorkeus;
	private float alustaX;
	private float alustaY;
	private bool suorita;
	private bool siirrytaan;
	private bool odota;
	private int odotusaika;
	private int siirtoaika;
	private int siirtosuunta;
	private int elamia;
	
	// Use this for initialization
	void Start () {
		alustaX = alusta.transform.position.x;
		alustaY = alusta.transform.position.y;
		alustaLeveys = alusta.transform.localScale.x;
		alustaKorkeus = alusta.transform.localScale.y;
		suorita = true;
		siirrytaan = false;
		odota = true;
		odotusaika = new System.Random ().Next (20, 61);
		siirtoaika = 0;
		siirtosuunta = 0;

		elamia = 2;
		//oikealle = true;
		
		xPos = alustaX;
		
		// Taking player's position
		yPos = transform.position.y;
		zPos = transform.position.z;
		
		siirtyma = 0.125f;
		viistosiirtyma = siirtyma * (1 / Mathf.Sqrt (2)); // Unit vector for <1,1>
	}
	
	// Update is called once per frame
	void Update () {
		if (suorita && !odota) {
			siirtoaika = siirtoaika - 1;
			if (siirrytaan) {
				siirraHahmoa(siirtosuunta);//Siiretaanko ();
			}
			if (siirtoaika <= 0) {
				odota = true;
				siirrytaan = false;
				odotusaika = new System.Random ().Next (20, 61);
			}
		} else if (odota) {
			odotusaika = odotusaika - 1;
			if (odotusaika <= 0) {
				System.Random ran = new System.Random ();
				odota = false;
				siirrytaan = true;
				siirtoaika = ran.Next (5, 21);
				siirtosuunta = ran.Next (1,8);
			}
		}
	}
	
	public float xPos;
	public float yPos;
	public float zPos;
	public Vector3 valimatka;
	public float vali_seuranta1 = 0;
	public float vali_seuranta2 = 0;
	public float kerto_seuranta = 0;
	
	void siirraHahmoa(int siirtoluku) {
		Vector3 siirtyma = teeVektori (siirtoluku);
		float vali = vektorinPituus(transform.position - alusta.transform.position);
		float kerto = (alustaLeveys / 2) + (transform.localScale.x / 2);
		kerto_seuranta = kerto;
		valimatka = alusta.transform.position - siirtyma;
		vali_seuranta1 = vali;
		if (vali > kerto) {
			elamia = elamia - 1;
			/*if (elamia <= 0) {
				suorita = false; 
				transform.gameObject.SetActive(false);
			} else { */
			//alusta.SetActive (false);
			transform.gameObject.SetActive(false);
			//suorita = false; 
				//siirtoaika = 0;
				//siirtyma = new Vector3(alustaX, alustaY,zPos);
			//}
		} /*else {
			alusta.SetActive(true);
		}*/
		Vector3 apu = ammus.transform.position;
		transform.position = siirtyma;
		ammus.transform.position = apu;
		vali_seuranta2 = vali;
	}
	
	float siirtyma;
	float viistosiirtyma;
	Vector3 teeVektori(int s) {
		if (s == 0) {
			yPos = yPos + siirtyma;
		} else if (s == 1) {
			xPos = xPos - viistosiirtyma;
			yPos = yPos + viistosiirtyma;
		} else if (s == 2) {
			xPos = xPos - siirtyma;
		} else if (s == 3) {
			xPos = xPos - viistosiirtyma;
			yPos = yPos - viistosiirtyma;
		} else if (s == 4) {
			yPos = yPos - siirtyma;
		} else if (s == 5) {
			xPos = xPos + viistosiirtyma;
			yPos = yPos - viistosiirtyma;
		} else if (s == 6) {
			xPos = xPos + siirtyma;
		} else if (s == 7) {
			xPos = xPos + viistosiirtyma;
			yPos = yPos + viistosiirtyma;
		}
		return new Vector3(xPos, yPos, zPos);
	}
	
	float vektorinPituus(Vector3 refer) {
		return Mathf.Sqrt (Mathf.Pow (refer.x, 2) + Mathf.Pow (refer.y, 2));
	}
	
	Vector3 UnitVector(Vector3 refer) {
		float jako = vektorinPituus(refer);
		float nX = refer.x / jako;
		float nY = refer.y / jako;
		return new Vector3 (nX, nY, zPos);
	}

}
