using System.IO;
using System.Text;         
using System.Diagnostics;

public class ReporteRuta
{
    public void Generar(ListaSimple<NodoCelda> ruta, string archivo)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("digraph Ruta {");
        sb.AppendLine("  rankdir=LR;");
        sb.AppendLine("  node [shape=box style=filled fontname=\"Courier\"];");

        Nodo<NodoCelda> actual = ruta.Primero;
        int i = 0;
        while (actual != null)
        {
            NodoCelda c = actual.Dato;
            string id = "n" + i;
            string color = c.EsEntrada ? "\"#C6FF3D\""
                         : c.TieneMilitar ? "\"#FF6FD8\""
                         : c.EsCivil || c.EsRecurso ? "\"#7C3AED\""
                         : "white";

            sb.AppendLine("  " + id + " [label=\"" + c.Fila + "," + c.Columna +
                          "\" fillcolor=" + color + "];");

            if (actual.Siguiente != null)
                sb.AppendLine("  " + id + " -> n" + (i + 1) + ";");

            actual = actual.Siguiente;
            i++;
        }

        sb.AppendLine("}");
        File.WriteAllText(archivo, sb.ToString());

    try{

        // dot -Tpng ruta.dot -o ruta.png
        Process.Start("dot", "-Tpng " + archivo + " -o " + archivo + ".png");
        Console.WriteLine("Imagen PNG generada correctamente.");
    }
        catch (Exception ex)
        {
        Console.WriteLine("Error al generar la imagen PNG: " + ex.Message);
        }
    }
}