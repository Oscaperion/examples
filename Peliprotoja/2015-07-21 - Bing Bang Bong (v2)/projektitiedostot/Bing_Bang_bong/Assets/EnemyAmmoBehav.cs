using UnityEngine;
using System.Collections;

public class EnemyAmmoBehav : MonoBehaviour {

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
		
		vihollinen = GameObject.Find("PlayerDot");
		omistaja = GameObject.Find ("EnemyDot");
		/*
		oikeaSeina = GameObject.Find ("RightWall");
		vasenSeina = GameObject.Find ("LeftWall");
		ylaSeina = GameObject.Find ("TopWall");
		alaSeina = GameObject.Find ("BottomWall"); */
		alusta = GameObject.Find ("Base");

		AsetaLentoRata ();
	}

	void AsetaLentoRata() {
		Vector3 pohja = - omistaja.transform.position + vihollinen.transform.position;
		float viisto = (float) ((new System.Random ().Next (-40, 41)) / 10);
		//Vector3 rata = new Vector3 (pohja.x + viisto, pohja.y, pohja.z);

		lentorata = UnitVector (new Vector3 (pohja.x + viisto, pohja.y, pohja.z)) * siirtyma;
	}
	
	// Update is called once per frame
	void Update () {
		if (!osuttuPaatyyn || (osuttuPaatyyn && transform.position.y < 0)) {
			siirraAmmusta ();
		} else {
			siirraAmmustaPelaajaan();
		}
	}

	void siirraAmmustaPelaajaan() {



		Vector3 yhd  = UnitVector(omistaja.transform.position - transform.position) * siirtyma;
		
		float uusiPosX = transform.position.x + yhd.x;
		float uusiPosY = transform.position.y + yhd.y;
		
		if (omistaja.transform.position.y <= uusiPosY) {
			/*odota = true;
			ammutaan = false;
			osuttuPaatyyn = false;
			odotusaika = new System.Random ().Next (20, 61);*/
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
			if (uusiPosY < 0) {
				osuttuPaatyyn = true;
			}
		}
		
		transform.position = new Vector3(uusiPosX,uusiPosY,zPos);

		if (vihollinen != null) {
			float etaisyysVastustajaan = vektorinPituus (- transform.position + vihollinen.transform.position);
			if (etaisyysVastustajaan < 1.25f) {
				if (vihollinen.GetComponent<MovePlayer>().Tuhotaanko()) {
					Destroy(vihollinen);
				}
				alusta.GetComponent<AudioSource>().Play();
				TuhoaAmmus();
				
				//PlayerDiskShooter other = (PlayerDiskShooter) vihollinen.GetComponent(typeof(PlayerDiskShooter));
				//other.IsantaTuhottu(true);
				//Destroy(vihollinen);
			}
		}
	}

	public void TuhoaAmmus() {
		omistaja.GetComponent<MoveEnemy>().VahennaAmmusMaaraa();
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
