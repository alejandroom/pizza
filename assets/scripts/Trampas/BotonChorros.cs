using UnityEngine;
using System.Collections;

public class BotonChorros : BotonGenerico {

	public string tagChorros="Chorro";

	private GameObject[] chorros;

	void Start () {
		chorros=GameObject.FindGameObjectsWithTag(tagChorros);
	}
	
	override public void encender(bool boolean){
		foreach( GameObject chorro in chorros){
			chorro.GetComponent<TrampaConParticulas>().pausa=boolean;
		}
	}
}
