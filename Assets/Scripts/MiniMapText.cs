using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapText : MonoBehaviour {
	public Text textolargo;

	public GameObject otherGameObject;
	private PlayerControllerOnline myplayer;


	// Use this for initialization
	void Start () {
		textolargo.text = "0";
		myplayer = this.gameObject.GetComponentInParent<PlayerControllerOnline>();
	}
		
	// Update is called once per frame
	void Update () {
		if (myplayer.isLocalPlayer) {
			textolargo.text = myplayer.getMonedas().ToString();
		}	


	}
}



