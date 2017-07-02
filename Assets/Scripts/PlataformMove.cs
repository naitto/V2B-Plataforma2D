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

		// evaluar si toco punto
		// Nota: jugamos un poco con la cantidad de puntos existente ( para este caso 2 puntos )
		if ( plataform.transform.position == currentPoint.position ) {
			pointSelection += 1; // incrementamos para saltar al siguiente

			// evaluamos si llego al puntoFin, resetamos el pointSelection
			if (pointSelection == points.Length) {
				pointSelection = 0; // para que reinicie ( vuelva a puntoIni )
			}

			// indicar nuevo a punto a seguir
			currentPoint = points [ pointSelection ];
		} 
		 
	}
}
