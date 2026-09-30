# 🎮 Taller 1 — Fundamentos de Unity 2D

<p align="center">
  <img src="https://img.shields.io/badge/Unity-2D-black?style=for-the-badge&logo=unity" alt="Unity">
  <img src="https://img.shields.io/badge/C%23-Development-blue?style=for-the-badge&logo=csharp" alt="C#">
  <img src="https://img.shields.io/badge/Physics-Rigidbody%202D-orange?style=for-the-badge" alt="Physics">
  <img src="https://img.shields.io/badge/Status-Completed-success?style=for-the-badge" alt="Status">
</p>

<p align="center">
  <strong>Configuración del Entorno · Personaje · Plataformas · Físicas y Colisiones</strong>
</p>

---

## 📌 Descripción

Este proyecto corresponde al **Taller 1 del módulo de Desarrollo de Videojuegos**, desarrollado utilizando **Unity 2D**.

El objetivo principal fue crear una escena 2D básica en la que un personaje pueda interactuar físicamente con una plataforma, aplicando conceptos fundamentales de **sprites, Rigidbody 2D, colliders, gravedad y detección de colisiones**.

El proyecto representa una primera implementación de una escena funcional de videojuego 2D.

---

## 🎯 Objetivos

* Configurar correctamente un proyecto 2D en Unity.
* Organizar los recursos gráficos del proyecto.
* Importar y utilizar sprites 2D.
* Crear y configurar un personaje.
* Crear una plataforma.
* Implementar físicas mediante `Rigidbody 2D`.
* Configurar colisiones mediante `Box Collider 2D`.
* Verificar el comportamiento físico de los objetos en modo Play.

---

## 🛠️ Tecnologías y herramientas

| Tecnología             | Uso                                         |
| ---------------------- | ------------------------------------------- |
| 🎮 **Unity**           | Motor utilizado para desarrollar la escena  |
| 🧩 **Unity 2D**        | Desarrollo de la escena y objetos 2D        |
| 💻 **C#**              | Lenguaje utilizado por Unity                |
| ⚙️ **Rigidbody 2D**    | Sistema de físicas del personaje            |
| 🔲 **Box Collider 2D** | Detección de colisiones                     |
| 🖼️ **Sprite Editor**  | Configuración y separación de sprites       |
| 🌿 **Git**             | Control de versiones                        |
| 🐙 **GitHub**          | Almacenamiento y documentación del proyecto |

---

# 🗂️ Estructura del proyecto

La organización principal de los recursos utilizados en la actividad es:

```text
Taller1_HancelEspin/
│
├── Assets/
│   └── Sprites/
│       ├── Player
│       └── Platform
│
├── Packages/
│
├── ProjectSettings/
│
├── screenshots/
│   ├── 01-proyecto-unity-hub.png
│   ├── 02-assets-importados.png
│   ├── 03-escena-configurada.png
│   └── 04-modo-play.png
│
└── README.md
```

La carpeta `Sprites` contiene los recursos gráficos utilizados para construir la escena.

---

# 🎮 Desarrollo del proyecto

## 01 — Creación y configuración del proyecto

Se creó un nuevo proyecto utilizando la plantilla **2D Core** de Unity Hub.

El proyecto fue denominado:

```text
Taller1_HancelEspin
```

Posteriormente se organizó el contenido del proyecto creando la carpeta:

```text
Assets/Sprites
```

Esta organización permite mantener separados los recursos gráficos utilizados durante el desarrollo.

---

## 02 — Importación de Assets

Se importaron recursos gráficos 2D correspondientes al personaje y a la plataforma.

Los recursos fueron almacenados dentro de:

```text
Assets/Sprites
```

Cuando fue necesario, los sprites fueron configurados mediante:

* **Sprite Mode: Multiple**
* **Sprite Editor**
* **Slice**

Esto permite preparar correctamente los recursos gráficos para utilizarlos dentro de la escena.

---

## 03 — Configuración de la escena

La escena fue construida utilizando dos elementos principales:

```text
Player
Platform
```

### 👤 Player

El personaje fue agregado a la escena y configurado para permanecer sobre la plataforma mediante el sistema de físicas de Unity.

### 🧱 Platform

La plataforma representa el suelo sobre el cual debe aterrizar el personaje.

La posición y escala de ambos objetos fueron ajustadas para conseguir una escena clara y funcional.

---

# ⚙️ Sistema de físicas

Uno de los objetivos principales del taller fue comprobar el funcionamiento de las físicas básicas de Unity.

### 👤 Player

Se configuraron los siguientes componentes:

```text
Rigidbody 2D
Box Collider 2D
```

El `Rigidbody 2D` permite que Unity aplique gravedad y comportamiento físico al personaje.

Además, se configuró:

```text
Freeze Rotation Z
```

para evitar que el personaje rote durante la simulación.

El `Box Collider 2D` permite detectar el contacto físico con la plataforma.

### 🧱 Platform

La plataforma utiliza:

```text
Box Collider 2D
```

Este componente permite que el personaje pueda colisionar correctamente con la superficie.

---

# 🔄 Flujo de funcionamiento

El comportamiento implementado puede representarse de la siguiente manera:

```text
┌───────────────┐
│     Player    │
│ Rigidbody 2D  │
│ Box Collider  │
└───────┬───────┘
        │
        │ Gravedad
        ▼
   ┌───────────┐
   │ Platform  │
   │   Collider│
   └───────────┘
        │
        ▼
   Colisión detectada
        │
        ▼
 Player permanece
 sobre la plataforma
```

---

# 🧪 Verificación

Para comprobar el funcionamiento del proyecto se ejecutó la escena utilizando el botón:

**▶ Play**

Durante la prueba se verificó que:

* ✅ El personaje cae debido a la gravedad.
* ✅ El `Rigidbody 2D` funciona correctamente.
* ✅ El `Box Collider 2D` detecta la plataforma.
* ✅ El personaje no atraviesa el suelo.
* ✅ El personaje permanece firme sobre la plataforma.
* ✅ La rotación del personaje se encuentra bloqueada en el eje Z.


---

# 📊 Componentes utilizados

|    Objeto   | Componente        | Función                        |
| :---------: | :---------------- | :----------------------------- |
|  👤 Player  | `Sprite Renderer` | Visualización del personaje    |
|  👤 Player  | `Rigidbody 2D`    | Física y gravedad              |
|  👤 Player  | `Box Collider 2D` | Detección de colisiones        |
| 🧱 Platform | `Sprite Renderer` | Visualización de la plataforma |
| 🧱 Platform | `Box Collider 2D` | Superficie de colisión         |

---

# ✅ Resultado final

El resultado del taller es una **escena 2D funcional en Unity**, donde el personaje utiliza el sistema de físicas para caer y aterrizar correctamente sobre una plataforma.

Este ejercicio permitió aplicar conceptos fundamentales de desarrollo de videojuegos 2D relacionados con:

**Sprites → Objetos → Física → Colisiones → Verificación**

---

# 📚 Conceptos aprendidos

Durante el desarrollo del taller se trabajaron los siguientes conceptos:

* Unity Hub
* Unity 2D
* Organización de Assets
* Sprites
* Sprite Editor
* Sprite Mode
* Rigidbody 2D
* Box Collider 2D
* Gravedad
* Colisiones
* Hierarchy
* Inspector
* Scene
* Play Mode

---

# 👨‍💻 Autor

<p align="center">

<strong>Hancel Espin</strong>

<br>

Desarrollo de Videojuegos · Unity 2D

<br><br>

📅 2026

</p>

---

## 📄 Entrega académica

La evidencia completa de la actividad se encuentra documentada mediante las capturas incluidas en este repositorio.

El proyecto fue desarrollado como parte del módulo de **Desarrollo de Videojuegos**.

---

<p align="center">
  <strong>🎮 Taller 1 — Fundamentos de Unity 2D</strong>
  <br>
  <sub>Proyecto académico · 2026</sub>
</p>
