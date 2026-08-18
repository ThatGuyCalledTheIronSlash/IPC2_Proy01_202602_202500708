class Program
{
    static SistemaControl sistema = new SistemaControl();
//------------------------------------------------------------------------
//-----------------Menú Principal-----------------------------------------
    static void Main()
    {
        bool salir = false;
        while (!salir)
        {
            MostrarMenu();
            switch (Console.ReadLine())
            {
                case "1": CargarArchivo(); Pausa(); break;
                case "2": sistema.Listar(); Pausa(); break;
                case "3": PlanificarMision("rescate"); Pausa(); break;
                case "4": PlanificarMision("extraccion"); Pausa(); break;
                case "5": sistema.GenerarReporte(); Pausa(); break;
                case "6": salir = true; break;
                default: Console.WriteLine("Opción inválida."); Pausa(); break;
            }
        }
    }
//--------------------Menú Retrabajado-----------------------------
    static void MostrarMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("     ╔═════════════════════════════════════════════╗");
        Console.WriteLine("     ║            CHAPIN WARRIORS · SISTEMA        ║");
        Console.WriteLine("     ╚═════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine("     ║1. Cargar archivo de configuración (XML)     ║");
        Console.WriteLine("     ║2. Ver ciudades y robots cargados            ║");
        Console.WriteLine("     ║3. Planificar misión de rescate              ║");
        Console.WriteLine("     ║4. Planificar misión de extracción de recurso║");
        Console.WriteLine("     ║5. Generar reporte Graphviz de la última ruta║");
        Console.WriteLine("     ║6. Salir                                     ║");
        Console.WriteLine("     ╚═════════════════════════════════════════════╝");
        Console.Write("\nOpción: ");
    }
//-------------------------------------------------------------------------
    static void Pausa()
    {
        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();
    }
//-----------------Funciones del Menú--------------------------------------
    static void PlanificarMision(string tipo)
    {
            MatrizCiudad? ciudad = sistema.SeleccionarCiudad(tipo);
                if (ciudad == null) return;

            Robot? robot = sistema.SeleccionarRobot(tipo);
                if (robot == null) return;

            NodoCelda? entrada = sistema.SeleccionarEntrada(ciudad);
                if (entrada == null) return;

            NodoCelda? objetivo = sistema.SeleccionarObjetivo(ciudad, tipo);
                if (objetivo == null) return;

            ListaSimple<NodoCelda>? ruta = new BuscadorRuta(ciudad).Buscar(entrada, objetivo, robot);
                if (ruta == null) { Console.WriteLine("Misión Imposible"); return; }

            sistema.ImprimirRuta(ruta, tipo, objetivo, robot);
    }
//--------------------------------Cargar Archivo--------------------------------
    static void CargarArchivo()
        {
            Console.Write("Ruta del archivo XML: ");
            string ruta = Console.ReadLine() ??"";

            try
            {
                new LectorConfiguracion(sistema).Cargar(ruta);
                Console.WriteLine("Archivo cargado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar el archivo: {ex.Message}");
            }
        }
}