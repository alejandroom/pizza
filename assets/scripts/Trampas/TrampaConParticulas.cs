using UnityEngine;
using System.Collections;

/* Si alternar esta a true, alterna entre activo y no activo cada <time> segundos
 * Si alternar esta a false, el valor de pausa determina si esta en funcionamiento o no */


public class TrampaConParticulas : MonoBehaviour {

	public EfectoGenerico trampa;
	public ParticleSystem chorro;
	public float time=4;
	public bool alternar=true;
	public bool pausa=false;
	
	private float baseTime;
	private bool activo=false;
	
	void Start () {
		baseTime=Time.realtimeSinceStartup;
		trampa.setActivo(true);
		chorro.Play();
		activo=true;
	}
	
	void Update () {
		if(alternar){
			if((Time.realtimeSinceStartup-baseTime)%(time*2) < time){
				if(!activo){
					trampa.setActivo(true);
					chorro.Play();
					activo=true;
				}
			}else{
				if(activo){
					trampa.setActivo(false);
					chorro.Stop();
					activo=false;
				}
			}
		}else{
			if(!pausa && !activo){
				Debug.Log("Activando chorro");
				trampa.setActivo(true);
				chorro.Play();
				activo=true;
			}else if(pausa && activo){
				Debug.Log("Desactivando chorro:"+pausa);
				trampa.setActivo(false);
				chorro.Stop();
				activo=false;
			}
		}
	}
}
