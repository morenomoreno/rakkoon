using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class texto_monedas : MonoBehaviour {
	public Text numero_monedas;
	public GameObject imagen;

	void Start () {
        imagen = GameObject.Find("Image");
		Camera cam = gameObject.GetComponent<Camera> ();
		if (gameObject != null) {
			if (this.gameObject.GetComponentInParent<PlayerControllerOnline> () != null) {
				this.gameObject.GetComponentInParent<PlayerControllerOnline> ().setCamera2 (cam);
			} 
		}
    }
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.GetComponentInParent<PlayerControllerOnline> () != null) {
			if (this.gameObject.GetComponentInParent<PlayerControllerOnline> ().isLocalPlayer == false) {
				imagen.SetActive (false);
			} else {
				imagen.SetActive (true);
			}
		}
		Vector3 namePos = GameObject.FindGameObjectWithTag("minimapa").GetComponent<Camera>().WorldToScreenPoint(this.transform.position);
		numero_monedas.transform.position = namePos;
		imagen.transform.position = namePos;


    }
}
