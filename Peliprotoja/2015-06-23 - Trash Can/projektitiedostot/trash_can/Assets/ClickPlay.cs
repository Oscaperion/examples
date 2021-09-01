using UnityEngine;
using System.Collections;

public class ClickPlay : MonoBehaviour
{
	public UnityEngine.UI.Button PlayButton;

	void Start ()
	{
		// http://answers.unity3d.com/questions/777818/46-ui-calling-function-on-button-click-via-script.html
		PlayButton.onClick.AddListener (() => {
			LataaTaso (); });
	}

	public void LataaTaso ()
	{
		Application.LoadLevel ("mainView");
	}
}
