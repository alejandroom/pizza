using UnityEngine;
using System.Collections;

public class Escenario : MonoBehaviour
{
	public bool pause = true;
	
	private bool Talentos;
	private bool nuevoTalento;
	
	private ThirdPersonController controlador;
	private TalentosController talentosController;

	private GUIText monedas;
	private GUITexture interfazTalentos;
	
	void Start ()
	{
		DontDestroyOnLoad(this.gameObject);
		Application.LoadLevel("Main Menu");
	}
	
	public void pausar(){
		controlador.isControllable=false;
		pause=true;
	}
	
	public void StartScene(bool instrucciones){
		int numMonedas=PlayerPrefs.GetInt("Monedas");
		if(numMonedas!=0 && instrucciones){
			foreach (GameObject aux in GameObject.FindGameObjectsWithTag("Masita")){
				aux.GetComponent<Moneditas>().valor=0;
			}
		}

		nuevoTalento=numMonedas>=10;
		Screen.showCursor = false;
		
		pause=false;

		controlador=GameObject.Find("Player").GetComponent<ThirdPersonController>();
		monedas=GameObject.Find("Interfaz_Monedas").guiText;
		interfazTalentos=GameObject.Find("Interfaz_Talentos").guiTexture;
		monedas.text="Masa: " + numMonedas;
		controlador.isControllable=true;
		
		talentosController=new TalentosController();
	}
	
	void Update ()
	{
		if(pause)
			return;
		if(monedas)
			monedas.text="Masa: " + PlayerPrefs.GetInt("Monedas");
		if(interfazTalentos){
			nuevoTalento=PlayerPrefs.GetInt("Monedas")>=10;
			if(nuevoTalento){
				if(Time.realtimeSinceStartup%2<1){
					interfazTalentos.color=new Color(0.5f,0.5f,0.5f);
				}else{
					interfazTalentos.color=new Color(0.6f,0.6f,0.6f);
				}
			}else{
				interfazTalentos.color=new Color(0.5f,0.5f,0.5f);
			}
		}
		if (Talentos) {
			if (Input.GetKeyDown (KeyCode.KeypadPlus) || Input.GetKeyDown (KeyCode.UpArrow)) {
				talentosController.aumenta();
			} else if (Input.GetKeyDown (KeyCode.KeypadMinus) || Input.GetKeyDown (KeyCode.DownArrow)) {
				talentosController.disminuye();
			} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
				talentosController.next();
			}  else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				talentosController.previous();
			} else if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.T)) {
				talentosController.hide();
				controlador.isControllable=true;
				Talentos=false;
			} 
		} else {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				talentosController.GuardaPoints();
				Application.LoadLevel ("Main Menu");
			} else if (Input.GetKeyDown (KeyCode.T)) {
				talentosController.show();
				controlador.isControllable=false;
				Talentos=true;
			}
		}
	}
}
