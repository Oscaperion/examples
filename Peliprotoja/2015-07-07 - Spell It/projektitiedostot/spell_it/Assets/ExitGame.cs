using UnityEngine;
using System.Collections;

public class ExitGame : MonoBehaviour {

	public UnityEngine.UI.Button ExitBut;

	// Use this for initialization
	void Start () {
		ExitBut.onClick.AddListener (() => {
			Application.Quit(); });
	}
}
