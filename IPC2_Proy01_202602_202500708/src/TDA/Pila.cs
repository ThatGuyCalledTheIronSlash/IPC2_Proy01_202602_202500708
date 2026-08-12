public class Pila<T>
{
    private Nodo<T> tope;
    private int cantidad;
    public int Cantidad { get { return cantidad; } }
    public bool EstaVacia { get { return tope == null; } }

    public void Apilar(T dato)
    {
        Nodo<T> nuevo = new Nodo<T>(dato);
        nuevo.Siguiente = tope;
        tope = nuevo;
        cantidad++;
    }

    public T Desapilar()
    {
        if (tope == null) return default(T);
        T dato = tope.Dato;
        tope = tope.Siguiente;
        cantidad--;
        return dato;
    }
}
