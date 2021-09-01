using UnityEngine;
using System.Collections;

public class TarkkuusMittari : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PaivitaTekstia(float pros) {
		string apu = "Näppäilytarkkuus: ";
		gameObject.GetComponent<TextMesh> ().text = apu + string.Format ("{0:0.00}", pros) + " %";
	}
}
