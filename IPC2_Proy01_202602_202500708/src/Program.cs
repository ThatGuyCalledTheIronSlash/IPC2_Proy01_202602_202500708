class Program
{
    static SistemaControl sistema = new SistemaControl();
    

    static void Main()
    {

        ProbarTDA();   // <-- borra o comenta esta línea después de verificar
        Console.WriteLine();


        bool salir = false;
        while (!salir)
        {
            Console.WriteLine("=== CHAPIN WARRIORS · SISTEMA DE CONTROL ===");
            Console.WriteLine("1. Cargar archivo de configuración (XML)");
            Console.WriteLine("2. Ver ciudades y robots cargados");
            Console.WriteLine("3. Planificar misión de rescate");
            Console.WriteLine("4. Planificar misión de extracción de recurso");
            Console.WriteLine("5. Generar reporte Graphviz de la última ruta");
            Console.WriteLine("6. Salir");
            Console.Write("Opción: ");

            switch (Console.ReadLine())
            {
                case "1": CargarArchivo(); break;
                case "2": sistema.Listar(); break;
                case "3": PlanificarMision("rescate"); break;
                case "4": PlanificarMision("extraccion"); break;
                case "5": sistema.GenerarReporte(); break;
                case "6": salir = true; break;
                default: Console.WriteLine("Opción inválida."); break;
            }
        }
    }

    static void PlanificarMision(string tipo)
    {
        MatrizCiudad ciudad = sistema.SeleccionarCiudad();
        if (ciudad == null) { Console.WriteLine("No hay ciudades cargadas."); return; }
        Robot robot = sistema.SeleccionarRobot(tipo);
        if (robot == null) { Console.WriteLine("No hay robots disponibles para este tipo de misión."); return; }
        NodoCelda entrada = sistema.SeleccionarEntrada(ciudad);
        if (entrada == null) { Console.WriteLine("No hay entrada disponible para esta ciudad."); return; }
        NodoCelda objetivo = sistema.SeleccionarObjetivo(ciudad, tipo);
        if (objetivo == null) { Console.WriteLine("No hay objetivo disponible para esta misión."); return; }

        ListaSimple<NodoCelda> ruta =
            new BuscadorRuta(ciudad).Buscar(entrada, objetivo, robot);

        if (ruta == null) { Console.WriteLine("Misión Imposible"); return; }

        sistema.ImprimirRuta(ruta, tipo, objetivo);
    }

    static void CargarArchivo()
        {
            Console.Write("Ruta del archivo XML: ");
            string ruta = Console.ReadLine();

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

//----------------------------------
static void ProbarTDA()
{
    Console.WriteLine("=== PRUEBA: ListaSimple<int> ===");
    ListaSimple<int> lista = new ListaSimple<int>();
    lista.Insertar(10);
    lista.Insertar(20);
    lista.Insertar(30);
    Console.WriteLine($"Cantidad esperada: 3 | Cantidad real: {lista.Cantidad}");
    for (int i = 0; i < lista.Cantidad; i++)
        Console.Write($"{lista.ObtenerEn(i)} ");
    Console.WriteLine("\n(Esperado: 10 20 30)\n");

    Console.WriteLine("=== PRUEBA: Pila<int> ===");
    Pila<int> pila = new Pila<int>();
    pila.Apilar(1);
    pila.Apilar(2);
    pila.Apilar(3);
    Console.WriteLine($"Cantidad esperada: 3 | Cantidad real: {pila.Cantidad}");
    Console.WriteLine("Desapilando (esperado: 3 2 1, orden LIFO):");
    while (!pila.EstaVacia)
        Console.Write($"{pila.Desapilar()} ");
    Console.WriteLine($"\n¿Vacía ahora? {pila.EstaVacia} (esperado: True)\n");

    Console.WriteLine("=== PRUEBA: Cola<int> ===");
    Cola<int> cola = new Cola<int>();
    cola.Encolar(100);
    cola.Encolar(200);
    cola.Encolar(300);
    Console.WriteLine("Desencolando (esperado: 100 200 300, orden FIFO):");
    while (!cola.EstaVacia)
        Console.Write($"{cola.Desencolar()} ");
    Console.WriteLine($"\n¿Vacía ahora? {cola.EstaVacia} (esperado: True)");
}


}