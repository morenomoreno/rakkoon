using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerControllerOnline : NetworkBehaviour {
	private float speed= 12.0f;
	private float jumpSpeed= 8.0f;
	public GameObject mazePrefab;
	public float gravity= 20.0f;
	public float rotSpeed= 100f;
	private float tiempo= 0.0f;
	private Vector3 moveDirection = Vector3.zero;
	private Vector3 rotation = Vector3.zero;
	Camera cam1;
	Camera cam2;
	GameObject protection;
	private bool  meta;
	private float monedas;
	private bool fastMode = false;
	private bool slowMode = false;
	private bool protectionMode = false;
	private float timeLastVelocity = -7.0f;
	private float timeLastSlowed = -7.0f;
	private float timeLastProtection = -7.0f;
	private float salud = 100;
	public GameObject energyBall;
	private Rigidbody rb;
	public float speedFire;
	public List<GameObject> tmonedas = new List<GameObject>();

	void  Start (){
		//cam = GameObject.FindWithTag("camera");
		meta = false;
		monedas = 0.0f ;
	//	transform.Translate (6.1f, 0.5f, -10.4f);
	}


	public override void OnStartClient(){
		Debug.Log ("start client");
	}
	public override void OnStartServer(){
		//GameObject.Find ("MazeSpawner").SetActive (false);
	}


	public override void OnStartLocalPlayer(){
		if (isServer == false) {
		//	GameObject.Find ("MazeSpawner").SetActive (true);
			transform.position = new Vector3(-64.8f,2.5f,-61.0f);
			transform.Rotate (0.0f,90.0f,0.0f);
			//GameObject maze = GameObject.Instantiate (mazePrefab);
		} else if (isServer) {
			transform.position = new Vector3(59.39113f,0.6300049f,49.47858f);
			transform.Rotate (0.0f,-90.0f,0.0f);
		//	GameObject.Find ("MazeSpawner").SetActive (true);
		}

	}
	// Update is called once per frame
	void Update () {
		
		if (isLocalPlayer) {
			if ((Time.time - timeLastVelocity < 5) && fastMode) {
				speed = 25;
				rotSpeed= 200f;
			}else if ((Time.time - timeLastVelocity < 5) && slowMode) {
				speed = 6;
				rotSpeed= 90f;
			}else {
				speed = 12;
				rotSpeed= 100f;
			}
			if ((Time.time - timeLastProtection > 10) && protectionMode) {
				protectionMode = false;
				quitProtection ();
			}
			cam1.enabled = true;


			cam2.enabled = true;
			tiempo += Time.deltaTime;
			CharacterController controller = GetComponent<CharacterController> ();
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
			controller.Move (moveDirection * Time.deltaTime);


			if (Input.GetKeyDown(KeyCode.LeftControl))            // If player presses left CONTROL,
			{
				GameObject energyBall_handler_tmp;
				energyBall_handler_tmp = Instantiate(energyBall, transform.position, transform.rotation) as GameObject;
				//           energyBall_handler_tmp = Instantiate(energyBall, p.transform.position, p.transform.rotation) as GameObject;

				// Adjust PS
				energyBall_handler_tmp.transform.Rotate(new Vector3(0.0f, -90.0f, 0.0f));

				// Rigid body component retrieval.
				rb = energyBall_handler_tmp.GetComponent<Rigidbody>();

				//rb_tmp.AddForce(speed * Camera.main.transform.forward);
				rb.velocity = speedFire * Camera.main.transform.forward;

				// Destroy bullet after blanking
				Destroy(energyBall_handler_tmp, 2.0f);
			}
				
		} else {
			if(cam1!=null && cam2!=null){
				cam1.enabled = false;
				cam2.enabled = false;
			}	
		}
	}


	void  OnTriggerEnter ( Collider obj  ){

		/*if (obj.tag == "attack") {
			if (!isLocalPlayer) {
				PerderMonedas (	);
				if (monedas == 0) {
					salud = salud - 10.0f;
				}
			}
		}*/
		if (obj.tag == "finish"){
		//	meta = true;
		}
		if (obj.tag == "coin")
		{
			tmonedas.Add(obj.gameObject);
			//Destroy(obj.gameObject);
			obj.gameObject.SetActive(false);
			//obj.gameObject.transform.position = new Vector3((Random.value * 100)-50, 1.0111ff, (Random.value * 100)-50);
			monedas++;
		}
		if (obj.tag == "velocity") {
			Destroy (obj.gameObject);
			timeLastVelocity = Time.time;
			fastMode = true;
			slowMode = false;
		}
		if (obj.tag == "slowed") {
			Destroy (obj.gameObject);
			timeLastSlowed = Time.time;
			fastMode = false;
			slowMode = true;
		}
			
		if (obj.tag == "Shield") {
			Destroy (obj.gameObject);
			timeLastProtection = Time.time;
			protectionMode = true;
			activeProtection ();
			//slowMode = true;
		}
	}

	void PerderMonedas()
	{
		CharacterController controller = GetComponent<CharacterController>();

		foreach (GameObject tmoneda in this.tmonedas)
		{
			if (tmoneda.active == false)
			{
				tmoneda.SetActive(true);
				tmoneda.transform.position = controller.transform.position + new Vector3(0,1,5);
				tmoneda.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-100,100), 0, Random.Range(-100,100)));
			}
		}

	}

	public void setCamera1(Camera camera)
	{
		cam1 = camera;
	}
	public void setCamera2(Camera camera)
	{
		cam2 = camera;
	}

	public void setProtection(GameObject protect){
		this.protection = protect;
	}
	public void activeProtection(){
		this.protection.SetActive (true);
	}
	public void quitProtection(){
		this.protection.SetActive (false);
	}
	public float getMonedas(){
		return this.monedas;
	}
	public void setMonedas(float m){
		this.monedas = this.monedas - m;
	}
	public void attack(float m,float s){
		this.setMonedas (m);
		if (monedas == 0) {
			this.salud = this.salud - s;
		}
		PerderMonedas (	);
	}
		
	void  OnGUI (){
		
		if (isLocalPlayer) {
			GUI.Box( new Rect(Screen.width-100, 125, 100, 70),"");
			GUI.Label( new Rect(Screen.width-95,130,100,20),"Salud: " + salud);
			GUI.Label( new Rect(Screen.width-95,150,100,20),"Monedas: " + this.getMonedas());
			GUI.Label( new Rect(Screen.width-95,170,100,20), "Tiempo: "+tiempo.ToString("f2"));

			if(meta){
				GUI.Box( new Rect(Screen.width/2 - 50, Screen.height/2  -50, 100, 100),"You Win");
				if(GUI.Button( new Rect(Screen.width/2 - 30, Screen.height/2  -20,60, 40),"Repetir")){
					Application.LoadLevel(Application.loadedLevel);
				}
			}
		}
	}
}