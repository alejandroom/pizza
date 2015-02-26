using UnityEngine;
using System.Collections;

public class FinalBatalla : MonoBehaviour {

	private float baseTime;

	void Start () {
		baseTime=Time.realtimeSinceStartup;

		if(PlayerPrefs.GetInt("TotalMonedas", 0)+5>70){
			PlayerPrefs.SetInt("Monedas",70-PlayerPrefs.GetInt("TotalMonedas", 0)+PlayerPrefs.GetInt("Monedas", 0));
			PlayerPrefs.SetInt("TotalMonedas", 70);
		}else{
			PlayerPrefs.SetInt("Monedas",PlayerPrefs.GetInt("Monedas", 0)+5);
			PlayerPrefs.SetInt("TotalMonedas", PlayerPrefs.GetInt("TotalMonedas", 0)+5);
		}
		
		GameObject.Find("Victory").particleSystem.Stop();
		switch(PlayerPrefs.GetInt("avance", 0)){
		case 1:
			GameObject.Find("Nombre Nivel").guiText.text="Llamas de Pasion";
			break;
		case 2:
			GameObject.Find("Nombre Nivel").guiText.text="Furia horneada";
			break;
		case 3:
			GameObject.Find("Nombre Nivel").guiText.text="Winter is coming";
			break;
		case 4:
			GameObject.Find("Nombre Nivel").guiText.text="Mind Freeze!";
			break;
		case 5:
			GameObject.Find("Nombre Nivel").guiText.text="Imagine there's no pizza...";
			break;
		case 6:
			GameObject.Find("Nombre Nivel").guiText.text="Almost done it!";
			break;
		case 7:
			GameObject.Find("Nombre Nivel").guiText.text="Giusseppe's Last Stand";
			break;
		case 8:
			GameObject.Find("Texto2").guiText.text="Has derrotado a Giusseppe!";
			GameObject.Find("Texto3").guiText.text="Las pizzas son libres!";
			GameObject.Find("Nombre Nivel").guiText.text="";
			GameObject.Find("Victory").particleSystem.Play();
			break;
		}
	}
	
	void Update () {
		if(Input.anyKeyDown && baseTime+3.0<Time.realtimeSinceStartup){
			Application.LoadLevel("Main Menu");
		}
	}
}
