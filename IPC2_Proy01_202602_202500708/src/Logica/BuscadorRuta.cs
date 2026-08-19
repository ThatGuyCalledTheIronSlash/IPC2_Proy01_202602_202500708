// Búsqueda de una ruta válida: backtracking sobre la matriz enlazada.

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

    private bool Explorar(NodoCelda? actual, NodoCelda objetivo, Robot robot)
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

        foreach (NodoCelda? vecino in VecinosHaciaObjetivo(actual, objetivo))
        {
            if (Explorar(vecino, objetivo, estado)) return true;
        }

        camino.Desapilar();                                  // retrocede
        visitado[actual.Fila, actual.Columna] = false;
        return false;
    }


//Ordena los 4 vecinos más cercanos al objetivo.
private NodoCelda?[] VecinosHaciaObjetivo(NodoCelda actual, NodoCelda objetivo)
{
    NodoCelda?[] vecinos = { actual.Arriba, actual.Abajo, actual.Izquierda, actual.Derecha };
    int[] distancias = new int[4];

    for (int i = 0; i < 4; i++)
        distancias[i] = vecinos[i] == null ? int.MaxValue : Distancia(vecinos[i]!, objetivo);

    // Ordenamiento burbuja
    for (int i = 0; i < 4; i++)
        for (int j = 0; j < 3 - i; j++)
            if (distancias[j] > distancias[j + 1])
            {
                (distancias[j], distancias[j + 1]) = (distancias[j + 1], distancias[j]);
                (vecinos[j], vecinos[j + 1]) = (vecinos[j + 1], vecinos[j]);
            }

    return vecinos;
}

private int Distancia(NodoCelda a, NodoCelda objetivo)
{
    return Math.Abs(a.Fila - objetivo.Fila) + Math.Abs(a.Columna - objetivo.Columna);
}

    private ListaSimple<NodoCelda> Reconstruir()
    {
        Pila<NodoCelda> invertida = new Pila<NodoCelda>();
        while (!camino.EstaVacia)
        {
            NodoCelda? nodo = camino.Desapilar();
            if (nodo != null) invertida.Apilar(nodo);
        }

        ListaSimple<NodoCelda> ruta = new ListaSimple<NodoCelda>();
        while (!invertida.EstaVacia)
        {
            NodoCelda? nodo = invertida.Desapilar();
            if (nodo != null) ruta.Insertar(nodo);
        }
        return ruta;
    }
}