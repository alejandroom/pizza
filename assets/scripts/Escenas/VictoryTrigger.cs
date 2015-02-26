using UnityEngine;
using System.Collections;

/* Script a adjuntar al trigger de victoria. 
 * 	Se encargara, cuando el jugador active el trigger, de desactivar los scripts de movimiento y camara del jugador.
 * 	Tambien mostrara los textos de victoria y llamara a la funcion Victoria de VictoryCamera
 * */
public class VictoryTrigger : MonoBehaviour {
	
	public GameObject player;
	public VictoryCamera script;	
	public ThirdPersonCamera script_a_desactivar1;	
	
	void OnTriggerEnter (Collider other) {
		if(other.name=="Player"){	/* En caso de ser el jugador quien colisiona, 
										pausa el controlador del jugador, 
										desactiva el script de la camara, 
                                                                                                                                                         										muestra los textos y llama a Victoria. */
			GameObject.Find("ControladorEscenarios").GetComponent<Escenario>().pausar();
			(script_a_desactivar1 as Behaviour).enabled = false;
			
			script.Victoria(other.transform);
		}
	}
}
