using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
	private float baseTime;
	private bool mensaje=false;
	private bool activo=true;

	void OnTriggerEnter(Collider other) {
		if(activo){
			GameObject.Find("Suelo").GetComponent<Ostion>().checkpoint=this.name;
			GameObject.Find("Checkpoint_Text").guiText.enabled=true;
			baseTime=Time.realtimeSinceStartup;
			mensaje=true;
		}
		activo=false;
	}

	void Update(){
		if(mensaje){
			if(Time.realtimeSinceStartup-baseTime > 2.0){
				GameObject.Find("Checkpoint_Text").guiText.enabled=false;
				mensaje=false;
			}
		}
	}
}
