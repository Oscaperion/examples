using UnityEngine;
using System.Collections;

public class EnemyDiskShooter : MonoBehaviour {

	// Ammuksen omistaja
	public GameObject omistaja;
	// Ammuksen vihollinen
	public GameObject vihollinen;
	public GameObject oikeaSeina;
	public GameObject vasenSeina;
	public GameObject ylaSeina;
	public GameObject alaSeina;
	public GameObject alusta;

	/*
	public AudioClip amp;
	public AudioSource kimp;
	private AudioSource lol;*/
	
	private bool ammutaan;
	private Vector3 lentorata;
	public Vector3 hiirenKohta;
	private float siirtyma;
	public bool osuttuPaatyyn;

	private bool odota;
	private int odotusaika;
	
	// Use this for initialization
	void Start () {
		//lol = GetComponent<AudioSource>();
		bool ammutaan = false;
		xPos = transform.position.x;
		yPos = transform.position.y;
		zPos = transform.position.z;
		siirtyma = 0.25f;
		
		testi1 = "lol";
		testi2 = "lel";


		
		osuttuPaatyyn = false;
		ammutaan = false;

		odota = true;
		odotusaika = new System.Random ().Next (20, 61);
	}
	
	// Update is called once per frame
	void Update () {
		if (vihollinen.activeSelf == true) {
			if (odota) {
				transform.position = omistaja.transform.position;
				odotusaika = odotusaika - 1;
				if (odotusaika <= 0) {
					odota = false;
					//ammutaan = true;
					//Vector3 yhd = - transform.position + vihollinen.transform.position;
					lentorata = UnitVector (- transform.position + vihollinen.transform.position) * siirtyma;

				}
			} else {
				ammu ();
				//lol.Play();
			}
		} else {
			transform.position = omistaja.transform.position;
		}
		/*if (Input.GetButton ("Fire1") && !ammutaan) {
			hiirenKohta = Camera.main.ScreenToWorldPoint(Input.mousePosition);//annaHiirenKoordinaatit();
			ammutaan = true;
			Vector3 yhd = hiirenKohta - transform.position;
			lentorata = UnitVector(yhd) * siirtyma;
		}*/
	}

	void ammu() {
		//if (ammutaan) {
			if (!osuttuPaatyyn || (osuttuPaatyyn && transform.position.y < 0)) {
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

		if (omistaja.transform.position.y <= uusiPosY) {
			odota = true;
			ammutaan = false;
			osuttuPaatyyn = false;
			odotusaika = new System.Random ().Next (20, 61);
		} 
		
		transform.position = new Vector3(uusiPosX,uusiPosY,zPos);
	}
	
	void siirraAmmusta() {
		float uusiPosX = transform.position.x + lentorata.x;
		
		if ((uusiPosX <= -(alusta.transform.localScale.x / 2) || (uusiPosX >= (alusta.transform.localScale.x / 2)))) {
			lentorata = new Vector3(-lentorata.x,lentorata.y,lentorata.z);
			uusiPosX = transform.position.x + lentorata.x;
		}
		
		float uusiPosY = transform.position.y + lentorata.y;
		
		if ((uusiPosY <= -(alusta.transform.localScale.y / 2) || (uusiPosY >= (alusta.transform.localScale.y / 2)))) {
			lentorata = new Vector3(lentorata.x,-lentorata.y,lentorata.z);
			uusiPosY = transform.position.y + lentorata.y;
			if (uusiPosY < 0) {
				osuttuPaatyyn = true;
			}
		}
		
		transform.position = new Vector3(uusiPosX,uusiPosY,zPos);

		float etaisyysVastustajaan = vektorinPituus(- transform.position + vihollinen.transform.position);
		if (etaisyysVastustajaan < 1.25f) {
			vihollinen.SetActive(false);

			//PlayerDiskShooter other = (PlayerDiskShooter) vihollinen.GetComponent(typeof(PlayerDiskShooter));
			//other.IsantaTuhottu(true);
			//Destroy(vihollinen);
		}
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
