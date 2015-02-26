using UnityEngine;
using System.Collections;

public class BatallaMenuController : MenuController {
	private int skillActual;
	private int maxSkills;
	
	private static string[] nombresBotones={"skill_button1", "skill_button2", "skill_button3", "skill_button4", "skill_button5"}; 
	private static string[] nombresMarcas={"skill_mark1", "skill_mark2", "skill_mark3", "skill_mark4", "skill_mark5"};  
	private static string[] descripciones={"Ataque basico.", /* Skill basica */
		                                   "Poderosa defensa que bloquea la mitad del siguiente \n" +
		                                   "  ataque enemigo.", /* Skill defensa */
		                                   "Ataque durante 3 turnos atacando cada vez mas fuerte \n" +
		                                   "  hasta un 50% mas de daño.", /* Skill baking 1 - Enajenacion */
		                                   "Ataque en 2 fases: \n" +
		                                   "  -Durante el primer turno se reduce la armadura del \n" +
		                                   "    enemigo en un 100%.\n" +
		                                   "  -En el segundo turno se descarga un devastador ataque.", /* Skill baking 2 - Porrazo */
		                                   "Ataque doble durante 2 turnos.", /* Skill mastery 1 - Rage */
		                                   "Durante los 3 siguientes turnos tienes un 100% de \n" +
		                                   "  probabilidad de critico y un 20% extra de daño.", /* Skill mastery 2 - Italian Fury*/
		                                   "Durante 3 turnos devuelves la mitad del daño que te \n" +
		                                   "  hacen, añadiendo un 60% de tu ataque.", /* Skill ingre 1 - Extra Spicy */
		                                   "Ganas 3 cargas de Juggernaut, que se pierden cada vez\n" +
		                                   "  que se recibe un ataque.\n" +
		                                   "    Juggernaut: Haces un 3% más y recibes un 20% menos\n" +
		                                   "      de daño."};  /* Skill ingre 2 - Juggernaut */
	
	private ArrayList marcas;
	private ArrayList skills;
	
	public BatallaMenuController(){
		cargaSkills();
		
		marcas=new ArrayList();
		for(int i=0;i<maxSkills;i++){
			marcas.Add(GameObject.Find(nombresMarcas[i]));
			((GameObject)marcas[i]).guiTexture.enabled=false;
		}	
		for(int i=maxSkills;i<5;i++){
			GameObject.Find(nombresMarcas[i]).guiTexture.enabled=false;
		}	
		
		skillActual=0;
		muestraSkill(skillActual);
	}
	
	public override void next(){
		if(skillActual<maxSkills-1){
			muestraSkill(skillActual+1);
			skillActual++;
		}
	}
	
	public override void previous(){
		if(skillActual>0){
			muestraSkill(skillActual-1);
			skillActual--;
		}
	}
	
	public override int actual(){
		return (int)skills[skillActual];
	}
	
	public override void mostrar(bool enable){
	}
	
	private void cargaSkills(){
		/* Se cargan los datos guardados del jugador. */
		int Bpoints = PlayerPrefs.GetInt("Bpoints", 0);
		int Mpoints = PlayerPrefs.GetInt("Mpoints", 0);
		int Ipoints = PlayerPrefs.GetInt("Ipoints", 0);

		skills=new ArrayList();
		skills.Add(0);
		skills.Add(1);
		ArrayList texturas = new ArrayList();
		texturas.Add(Resources.Load("Skills/skill_basica") as Texture);
		texturas.Add(Resources.Load("Skills/skill_defensa") as Texture);
		if(Bpoints>1){
			texturas.Add(Resources.Load("Skills/skill_baking1") as Texture);
			skills.Add(2);
		}
		if(Bpoints>4){
			texturas.Add(Resources.Load("Skills/skill_baking2") as Texture);
			skills.Add(3);
		}
		if(Mpoints>1){
			texturas.Add(Resources.Load("Skills/skill_mastery1") as Texture);
			skills.Add(4);
		}
		if(Mpoints>4){
			texturas.Add(Resources.Load("Skills/skill_mastery2") as Texture);
			skills.Add(5);
		}
		if(Ipoints>1){
			texturas.Add(Resources.Load("Skills/skill_ingre1") as Texture);
			skills.Add(6);
		}
		if(Ipoints>4){
			texturas.Add(Resources.Load("Skills/skill_ingre2") as Texture);
			skills.Add(7);
		}
		
		for(int i=0;i<texturas.Count;i++){
			GameObject.Find(nombresBotones[i]).guiTexture.texture=(Texture)texturas[i];
		}	
		
		for(int i=texturas.Count;i<5;i++)
			GameObject.Find(nombresBotones[i]).guiTexture.enabled=false;
		
		maxSkills=texturas.Count;
	}
	
	private void muestraSkill(int skill){
		GameObject.Find("Descripcion").guiText.text=descripciones[(int)skills[skill]];
		if(skill==skillActual)
			foreach(GameObject aux in marcas)
				aux.guiTexture.enabled=false;
		else
			((GameObject)marcas[skillActual]).guiTexture.enabled=false;	
		((GameObject)marcas[skill]).guiTexture.enabled=true;	
	}
}
