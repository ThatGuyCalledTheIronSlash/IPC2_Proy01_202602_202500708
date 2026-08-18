public class SistemaControl
{
    private ListaSimple<MatrizCiudad> ciudades = new ListaSimple<MatrizCiudad>();
    private ListaSimple<Robot> robots = new ListaSimple<Robot>();

    // Se guardan para poder generar el reporte Graphviz después (opción 5 del menú)
    private ListaSimple<NodoCelda>? ultimaRuta;
    private string? ultimoTipoMision;
    private NodoCelda? ultimoObjetivo;

    private ReporteRuta reporte = new ReporteRuta();

    // ---------- Carga / actualización ----------

    // Si el nombre ya existe, se reemplaza el dato del nodo (no se duplica).
    public void AgregarOActualizarCiudad(MatrizCiudad ciudad)
    {
        Nodo<MatrizCiudad>? actual = ciudades.Primero;
        while (actual != null)
        {
            if (actual.Dato.Nombre == ciudad.Nombre)
            {
                actual.Dato = ciudad;
                return;
            }
            actual = actual.Siguiente;
        }
        ciudades.Insertar(ciudad);
    }

    public void AgregarOActualizarRobot(Robot robot)
    {
        Nodo<Robot>? actual = robots.Primero;
        while (actual != null)
        {
            if (actual.Dato.Nombre == robot.Nombre)
            {
                actual.Dato = robot;
                return;
            }
            actual = actual.Siguiente;
        }
        robots.Insertar(robot);
    }

    // ---------- Listar cargados ----------

    public void Listar()
    {
        Console.WriteLine("--- Ciudades cargadas ---");
        if (ciudades.Cantidad == 0) Console.WriteLine("(ninguna)");
        Nodo<MatrizCiudad>? nc = ciudades.Primero;
        while (nc != null)
        {
            Console.WriteLine($"- {nc.Dato.Nombre} ({nc.Dato.Filas}x{nc.Dato.Columnas})");
            nc = nc.Siguiente;
        }

        Console.WriteLine("--- Robots cargados ---");
        if (robots.Cantidad == 0) Console.WriteLine("(ninguno)");
        Nodo<Robot>? nr = robots.Primero;
        while (nr != null)
        {
            string extra = nr.Dato is ChapinFighter f ? $" (capacidad {f.Capacidad})" : "";
            Console.WriteLine($"- {nr.Dato.Nombre} ({nr.Dato.Tipo}){extra}");
            nr = nr.Siguiente;
        }
    }

    // ---------- Selección para planificar misión ----------

    public MatrizCiudad? SeleccionarCiudad(String tipoMision)
    {
        ListaSimple<MatrizCiudad> candidatas = new ListaSimple<MatrizCiudad>();
            Nodo<MatrizCiudad>? actual = ciudades.Primero;
            while (actual != null)
            {
                if (TieneCeldaTipo(actual.Dato, tipoMision)) candidatas.Insertar(actual.Dato);
                actual = actual.Siguiente;
            }

            string etiqueta = tipoMision == "rescate" ? "unidades civiles" : "recursos";

            if (candidatas.Cantidad == 0)
            {
                Console.WriteLine($"No hay ciudades con {etiqueta} disponibles para este tipo de misión.");
                return null;
            }
            if (candidatas.Cantidad == 1) return candidatas.ObtenerEn(0);

            Console.WriteLine("Seleccione una ciudad:");
            for (int i = 0; i < candidatas.Cantidad; i++)
                Console.WriteLine($"{i + 1}. {candidatas.ObtenerEn(i)?.Nombre}");

            int idx = LeerOpcion(candidatas.Cantidad);
            return idx == -1 ? null : candidatas.ObtenerEn(idx - 1);
    }

    private bool TieneCeldaTipo(MatrizCiudad ciudad, string tipoMision)
    {
        for (int f = 1; f <= ciudad.Filas; f++)
            for (int c = 1; c <= ciudad.Columnas; c++)
            {
                NodoCelda? celda = ciudad.Obtener(f, c);
                if (celda == null) continue;
                if (tipoMision == "rescate" && celda.EsCivil) return true;
                if (tipoMision == "extraccion" && celda.EsRecurso) return true;
            }
        return false;
    }


    public Robot? SeleccionarRobot(string tipoMision)
    {
        string tipoRequerido = tipoMision == "rescate" ? "ChapinRescue" : "ChapinFighter";

        ListaSimple<Robot> candidatos = new ListaSimple<Robot>();
        Nodo<Robot>? actual = robots.Primero;
        while (actual != null)
        {
            if (actual.Dato.Tipo == tipoRequerido) candidatos.Insertar(actual.Dato);
            actual = actual.Siguiente;
        }

        if (candidatos.Cantidad == 0)
        {
            Console.WriteLine($"No hay robots de tipo {tipoRequerido} disponibles.");
            return null;
        }
        if (candidatos.Cantidad == 1) return candidatos.ObtenerEn(0);

        Console.WriteLine($"Seleccione un {tipoRequerido} (varios disponibles):");
        for (int i = 0; i < candidatos.Cantidad; i++)
        {
            Robot? r = candidatos.ObtenerEn(i);
            if (r == null) continue;
            string extra = r is ChapinFighter f ? $" - capacidad {f.Capacidad}" : "";
            Console.WriteLine($"{i + 1}. {r.Nombre}{extra}");
        }

        int idx = LeerOpcion(candidatos.Cantidad);
        return idx == -1 ? null : candidatos.ObtenerEn(idx - 1);
    }

    public NodoCelda? SeleccionarEntrada(MatrizCiudad ciudad)
    {
        if (ciudad == null) return null;

        ListaSimple<NodoCelda> entradas = new ListaSimple<NodoCelda>();
        for (int f = 1; f <= ciudad.Filas; f++)
            for (int c = 1; c <= ciudad.Columnas; c++)
            {
                NodoCelda? celda = ciudad.Obtener(f, c);
                if (celda != null && celda.EsEntrada) entradas.Insertar(celda);
            }

        if (entradas.Cantidad == 0)
        {
            Console.WriteLine("Esta ciudad no tiene punto de entrada.");
            return null;
        }
        if (entradas.Cantidad == 1) return entradas.ObtenerEn(0);

        Console.WriteLine("Seleccione un punto de entrada:");
        for (int i = 0; i < entradas.Cantidad; i++)
        {
            NodoCelda? e = entradas.ObtenerEn(i);
            Console.WriteLine($"{i + 1}. Entrada en fila {e?.Fila}, columna {e?.Columna}");
        }

        int idx = LeerOpcion(entradas.Cantidad);
        return idx == -1 ? null : entradas.ObtenerEn(idx - 1);
    }

    public NodoCelda? SeleccionarObjetivo(MatrizCiudad ciudad, string tipoMision)
    {
        if (ciudad == null) return null;

        ListaSimple<NodoCelda> candidatos = new ListaSimple<NodoCelda>();
        for (int f = 1; f <= ciudad.Filas; f++)
            for (int c = 1; c <= ciudad.Columnas; c++)
            {
                NodoCelda? celda = ciudad.Obtener(f, c);
                if (celda == null) continue;
                if (tipoMision == "rescate" && celda.EsCivil) candidatos.Insertar(celda);
                if (tipoMision == "extraccion" && celda.EsRecurso) candidatos.Insertar(celda);
            }

        string etiqueta = tipoMision == "rescate" ? "civil a rescatar" : "recurso a extraer";

        if (candidatos.Cantidad == 0)
        {
            Console.WriteLine($"Esta ciudad no tiene {etiqueta}s.");
            return null;
        }
        if (candidatos.Cantidad == 1) return candidatos.ObtenerEn(0);

        Console.WriteLine($"Seleccione un {etiqueta}:");
        for (int i = 0; i < candidatos.Cantidad; i++)
        {
            NodoCelda? o = candidatos.ObtenerEn(i);
            Console.WriteLine($"{i + 1}. {o?.Fila},{o?.Columna}");
        }

        int idx = LeerOpcion(candidatos.Cantidad);
        return idx == -1 ? null : candidatos.ObtenerEn(idx - 1);
    }

    // ---------- Resultado de misión (opción 3 y 4 del menú) ----------

    public void ImprimirRuta(ListaSimple<NodoCelda> ruta, string tipoMision, NodoCelda objetivo, Robot robot)
    {
    Console.WriteLine();
        if (tipoMision == "rescate")
        {
            Console.WriteLine("Ruta de rescate:");
            Console.WriteLine("Tipo de misión: rescate");
            Console.WriteLine($"Unidad civil rescatada: {objetivo.Fila},{objetivo.Columna}");
            Console.WriteLine($"Robot utilizado: {robot.Nombre} (ChapinRescue)");
        }
        else
        {
            Console.WriteLine("Ruta de extracción de recurso:");
            Console.WriteLine("Tipo de misión: extracción de recursos");
            Console.WriteLine($"Recurso extraído: {objetivo.Fila},{objetivo.Columna}");

            ChapinFighter fighter = (ChapinFighter)robot;
            int capacidadInicial = fighter.Capacidad;
            int capacidadFinal = capacidadInicial;
            for (int i = 0; i < ruta.Cantidad; i++)
            {
                NodoCelda? c = ruta.ObtenerEn(i);
                if (c == null) continue;
                if (c.TieneMilitar) capacidadFinal -= c.CapacidadMilitar;
            }
            Console.WriteLine($"Robot utilizado: {robot.Nombre} (ChapinFighter – Capacidad de combate inicial {capacidadInicial}, Capacidad de combate final {capacidadFinal})");
        }

        Console.WriteLine();
        Console.WriteLine("Camino recorrido:");
        for (int i = 0; i < ruta.Cantidad; i++)
        {
            NodoCelda? c = ruta.ObtenerEn(i);
            Console.Write($"({c?.Fila},{c?.Columna})");
            if (i < ruta.Cantidad - 1) Console.Write(" -> ");
        }
        Console.WriteLine();

        ultimaRuta = ruta;
        ultimoTipoMision = tipoMision;
        ultimoObjetivo = objetivo;
    }

    // ---------- Reporte Graphviz (opción 5 del menú) ----------

    public void GenerarReporte()
    {
        if (ultimaRuta == null)
        {
            Console.WriteLine("No hay ruta para generar reporte. Planifique una misión primero.");
            return;
        }

        string archivo = "../reportes/ruta.dot";
        new ReporteRuta().Generar(ultimaRuta, archivo);
        Console.WriteLine($"Reporte generado en {archivo} y {archivo}.png");
    }

    // ---------- Utilidad de menú ----------

    private int LeerOpcion(int max)
    {
        Console.Write("Opción: ");
        if (int.TryParse(Console.ReadLine(), out int opcion) && opcion >= 1 && opcion <= max)
            return opcion;

        Console.WriteLine("Opción inválida.");
        return -1;
    }
}