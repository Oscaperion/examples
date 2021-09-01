using UnityEngine;
using System.Collections;

public class WallBehaviourScript : MonoBehaviour {

	private int hp = 2;

	// Use this for initialization
	void Start () {
		//GameObject.Find("Main Camera").SetActive(true);
		gameObject.GetComponent<Renderer> ().material.color = Color.green;
		//gameObject.renderer.material.color = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool Tuhoutuuko() {
		if (hp <= 0) {
			return true;
		} 
		return false;
	}

	public void vahennaHP() {
		if (hp == 2) {
			gameObject.GetComponent<Renderer> ().material.color = Color.yellow;
		} if (hp == 1) {
			gameObject.GetComponent<Renderer> ().material.color = Color.red;
		} 
		hp = hp - 1;
	}
}
