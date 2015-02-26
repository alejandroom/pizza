using UnityEngine;
using System.Collections;

public class ControlLuces : MonoBehaviour {
	private bool encendiendo=false;
	private bool grande=false;
	
	private Light luzGrande;
	private Light foquito;

	// Use this for initialization
	void Start () {
		encendiendo=true;
		grande=false;
		
		foquito=GameObject.Find ("Spotlight").light;
		foquito.intensity=0.0f;

		luzGrande=GameObject.Find ("Principal Light").light;
		luzGrande.color=Color.black;
	}
	
	// Update is called once per frame
	void Update () {
		if(encendiendo){
			if(Time.realtimeSinceStartup<4){
				/*Luz aumenta poco*/
				foquito.intensity+=0.01f;
			}else{
				/*Luz max*/
				foquito.intensity=3.0f;
				encendiendo=false;
			}
		}
		if(!grande && Time.realtimeSinceStartup>10){
			foquito.intensity-=0.1f;
			luzGrande.color=new Color(luzGrande.color.r+0.1f,luzGrande.color.g+0.1f,luzGrande.color.b+0.1f);
			if(luzGrande.color==Color.white){
				foquito.intensity=0.0f;
				grande=true;
			}
		}
	
	}
}
