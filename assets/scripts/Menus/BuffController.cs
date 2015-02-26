using UnityEngine;
using System.Collections;

public class BuffController {
	private static int maxBuffs=5;
	
	private static string[] nombresBuff={"Extra_Spicy", "Juggernaut", "Enajenacion", "Porrazo", "Rage", "Italian_Fury"}; 
	private static int[] turnosBuff    ={3            , 3           , 3            , 2        , 2     , 3             };
	
	private static string[] nombresMarcas={"buff1", "buff2", "buff3", "buff4", "buff5"}; 
	private static string[] nombresMarcasText={"buffText1", "buffText2", "buffText3", "buffText4", "buffText5"}; 
	
	private ArrayList marcas;        /* Todos los huecos para iconos */
	private ArrayList marcasText;    /* Todos los huecos para textos */
	
	private ArrayList texturas;      /* Todas las texturas posibles */
	
	private ArrayList turnos;        /* Todos los turnos restantes actuales */
	private ArrayList buffsActuales; /* Todos los buffs actuales */
	
	public BuffController(){
		marcas=new ArrayList();
		for(int i=0;i<maxBuffs;i++){
			marcas.Add(GameObject.Find(nombresMarcas[i]));
			((GameObject)marcas[i]).guiTexture.enabled=false;
		}
		
		marcasText=new ArrayList();
		for(int i=0;i<maxBuffs;i++){
			marcasText.Add(GameObject.Find(nombresMarcasText[i]));
			((GameObject)marcasText[i]).guiText.text="";
		}
		
		
		texturas=new ArrayList();
		for(int i=0;i<nombresBuff.Length;i++){
			texturas.Add(Resources.Load("Buffs/" + nombresBuff[i]) as Texture);
		}
		
		
		buffsActuales=new ArrayList();
		turnos=new ArrayList();
	}
	
	public void nuevoBuff(string buff){
		int i=0, numeroBuff=0;
		/* Si ya tenemos el maximo numero de buffs, salimos */
		if(buffsActuales.Count>=maxBuffs)
			return;

		/* Guardamos el numero de buff. Si el nombre no se encuentra entre los buffs disponibles, salimos. */
		for( i=0;i<nombresBuff.Length;i++){
			if(buff.Equals(nombresBuff[i])){
				numeroBuff=i;
			}
		}
		if(numeroBuff==0)
			return;
		
		/* Recorremos los buffs actuales buscando coincidencias. Si coincide renovamos los turnos y salimos. */
		for (i=0;i<buffsActuales.Count;i++){
			if(buff.Equals(buffsActuales[i])){
				turnos[i]=turnosBuff[numeroBuff];
				((GameObject)marcasText[i]).guiText.text=turnosBuff[numeroBuff].ToString();
				return;
			}
		}
		
		/* Asignamos el ultimo buff */
		turnos.Add(turnosBuff[numeroBuff]);
		((GameObject)marcasText[i]).guiText.text=turnosBuff[numeroBuff].ToString();
		((GameObject)marcas[i]).guiTexture.texture=(Texture)texturas[numeroBuff];
		((GameObject)marcas[i]).guiTexture.enabled=true;
		buffsActuales.Add(buff);
	}
	
	public void siguienteTurno(){
		for(int i=0;i<buffsActuales.Count;i++){
			turnos[i]=((int)turnos[i])-1;
			if(((int)turnos[i])==0){
				for(int j=i;j<buffsActuales.Count-1;j++){
					turnos[j]=((int)turnos[j+1]);
					buffsActuales[j]=buffsActuales[j+1];
					((GameObject)marcasText[j]).guiText.text=((GameObject)marcasText[j+1]).guiText.text;
					((GameObject)marcas[j]).guiTexture.texture=((GameObject)marcas[j+1]).guiTexture.texture;
				}
				((GameObject)marcasText[buffsActuales.Count-1]).guiText.text="";
				((GameObject)marcas[buffsActuales.Count-1]).guiTexture.enabled=false;
				buffsActuales.RemoveAt(buffsActuales.Count-1);
				i--;
			}else{
				((GameObject)marcasText[i]).guiText.text=((int)turnos[i]).ToString();
			}
		}
	}
	
	public string []dameBuffs(){/*TODO*/
		return nombresBuff;
	}
}
