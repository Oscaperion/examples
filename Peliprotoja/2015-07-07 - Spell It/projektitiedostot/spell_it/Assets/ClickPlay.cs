using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClickPlay : MonoBehaviour {

	public Button PlayButton;

	// Use this for initialization
	void Start ()
	{
		// http://answers.unity3d.com/questions/777818/46-ui-calling-function-on-button-click-via-script.html
		PlayButton.onClick.AddListener (() => {
			LataaTaso (); });
	}
	
	public void LataaTaso ()
	{
		Application.LoadLevel ("gameScene");
	}
}
