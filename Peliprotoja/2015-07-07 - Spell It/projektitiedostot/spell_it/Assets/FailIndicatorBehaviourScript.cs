using UnityEngine;
using System.Collections;

public class FailIndicatorBehaviourScript : MonoBehaviour {

	private bool noFail = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool isNoFail() {
		return noFail;
	}

	public void MuutaTilaa() {
		if (noFail) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("fail");
			noFail = false;
		} else {
			gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("no_fail");
			noFail = true;
		}
	}
}
