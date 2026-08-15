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