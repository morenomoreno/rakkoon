using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Estadistica : MonoBehaviour {
    string[] nombres = { "Jugador1", "Jugador2", "Jugador3", "Jugador4" };
    int[] numero_monedas = { 10,20,40,15};
    public Text puesto1;
    public Text puntuacion1;
    public Text puesto2;
    public Text puntuacion2;
    public Text puesto3;
    public Text puntuacion3;
    public Text puesto4;
    public Text puntuacion4;
    private int valormaximo = 0;
    private int tope;
    int[] impreso = { 0, 0, 0, 0 };
    private int posicionmaxima;
    // Use this for initialization
	void Start () {
        for (int i=0;i<nombres.Length;i++)
        {
            if (numero_monedas[i]>valormaximo)
            {
                puesto1.text = nombres[i];
                puntuacion1.text = numero_monedas[i].ToString() + " monedas";
                valormaximo = numero_monedas[i];
                posicionmaxima = i;
            }
        }
        impreso[posicionmaxima] = 1;
        //Puesto2
        valormaximo = 0;
        posicionmaxima = 0;
        for (int i = 0; i < nombres.Length; i++)
        {
            if ((numero_monedas[i] > valormaximo) & (impreso[i]==0))
            {
                puesto2.text = nombres[i];
                puntuacion2.text = numero_monedas[i].ToString() + " monedas";
                valormaximo = numero_monedas[i];
                posicionmaxima = i;
            }
        }
        impreso[posicionmaxima] = 1;
        //Puesto3
        valormaximo = 0;
        posicionmaxima = 0;
        for (int i = 0; i < nombres.Length; i++)
        {
            if ((numero_monedas[i] > valormaximo) & (impreso[i] == 0))
            {
                puesto3.text = nombres[i];
                puntuacion3.text = numero_monedas[i].ToString() + " monedas";
                valormaximo = numero_monedas[i];
                posicionmaxima = i;
            }
        }
        impreso[posicionmaxima] = 1;
        //Puesto4
        valormaximo = 0;
        posicionmaxima = 0;
        for (int i = 0; i < nombres.Length; i++)
        {
            if ((numero_monedas[i] > valormaximo) & (impreso[i] == 0))
            {
                puesto4.text = nombres[i];
                puntuacion4.text = numero_monedas[i].ToString() + " monedas";
                valormaximo = numero_monedas[i];
                posicionmaxima = i;
            }
        }
        impreso[posicionmaxima] = 1;
    }
	
	// Update is called once per frame
	void Update () {
	    	
	}
}
