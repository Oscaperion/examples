using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

	public bool oikea;
	private bool painettuna;
	private float siirtyma;
	public GameObject pohja;
	private bool _kannossa;

	public bool Kannossa {
		get { return _kannossa; }
		set { _kannossa = value; }
	}

	public bool onkoOikea() {
		return oikea;
	}

	// Use this for initialization
	void Start () {
		siirtyma = 0.10f;

		oikea = true;
		painettuna = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Horizontal")) {
			painettuna = true;
			if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
				oikea = false;
			}
			if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
				oikea = true;
			}
		} else {
			painettuna = false;
		}

		if (painettuna) {
			float raja = pohja.transform.localScale.x / 2;
			Vector3 siirrytaan;
			if (oikea) {
				if (!(raja < gameObject.transform.position.x)) {
					siirrytaan = new Vector3 (gameObject.transform.position.x + siirtyma, gameObject.transform.position.y, gameObject.transform.position.z);
					gameObject.transform.position = siirrytaan;
				}
			} else {
				if (!(-raja > gameObject.transform.position.x)) {
					siirrytaan = new Vector3 (gameObject.transform.position.x - siirtyma, gameObject.transform.position.y, gameObject.transform.position.z);
					gameObject.transform.position = siirrytaan;
				}
			}
		}
	}
}