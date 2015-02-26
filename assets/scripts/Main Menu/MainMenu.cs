using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	private bool Confirmando_salida;
	private bool Confirmando_nuevo;
	private bool Seleccion;
	
	private SelectorEscenasController selector;
	private BoxMenuController confirmMenuController;
	private BoxMenuController mainMenuController;
		
	/* Cositas de la seleccion de escenas */
	private static int numEscenas = 7;
	private static string[] escenas = {"laberintoFuego","laberintoFuego2","laberintoFrigo","laberintoFrigo2","Laberinto","LaberintoCasiFinal","LaberintoFinal"};
	private static string[] nombresEscenas = {"Llamas de Pasion","Furia horneada","Winter is coming","Mind Freeze!","Imagine there's no pizza...","Almost done it!","Giusseppe's Last Stand"};

	void Start ()
	{
		Screen.showCursor = false;
		
		Confirmando_salida = false;
		Confirmando_nuevo = false;
		Seleccion = false;
		
		confirmMenuController=new ConfirmMenuController();
		mainMenuController=new MainMenuController();
		selector=new SelectorEscenasController(numEscenas, escenas, nombresEscenas);
	}
	
	void Update ()
	{
		if (Seleccion) {
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				selector.previous();
			} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				selector.next();
			} else if (Input.GetKeyDown (KeyCode.Return)) {
				selector.actual();
			} else if (Input.GetKeyDown (KeyCode.Escape)) {
				selector.hide();
				Seleccion=false;
				mainMenuController.play ();
			}
		} else if (Confirmando_salida || Confirmando_nuevo) {
			if(Input.GetKeyDown(KeyCode.LeftArrow)) {
				confirmMenuController.next ();
			} else if(Input.GetKeyDown(KeyCode.RightArrow)) {
				confirmMenuController.previous ();
			} else if(Input.GetKeyDown(KeyCode.Return)) {
				if(confirmMenuController.actual()==0) {
					if(Confirmando_salida) {
						confirmMenuController.hide ();
						mainMenuController.play ();
						Confirmando_salida = false;
					}
					if(Confirmando_nuevo) {
						confirmMenuController.hide ();
						mainMenuController.play ();
						Confirmando_nuevo = false;
					}
				} else {
					if(Confirmando_salida)
						Application.Quit();
					if(Confirmando_nuevo) {
						PlayerPrefs.SetInt("Monedas", 0);
						PlayerPrefs.SetInt("TotalMonedas", 0);
						PlayerPrefs.SetInt("Bpoints", 0);
						PlayerPrefs.SetInt("Mpoints", 0);
						PlayerPrefs.SetInt("Ipoints", 0);
						PlayerPrefs.SetInt("avance", 1);
						Application.LoadLevel ("Instrucciones");
					}
				}
			}
		} else {
			if(Input.GetKeyDown(KeyCode.DownArrow)) {
				mainMenuController.next();
			} else if(Input.GetKeyDown(KeyCode.UpArrow)) {
				mainMenuController.previous ();
			} else if(Input.GetKeyDown(KeyCode.Return)) {
				switch(mainMenuController.actual()){
				case 1:
					confirmMenuController.show();
					mainMenuController.pause();
					Confirmando_nuevo = true;
					break;
				case 2:
					Seleccion=true;
					selector.show();
					mainMenuController.pause();
					break;
				case 3:
					Application.LoadLevel ("Instrucciones");
					break;
				default:
					confirmMenuController.show();
					Confirmando_salida = true;
					mainMenuController.pause();
					break;
				}
			}
		}
	}
}
