# Api Soluci&oacute;n de Usuarios

Este proyecto contiene la API para crear un usuario y pruebas unitarias para el controlador `UserController` y el repositorio `UserRepository`, que utiliza xUnit para las pruebas y FakeItEasy para simular dependencias.

## Requisitos Previos

Aseg&uacute;rate de tener instalados los siguientes elementos en tu m&aacute;quina antes de ejecutar el proyecto:

- [.NET SDK] version 8.
- [Visual Studio Code] o [Visual Studio].
- [xUnit] para las pruebas.
- [FakeItEasy] para crear mocks en las pruebas.
- [Microsoft.EntityFrameworkCore.InMemory] permite guardar y recuperar datos en memoria.

## Clonar el Repositorio

- Clona el repositorio a tu m&aacute;quina local usando el siguiente comando:

   ```bash
   git clone https://github.com/Castilloo/Solution-Toker.es.git
   ```

- Navega al directorio del proyecto.

## Restaurar dependencias

- Para restaurar los paquetes de forma autom&aacute;tica o manual realiza los siguientes pasos:

  - En Visual Studio dirigirse a: *Herramientas>Opciones>Administrador de paquetes NuGet>General.*

  - Si tienes instalado Dotnet CLI agrega las dependencias requeridas del proyecto con el siguiente comando:

    ```bash
    dotnet restore
    ```

> [!NOTE]
> Para más información dirigirse al link de Microsoft [aquí](https://learn.microsoft.com/es-es/nuget/consume-packages/package-restore#restore-using-visual-studio) .

## Ejecutar la api

- Para ejecutar la api mediante Visual Studio dir&iacute;jase al botón de *Run*.

- Para Dotnet CLI en VS Code ejecuta el comando en la carpeta *UsersApiSolution*:

  ```bash
  dotnet run
  ```

## Ejecutar las pruebas

- Ejecuta las pruebas unitarias en Visual Studio en  
*Prueba>Ejecutar todas las pruebas*.

- En Dotnet CLI diríjase a la carpeta de los *Tests* y ejecuta:

```bash
  dotnet test
```

# Endpoints

- **GET /users:** Obtiene todos los usuarios.
  - Response:

    ```bash
    [
      {
        "id": "05a5098c-d741-4dd7-b58a-ac131bded6af",
        "nombre": "Juan Pérez",
        "telefono": "123456789"
      },
      {
        "id": "5086d84b-743c-483a-9895-72f20af9edba",
        "nombre": "Ana Gómez",
        "telefono": "987654321"
      }
    ]
    ```

- **POST /api/usuarios:** Crea un nuevo usuario ejecutando los logs respectivos.
  - Request:
    ```bash
    {
      "nombre": "Daniel Cogollo",
      "telefono": "91651516"
    }
    ```
  - Response:
    ```bash
    {
      "mensaje": "Los datos fueron recibidos correctamente"
    }
    ```
