using UnityEngine;
using System.Collections;

public class Inestable : EfectoGenerico {

	public GameObject objeto;
	public float trembleTime=1.5f;
	public float standTime=0.5f;
	public float resetTime=8f;
	public float speedCaida=8f;
	
	private float baseTime;
	private float totalTime;
	private bool activo=true;
	private bool cayendo=false;
	private bool tembleque=false;

	private Rotator rotator;
	private Vector3 posicionInicial;
	
	void Start () {

		posicionInicial=objeto.transform.position;
		rotator=objeto.GetComponent<Rotator>();
		totalTime=trembleTime+standTime+resetTime;


		rotator.speed=0;
	}

	void OnTriggerEnter(){
		if(!cayendo && activo){
			baseTime=Time.realtimeSinceStartup;
			cayendo=true;
		}
	}

	void Update(){
		if(cayendo){
			if((Time.realtimeSinceStartup-baseTime)%(totalTime*2) < trembleTime){
				if(!tembleque){
					rotator.speed=10;
					rotator.time=0.1f;
					tembleque=true;
				}
			}else if((Time.realtimeSinceStartup-baseTime)%(totalTime*2) < trembleTime+standTime){
				if(tembleque){
					rotator.speed=0;
					tembleque=false;
				}
			}else if((Time.realtimeSinceStartup-baseTime)%(totalTime*2) < totalTime){
				rotator.speed=0;
				tembleque=false;
				objeto.transform.Translate(Vector3.down*Time.deltaTime*speedCaida,Space.World);
			}else{
				objeto.transform.position=posicionInicial;
				cayendo=false;
			}
		}
	}

	override public void setActivo(bool act){
		activo=act;
	}
}
