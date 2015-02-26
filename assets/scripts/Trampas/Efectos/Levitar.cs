using UnityEngine;
using System.Collections;

public class Levitar : EfectoGenerico {
	
	public float gravity=-10;
	
	private bool activo=true;
	private bool dentro=false;
	private float gravityOld=10f;
	private Collider obj;
	
	void OnTriggerEnter (Collider other) {
		if(other.name=="Player" && activo){
			gravityOld=other.GetComponent<ThirdPersonController>().gravity;
			other.GetComponent<ThirdPersonController>().gravity=gravity;
		}
		obj=other;
		dentro=true;
	}
	
	void OnTriggerExit (Collider other) {
		if(other.name=="Player"){
			other.GetComponent<ThirdPersonController>().gravity=gravityOld;
		}
		dentro=false;
	}

	override public void setActivo(bool act){
		activo=act;
		if(act && dentro){
			if(obj.name=="Player"){
				obj.GetComponent<ThirdPersonController>().gravity=gravity;
			}
		}
		if(!act && dentro){
			if(obj.name=="Player"){
				obj.GetComponent<ThirdPersonController>().gravity=gravityOld;
			}
		}
	}
}
