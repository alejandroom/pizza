using UnityEngine;
using System.Collections;

/* Clase abstracta.
 * 	Controlador de menu formado por cajas.
 * 	Incluye los metodos pause() y play() para activar y desactivar el movimiento de las cajas. 
 * */
public abstract class BoxMenuController : MenuController {
	public abstract void pause();
	public abstract void play();
}
