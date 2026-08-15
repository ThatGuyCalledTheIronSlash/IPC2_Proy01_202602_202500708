public abstract class Robot
{
    private string nombre;
    private string tipo;

    protected Robot(string nombre, string tipo)
    {
        this.nombre = nombre;
        this.tipo = tipo;
    }

    public string Nombre { get { return nombre; } }
    public string Tipo   { get { return tipo; } }

    // ¿Puede pasar por esta celda con su estado actual?
    public abstract bool PuedeTransitar(NodoCelda celda);

    // Efecto de entrar a la celda (combate, gasto de capacidad)
    public abstract void Ingresar(NodoCelda celda);

    public abstract Robot Clonar();   // el backtracking prueba y descarta estados
}



