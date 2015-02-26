using UnityEngine;
using System.Collections;

public class MainMenuController : BoxMenuController {
	
	private static int MAX_SPEED = 30;
	private static int MIN_SPEED = 0;
	
	private Box New_Box;
	private Box Scene_Box;
	private Box Help_Box;
	private Box Quit_Box;
	
	private Quaternion EX_QUA;
	
	private string boton_seleccionado;
	
	public MainMenuController (){
		boton_seleccionado = "Scene";
		
		EX_QUA = GameObject.Find("EX_BOX").transform.rotation;
		
		New_Box = new  Box(GameObject.Find("New_Box"), EX_QUA, MAX_SPEED, MIN_SPEED);
		Scene_Box = new  Box(GameObject.Find("Scene_Box"), EX_QUA, MAX_SPEED, MIN_SPEED);
		Help_Box = new  Box(GameObject.Find("Help_Box"), EX_QUA, MAX_SPEED, MIN_SPEED);
		Quit_Box = new  Box(GameObject.Find("Quit_Box"), EX_QUA, MAX_SPEED, MIN_SPEED);
		
		New_Box.acelerar(false);
		Scene_Box.acelerar(true);
		Help_Box.acelerar(false);
		Quit_Box.acelerar(false);
	}
	
	public override void next(){
		if(boton_seleccionado == "New"){
			boton_seleccionado = "Scene";
			New_Box.acelerar(false);
			Scene_Box.acelerar(true);
		} else if(boton_seleccionado == "Scene"){
			boton_seleccionado = "Help";
			Help_Box.acelerar(true);
			Scene_Box.acelerar(false);
		} else if(boton_seleccionado == "Help"){
			boton_seleccionado = "Quit";
			Quit_Box.acelerar(true);
			Help_Box.acelerar(false);
		}
	}
	
	public override void previous(){
		if(boton_seleccionado == "Scene"){
			boton_seleccionado = "New";
			New_Box.acelerar(true);
			Scene_Box.acelerar(false);
		} else if(boton_seleccionado == "Help"){
			boton_seleccionado = "Scene";
			Scene_Box.acelerar(true);
			Help_Box.acelerar(false);
		} else if(boton_seleccionado == "Quit"){
			boton_seleccionado = "Help";
			Help_Box.acelerar(true);
			Quit_Box.acelerar(false);
		}
	}
	
	public override int actual(){
		if(boton_seleccionado == "New"){
			return 1;
		} else if(boton_seleccionado == "Scene"){
			return 2;
		} else if(boton_seleccionado == "Help"){
			return 3;
		} else {
			return 4;
		}
	}
	
	public override void pause(){
		New_Box.acelerar(false);
		Scene_Box.acelerar(false);
		Help_Box.acelerar(false);
		Quit_Box.acelerar(false);
	}
	
	public override void play(){
		if(boton_seleccionado == "New"){
		New_Box.acelerar(true);
		} else if(boton_seleccionado == "Scene"){
		Scene_Box.acelerar(true);
		} else if(boton_seleccionado == "Help"){
		Help_Box.acelerar(true);
		} else if(boton_seleccionado == "Quit"){
		Quit_Box.acelerar(true);
		}
	}
	
	public override void mostrar (bool enable)
	{
		New_Box.apagar(enable);
		Scene_Box.apagar(enable);	
		Help_Box.apagar(enable);
		Quit_Box.apagar(enable);
	}
}
