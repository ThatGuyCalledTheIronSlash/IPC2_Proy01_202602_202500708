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

public class ChapinRescue : Robot
{
    public ChapinRescue(string nombre) : base(nombre, "ChapinRescue") { }

    // Sin capacidad de combate: descarta toda celda con unidad militar
    public override bool PuedeTransitar(NodoCelda celda)
    {
        if (celda == null || celda.EsIntransitable) return false;
        if (celda.EsRecurso) return false;
        return !celda.TieneMilitar;
    }

    public override void Ingresar(NodoCelda celda) { }

    public override Robot Clonar() { return new ChapinRescue(Nombre); }
}

public class ChapinFighter : Robot
{
    private int capacidad;

    public ChapinFighter(string nombre, int capacidad) : base(nombre, "ChapinFighter")
    {
        this.capacidad = capacidad;
    }

    public int Capacidad { get { return capacidad; } set { capacidad = value; } }

    // Vence a la unidad militar solo si su capacidad es MAYOR
    public override bool PuedeTransitar(NodoCelda celda)
    {
        if (celda == null || celda.EsIntransitable) return false;
        if (celda.TieneMilitar) return capacidad > celda.CapacidadMilitar;
        return true;
    }

    public override void Ingresar(NodoCelda celda)
    {
        if (celda.TieneMilitar) capacidad -= celda.CapacidadMilitar;
    }

    public override Robot Clonar() { return new ChapinFighter(Nombre, capacidad); }
}