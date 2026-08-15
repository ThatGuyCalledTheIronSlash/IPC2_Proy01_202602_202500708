// Búsqueda de una ruta válida: backtracking sobre la matriz enlazada.
// Basta con encontrar UNA ruta que cumpla la misión.

public class BuscadorRuta
{
    private MatrizCiudad ciudad;
    private bool[,] visitado;
    private Pila<NodoCelda> camino = new Pila<NodoCelda>();

    public BuscadorRuta(MatrizCiudad ciudad)
    {
        this.ciudad = ciudad;
        visitado = new bool[ciudad.Filas + 2, ciudad.Columnas + 2];
    }

    public ListaSimple<NodoCelda>? Buscar(NodoCelda entrada, NodoCelda objetivo, Robot robot)
    {
        if (Explorar(entrada, objetivo, robot)) return Reconstruir();
        return null;   // ninguna ruta posible -> "Misión Imposible"
    }

    private bool Explorar(NodoCelda actual, NodoCelda objetivo, Robot robot)
    {
        if (actual == null) return false;
        if (visitado[actual.Fila, actual.Columna]) return false;

        // El recurso es objetivo pero no se transita: se llega a una celda vecina
        bool esObjetivo = actual.Fila == objetivo.Fila && actual.Columna == objetivo.Columna;
        if (!esObjetivo && !robot.PuedeTransitar(actual)) return false;

        visitado[actual.Fila, actual.Columna] = true;
        Robot estado = robot.Clonar();
        estado.Ingresar(actual);
        camino.Apilar(actual);

        if (esObjetivo) return true;

        if (Explorar(actual.Arriba, objetivo, estado)) return true;
        if (Explorar(actual.Derecha, objetivo, estado)) return true;
        if (Explorar(actual.Abajo, objetivo, estado)) return true;
        if (Explorar(actual.Izquierda, objetivo, estado)) return true;

        camino.Desapilar();                                  // retrocede
        visitado[actual.Fila, actual.Columna] = false;
        return false;
    }

    private ListaSimple<NodoCelda> Reconstruir()
    {
        Pila<NodoCelda> invertida = new Pila<NodoCelda>();
        while (!camino.EstaVacia) invertida.Apilar(camino.Desapilar());

        ListaSimple<NodoCelda> ruta = new ListaSimple<NodoCelda>();
        while (!invertida.EstaVacia) ruta.Insertar(invertida.Desapilar());
        return ruta;
    }
}