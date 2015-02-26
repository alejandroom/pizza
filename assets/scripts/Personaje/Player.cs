using UnityEngine;
using System.Collections;

public class Player : Personaje {
	/*private int Bpoints;
	private int Ipoints;
	private int Mpoints;*/
	
	private int health;
	private int maxHealth;
	private float crit=0.1f;
	private float critDamage=2f;
	private float multiDamage=1f;
	private bool defensa;
	private float resistencia=1f;
	private float leech=0f;
	private float stun=0f;
	
	private int enajenacion=0;
	private int Juggernaut=0;
	private int fury=0;
	private int rage=0;
	private int porrazo=0;
	private int spicy=0;
	
	private bool acido=false;
	private bool ira=false;
	
	private LoadingBar healthBar;
	private ParticleSystem Clash;
	
	private PlayerModelController controler;
	private BuffController buffos;
	private NPC enemy;
	private ParticleSystem PS_spicy;
	private TextoBatalla texto;
	
	public Player(PlayerModelController playerCon, BuffController buffs,NPC enem){
		controler=playerCon;
		buffos=buffs;
		enemy=enem;
		
		maxHealth=200;
		health=maxHealth;
		healthBar=GameObject.Find ("PlayerHP").GetComponent<LoadingBar>();
		healthBar.barDisplay=1.0f;
		healthBar.barMin=1.0f;
		healthBar.emptyTex=Resources.Load("health_empty") as Texture2D;
		healthBar.fullTex=Resources.Load("health_full") as Texture2D;
		healthBar.woundTex=Resources.Load("health_wound") as Texture2D;
		
		GameObject.Find ("Shield").renderer.enabled=false;
		defensa=false;
		
		Clash=GameObject.Find("Clash").particleSystem;
		Clash.Stop();

		PS_spicy=GameObject.Find("PS_Spicy").GetComponent<ParticleSystem>();
		PS_spicy.Stop();
		
		int points = PlayerPrefs.GetInt("Bpoints", 0);
		if(points>0)
			multiDamage+=0.2f;
		if(points>2)
			acido=true;
		if(points>3)
			multiDamage+=0.3f;
		
		points = PlayerPrefs.GetInt("Mpoints", 0);
		if(points>0)
			crit+=0.15f;
		if(points>2)
			critDamage+=0.4f;
		if(points>3)
			ira=true;
		
		points = PlayerPrefs.GetInt("Ipoints", 0);
		if(points>0)
			resistencia-=0.2f;
		if(points>2)
			leech+=0.3f;
		if(points>3)
			stun+=0.2f;
		
		texto=GameObject.Find("InfoBatalla").GetComponent<TextoBatalla>();
	}
	
	public int ejecutaSkill(int skill){
		float damage=15;
		if(enajenacion>0){
			if(enajenacion==2){
				controler.attack();
				controler.bigger();
				damage=damage*1.25f;
			}
			if(enajenacion==1){
				controler.attack();
				controler.resetSize();
				damage=damage*1.5f;
			}
			enajenacion--;
		}else if(porrazo==1){
			Clash.Play();
			damage*=2;
			porrazo=0;
		}else if(rage==1){
			damage*=2f;
			rage=0;
			controler.doubleAttack();
		}else if(skill==0){
			controler.attack();
		} else if(skill==1){
			GameObject.Find ("Shield").renderer.enabled=true;
			defensa=true;
			damage=0;
		} else if(skill==2){
			/* Enajenacion: ataque en 3 turnos agrandandose: 100%, 125%, 150%*/
			buffos.nuevoBuff("Enajenacion");
			enajenacion=2;
			controler.attack();
			controler.bigger();
		}else if(skill==3){
			/* Porrazo: 1o 100% defensa enemigo rota. 2o superataque */
			buffos.nuevoBuff("Porrazo");
			GameObject.Find("EnemyShield").GetComponent<ControlEnemyShield>().go();
			damage=0;
			porrazo=1;
		}else if(skill==4){
			/* Doble durante 3 turnos */
			buffos.nuevoBuff("Rage");
			rage=1;
			damage*=2f;
			controler.doubleAttack();
		}else if(skill==5){
			/* Italian Fury: 3 turnos: Mas 100% de critico y 20% mas de daño */
			buffos.nuevoBuff("Italian_Fury");
			controler.enrage();
			fury=3;
			controler.attack();
		}else if(skill==6){
			/* Extra Spicy: Devuelves la mitad del daño recibido durante 3 turnos */
			buffos.nuevoBuff("Extra_Spicy");
			damage=0;
			spicy=3;
			PS_spicy.Play();
		}else if(skill==7){
			/* Juggernaut: 3 cargas de 3%damage + 20%reduc */
			buffos.nuevoBuff("Juggernaut");
			GameObject.Find("Jugger1").renderer.enabled=true;
			GameObject.Find("Jugger2").renderer.enabled=true;
			GameObject.Find("Jugger3").renderer.enabled=true;
			Juggernaut=4;
			damage=0;
		}

		if(fury>0){
			damage*=critDamage;
			damage*=1.2f;
			fury--;
			if(fury==0)
				controler.shiv();
		}else{
			if(Random.value<crit && damage!=0){
				damage*=critDamage;
				texto.muestra("Daño critico!");
			}
		}
		if(Juggernaut>0){
			damage*=(1+Juggernaut*0.03f);
			Juggernaut--;
			if(Juggernaut==2)
				GameObject.Find("Jugger1").renderer.enabled=false;
			if(Juggernaut==1)
				GameObject.Find("Jugger2").renderer.enabled=false;
			if(Juggernaut==0)
				GameObject.Find("Jugger3").renderer.enabled=false;
		}
		
		if(acido)
			multiDamage+=0.01f;

		if(ira)
			crit+=0.02f;

		if(Random.value<stun)
			enemy.stun();

		damage*=multiDamage;

		if(health+damage*leech>maxHealth){
			health=maxHealth;
			healthBar.barMin=(health*0.01f);
		}else{
			health=health+Mathf.RoundToInt(damage*leech);
			healthBar.barMin=(health*0.01f);
		}

		return Mathf.RoundToInt(damage);
	}
	
	public override int quitarVida(int pupa){
		if(pupa==0)
			return health;

		if(spicy>0){
			spicy--;
			enemy.quitarVidaPlana(pupa/2);
			if(spicy==0)
				PS_spicy.Stop();
		}
		
		if(defensa){
			GameObject.Find ("Shield").renderer.enabled=false;
			defensa=false;
			pupa=pupa/2;
		}
		if(Juggernaut>0){
			pupa=Mathf.RoundToInt(pupa*(1-Juggernaut*0.2f));
		}
			
		pupa=Mathf.RoundToInt(pupa*resistencia);
		health=health-pupa;
		healthBar.barMin=((health*1.0f)/(maxHealth*1.0f));
		
		if(health<=0)
			controler.death();
		else
			controler.wound();
		
		return health;
	}
}
