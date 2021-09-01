using UnityEngine;
using System.Collections;

public class PlayerAmmoBehav : MonoBehaviour {

	private GameObject omistaja;
	private GameObject vihollinen;
	/*
	private GameObject oikeaSeina;
	private GameObject vasenSeina;
	private GameObject ylaSeina;
	private GameObject alaSeina; */
	private GameObject alusta;

	//private bool ammutaan;
	private Vector3 lentorata;
	//public Vector3 hiirenKohta;
	private float siirtyma;
	private bool osuttuPaatyyn;
	
	// Use this for initialization
	void Start () {
		//bool ammutaan = false;
		xPos = transform.position.x;
		yPos = transform.position.y;
		zPos = transform.position.z;
		siirtyma = 0.25f;
		
		//testi1 = "lol";
		//testi2 = "lel";
		
		osuttuPaatyyn = false;
		
		//BoxCollider boxc = oikeaSeina.AddComponent<BoxCollider> ();

		omistaja = GameObject.Find("PlayerDot");
		vihollinen = GameObject.Find ("EnemyDot");
		/*
		oikeaSeina = GameObject.Find ("RightWall");
		vasenSeina = GameObject.Find ("LeftWall");
		ylaSeina = GameObject.Find ("TopWall");
		alaSeina = GameObject.Find ("BottomWall"); */
		alusta = GameObject.Find ("Base");

		AsetaLentorata();
	}
	
	public void IsantaTuhottu(bool b) {
		transform.gameObject.SetActive(!b);
	}


	void AsetaLentorata() {
		//hiirenKohta = Camera.main.ScreenToWorldPoint(Input.mousePosition);//annaHiirenKoordinaatit();
		//ammutaan = true;
		//Vector3 yhd = hiirenKohta - transform.position;
		Vector3 apu = omistaja.GetComponent<MovePlayer> ().AnnaLentoRata ();
		lentorata = UnitVector(apu) * siirtyma;
	} 
	
	// Update is called once per frame
	void Update () {

		/*
		if (!ammutaan) {
			transform.position = omistaja.transform.position;
		} */
		
		//if (ammutaan) {
			if (!osuttuPaatyyn || (osuttuPaatyyn && transform.position.y > 0)) {
				siirraAmmusta ();
			} else {
				siirraAmmustaPelaajaan();
			}
		//}
	}
	
	public string testi1;
	public string testi2;/*
	void OnCollisionEnter (Collision col) {

		if(col.gameObject.name == oikeaSeina.name)
		{
			lentorata = new Vector3(- lentorata.x, lentorata.y, lentorata.z);
		}
	} */
	
	void siirraAmmustaPelaajaan() {
		Vector3 yhd  = UnitVector(omistaja.transform.position - transform.position) * siirtyma;
		
		float uusiPosX = transform.position.x + yhd.x;
		float uusiPosY = transform.position.y + yhd.y;
		
		if (omistaja.transform.position.y >= uusiPosY) {
			//ammutaan = false;
			//osuttuPaatyyn = false;
			TuhoaAmmus();
		}
		
		transform.position = new Vector3(uusiPosX,uusiPosY,zPos);
	}
	
	void siirraAmmusta() {
		float uusiPosX = transform.position.x + lentorata.x;
		
		if ((uusiPosX <= -(alusta.transform.localScale.x / 2) || (uusiPosX >= (alusta.transform.localScale.x / 2)))) {
			lentorata = new Vector3(-lentorata.x,lentorata.y,lentorata.z);
			uusiPosX = transform.position.x + lentorata.x;
			GetComponent<AudioSource>().Play();
		}
		
		float uusiPosY = transform.position.y + lentorata.y;
		
		if ((uusiPosY <= -(alusta.transform.localScale.y / 2) || (uusiPosY >= (alusta.transform.localScale.y / 2)))) {
			lentorata = new Vector3(lentorata.x,-lentorata.y,lentorata.z);
			uusiPosY = transform.position.y + lentorata.y;
			GetComponent<AudioSource>().Play();
			if (uusiPosY > 0) {
				osuttuPaatyyn = true;
			}
		}
		
		transform.position = new Vector3(uusiPosX,uusiPosY,zPos);

		GameObject[] vihollisAmmukset = GameObject.FindGameObjectsWithTag ("EnemyDisks");
		if (vihollisAmmukset.Length > 0) {
			for (int i = 0; i < vihollisAmmukset.Length; i++) {
				float etaisyysAmmukseen = vektorinPituus (- transform.position + vihollisAmmukset[i].transform.position);
				if (etaisyysAmmukseen < 1.0f) {
					//Destroy(vihollisAmmukset[i]);
					vihollisAmmukset[i].GetComponent<EnemyAmmoBehav>().TuhoaAmmus();
					//Destroy(gameObject);
					TuhoaAmmus();

					break;
				}
			}
		}

		if (vihollinen != null) {
			float etaisyysVastustajaan = vektorinPituus (- transform.position + vihollinen.transform.position);
			if (etaisyysVastustajaan < 1.25f) {
				if (vihollinen.GetComponent<MoveEnemy>().Tuhotaanko()) {
					Destroy(vihollinen);
				}
				alusta.GetComponent<AudioSource>().Play();
				TuhoaAmmus();
				//vihollinen.SetActive (false);
				//Destroy(vihollinen);
			}
		}
	}

	void TuhoaAmmus() {
		omistaja.GetComponent<MovePlayer>().VahennaAmmusMaaraa();
		Destroy(gameObject);
	}
	
	public float xPos;
	public float yPos;
	public float zPos;
	Vector3 UnitVector(Vector3 refer) {
		float jako = vektorinPituus(refer);
		float nX = refer.x / jako;
		float nY = refer.y / jako;
		return new Vector3 (nX, nY, zPos);
	}
	float vektorinPituus(Vector3 refer) {
		return Mathf.Sqrt (Mathf.Pow (refer.x, 2) + Mathf.Pow (refer.y, 2));
	}
}
