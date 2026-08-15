public class ListaSimple<T>
{
    private Nodo<T>? primero;
    private int cantidad;

    public int Cantidad { get { return cantidad; } }
    public Nodo<T>? Primero { get { return primero; } }

    public void Insertar(T dato)
    {
        Nodo<T> nuevo = new Nodo<T>(dato);
        if (primero == null) { primero = nuevo; }
        else
        {
            Nodo<T>? actual = primero;
            while (actual.Siguiente != null) actual = actual.Siguiente;
            actual.Siguiente = nuevo;
        }
        cantidad++;
    }

    public T? ObtenerEn(int indice)
    {
        Nodo<T>? actual = primero;
        for (int i = 0; i < indice && actual != null; i++) actual = actual.Siguiente;
        return actual == null ? default(T) : actual.Dato;
    }
}
