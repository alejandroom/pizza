public abstract class NPC : Personaje {
	public int health;
	public abstract int ejecutaTurno();
	public abstract void stun();
	public abstract int quitarVidaPlana(int pupa);
}
