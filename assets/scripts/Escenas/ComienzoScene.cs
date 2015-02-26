using UnityEngine;
using System.Collections;

/* Script a adjuntar en objeto del escenario, camara preferentemente.
 * 	Se encargara de activar el controlador del escenario.
 * */
public class ComienzoScene : MonoBehaviour {
	public bool instrucciones=false;

	void Start () {
		GameObject.Find("ControladorEscenarios").GetComponent<Escenario>().StartScene(instrucciones);
	}
}
