using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

	public GameObject alusta;
	public GameObject ammus;
	private float alustaLeveys;
	private float alustaKorkeus;
	private float alustaX;
	private float alustaY;
	private bool suorita;

	// Use this for initialization
	void Start () {
		alustaX = alusta.transform.position.x;
		alustaY = alusta.transform.position.y;
		alustaLeveys = alusta.transform.localScale.x;
		alustaKorkeus = alusta.transform.localScale.y;
		suorita = true;

		//oikealle = true;
		
		xPos = alustaX;

		// Taking player's position
		yPos = transform.position.y;
		zPos = transform.position.z;

		siirtyma = 0.125f;
		viistosiirtyma = siirtyma * (1 / Mathf.Sqrt (2)); // Unit vector for <1,1>

		GameObject soundObject = GameObject.Find("");
	}
	
	// Update is called once per frame
	void Update () {
		if (suorita) {
			//siirraHahmoa(6);
			Siiretaanko();
		}
	}

	void Siiretaanko() {
		int painettuna = 0;
		//bool onPainettu = false;
		bool oikea = false;
		bool vasen = false;
		bool ylos = false;
		bool alas = false;

		if (Input.GetButton ("Horizontal"))
		{
			if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
			{
				oikea = true;
				painettuna++;
			}
			if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
			{
				vasen = true;
				painettuna++;
			}
		} 

		if (Input.GetButton ("Vertical")) {
			if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
				ylos = true;
				painettuna++;
			}
			if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
				alas = true;
				painettuna++;
			}
		}

		bool siirretaan = true;
		int siirrytaan = 8;

		if ((painettuna > 2) && !(vasen && oikea) && !(ylos && alas)) {
			siirretaan = false;
		} else {
			if (ylos && oikea) {
				siirrytaan = 1;
			} else if (ylos && vasen) {
				siirrytaan = 7;
			} else if (alas && vasen) {
				siirrytaan = 5;
			} else if (alas && oikea) {
				siirrytaan = 3;
			} else if (ylos) {
				siirrytaan = 0;
			} else if (alas) {
				siirrytaan = 4;
			} else if (vasen) {
				siirrytaan = 6;
			} else if (oikea) {
				siirrytaan = 2;
			}
		}

		if (siirretaan && siirrytaan >= 0 && siirrytaan <= 7) {
			siirraHahmoa(siirrytaan);
		}
	}

	public float xPos;
	public float yPos;
	public float zPos;
	//private bool oikealle;
	public Vector3 valimatka;
	public float vali_seuranta1 = 0;
	public float vali_seuranta2 = 0;
	public float kerto_seuranta = 0;

	void siirraHahmoa(int siirtoluku) {
		//float siirtyma = 0.25f;
		Vector3 siirrytaan = teeVektori (siirtoluku);
		float vali = vektorinPituus(transform.position - alusta.transform.position);
		float kerto = (alustaLeveys / 2) + (transform.localScale.x / 2);
		kerto_seuranta = kerto;
		valimatka = alusta.transform.position - siirrytaan;
		vali_seuranta1 = vali;
		if (vali > kerto) {
			//vali = kerto;
			//Vector3 nV = siirrytaan - alusta.transform.position;
			//nV = UnitVector(nV) * kerto;

			//siirrytaan = alusta.transform.position + nV;
			//nV = nV * kerto;

			//float jako = Mathf.Sqrt(Mathf.Pow(valimatka.x,2) + Mathf.Pow(valimatka.y,2));
			//float uusiX = kerto * (valimatka.x / jako);
			//float uusiY = (kerto * (valimatka.y / jako)) + alusta.transform.position.y;
			//transform.position = siirrytaan;//new Vector3(uusiX, uusiY, zPos);

			transform.gameObject.SetActive (false);
			//suorita = false;
		} else {
			alusta.SetActive(true);
			Vector3 apu = ammus.transform.position;
			transform.position = siirrytaan;
			ammus.transform.position = apu;
		}
		vali_seuranta2 = vali;
	}

	float siirtyma;
	float viistosiirtyma;
	Vector3 teeVektori(int s) {
		//siirtyma = 0.25f;
		//viistosiirtyma = siirtyma * (1 / Mathf.Sqrt (2)); // Unit vector for <1,1>

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

	/*
	bool voikoSiirtaa(float raja, float pelaaja) {
		if (!oikealle) {
			if (pelaaja <= raja) {
				return false;
			}
		} else { if (pelaaja >= raja) {return false;
			}
		}return true;
	}*/
}
