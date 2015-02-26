using UnityEngine;
using System.Collections;

public class Fuerza : EfectoGenerico {
	
	public Vector3 translation=Vector3.left;
	public float fuerza=2.0f;

	private bool activo=true;
	
	void OnTriggerStay (Collider other) {
		if(other.name=="Player" && activo){
			other.transform.Translate(translation*Time.deltaTime*fuerza, Space.World);
		}
	}

	override public void setActivo(bool act){
		activo=act;
	}
}
