# IPC2_Proy01_202602_#Carnet

Sistema de control de misiones para Chapín Warriors, S.A. — Proyecto 1 del curso
Introducción a la Programación y Computación 2 (IPC2), USAC.

> ⚠️ Reemplaza `#Carnet` en el nombre de este repositorio por tu número de carnet
> antes de crearlo en GitHub, tal como lo exige el enunciado.

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

```
├── src/ChapinWarriors/   # Código fuente del proyecto
│   ├── TDA/               # Estructuras de datos propias
│   ├── Modelo/             # Clases del dominio (Ciudad, Celda, Robot, etc.)
│   ├── XML/               # Lectura y parseo de archivos de configuración
│   ├── Mision/             # Lógica de misiones (rutas, combate)
│   └── Graficador/         # Generación de salida gráfica con Graphviz
├── docs/                 # Documentación / ensayo del curso
├── entradas/             # Archivos XML de ejemplo para pruebas
└── README.md
```

## Cómo ejecutar

```bash
cd src/ChapinWarriors
dotnet run
```

## Releases

Este proyecto requiere un mínimo de 4 releases (uno por semana):

| Release | Fecha objetivo | Contenido planificado |
|---------|-----------------|------------------------|
| v0.1    |                 | Estructura base, TDA iniciales |
| v0.2    |                 | Carga de XML y modelo de ciudad/robots |
| v0.3    |                 | Lógica de misiones y búsqueda de caminos |
| v1.0    |                 | Integración con Graphviz, versión final |

## Documentación

La documentación (ensayo, diagrama de clases y diagramas de actividad) se
encuentra en la carpeta [`docs/`](./docs).

## Colaboradores

- (Agregar aquí a los auxiliares del curso como colaboradores del repositorio)
