public class MatrizCiudad
{
    private NodoCelda? inicio;   // esquina (1,1)
    private int filas, columnas;
    private string nombre;

    public MatrizCiudad(string nombre, int filas, int columnas)
    {
        this.nombre = nombre;
        this.filas = filas;
        this.columnas = columnas;
    }

    public string Nombre  { get { return nombre; } }
    public int Filas      { get { return filas; } }
    public int Columnas   { get { return columnas; } }

    // Inserta enlazando con el vecino de arriba y el de la izquierda
    public void Insertar(int fila, int columna, char contenido)
    {
        NodoCelda nuevo = new NodoCelda(fila, columna, contenido);
        if (inicio == null) { inicio = nuevo; return; }

        NodoCelda? izq = Obtener(fila, columna - 1);
        if (izq != null) { izq.Derecha = nuevo; nuevo.Izquierda = izq; }

        NodoCelda? arr = Obtener(fila - 1, columna);
        if (arr != null) { arr.Abajo = nuevo; nuevo.Arriba = arr; }
    }

    public NodoCelda? Obtener(int fila, int columna)
    {
        if (fila < 1 || columna < 1) return null;
        NodoCelda? actual = inicio;
        for (int f = 1; f < fila && actual != null; f++) actual = actual.Abajo;
        for (int c = 1; c < columna && actual != null; c++) actual = actual.Derecha;
        return actual;
    }

    public void ColocarMilitar(int fila, int columna, int capacidad)
    {
        NodoCelda? n = Obtener(fila, columna);
        if (n != null) n.CapacidadMilitar = capacidad;
    }
}