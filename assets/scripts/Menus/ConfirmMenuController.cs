using UnityEngine;
using System.Collections;

public class ConfirmMenuController : BoxMenuController {
	
	private static int MAX_SPEED = 30;
	private static int MIN_SPEED = 0;
	
	private Box Confirm_Box;
	private Box Yes_Box;
	private Box No_Box;
	
	private Quaternion EX_QUA;
	
	private bool estado_confirmacion;
	
	public ConfirmMenuController(){
		
		EX_QUA = GameObject.Find("EX_BOX").transform.rotation;
		
		Confirm_Box = new  Box(GameObject.Find("Confirm_Box"), EX_QUA, MAX_SPEED, MIN_SPEED);
		Yes_Box = new  Box(GameObject.Find("Yes_Box"), EX_QUA, MAX_SPEED, MIN_SPEED);
		No_Box = new  Box(GameObject.Find("No_Box"), EX_QUA, MAX_SPEED, MIN_SPEED);
		
		this.mostrar (false);
	}
	
	public override void next(){
		if(!estado_confirmacion) {
			estado_confirmacion = true;
			Yes_Box.acelerar(true);
			No_Box.acelerar(false);
		}
	}
	public override void previous(){
		if(estado_confirmacion) {
			estado_confirmacion = false;
			Yes_Box.acelerar(false);
			No_Box.acelerar(true);
		}
	}
	public override int actual(){
		if(estado_confirmacion)
			return 1;
		return 0;
	}
	
	public override void pause(){
		Yes_Box.acelerar(false);
		No_Box.acelerar(false);
	}
	
	public override void play(){
		if(estado_confirmacion)
			Yes_Box.acelerar(true);
		else
			No_Box.acelerar(true);
			
	}
	
	public override void mostrar (bool enable)
	{
		if(enable){
			Yes_Box.acelerar(true);
			No_Box.acelerar(false);
			estado_confirmacion = true;		
		}
		Yes_Box.apagar(enable);
		No_Box.apagar(enable);	
		Confirm_Box.apagar(enable);
	}
}
