# CryptoClassic

> **Librería y demostrador WinForms de _cifrados clásicos_ escrito en C# / .NET Framework 4.8.**

---

## 0 · ¿De qué trata?

Este repositorio contiene **dos proyectos**:

| Proyecto | Tipo | Rol |
| -------- | ---- | --- |
| **CryptoClassic.Core** | Class Library | Implementa los algoritmos (cada uno hereda de `ICipher`). |
| **CryptoClassic.Demo** | WinForms App | UI sencilla para cifrar / descifrar y visualizar resultados. |

La idea es que cualquiera de los integrantes pueda **añadir un nuevo cifrado sin tocar la interfaz**: basta con programar la clase en `Core` y registrarla en un `List<ICipher>` que ya usa el formulario.

---

## 1. 🚀 Instalación y ejecución

### ⚙️ Requisitos

| Herramienta | Versión |
|-------------|---------|
| **Visual Studio** (Community / Pro / Enterprise) | 2022 o 2019 |
| **.NET Framework Developer Pack** | 4.8 |

### Pasos

```bash
# 1. Clona el repositorio
git clone https://github.com/<tu-usuario>/CryptoClassic.git
cd CryptoClassic

# 2. Abre la solución
#    - Doble clic en CryptoClassic.sln    (o)
#    - File ▸ Open ▸ Project/Solution…    (en Visual Studio)

# 3. Compila y ejecuta
Ctrl+Shift+B       # Build Solution
F5                 # Run – abre CryptoClassic.Demo
```

> **Nota:** *CryptoClassic.Demo* está marcado como **Startup Project**; si no lo estuviera, haz clic derecho ➜ **Set as Startup Project**.


## 📂 Estructura de carpetas relevante

```
CryptoClassic/
├─ CryptoClassic.Core/
│  ├─ Interfaces/         ← ICipher.cs (contrato común)
│  ├─ Caesar/             ← CaesarCipher.cs (ejemplo completo)
│  ├─ Transposition/
│  └─ …                   ← Añade aquí tu nuevo algoritmo
└─ CryptoClassic.Demo/
   └─ Form1.cs            ← ComboBox + botones Cifrar/Descifrar
```

#### Cómo **agregar un nuevo algoritmo**

1. Crea carpeta y clase (`MiNuevoCipher.cs`) dentro de **CryptoClassic.Core**.
2. Hereda de `ICipher` e implementa `Encrypt` y `Decrypt`.
3. En **Form1.cs** (Demo) agrega la instancia al listado:

```
csharp
_ciphers = new List<ICipher>
{
    new CaesarCipher(),
    new MiNuevoCipher()   // ← tu clase
};
```

4. Build & Run ► ¡la UI lo detecta automáticamente!

---

## 2 · Algoritmos incluidos / planificados

| Algoritmo                                | Tipo                       | Resumen rápido                                                      |
| ---------------------------------------- | -------------------------- | ------------------------------------------------------------------- |
| **César (Shift)**                        | Sustitución monoalfabética | Desplaza cada letra un número *k* de posiciones.                    |
| **Palabra Clave**                        | Sustitución monoalfabética | Alfabeto empieza con la palabra clave sin repetir letras.           |
| **Afin**                                 | Sustitución monoalfabética | `E(x)= (a·x + b) mod m`, requiere `gcd(a,m)=1`.                     |
| **Vigenère / Polialfabética**            | Sustitución polialfabética | Usa una clave que se repite para seleccionar alfabetos rotados.     |
| **OTP / Monogramica Polialfabeto**       | Sust. polialfabética       | Clave tan larga como el texto (One-Time-Pad simplificado).          |
| **Transposición – Grupos**               | Transposición              | Se agrupa y permuta el texto por bloques.                           |
| **Transposición – Series**               | Transposición              | Se escribe en serie de columnas y se lee en distinto orden.         |
| **Transposición – Filas / Columnas**     | Transposición              | Ordena las filas o columnas según una clave numérica.               |
| **Transposición – Zig-Zag (Rail Fence)** | Transposición              | Se coloca el texto en zig-zag y se lee por filas.                   |
| **Anagramación**                         | Transposición              | Baraja filas o columnas parcial/totalmente.                         |
| **Playfair**                             | Sustitución digráfica      | Matriz 5×5, cifra en pares de letras.                               |
| **Hill (2×2, 3×3)**                      | Sustitución poligráfica    | Utiliza álgebra matricial mod *m*.                                  |
| **Ataque de Kasiski**                    | Cripto-análisis            | Detecta longitud de clave en Vigenère mediante trigramas repetidos. |



> ***Estado actual***
>
> * ✅ Cifrado César implementada y funcional en la GUI.
> * ⏳ Se irán añadiendo los demás siguiendo el mismo patrón (`ICipher`).
---
