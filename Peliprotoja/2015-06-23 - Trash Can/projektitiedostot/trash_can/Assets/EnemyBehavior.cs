using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	//public GameObject enemySprite;

	//private float xPos;
	//public float pointo;
	//public string title;
	public bool oikealle = true;
	//public Sprite enemy3;


	// Use this for initialization
	void Start () {
		//title = "TBA";
		//m_LastRealTime = Time.realtimeSinceStartup;
		/*
		maxMaara = 4;
		odota = 20; */
		//xPos = this.transform.localPosition.x; 
		//moveUnitsPerSecond.value = new Vector3(7f,0f,0f);
		//gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("npcSprite3");
	} 
	
	// Update is called once per frame
	void Update () { 
		//title = tag;
		//pointo = gameObject.transform.position.x;
		//if ((gameObject.transform.position.x > 10f && oikealle) || (!oikealle && gameObject.transform.position.x < -10f)) {
		if (gameObject.transform.position.x > 10f || gameObject.transform.position.x < -10f) {
			Destroy(gameObject);
		}
		/*
		if (odota > 0) {
			odota--;
		} else {
			odota = 20;
			//moveUnitsPerSecond.value = new Vector3 (7f, 0f, 0f);
			Instantiate(enemySprite, new Vector3(xPos, enemySprite.transform.localPosition.y, enemySprite.transform.localPosition.z), Quaternion.identity);


			//enemySprite.GetComponent<UnityStandardAssets.Utility.AutoMoveAndRotate>().moveUnitsPerSecond;
		} */
	}
}