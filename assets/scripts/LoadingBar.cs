using UnityEngine;
using System.Collections;
 
public class LoadingBar : MonoBehaviour {
    public float barDisplay;
    public Vector2 pos = new Vector2(0,28);
    public Vector2 size = new Vector2(600,200);
    public Texture2D emptyTex;
    public Texture2D fullTex;
    public Texture2D woundTex;
	
	public float barMin;
 
    void OnGUI() {
       //draw the background:
       GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
         GUI.Box(new Rect(0,0, size.x, size.y), emptyTex);
 
		
         //draw the filled-in part:
         GUI.BeginGroup(new Rect(0,0, size.x * barDisplay, size.y));
          GUI.Box(new Rect(0,0, size.x, size.y), woundTex);
		
         //draw the wound-in part:
          GUI.BeginGroup(new Rect(0,0, size.x * barMin, size.y));
           GUI.Box(new Rect(0,0, size.x, size.y), fullTex);
          GUI.EndGroup();
         GUI.EndGroup();
       GUI.EndGroup();
    }
	
	void Update(){
		if(barMin<barDisplay)
			barDisplay-=0.005f;
	}
}