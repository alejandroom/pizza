using UnityEngine;
using System.Collections;

public class Batalla : MonoBehaviour
{	
	public GenericModelController enemyController;
	public PlayerModelController playerController;
	
	private BoxMenuController confirmMenuController;
	private BatallaMenuController controladorMenu;
	private BatallaController controlador;
	
	private bool Confirmando_salida;
	private bool final;
	private bool victoria;
	private bool enemigo;
	private float inicio;
	private float baseTime=0f;
	
	private int numLevel;
	public string nextLevel = "Main Menu";
	
	void Start ()
	{
		Screen.showCursor = false;
		
		confirmMenuController=new ConfirmMenuController();
		controladorMenu=new BatallaMenuController();
		controlador=new BatallaController(enemyController, playerController);
		
		final=false;
		victoria=true;
		enemigo=!controlador.turno();
		
		numLevel=PlayerPrefs.GetInt("Dificultad", 0)+1;
	}
	
	void Update ()
	{
		if (Confirmando_salida) {
			if(Input.GetKeyDown(KeyCode.LeftArrow)) {
				confirmMenuController.next ();
			} else if(Input.GetKeyDown(KeyCode.RightArrow)) {
				confirmMenuController.previous ();
			} else if(Input.GetKeyDown(KeyCode.Return)) {
				if(confirmMenuController.actual()==0) {
					confirmMenuController.hide ();
					Confirmando_salida = false;
				} else {
					Application.LoadLevel("Main Menu");
				}
			}
		}else if(enemigo && Time.realtimeSinceStartup-baseTime>2){
			int ret = controlador.action(controladorMenu.actual());
			if(ret<=0){
				victoria=(ret==0);
				final=true;
				inicio=Time.realtimeSinceStartup;
			}
			if(ret==1){
				enemigo=true;
				baseTime=Time.realtimeSinceStartup;
			}else{
				enemigo=false;
			}		
		}else if(final){
			if(Input.anyKeyDown && inicio+4.0<Time.realtimeSinceStartup){
				if(victoria){
					if(PlayerPrefs.GetInt("avance", 0)<numLevel)
						PlayerPrefs.SetInt("avance", numLevel);
					Application.LoadLevel("Final Batalla");
				}else{
					Application.LoadLevel("BattleDefault");
				}
			}
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			controladorMenu.next();
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			controladorMenu.previous();
		} else if (Input.GetKeyDown (KeyCode.Return)) {
			int ret = controlador.action(controladorMenu.actual());
			if(ret<=0){
				victoria=(ret==0);
				final=true;
	 			inicio=Time.realtimeSinceStartup;
			}
			if(ret==1){
				enemigo=true;
				baseTime=Time.realtimeSinceStartup;
			}else if(ret==2){
				enemigo=false;
			}
		} else if (Input.GetKeyDown (KeyCode.Escape)) {
			confirmMenuController.show();
			Confirmando_salida = true;
		}
	}
}