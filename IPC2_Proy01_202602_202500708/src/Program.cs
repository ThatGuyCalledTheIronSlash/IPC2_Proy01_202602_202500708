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
                case "6": VerMapa(); Pausa(); break;
                case "7": salir = true; break;
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
        Console.WriteLine("     ║6. Ver Mapa de una Ciudad                    ║");
        Console.WriteLine("     ║7. Salir                                     ║");
        Console.WriteLine("     ╚═════════════════════════════════════════════╝");
        Console.Write("\nOpción: ");
    }
//-------------------------------------------------------------------------
    static void Pausa()
    {
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();
    }
//-----------------Funciones del Menú--------------------------------------
    static void PlanificarMision(string tipo)
    {
            MatrizCiudad? ciudad = sistema.SeleccionarCiudad(tipo);
                if (ciudad == null) return;

            sistema.ImprimirMatriz(ciudad); //Muestra mapa de la ciudad seleccionada.

            Robot? robot = sistema.SeleccionarRobot(tipo);
                if (robot == null) return;

            NodoCelda? entrada = sistema.SeleccionarEntrada(ciudad);
                if (entrada == null) return;

            NodoCelda? objetivo = sistema.SeleccionarObjetivo(ciudad, tipo);
                if (objetivo == null) return;

            ListaSimple<NodoCelda>? ruta = new BuscadorRuta(ciudad).Buscar(entrada, objetivo, robot);
                if (ruta == null) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Misión Imposible"); Console.ResetColor(); return; }

            sistema.ImprimirRuta(ruta, tipo, objetivo, robot, ciudad);
    }
//-------------------------------------------------------------------------------
    static void VerMapa()
    {
        MatrizCiudad? ciudad = sistema.SeleccionarCiudadCualquiera();
        if (ciudad == null) return;

        sistema.ImprimirMatriz(ciudad);
    }
//--------------------------------Cargar Archivo--------------------------------
    static void CargarArchivo()
        {
            Console.Write("Ruta del archivo XML: ");
            string ruta = Console.ReadLine() ??"";

            try
            {
                new LectorConfiguracion(sistema).Cargar(ruta);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("     ╔═══════════════════════════════╗");
                Console.WriteLine("     ║ Archivo cargado correctamente ║");
                Console.WriteLine("     ╚═══════════════════════════════╝");
                Console.ResetColor();
            }
            catch (Exception)    
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("     ╔═══════════════════════════════╗");
                Console.WriteLine("     ║   Error al cargar el archivo  ║");
                Console.WriteLine("     ╚═══════════════════════════════╝");
                Console.ResetColor();
            }
        }
}