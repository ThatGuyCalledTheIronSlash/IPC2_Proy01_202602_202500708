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

        Nodo<NodoCelda>? actual = ruta.Primero;
        int i = 0;
        while (actual != null)
        {
            NodoCelda? c = actual.Dato;
            string id = "n" + i;
            string color;
            if (c.EsEntrada) color = "\"#C6FF3D\"";
                else if (c.TieneMilitar) color = "\"#FF6FD8\"";
                    else if (c.EsCivil || c.EsRecurso) color = "\"#7C3AED\"";
                        else color = "white";

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

        var proceso = Process.Start("dot", "-Tpng " + archivo + " -o " + archivo + ".png");
        proceso.WaitForExit();
        Console.WriteLine("Imagen PNG generada correctamente.");
    }
        catch (Exception)
        {
            Console.WriteLine("Graphviz no está instalado o no se encontró 'dot' en el PATH.");
            Console.WriteLine($"El archivo {archivo} sí se generó — puedes compilarlo manualmente después.");
                
        }
    }
}