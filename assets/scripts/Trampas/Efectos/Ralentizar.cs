using UnityEngine;
using System.Collections;

public class Ralentizar : EfectoGenerico {

	public float slowPercent=0.5f;
	public bool coloreable=true;
	public Color color;
	
	private bool activo=true;
	private bool dentro=false;
	private Color colorOld;
	private GameObject slime;

	private Collider obj;

	void Start(){
		colorOld=Color.white;
		slime=GameObject.Find("slimelesser");
	}
	
	void OnTriggerEnter (Collider other) {
		if(other.name=="Player" && activo){
			other.GetComponent<ThirdPersonController>().walkSpeed*=slowPercent;
			other.GetComponent<ThirdPersonController>().trotSpeed*=slowPercent;
			other.GetComponent<ThirdPersonController>().runSpeed*=slowPercent;

			if(coloreable){
				slime.renderer.material.color=color;
			}
		}
		obj=other;
		dentro=true;
	}

	void OnTriggerExit (Collider other) {
		if(other.name=="Player" && activo){
			other.GetComponent<ThirdPersonController>().walkSpeed/=slowPercent;
			other.GetComponent<ThirdPersonController>().trotSpeed/=slowPercent;
			other.GetComponent<ThirdPersonController>().runSpeed/=slowPercent;

			if(coloreable)
				slime.renderer.material.color=colorOld;
		}
		dentro=false;
	}

	override public void setActivo(bool act){
		activo=act;
		if(act && dentro){
			if(obj.name=="Player"){
				obj.GetComponent<ThirdPersonController>().walkSpeed*=slowPercent;
				obj.GetComponent<ThirdPersonController>().trotSpeed*=slowPercent;
				obj.GetComponent<ThirdPersonController>().runSpeed*=slowPercent;
				
				if(coloreable){
					slime.renderer.material.color=color;
				}
			}
		}
		if(!act && dentro){
			if(obj.name=="Player"){
				obj.GetComponent<ThirdPersonController>().walkSpeed/=slowPercent;
				obj.GetComponent<ThirdPersonController>().trotSpeed/=slowPercent;
				obj.GetComponent<ThirdPersonController>().runSpeed/=slowPercent;
				
				if(coloreable)
					slime.renderer.material.color=colorOld;
			}
		}
	}
}
