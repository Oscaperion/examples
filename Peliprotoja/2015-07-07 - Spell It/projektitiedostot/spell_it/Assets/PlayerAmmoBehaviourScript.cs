using UnityEngine;
using System.Collections;

public class PlayerAmmoBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.localPosition.x > 8) {
			Destroy(gameObject);
		}
	}
}
