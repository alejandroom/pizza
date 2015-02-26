using UnityEngine;
using System.Collections;

public class DefaultEnemy : NPC {
	public float damage=10;
	private int maxHealth;

	private LoadingBar healthBar;
	private GenericModelController controler;
	private bool stunOn;
	private TextoBatalla texto;
	private bool special;
	
	private static int[] healths  ={50 ,100,120,140,160,180,200,220};
	private static float[] damages={8  ,10 ,15 ,20 ,25 ,30 ,35 ,40}; 

	private static string[] imagenes={"Bosses/arka","Bosses/pina","Bosses/carne","Bosses/champi","Bosses/pimiento","Bosses/salsa","Bosses/queso","Bosses/pepe"};

	private float dificultad;
	
	public DefaultEnemy(GenericModelController enemyCon){
		controler=enemyCon;

		int dificultad=PlayerPrefs.GetInt("Dificultad", 0);

		stunOn=false;
		special=false;
		
		maxHealth=healths[dificultad];
		damage=damages[dificultad];
		GameObject.Find("Enemy_Face").renderer.material.mainTexture=Resources.Load(imagenes[dificultad]) as Texture;

		health=maxHealth;
		healthBar=GameObject.Find ("EnemyHP").GetComponent<LoadingBar>();
		healthBar.barDisplay=1.0f;
		healthBar.barMin=1.0f;
		healthBar.emptyTex=Resources.Load("health_empty") as Texture2D;
		healthBar.fullTex=Resources.Load("health_full") as Texture2D;
		healthBar.woundTex=Resources.Load("health_wound") as Texture2D;

		texto=GameObject.Find("InfoBatalla").GetComponent<TextoBatalla>();
	}
	
	public override int ejecutaTurno(){
		if(stunOn){
			texto.muestra("El enemigo esta atontado!");
			stunOn=false;
			return 0;
		}
		if(special){
			controler.specialAttack();
			special=false;
			controler.resetSize();
			return Mathf.RoundToInt(damage*5);
		}
		if(Random.value<1.0f/3.0f){
			controler.bigger();
			texto.muestra("El enemigo esta preparando un superataque!");
			special=true;
			return 0;
		}
		controler.attack();
		return Mathf.RoundToInt(damage);
	}
	
	public override int quitarVida(int pupa){
		if(pupa==0)
			return health;
		
		health=health-pupa;
		healthBar.barMin=(health*1.0f)/(maxHealth*1.0f);
		
		if(health<=0){
			controler.death();
		}else
			controler.wound();
		
		return health;
	}
	
	public override int quitarVidaPlana(int pupa){
		if(pupa==0)
			return health;
		
		health=health-pupa;
		
		if(health<=0)
			health=1;

		healthBar.barMin=(health*0.01f);
		
		return health;
	}

	public override void stun(){
		texto.muestra("Has atontado al enemigo!");
		stunOn=true;
	}
}
