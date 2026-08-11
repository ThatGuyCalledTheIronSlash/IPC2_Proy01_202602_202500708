# IPC2_Proy01_202602_202500708

Sistema de control de misiones para Chapín Warriors, S.A. — Proyecto 1 del curso
Introducción a la Programación y Computación 2 (IPC2), USAC.


## Descripción

El sistema recibe archivos de configuración XML con la malla de celdas de ciudades
en conflicto y una lista de robots (`ChapinRescue` y `ChapinFighter`). A partir de
esa información, permite ejecutar:

- **Misiones de rescate**: un robot `ChapinRescue` debe llegar desde un punto de
  entrada hasta una unidad civil, evitando unidades militares.
- **Misiones de extracción de recursos**: un robot `ChapinFighter` debe llegar
  desde un punto de entrada hasta un recurso, pudiendo enfrentar unidades
  militares si su capacidad de combate es suficiente.

El resultado de cada misión se representa gráficamente utilizando **Graphviz**.

## Tecnologías

- Lenguaje: C# (.NET)
- Estructuras de datos: TDA propios (prohibido usar `List`, `Queue`, `Stack`, etc.)
- Paradigma: Programación Orientada a Objetos
- Entrada: archivos XML
- Salida gráfica: Graphviz

## Estructura del repositorio

IPC2_Proy01_202602_#Carnet/
├─ src/
│  ├─ Program.cs              menú en bucle
│  ├─ TDA/
│  │  ├─ Nodo.cs
│  │  ├─ ListaSimple.cs
│  │  ├─ Pila.cs
│  │  └─ Cola.cs
│  ├─ Modelo/
│  │  ├─ NodoCelda.cs
│  │  ├─ MatrizCiudad.cs
│  │  ├─ Robot.cs             abstracta
│  │  ├─ ChapinRescue.cs
│  │  └─ ChapinFighter.cs
│  ├─ Logica/
│  │  ├─ BuscadorRuta.cs      backtracking
│  │  └─ SistemaControl.cs
│  └─ IO/
│     ├─ LectorConfiguracion.cs
│     └─ ReporteRuta.cs       genera .dot
├─ entradas/
│  ├─ config_ciudadAlfa.xml
│  └─ config_ciudadBeta.xml
├─ reportes/
│  ├─ ruta.dot
│  └─ ruta.png
├─ docs/
│  └─ ensayo.pdf              4–7 páginas
├─ .gitignore
└─ README.md           
```

## Cómo ejecutar
