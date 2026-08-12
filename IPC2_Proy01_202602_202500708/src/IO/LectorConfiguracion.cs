using System.Xml;

// Carga del XML. Si el nombre de la ciudad o del robot ya existe,
// los datos se ACTUALIZAN en el sistema de control.

public class LectorConfiguracion
{
    private SistemaControl sistema;

    public LectorConfiguracion(SistemaControl sistema) { this.sistema = sistema; }

    public void Cargar(string ruta)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(ruta);

        foreach (XmlNode nodoCiudad in doc.SelectNodes("//ciudad"))
        {
            XmlNode nombre = nodoCiudad.SelectSingleNode("nombre");
            string nom = nombre.InnerText.Trim();
            int filas = int.Parse(nombre.Attributes["filas"].Value);
            int cols  = int.Parse(nombre.Attributes["columnas"].Value);

            MatrizCiudad ciudad = new MatrizCiudad(nom, filas, cols);

            int f = 1;
            foreach (XmlNode fila in nodoCiudad.SelectNodes("fila"))
            {
                string valor = fila.InnerText.Replace("\"", "");
                for (int c = 1; c <= cols; c++)
                    ciudad.Insertar(f, c, c <= valor.Length ? valor[c - 1] : ' ');
                f++;
            }

            foreach (XmlNode um in nodoCiudad.SelectNodes("unidadMilitar"))
            {
                int uf = int.Parse(um.Attributes["fila"].Value);
                int uc = int.Parse(um.Attributes["columna"].Value);
                ciudad.ColocarMilitar(uf, uc, int.Parse(um.InnerText.Trim()));
            }

            sistema.AgregarOActualizarCiudad(ciudad);
        }

        foreach (XmlNode r in doc.SelectNodes("//robot"))
        {
            XmlNode nombreNodo = r.SelectSingleNode("nombre");
            string nombre = nombreNodo.InnerText.Trim();
            string tipo   = nombreNodo.Attributes["tipo"].Value;

            Robot robot = tipo == "ChapinFighter"
                ? new ChapinFighter(nombre, int.Parse(nombreNodo.Attributes["capacidad"].Value))
                : (Robot)new ChapinRescue(nombre);

            sistema.AgregarOActualizarRobot(robot);
        }
    }
}