using UnityEngine;
using System.Collections;

public class BotonGenerico : MonoBehaviour {

	public bool temporizado;
	public float resetTime=4;
	public GameObject boton;

	private float baseTime;
	private bool activo=true;
	private bool pulsado=false;
	private float alturaBoton=0.08f;
	
	void OnTriggerEnter(){
		if(!pulsado && activo){
			bajarBoton(true);
		}
	}

	void OnTriggerExit(){
		if(!pulsado && activo){
			encender(true);
			baseTime=Time.realtimeSinceStartup;
			pulsado=true;
		}
	}

	void Update(){
		if(pulsado && temporizado){
			if((Time.realtimeSinceStartup-baseTime) > resetTime){
				encender(false);
				pulsado=false;
				bajarBoton(false);
			}
		}
	}
	
	virtual public void encender(bool boolean){
		Debug.Log("Encendiendo:"+boolean);
	}
	
	private void bajarBoton(bool bajar){
		Vector3 aux=boton.transform.position;
		if(bajar)
			aux.y=aux.y-alturaBoton;
		else
			aux.y=aux.y+alturaBoton;
		boton.transform.position=aux;
	}
}
