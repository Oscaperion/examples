using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

	public GameObject enemyChar;
	
	private int odota = 50;
	private int odotaLasku;
	private float xPos;
	//private bool muuta = true;
	private float siirtymaNopeusMin = 4f;
	private float siirtymaNopeusMax = 6f;
	private Vector3 oikSiirtyma;
	private Vector3 vasSiirtyma;

	// Use this for initialization
	void Start () {
		odotaLasku = odota;
		xPos = enemyChar.transform.localPosition.x;
		enemyChar.GetComponent<UnityStandardAssets.Utility.AutoMoveAndRotate> ().moveUnitsPerSecond.value = new Vector3 (0f, 0f, 0f);
		//oikSiirtyma = new Vector3 (siirtymaNopeus, 0f, 0f);
		//vasSiirtyma = new Vector3 (-siirtymaNopeus, 0f, 0f);
	}

	//private GameObject newEnm;
	// Update is called once per frame
	void Update () {
		if (odotaLasku > 0) {
			odotaLasku--;
		} else {
			odotaLasku = odota;
			//moveUnitsPerSecond.value = new Vector3 (7f, 0f, 0f);
			Vector3 siirtyma = arvoSiirtyma(); 
			float apu = xPos;
			if (siirtyma.x < 0) {
				apu = -xPos;
			}

			//http://forum.unity3d.com/threads/create-a-copy-of-not-reference-to-prefab-gameobject-in-script.254471/
			GameObject newEnm = GameObject.Instantiate(enemyChar, new Vector3 (apu, enemyChar.transform.localPosition.y, enemyChar.transform.localPosition.z), Quaternion.identity) as GameObject;

			//if (siirtyma.x < 0) {
			newEnm.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>(arvoVari());
			//}

			/*
			if (!muuta) {
				siirtyma = vasSiirtyma;
				newEnm.GetComponent<EnemyBehavior>().oikealle = false;
				muuta = true;
			} else {
				muuta = false;
			} */
			newEnm.GetComponent<UnityStandardAssets.Utility.AutoMoveAndRotate> ().moveUnitsPerSecond.value = siirtyma;
			//newEnm.tag = "NotMain";

			//Instantiate (newEnm, new Vector3 (xPos, enemyChar.transform.localPosition.y, enemyChar.transform.localPosition.z), Quaternion.identity);
		}
	}

	string arvoVari() {
		int valinta = new System.Random ().Next (0, 3);
		if (valinta == 0) {
			return "npcSprite1";
		} if (valinta == 1) {
			return "npcSprite2";
		} return "npcSprite3";
	}

	Vector3 arvoSiirtyma() {
		int valinta = new System.Random ().Next (0, 2);
		if (valinta == 0) {
			return new Vector3 (arvoSiirtymaNopeus(), 0f, 0f);
			//return oikSiirtyma;
		}
		return new Vector3 (-arvoSiirtymaNopeus(), 0f, 0f);
		//return vasSiirtyma;
	}

	float arvoSiirtymaNopeus() {
		int kerto = new System.Random ().Next (1, 100);
		float anto = ((siirtymaNopeusMax - siirtymaNopeusMin) / 100) * kerto;
		return anto + siirtymaNopeusMin;
	}
}
