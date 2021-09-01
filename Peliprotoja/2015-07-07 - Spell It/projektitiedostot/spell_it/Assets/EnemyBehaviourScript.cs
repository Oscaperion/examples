using UnityEngine;
using System.Collections;

public class EnemyBehaviourScript : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		//gameObject.GetComponent<Renderer> ().material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update ()
	{
		GameObject apu = GameObject.Find ("Wall");
		if (apu != null) {
			if ((gameObject.transform.localPosition.x - 0.75f) < apu.transform.localPosition.x) {
				if (!apu.GetComponent<WallBehaviourScript> ().Tuhoutuuko ()) {
					Destroy (gameObject);
					// Muuta seinän väriä!
					apu.GetComponent<WallBehaviourScript> ().vahennaHP ();
				} else {
					Destroy(apu);
					GameObject.Find ("PlayerDot").GetComponent<PlayerBehaviour>().PeliLoppui();
					//string pros = 
					GameObject.Find ("NappailyTulos").GetComponent<TextMesh> ().text = "Näppäilytarkkuutesi oli " + string.Format ("{0:0.00}", GameObject.Find ("PlayerDot").GetComponent<PlayerBehaviour>().NappailyTarkkuus()) + " %";
					GameObject.Find("Main Camera").transform.localPosition = new Vector3(-13f, 0f, -10f);
				
					GameObject.Find("HavisitTeksti").GetComponent<PaluuAlkuun>().PeliLoppui();
					//GameObject.Find("Result Camera").SetActive(true);
					// GameObject.Find ("Main ")
				}
			}
		}

		GameObject[] apu2 = GameObject.FindGameObjectsWithTag ("Player");
		if (apu2.Length > 0) {
			for (int i = 0; i < apu2.Length; i++) {
				if ((gameObject.transform.localPosition.y == apu2 [i].transform.localPosition.y) && (gameObject.transform.localPosition.x < apu2 [i].transform.localPosition.x)) {
					Destroy (apu2 [i]);
					Destroy (gameObject);
				}
			}
		}
	}
}
