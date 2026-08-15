public class Nodo<T>
{
    private T dato;
    public Nodo<T>? Siguiente;
    public Nodo(T dato) { this.dato = dato; }
    public T Dato { get { return dato; } set { dato = value; } }
}