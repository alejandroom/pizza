using UnityEngine;
using System.Collections;

public class SelectorEscenasController : MenuController {

	private GUITexture flecha_izk;
	private GUITexture flecha_der;
	private GUIText seleccionado;
	private int actualScene;
	private GUITexture escena;
	private ArrayList paginas;
	private int avance;
	
	private int numEscenas;
	private string[] escenas;
	private string[] nombresEscenas;
	
	private GameObject[] escenasTexts;
	private GameObject[] escenasTextures;
	
	public SelectorEscenasController(int numEscenas, string[] escenas, string[] nombresEscenas){
		this.numEscenas=numEscenas;
		this.escenas=escenas;
		this.nombresEscenas=nombresEscenas;
		avance = PlayerPrefs.GetInt("avance", 1);
		paginas = new ArrayList();
		GUITexture[] aux;
		
		flecha_der = GameObject.Find ("Flecha_derecha").guiTexture;
		flecha_izk = GameObject.Find ("Flecha_izquierda").guiTexture;
		seleccionado = GameObject.Find ("SS_Selected").guiText;
		
		int maxPagina = (numEscenas/6)+1;
		for(int i=1;i<=maxPagina;i++){
			paginas.Add(GameObject.Find("Pagina " + i));		
		}
		
		for(int i=0;i<maxPagina;i++){		
			aux = ((GameObject)paginas.ToArray(typeof(GameObject)).GetValue(i)).GetComponentsInChildren<GUITexture>();
			foreach(GUITexture aux_aux in aux){
				if(aux_aux.name.Contains("Scene"))
					aux_aux.color=new Color(aux_aux.color.r,aux_aux.color.g,aux_aux.color.b, 0.1f);
			}
			
			enciendePagina(i, false);
		}
	
		aux = ((GameObject)paginas.ToArray(typeof(GameObject)).GetValue(1)).GetComponentsInChildren<GUITexture>();
		foreach(GUITexture aux_aux in aux){
			if(aux_aux.name==("Scene 1"))
				escena = aux_aux;
		}
		
		flecha_izk.color=new Color(flecha_izk.color.r,flecha_izk.color.g,flecha_izk.color.b, 0.1f);
		flecha_der.color=new Color(flecha_der.color.r,flecha_der.color.g,flecha_der.color.b, 1f);
		
		seleccionado.text=(string)escenas.GetValue(0);
		
		escenasTexts = GameObject.FindGameObjectsWithTag ("Talentos_text");
		escenasTextures = GameObject.FindGameObjectsWithTag ("Talentos");
		mostrar(false);
	}
	
	public override void next(){
		if(actualScene==1)
			return;
		muestraSeleccion (actualScene-1);
	}
	
	public override void previous(){
		if(actualScene==numEscenas)
			return;
		muestraSeleccion (actualScene+1);	
	}
		
	public override int actual(){
		if(actualScene>avance){
			Debug.LogError ("Tramposo!!");
			return -1;
		}
		Application.LoadLevel ((string)escenas.GetValue(actualScene-1));
		return 0;
	}
	
	public override void mostrar(bool enable)
	{
		for (int i = 0; i < escenasTexts.Length; i++)
			escenasTexts[i].guiText.enabled = enable;	
		
		for (int i = 0; i < escenasTextures.Length; i++)
			escenasTextures[i].guiTexture.enabled = enable;
		
		if(!enable){
			int maxPagina = (numEscenas/6)+1;
			for(int i=0;i<maxPagina;i++)
				enciendePagina(i,false);
		}
		if(enable){
			actualScene=avance;
			this.muestraSeleccion(avance);
		}
	}
	
	private void enciendePagina(int i, bool enable){
		int pagina = i;
		int paginaAvance = ((avance-1)/6);
		GUITexture[] aux = ((GameObject)paginas.ToArray(typeof(GameObject)).GetValue(i)).GetComponentsInChildren<GUITexture>();
		
		if(paginaAvance<pagina || !enable){
			foreach(GUITexture aux_aux in aux){
				aux_aux.enabled=enable;
			}
		}else if(paginaAvance>pagina){
			foreach(GUITexture aux_aux in aux){
				if(aux_aux.name.Contains("Scene"))
					aux_aux.enabled=enable;
			}
		}else{
			int escenaAvance = ((avance-1)%6+1);
			foreach(GUITexture aux_aux in aux){
				if(aux_aux.name.Contains("Scene")){
					aux_aux.enabled=enable;
				}else{
					string[] a1 = aux_aux.name.Split(' ');
					int indice;
					int.TryParse((string)a1.GetValue(1), out indice);
					if(escenaAvance>=indice)
						aux_aux.enabled=false;
					else
						aux_aux.enabled=true;
				}
			}
		}
	}
	
	private void muestraSeleccion(int siguiente){
		if(siguiente==1)
			flecha_izk.color=new Color(flecha_izk.color.r,flecha_izk.color.g,flecha_izk.color.b, 0.1f);
		else
			flecha_izk.color=new Color(flecha_izk.color.r,flecha_izk.color.g,flecha_izk.color.b, 1f);
		
		if(siguiente==numEscenas)
			flecha_der.color=new Color(flecha_der.color.r,flecha_der.color.g,flecha_der.color.b, 0.1f);
		else
			flecha_der.color=new Color(flecha_der.color.r,flecha_der.color.g,flecha_der.color.b, 1f);
		
		int pagina = ((siguiente-1)/6);
		int escena_aux = ((siguiente-1)%6+1);
		
		if(pagina != ((actualScene-1)/6)){
			enciendePagina(((actualScene-1)/6), false);
		}
		enciendePagina(pagina, true);
		
		GUITexture[] aux = ((GameObject)paginas.ToArray(typeof(GameObject)).GetValue(pagina)).GetComponentsInChildren<GUITexture>();
		GUITexture AEncender = null;
		foreach(GUITexture aux_aux in aux){
			if(aux_aux.name==("Scene " + escena_aux))
				AEncender = aux_aux;
		}
		
		escena.color=new Color(escena.color.r,escena.color.g,escena.color.b, 0.1f);
		AEncender.color=new Color(AEncender.color.r,AEncender.color.g,AEncender.color.b, 1f);
		
		escena=AEncender;
		actualScene=siguiente;

		this.seleccionado.text=(string)nombresEscenas.GetValue(siguiente-1);
	}
}
