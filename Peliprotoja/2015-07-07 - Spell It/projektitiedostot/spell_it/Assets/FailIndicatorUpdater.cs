using UnityEngine;
using System.Collections;

public class FailIndicatorUpdater : MonoBehaviour {

	public GameObject SourceIndicator;
	private GameObject[] indikaattorit;
	private int maxMaara;
	private int kasittelyssa;

	// Use this for initialization
	void Start () {
		maxMaara = GameObject.Find ("PlayerDot").GetComponent<PlayerBehaviour>().AnnaMaxMaara();
		kasittelyssa = maxMaara;
		float xSiirtyma = 0.75f;

		indikaattorit = new GameObject[maxMaara];
		indikaattorit [0] = SourceIndicator;

		for (int i = 1; i < maxMaara; i++) {
			GameObject newIndi = GameObject.Instantiate(SourceIndicator, new Vector3 (SourceIndicator.transform.position.x - (xSiirtyma * i), SourceIndicator.transform.position.y, SourceIndicator.transform.position.z), Quaternion.identity) as GameObject;
			indikaattorit[i] = newIndi;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MuutaIndikaattori() {
		kasittelyssa = kasittelyssa - 1;
		if (kasittelyssa >= 0) {
			indikaattorit [kasittelyssa].GetComponent<FailIndicatorBehaviourScript> ().MuutaTilaa ();
		}
	}

	public void MuutaTakaisin() {
		for (int i = 0; i < maxMaara; i++) {
			if (indikaattorit[i].GetComponent<FailIndicatorBehaviourScript>().isNoFail() == false) {
				indikaattorit[i].GetComponent<FailIndicatorBehaviourScript>().MuutaTilaa();
			}
		}
		kasittelyssa = maxMaara;
	}
}
