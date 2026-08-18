# Chapin Warriors, S.A. — Sistema de Control de Robots

Proyecto 1 del curso **Introducción a la Programación y Computación 2 (IPC2)**, Universidad de San Carlos de Guatemala, Facultad de Ingeniería.

## Descripción

El sistema simula el control de robots de rescate en ciudades afectadas, representadas como una matriz de celdas enlazadas dinámicamente (sin arreglos ni colecciones nativas de C#). A partir de un archivo de configuración XML, el sistema carga ciudades y robots, y permite planificar dos tipos de misión: **rescate de civiles** (con robots `ChapinRescue`) y **extracción de recursos** (con robots `ChapinFighter`, capaces de combatir unidades militares). La ruta encontrada mediante backtracking se puede exportar como grafo con Graphviz.

## Funcionalidades

- **Cargar archivo de configuración (XML)**: carga o actualiza ciudades y robots. Si un nombre ya existe, sus datos se actualizan en vez de duplicarse.
- **Ver ciudades y robots cargados**: lista lo que hay actualmente en el sistema.
- **Planificar misión de rescate**: selecciona ciudad (solo las que tienen civiles), robot `ChapinRescue`, entrada y civil objetivo; busca una ruta mediante backtracking.
- **Planificar misión de extracción de recurso**: igual que rescate, pero con robots `ChapinFighter` (que pueden combatir unidades militares si su capacidad lo permite) y ciudades con recursos.
- **Generar reporte Graphviz de la última ruta**: exporta la última ruta encontrada como archivo `.dot` y `.png`.

## Estructura del proyecto
IPC2_Proy01_202602_202500708/

├─ src/

│ ├─ Program.cs # Punto de entrada y menú principal

│ ├─ ChapinWarriors.csproj

│ ├─ TDA/ # Tipos de dato abstracto propios

│ │ ├─ Nodo.cs

│ │ ├─ ListaSimple.cs

│ │ ├─ Pila.cs

│ │ └─ Cola.cs

│ ├─ Modelo/

│ │ ├─ NodoCelda.cs

│ │ ├─ MatrizCiudad.cs

│ │ ├─ Robot.cs # Clase abstracta base

│ │ ├─ ChapinRescue.cs

│ │ └─ ChapinFighter.cs

│ ├─ Logica/

│ │ ├─ BuscadorRuta.cs # Backtracking

│ │ └─ SistemaControl.cs # Orquesta ciudades, robots y misiones

│ └─ IO/

│ ├─ LectorConfiguracion.cs # Carga del XML

│ └─ ReporteRuta.cs # Generación de reporte Graphviz

├─ entradas/ # Archivos XML de prueba

├─ reportes/ # Salida de reportes .dot/.png

└─ docs/ # Documentación (ensayo, diagramas)


## Principios de POO aplicados

- **Abstracción**: `Robot` es una clase abstracta que define el comportamiento común a todo robot (`PuedeTransitar`, `Ingresar`, `Clonar`), sin permitir instanciarla directamente.
- **Herencia**: `ChapinRescue` y `ChapinFighter` heredan de `Robot` y reutilizan su lógica base mediante `base(...)`.
- **Encapsulamiento**: los atributos de `NodoCelda`, `MatrizCiudad` y `Robot` son privados y se exponen mediante propiedades de solo lectura donde corresponde.
- **Polimorfismo**: `PuedeTransitar`, `Ingresar` y `Clonar` son `abstract` en `Robot`, y cada tipo de robot los sobrescribe (`override`) con su propio comportamiento (por ejemplo, `ChapinRescue` evita toda celda con unidad militar, mientras `ChapinFighter` la vence si su capacidad es suficiente).

## Requisitos

- [.NET SDK](https://dotnet.microsoft.com/download) 8.0 o superior
- [Graphviz](https://graphviz.org/download/) (con `dot` disponible en el PATH) para generar los reportes en PNG

## Cómo ejecutar
Dentro de la consola/terminal:

	git clone https://github.com/ThatGuyCalledTheIronSlash/IPC2_Proy01_202602_202500708.git

	cd IPC2_Proy01_202602_202500708/src

	dotnet run

## Autor

David Antonio Meza Silva "Iron" — 202500708
IPC2, USAC — 2026

