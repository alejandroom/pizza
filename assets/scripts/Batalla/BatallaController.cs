using UnityEngine;
using System.Collections;

public class BatallaController {
	private NPC enemy;
	private Player player;
	
	private float segundos=2.0f;
	private float tiempoBase;
	private bool turnoPlayer;
	private BuffController buffos;
	
	
	public BatallaController(GenericModelController enemyCon, PlayerModelController playerCon){
		buffos=new BuffController();	
		enemy=new DefaultEnemy(enemyCon);
		player=new Player(playerCon, buffos, enemy);
		turnoPlayer=true;
	}
	
	public bool turno(){
		return turnoPlayer;	
	}
	
	public int action(int skill){
		int ret;
		if(tiempoBase+segundos>Time.realtimeSinceStartup)
			return 3;
		if(turnoPlayer){
			/* Se ejecuta la skill de player, se llama a las animaciones correspondientes.*/
			ret = enemy.quitarVida(player.ejecutaSkill(skill));
			tiempoBase=Time.realtimeSinceStartup;
			turnoPlayer=false;

			if(ret<=0){
				GameObject.Find ("VictoryText1").guiText.text="Has ganado!";
				GameObject.Find ("VictoryText2").guiText.text="Pulse cualquier tecla para continuar.";
				ret=0;
			}else{
				ret=1;
			}
		}else{
			/* Se ejecuta la skill de enemigo, se llama a las animaciones correspondientes.*/
			ret = player.quitarVida(enemy.ejecutaTurno());
			tiempoBase=Time.realtimeSinceStartup;
			turnoPlayer=true;

			buffos.siguienteTurno();

			if(ret<=0){
				GameObject.Find ("VictoryText1").guiText.text="Has perdido!";
				GameObject.Find ("VictoryText2").guiText.text="Pulse cualquier tecla para continuar.";
				ret=-1;
			}else{
				ret=2;
			}
		}

		return ret;
	}
}
