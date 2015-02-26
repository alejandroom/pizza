using UnityEngine;
using System.Collections;

/* Script a adjuntar a la camara de los escenarios no-batalla.
 * 	Se encargara de hacerla girar cuando se llame a Victoria(transform objetivo), y de cambiar de escena cuando se pulse un boton cualquiera.
 * 	Esta funcion se llama desde el script VictoryTrigger
 * */

public class VictoryCamera : MonoBehaviour {
	
	public string nextLevel = "BattleDefault";
	public int dificultad = 0;
	public GUIText text1;	
	public GUIText text2;

	private Transform target;
	private bool activo = false;
	
	private float inicio;
	
	void Start(){
		text1.enabled = false;
		text2.enabled = false;
	}
	
	void Update () {
		if(activo){	/* Si esta activo, girara en torno a target. Durante 4.0 no respondera a ninguna tecla. */
		    gameObject.transform.LookAt(target);
			gameObject.transform.Translate(Vector3.right * Time.deltaTime);
			if(inicio+4.0<Time.realtimeSinceStartup){
				text2.enabled = true;
				if(Input.anyKeyDown){
					PlayerPrefs.SetInt("Dificultad", dificultad);
					Application.LoadLevel(nextLevel);	
				}
			}
		}
	}
	
	public void Victoria(Transform objetivo){
		/* Se guarda el tiempo, se fija el objetivo y se activa la rotacion. */
	 	inicio=Time.realtimeSinceStartup;
		target=objetivo;
		activo=true;
		
		text1.enabled = true;
	}
}
