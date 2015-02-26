using UnityEngine;
using System.Collections;

public class TextoBatalla : MonoBehaviour {

	private float baseTime;
	private bool activo;
	private Color baseColor;

	void Start () {
		this.guiText.text="";
		activo=false;
		baseColor=this.guiText.color;
	}

	void Update () {
		if(activo){
			if(Time.realtimeSinceStartup-baseTime>1){
				this.guiText.color=new Color(this.guiText.color.r,this.guiText.color.g,this.guiText.color.b,this.guiText.color.a-0.01f);
				if(this.guiText.color.a<=0){
					this.guiText.text="";
					this.guiText.color=baseColor;
					activo=false;
				}
			}
		}	
	}

	public void muestra(string texto){
		baseTime=Time.realtimeSinceStartup;
		this.guiText.text=texto;
		activo=true;
	}
}
