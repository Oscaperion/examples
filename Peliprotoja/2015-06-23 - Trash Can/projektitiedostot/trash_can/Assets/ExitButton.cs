using UnityEngine;
using System.Collections;

public class ExitButton : MonoBehaviour {
	public UnityEngine.UI.Button ExitBut;
	
	void Start ()
	{
		// http://answers.unity3d.com/questions/777818/46-ui-calling-function-on-button-click-via-script.html
		ExitBut.onClick.AddListener (() => {
			LopetaPeli (); });
	}
	
	public void LopetaPeli ()
	{
		Application.Quit();
	}
}
