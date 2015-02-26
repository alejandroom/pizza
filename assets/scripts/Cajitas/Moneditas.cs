using UnityEngine;
using System.Collections;

public class Moneditas: MonoBehaviour {
	public int valor=1;
	
	private float baseTime;
	private bool activo=true;
	private bool mensaje=false;

	void OnTriggerEnter(Collider other) {
		if(activo){
			if(PlayerPrefs.GetInt("TotalMonedas")==70){
				GameObject.Find("Interfaz_MaxMonedas").guiText.enabled=true;
				baseTime=Time.realtimeSinceStartup;
				mensaje=true;
			}else if(PlayerPrefs.GetInt("TotalMonedas")+valor>70){
				GameObject.Find("Interfaz_MaxMonedas").guiText.enabled=true;
				baseTime=Time.realtimeSinceStartup;
				mensaje=true;

				PlayerPrefs.SetInt("TotalMonedas",70);
				if(PlayerPrefs.GetInt("Monedas")+valor>70){
					PlayerPrefs.SetInt("Monedas",70);
				}else{
					PlayerPrefs.SetInt("Monedas",PlayerPrefs.GetInt("Monedas")+valor);
				}

				this.renderer.enabled=false;
				activo=false;
			}else{
				PlayerPrefs.SetInt("TotalMonedas",PlayerPrefs.GetInt("TotalMonedas")+valor);
				PlayerPrefs.SetInt("Monedas",PlayerPrefs.GetInt("Monedas")+valor);

				this.renderer.enabled=false;
				activo=false;
			}
		}
	}
	
	void Update(){
		if(mensaje){
			if(Time.realtimeSinceStartup-baseTime > 2.0){
				GameObject.Find("Interfaz_MaxMonedas").guiText.enabled=false;
				mensaje=false;
			}
		}
	}
}
