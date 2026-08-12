public class Cola<T>
{
    private Nodo<T> frente, final;
    public bool EstaVacia { get { return frente == null; } }

    public void Encolar(T dato)
    {
        Nodo<T> nuevo = new Nodo<T>(dato);
        if (final == null) { frente = final = nuevo; }
        else { final.Siguiente = nuevo; final = nuevo; }
    }

    public T Desencolar()
    {
        if (frente == null) return default(T);
        T dato = frente.Dato;
        frente = frente.Siguiente;
        if (frente == null) final = null;
        return dato;
    }
}