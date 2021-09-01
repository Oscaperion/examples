using UnityEngine;
using System.Collections;

public class SpawnTrash : MonoBehaviour {

	public GameObject roska;
	public GameObject pohja;

	private int aikavali;

	// Use this for initialization
	void Start () {
		aikavali = arvoAika();
	}
	
	// Update is called once per frame
	void Update () {
		if (aikavali > 0) {
			aikavali--;
		} else {
			//if (Input.GetKeyDown (KeyCode.W)) {
			if (!(((-pohja.transform.localScale.x / 2) > gameObject.transform.position.x) || ((pohja.transform.localScale.x / 2) < gameObject.transform.position.x))) {
				float apu = gameObject.transform.localPosition.x;
				//http://forum.unity3d.com/threads/create-a-copy-of-not-reference-to-prefab-gameobject-in-script.254471/
				//GameObject newTrash = GameObject.
				Instantiate (roska, new Vector3 (apu, roska.transform.localPosition.y, roska.transform.localPosition.z), Quaternion.identity); //as GameObject;
			}
			aikavali = arvoAika();
		}
	}

	int arvoAika() {
		return new System.Random ().Next (100, 200);
	}
}
