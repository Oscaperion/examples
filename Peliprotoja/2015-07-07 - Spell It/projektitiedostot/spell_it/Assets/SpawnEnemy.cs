using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemy : MonoBehaviour {

	public double odota;
	//public float nopeus;
	private double odotaLasku;
	private Vector3 siirtyma;
	public GameObject originalEnemy;

	// Use this for initialization
	void Start () {
		siirtyma = originalEnemy.GetComponent<UnityStandardAssets.Utility.AutoMoveAndRotate> ().moveUnitsPerSecond.value;
		odotaLasku = odota;
		originalEnemy.GetComponent<UnityStandardAssets.Utility.AutoMoveAndRotate> ().moveUnitsPerSecond.value = new Vector3 (0f, 0f, 0f);
		//siirtyma = new Vector3 (-nopeus, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (odotaLasku > 0) {
			odotaLasku--;
		} else {
			odotaLasku = odota;
			
			//http://forum.unity3d.com/threads/create-a-copy-of-not-reference-to-prefab-gameobject-in-script.254471/
			GameObject newEnm = GameObject.Instantiate(originalEnemy, arvoRivi(), Quaternion.identity) as GameObject;

			newEnm.GetComponent<UnityStandardAssets.Utility.AutoMoveAndRotate> ().moveUnitsPerSecond.value = siirtyma;
		}
	}

	Vector3 arvoRivi() {
		string apu = "Line" + new System.Random ().Next(1,5);
		return new Vector3 (originalEnemy.transform.localPosition.x, GameObject.Find (apu).transform.localPosition.y, originalEnemy.transform.localPosition.z);
	}

	public void LisaaSpawnTiheytta() {
		odota = odota / 2;
		odotaLasku = odotaLasku - odota;
	}
}
