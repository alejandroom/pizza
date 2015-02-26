using UnityEngine;
using System.Collections;

/* Clase controladora del menu de talentos.
 * 	Se maneja con los metodos abstractos de MenuController y dos metodos adicionales:
 * 		-disminuye() y aumenta() retiran o invierten puntos en la rama de talentos actual.
 * 	*/
public class TalentosController : MenuController {
	private int estado; /* 1=Ingredients, 2=Baking, 3=Mastery */
		
	private int monedas;
	private int Bpoints;
	private int Mpoints;
	private int Ipoints;
	
	private GameObject[] GuiTextObjs;
	private GameObject[] GuiTextureObjs;
	private GameObject[] GuiTextureMastery;
	private GameObject[] GuiTextureIngredients;
	private GameObject[] GuiTextureBaking;
	
	public TalentosController(){
		/* Se guardan todos los elementos del menu por tipos para mostrarlos y ocultarlos posteriormente. */
		GuiTextObjs = GameObject.FindGameObjectsWithTag ("Talentos_text");
		GuiTextureObjs=GameObject.FindGameObjectsWithTag ("Talentos");
		GuiTextureIngredients=GameObject.FindGameObjectsWithTag ("Ingredients");
		GuiTextureMastery=GameObject.FindGameObjectsWithTag ("Mastery");
		GuiTextureBaking=GameObject.FindGameObjectsWithTag ("Baking");
		
		/* Se cargan los datos guardados del jugador. */
		monedas = PlayerPrefs.GetInt("Monedas", 0);
		Bpoints = PlayerPrefs.GetInt("Bpoints", 0);
		Mpoints = PlayerPrefs.GetInt("Mpoints", 0);
		Ipoints = PlayerPrefs.GetInt("Ipoints", 0);
		
		/* Se actualiza la visualizacion con los datos cargados y se oculta el menu. */
		actualizaTalentos();
		mostrar (false);
	}
	
	/* FUNCIONES PUBLICAS */
	
	public override void next(){
		if(estado==1){ /* Si se esta en ingredients, se pasa a baking. */
			mostrarIngredients(false);
			mostrarBaking(true);
			estado=2;
		} else if(estado==2){ /* baking -> mastery */
			mostrarBaking(false);
			mostrarMastery(true);
			estado=3;
		}		
	}
	
	public override void previous(){
		if(estado==3){ /* mastery -> baking */
			mostrarMastery(false);
			mostrarBaking(true);
			estado=2;
		} else if(estado==2){ /* baking -> ingredients */
			mostrarBaking(false);
			mostrarIngredients(true);
			estado=1;
		}		
	}
	
	public override int actual(){
		return estado;
	}
	
	public override void mostrar(bool enable){
		monedas = PlayerPrefs.GetInt("Monedas", 0);
		estado=2;
		/* Se ocultan/muestran las texturas y textos de fondo. */
		for (int i = 0; i < GuiTextObjs.Length; i++)
			GuiTextObjs [i].guiText.enabled = enable;	
		for (int i = 0; i < GuiTextureObjs.Length; i++)
			GuiTextureObjs [i].guiTexture.enabled = enable;	
	
		mostrarBaking(enable);
		mostrarMastery(false);	/* Mastery y ingredients nunca se inician visibles. */
		mostrarIngredients(false);
		
		if(!enable)
			this.GuardaPoints();
	}
	
	public void GuardaPoints(){ /* Guarda los puntos actuales. */
		PlayerPrefs.SetInt("Bpoints", Bpoints);
		PlayerPrefs.SetInt("Mpoints", Mpoints);
		PlayerPrefs.SetInt("Ipoints", Ipoints);
		PlayerPrefs.SetInt("Monedas", monedas);
	}
	
	public void aumenta(){
		switch(estado){
		case 1: /* Ingredients */
			if(monedas>9 && Ipoints<5){	/* En caso de quedar puntos sin gastar, y no haber llegado al maximo de la rama. */
				Ipoints++;
				monedas-=10;
				PlayerPrefs.SetInt("Monedas", monedas);
				actualizaTalentos();
			}		
			break;
		case 2: /* Baking */
			if(monedas>9 && Bpoints<5){
				Bpoints++;
				monedas-=10;
				PlayerPrefs.SetInt("Monedas", monedas);
				actualizaTalentos();
			}	
			break;
		case 3: /* Mastery */
			if(monedas>9 && Mpoints<5){
				Mpoints++;
				monedas-=10;
				PlayerPrefs.SetInt("Monedas", monedas);
				actualizaTalentos();
			}
			break;
		}
	}
	
	public void disminuye(){
		switch(estado){
		case 1: /* Ingredients */
			if(Ipoints>0){	/* Mientras queden puntos en la rama. */
				Ipoints--;
				monedas+=10;
				PlayerPrefs.SetInt("Monedas", monedas);
				actualizaTalentos();
			}	
			break;
		case 2: /* Baking */
			if(Bpoints>0){
				Bpoints--;
				monedas+=10;
				PlayerPrefs.SetInt("Monedas", monedas);
				actualizaTalentos();
			}
			break;
		case 3: /* Mastery */
			if(Mpoints>0){
				Mpoints--;
				monedas+=10;
				PlayerPrefs.SetInt("Monedas", monedas);
				actualizaTalentos();
			}
			break;
		}
	}
	
	/* FUNCIONES PRIVADAS */
	
	private void mostrarBaking(bool enable){
		for (int i = 0; i < GuiTextureBaking.Length; i++)
			GuiTextureBaking [i].guiTexture.enabled = enable;	
	}
	
	private void mostrarMastery(bool enable){
		for (int i = 0; i < GuiTextureMastery.Length; i++)
			GuiTextureMastery [i].guiTexture.enabled = enable;	
	}
	
	private void mostrarIngredients(bool enable){
		for (int i = 0; i < GuiTextureIngredients.Length; i++)
			GuiTextureIngredients [i].guiTexture.enabled = enable;	
	}
	
	private void actualizaTalentos(){	/* Se iluminan los botones y opciones disponibles/seleccionados y se oscurecen los otros. */
		/* Skills Mastery */
		if(Mpoints>0){
			GameObject.Find("MI").guiText.color=new Color(0f,0f,0f);	
		}else{
			GameObject.Find("MI").guiText.color=new Color(1f,1f,1f);	
		}
			
		if(Mpoints>1){
			GameObject.Find("MII").guiText.material.color=new Color(0f,0f,0f);	
		}else{
			GameObject.Find("MII").guiText.material.color=new Color(1f,1f,1f);
		}
			
		if(Mpoints>2){
			GameObject.Find("MIII").guiText.color=new Color(0f,0f,0f);	
		}else{
			GameObject.Find("MIII").guiText.color=new Color(1f,1f,1f);	
		}
		
		if(Mpoints>3){
			GameObject.Find("MIV").guiText.color=new Color(0f,0f,0f);	
		}else{
			GameObject.Find("MIV").guiText.color=new Color(1f,1f,1f);	
		}
			
		if(Mpoints>4){
			GameObject.Find("MV").guiText.color=new Color(0f,0f,0f);	
		}else{
			GameObject.Find("MV").guiText.color=new Color(1f,1f,1f);	
		}
		
		/* Skills Baking */
		if(Bpoints>0){
			GameObject.Find("BI").guiText.color=new Color(0f,0f,0f);	
		}else{
			GameObject.Find("BI").guiText.color=new Color(1f,1f,1f);	
		}
			
		if(Bpoints>1){
			GameObject.Find("BII").guiText.color=new Color(0f,0f,0f);		
		}else{
			GameObject.Find("BII").guiText.color=new Color(1f,1f,1f);	
		}
			
		if(Bpoints>2){
			GameObject.Find("BIII").guiText.color=new Color(0f,0f,0f);	
		}else{
			GameObject.Find("BIII").guiText.color=new Color(1f,1f,1f);	
		}
		
		if(Bpoints>3){
			GameObject.Find("BIV").guiText.color=new Color(0f,0f,0f);	
		}else{
			GameObject.Find("BIV").guiText.color=new Color(1f,1f,1f);	
		}
			
		if(Bpoints>4){
			GameObject.Find("BV").guiText.color=new Color(0f,0f,0f);	
		}else{
			GameObject.Find("BV").guiText.color=new Color(1f,1f,1f);	
		}
		
		/* Skills Ingredients */
		if(Ipoints>0){
			GameObject.Find("II").guiText.color=new Color(0f,0f,0f);	
		}else{
			GameObject.Find("II").guiText.color=new Color(1f,1f,1f);	
		}
			
		if(Ipoints>1){
			GameObject.Find("III").guiText.color=new Color(0f,0f,0f);	
		}else{
			GameObject.Find("III").guiText.color=new Color(1f,1f,1f);	
		}
			
		if(Ipoints>2){
			GameObject.Find("IIII").guiText.color=new Color(0f,0f,0f);	
		}else{
			GameObject.Find("IIII").guiText.color=new Color(1f,1f,1f);	
		}
		
		if(Ipoints>3){
			GameObject.Find("IIV").guiText.color=new Color(0f,0f,0f);	
		}else{
			GameObject.Find("IIV").guiText.color=new Color(1f,1f,1f);	
		}
			
		if(Ipoints>4){
			GameObject.Find("IV").guiText.color=new Color(0f,0f,0f);	
		}else{
			GameObject.Find("IV").guiText.color=new Color(1f,1f,1f);	
		}
		
		GUITexture boton;
		if(monedas==0){
			boton = GameObject.Find("Plus_Damage").guiTexture;
			boton.color=new Color(boton.color.r,boton.color.g,boton.color.b, 0.1f);
			
			boton = GameObject.Find("Plus_Defensive").guiTexture;
			boton.color=new Color(boton.color.r,boton.color.g,boton.color.b, 0.1f);
			
			boton = GameObject.Find("Plus_Mastery").guiTexture;
			boton.color=new Color(boton.color.r,boton.color.g,boton.color.b, 0.1f);
		} else {
			boton = GameObject.Find("Plus_Damage").guiTexture;
			boton.color=new Color(boton.color.r,boton.color.g,boton.color.b, 1f);
			
			boton = GameObject.Find("Plus_Defensive").guiTexture;
			boton.color=new Color(boton.color.r,boton.color.g,boton.color.b, 1f);
			
			boton = GameObject.Find("Plus_Mastery").guiTexture;
			boton.color=new Color(boton.color.r,boton.color.g,boton.color.b, 1f);
		}
		if(Bpoints==0){
			boton = GameObject.Find("Minus_Damage").guiTexture;
			boton.color=new Color(boton.color.r,boton.color.g,boton.color.b, 0.1f);
		} else {
			boton = GameObject.Find("Minus_Damage").guiTexture;
			boton.color=new Color(boton.color.r,boton.color.g,boton.color.b, 1f);
		}
		if(Mpoints==0){
			boton = GameObject.Find("Minus_Mastery").guiTexture;
			boton.color=new Color(boton.color.r,boton.color.g,boton.color.b, 0.1f);
		} else {
			boton = GameObject.Find("Minus_Mastery").guiTexture;
			boton.color=new Color(boton.color.r,boton.color.g,boton.color.b, 1f);
		}
		if(Ipoints==0){
			boton = GameObject.Find("Minus_Defensive").guiTexture;
			boton.color=new Color(boton.color.r,boton.color.g,boton.color.b, 0.1f);
		} else {
			boton = GameObject.Find("Minus_Defensive").guiTexture;
			boton.color=new Color(boton.color.r,boton.color.g,boton.color.b, 1f);
		}
	}
}
