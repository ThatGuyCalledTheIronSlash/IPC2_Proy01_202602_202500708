public class NodoCelda
{
    private char contenido;        // '*'  ' '  'E'  'C'  'R'
    private int fila, columna;
    private int capacidadMilitar;  // 0 si la celda no tiene unidad militar

    public NodoCelda? Arriba, Abajo, Izquierda, Derecha;

    public NodoCelda(int fila, int columna, char contenido)
    {
        this.fila = fila;
        this.columna = columna;
        this.contenido = contenido;
        this.capacidadMilitar = 0;
    }

    public int Fila     { get { return fila; } }
    public int Columna  { get { return columna; } }
    public char Contenido { get { return contenido; } set { contenido = value; } }
    public int CapacidadMilitar { get { return capacidadMilitar; } set { capacidadMilitar = value; } }

    public bool EsIntransitable  { get { return contenido == '*'; } }
    public bool EsRecurso        { get { return contenido == 'R'; } }
    public bool EsCivil          { get { return contenido == 'C'; } }
    public bool EsEntrada        { get { return contenido == 'E'; } }
    public bool TieneMilitar     { get { return capacidadMilitar > 0; } }
}