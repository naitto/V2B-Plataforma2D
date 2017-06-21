using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformMove : MonoBehaviour {

	public GameObject plataform;		// obtejo a mover
	public float speed;					// velocidad
	public Transform[] points; 			// puntos ( asignar objetos -> puntoInicio | puntoFin )
	public Transform currentPoint; 		// punto acual a usar ( solo uno del arreglo )
	public int pointSelection;			// punto seleccionado ( inicio o fin, esto lo determina " currentPoint " )

	// Use this for initialization
	void Start () {
		currentPoint = points[ pointSelection ]; // aqui definimos indice del punto seleccionado ( inicio o fin )
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		// definir un movimiento ( usando MoveTowards )
		plataform.transform.position = Vector3.MoveTowards (
			plataform.transform.position,	// punto actual
			currentPoint.position,			// punto destino
			Time.deltaTime * speed			// velocidad de movimiento
		);

		// 


	}
}
