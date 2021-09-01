using UnityEngine;
using System.Collections;

public class PlayerDiskShooter : MonoBehaviour {

	public GameObject omistaja;
	public GameObject vihollinen;
	public GameObject oikeaSeina;
	public GameObject vasenSeina;
	public GameObject ylaSeina;
	public GameObject alaSeina;
	public GameObject alusta;

	private bool ammutaan;
	private Vector3 lentorata;
	public Vector3 hiirenKohta;
	private float siirtyma;
	private bool osuttuPaatyyn;

	// Use this for initialization
	void Start () {
		bool ammutaan = false;
		xPos = transform.position.x;
		yPos = transform.position.y;
		zPos = transform.position.z;
		siirtyma = 0.25f;

		testi1 = "lol";
		testi2 = "lel";

		osuttuPaatyyn = false;

		//BoxCollider boxc = oikeaSeina.AddComponent<BoxCollider> ();
	}

	public void IsantaTuhottu(bool b) {
		transform.gameObject.SetActive(!b);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire1") && !ammutaan) {
			hiirenKohta = Camera.main.ScreenToWorldPoint(Input.mousePosition);//annaHiirenKoordinaatit();
			ammutaan = true;
			Vector3 yhd = hiirenKohta - transform.position;
			lentorata = UnitVector(yhd) * siirtyma;
		}

		if (!ammutaan) {
			transform.position = omistaja.transform.position;
		}

		if (ammutaan) {
			if (!osuttuPaatyyn || (osuttuPaatyyn && transform.position.y > 0)) {
				siirraAmmusta ();
			} else {
				siirraAmmustaPelaajaan();
			}
		}
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
			ammutaan = false;
			osuttuPaatyyn = false;

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
			if (uusiPosY > 0) {
				osuttuPaatyyn = true;
			}
		}

		transform.position = new Vector3(uusiPosX,uusiPosY,zPos);

		float etaisyysVastustajaan = vektorinPituus(- transform.position + vihollinen.transform.position);
		if (etaisyysVastustajaan < 1.25f) {
			vihollinen.SetActive(false);
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
