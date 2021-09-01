using UnityEngine;
using System.Collections;

public class PoisPelista : MonoBehaviour {

	public UnityEngine.UI.Button ExitNappi;

	// Use this for initialization
	void Start () {
		ExitNappi.onClick.AddListener (() => {
			Application.LoadLevel ("titleView"); });
	}



	// Update is called once per frame
	/*
	void Update () {
	
	} */
}
