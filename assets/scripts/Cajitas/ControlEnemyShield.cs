using UnityEngine;
using System.Collections;

public class ControlEnemyShield : MonoBehaviour {
	private bool activo=false;
	private float baseTime;

	public void go(){
		activo=true;
		baseTime=Time.realtimeSinceStartup;
		this.renderer.enabled=true;
	}

	public void Update(){
		if(activo){
			if(Time.realtimeSinceStartup-baseTime>1){
				this.renderer.enabled=false;
				activo=false;
			}
		}
	}
}
