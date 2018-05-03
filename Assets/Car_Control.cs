using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Control : MonoBehaviour
{
    public Vector3 temp1;
    public WheelCollider[] Ruedas;

    public float motorForce = 260.0f;
    public float wheelAngle = 45.0f;

    public GameObject[] RuedasDelanteras;
    public GameObject RuedaDelantera;
    public float angulogiro = 0.0f;

    void Girar_Ruedas()
    {
        RuedasDelanteras = GameObject.FindGameObjectsWithTag("RuedaDelantera");
        foreach (GameObject RuedaDelantera in RuedasDelanteras)
        {
            temp1 = RuedaDelantera.transform.localEulerAngles;
            temp1.y = Ruedas[0].steerAngle;
            //RuedaDelantera.transform.Rotate(Ruedas[0].rpm / 60 * 360 * Time.deltaTime, 0, 0);
            RuedaDelantera.transform.localEulerAngles = temp1;

        }
        
    }
    void Rotar_Ruedas()
    {
        RuedasDelanteras = GameObject.FindGameObjectsWithTag("RuedaDelantera");
        foreach (GameObject RuedaDelantera in RuedasDelanteras)
        {
            //RuedaDelantera.transform.Rotate(Ruedas[0].rpm / 60 * 360 * Time.deltaTime, 0, 0);
            RuedaDelantera.transform.Rotate(0,Ruedas[0].rpm / 60 * 360 * Time.deltaTime,  0);
        }
    }
    
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
        for (int i=0;i<Ruedas.Length;i++)
        {
            Ruedas[i].motorTorque = Input.GetAxis("Vertical") * motorForce;
            Ruedas[i].steerAngle = Input.GetAxis("Horizontal") * wheelAngle;
        }
        Girar_Ruedas();
        //Rotar_Ruedas();

    }
}
