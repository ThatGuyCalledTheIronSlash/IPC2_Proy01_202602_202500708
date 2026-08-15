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