using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// variables tipo componentes
	private Rigidbody2D _rigidbody;
	private Animator _animator;

	// variables para aplicar salto
	public LayerMask maskFloor; 		// mascara ( se le asignara el layer "floor", su indice es 8 )
	public Transform testFloor; 		// testear ( se le asingara el objeto "Player -> testFloor", obteniendo asi solo su componente Transform )
	public float forceJump = 500f;		// fuerza de salto
	private bool isWalking; 			// ¿esta caminando?
	private bool isFloor = true; 		// por default estoy en el piso
	private float radio = 0.07f;		// radio 

	// variables para aplicar doble salto
	private bool jump2 = false;

	// variables para caminar
	private float speed = 3f; 			// velocidad de movimiento horizontal
	private bool isRunning = false; 	// por default no ando corriendo 	
	public float factor = 1f;  			// permite acelerar

	// variables para controlar caida
	public Vector2 posINI;



	//////////////////////////////////////////////


	// Use this for initialization
	void Start () {
		// asignar componentes
		_rigidbody = GetComponent<Rigidbody2D> ();
		_animator = GetComponent<Animator> ();
		// asignar valores
		posINI = transform.position;

	}
	
	// Update is called once per frame
	void Update () {

		/// CAMINAR 
			// ir derecha  ( -> )
			if ( Input.GetKeyDown (KeyCode.D) ) {
				_animator.SetBool ("isWalk", true); // cambia estado animación
				isWalking = true;
				
				transform.rotation = Quaternion.Euler(0,0,0); // Quaternion representa las rotaciones en grados ( usando Euler )
				speed = 5f; // direccion speed
				//factor = 1f; // aumentamos el factor de aceleracion
			}

			// ir izq ( <- )
			if ( Input.GetKeyDown (KeyCode.A) ) {
				_animator.SetBool ("isWalk", true); // cambia estado animación
				isWalking = true;

				transform.rotation = Quaternion.Euler(0,180,0); // Quaternion representa las rotaciones en grados ( usando Euler )
				speed = -5f; // direccion speed
				//factor = 1f; // aumentamos el factor de aceleracion
			}

			// soltar teclas
			if ( Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.D) ) {
				_animator.SetBool ("isWalk", false); // cambia estado animación
				isWalking = false;
			}

				// realizar movimiento
				if ( isWalking ) {
					_rigidbody.velocity = new Vector2 (speed * factor, _rigidbody.velocity.y);
				}

		/// CORRER
			// acelerar movimiento
			if ( Input.GetKeyDown(KeyCode.LeftShift )) {
				factor = 3f; // aumentamos el factor de aceleracion
				isRunning = true;
			}

			if ( Input.GetKeyUp(KeyCode.LeftShift )) {
				factor = 1f; // aumentamos el factor de aceleracion
				isRunning = false;
			}

			// cambiar estado animación
			if (isRunning) {
				_animator.SetBool ("isRun", true);
			} else {
				_animator.SetBool ("isRun", false);
			}


		//// SALTAR
		if ( Input.GetKeyDown (KeyCode.Space) ) {
			// verificar si toca piso ( isFloor ) o si hay salto2 ( !jump2 )
			if ( isFloor || !jump2 ) {
				// asignar velocidad y fuerza
				_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, forceJump); // aumenta o incrementa consecutivamente la velocidad
				_rigidbody.AddForce ( new Vector2(0, forceJump) ); // aplica fuerza de salto
			}

			// Verificar no toca piso ( !isFloor ) o si hay salto2 ( !jump2 )
			if ( !isFloor && !jump2 ) {
				jump2 = true;
			}
		}

	}

	// esta funcion es utilizada para temas de fisicas aplicadas en cuerpos ( rigidbody )
	void FixedUpdate () {

		// asigna una especie de sensor que indica cuando la posicion del objeto "testFloor" este cerca algun elemento con mascara "floor"
		isFloor = Physics2D.OverlapCircle( testFloor.position, radio, maskFloor );
		// setea si esta o no saltando
		_animator.SetBool("isJump",!isFloor); // "isJump" sera true cuando "isFloor" sea false

		// cuando toca piso ( isJump )
		if ( isFloor ) {
			jump2 = false;
		}

		// controlar caida personaje ( reposiciona cuando cae al vacio )
		if ( transform.position.y <= -15) {
			transform.position = posINI; 
		}
	}
}
