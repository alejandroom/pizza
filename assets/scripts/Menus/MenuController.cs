/* Clase abstracta.
 * 	Controlador de menu generico.
 * 	Incluye los siguientes metodos:
 * 		-next() y previous() para desplazarse.
 * 		-actual() devolvera la posicion actual del menu.
 * 		-show() y hide() para hacer aparecer y desaparecer el menu.
 * */

public abstract class MenuController {	
	public abstract void next();
	public abstract void previous();
	public abstract int actual();
	
	public abstract void mostrar(bool enable);
	
	public void show(){
		this.mostrar(true);
	}
	
	public void hide(){
		this.mostrar(false);
	}
}
