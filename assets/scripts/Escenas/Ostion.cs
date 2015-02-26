using UnityEngine;
using System.Collections;

/* Script que devuelve el jugador al comienzo si se activa el trigger.
 * 	*/

public class Ostion : MonoBehaviour {
	public string checkpoint="Posicion Inicial";
	
	void OnTriggerEnter(Collider other) {
		other.gameObject.transform.position=GameObject.Find (checkpoint).transform.position;
    }
}
