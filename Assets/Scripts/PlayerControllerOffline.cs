using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerOffline : MonoBehaviour {
    private bool bDetectKey;
    private KeyCode kCode;
    public float speed= 16.0f;
	public float jumpSpeed= 8.0f;
	public float gravity= 20.0f;
	public float rotSpeed= 100f;
	private float tiempo= 20.0f;
	private Vector3 moveDirection = Vector3.zero;
	private Vector3 rotation = Vector3.zero;
	private bool  meta;
	private bool  lost;
	public int monedas=0;
	private Vector3 offset = Vector3.zero;
	public float yOffset= 2.0f;
    private GameObject micamara;
	public float maxRange = 10;
	private GameObject cam;
	private Vector3 originalCamPosition;
	private Transform prevTransObject;
	public float alphaValue = 0.5f; // our alpha value
	public List<int> transparentLayers = new List<int>();   // transparency layers.
	public float maxDistance = 20f; // Max distance from target to camera object.
	Dictionary<int, Transform> m_instanceMap = new Dictionary<int, Transform>();
	void  Start (){
		cam = GameObject.FindWithTag("MainCamera");
		originalCamPosition = cam.transform.position;
		meta = false;
		lost = false;
		monedas = 0;
		transform.position = new Vector3(-64.8f,2.5f,-61.0f);
		transform.Rotate (0.0f,90.0f,0.0f);
        micamara = GameObject.Find("Camera");
		//cam = micamara.GetComponent<Camera>();
    }


	// Update is called once per frame
	void Update () {
		tiempo -= Time.deltaTime;
		CharacterController controller = GetComponent<CharacterController>();
		if (controller.isGrounded) { 

			moveDirection = new Vector3 (0, 0, Input.GetAxis ("Vertical"));
			moveDirection = transform.TransformDirection (moveDirection);

			rotation = new Vector3 (0, Input.GetAxis ("Horizontal") * rotSpeed * Time.deltaTime, 0);
			transform.Rotate (rotation);

			moveDirection *= speed;   
			if (Input.GetButton ("Jump"))
				moveDirection.y = jumpSpeed;

		} 
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);

        if (Input.inputString == "+")
        {
            Debug.Log("Pulsado +");
            micamara.transform.position = new Vector3(micamara.transform.position.x, micamara.transform.position.y - 1, micamara.transform.position.z);
        }
        else{
            if (Input.inputString == "-")
            {
                Debug.Log("Pulsado -");
            micamara.transform.position = new Vector3(micamara.transform.position.x, micamara.transform.position.y + 1, micamara.transform.position.z);
        	}
       	}

		if (tiempo <= 0) {
			lost = true;
		}
    }

    
    void  OnGUI (){
		GUI.Box( new Rect(5, 5, 100, 50),"");
		GUI.Label( new Rect(10,30,100,20), "Tiempo: "+tiempo.ToString("f2"));
		GUI.Label( new Rect(10,10,100,20),"Monedas: " + monedas);
		//GUI.Label( new Rect(10,50,100,20),"Vidas: ");
		//	GUI.Label( new Rect(10,70,100,20),"Tiempo Bola: "+ tiempoBola);

		if(meta){
			GUI.Box( new Rect(Screen.width/2 - 50, Screen.height/2  -50, 100, 100),"You Win");
			if(GUI.Button( new Rect(Screen.width/2 - 30, Screen.height/2  -20,60, 40),"Repetir")){
				Application.LoadLevel(Application.loadedLevel);
			}
		}
		if (lost) {
			tiempo = 0;
			GUI.Box( new Rect(Screen.width/2 - 50, Screen.height/2  -50, 100, 100),"Has perdido");
			if(GUI.Button( new Rect(Screen.width/2 - 30, Screen.height/2  -20,60, 40),"Repetir")){
				Application.LoadLevel(Application.loadedLevel);
			}
		}

	}
	void  OnTriggerEnter ( Collider obj  ){
		if (obj.tag == "finish"){
			meta = true;
		}
		if (obj.tag == "coin" || obj.tag == "Coin")
		{
			Destroy(obj.gameObject);
			//obj.gameObject.transform.position = new Vector3((Random.value * 100)-50, 1.0111ff, (Random.value * 100)-50);
			monedas++;
			tiempo += 4;

        }

	}
		
}