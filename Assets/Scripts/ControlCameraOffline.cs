using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCameraOffline : MonoBehaviour {
	Transform player;
	public float rotSpeed= 100.0f;
	public float yOffset= 2.0f;
	private Vector3  offset = new Vector3 (-4.9f, 4.7f, 0.0f);
	private int maxRange =10;
	private Vector3 originCamPos;
	private Vector3 offsetOrigin;
	private Vector3 direction;
	private int counter=0;
	private bool wallBetween = false;
	void  Start (){
		player =  this.transform.parent;
		originCamPos = player.position + offset; 
		offsetOrigin = this.transform.position - player.position;
	}
	void  Update (){
			
		player =  this.transform.parent;
		originCamPos = player.position + offset; 
		Debug.Log ("Player position " + player.position);
		Debug.Log ("aux off" + (transform.position - player.position));
		RaycastHit hit;
		if(Physics.Linecast(originCamPos,player.transform.position,out hit)){
			Transform objectHit = hit.transform;
			if (objectHit.name == "WallPrefab(Clone)") {
				if (wallBetween == false) {
					this.transform.position = player.position + new Vector3(0.0f,1.9f,0.0f);
					wallBetween = true;
				}
				offset = Quaternion.AngleAxis (Input.GetAxis ("Horizontal") * rotSpeed * Time.deltaTime, Vector3.up) * offset;

			} else {
				wallBetween = false;

				if (IsNotGrounded ()) {
					offset = Quaternion.AngleAxis (Input.GetAxis ("Horizontal") * rotSpeed * Time.deltaTime, Vector3.up) * offset;
					transform.LookAt (new Vector3 (player.position.x, player.position.y + yOffset, player.position.z));
					transform.position = originCamPos;
				}
			}
		}


	}

	bool IsNotGrounded (){
		return Physics.Raycast(player.position, -Vector3.up,0.9f);
	}
}

