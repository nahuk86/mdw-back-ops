# MDW-Back-Ops

## Descripción
MDW-Back-Ops es una API desarrollada en C# utilizando ASP.NET Core. Esta API proporciona varias funcionalidades relacionadas con la gestión de usuarios y autenticación.

## Servicios Expone
- **Registro de usuarios**: Permite registrar nuevos usuarios en la base de datos.
- **Inicio de sesión**: Autentica a los usuarios y genera tokens JWT.
- **Validación de token**: Verifica la validez de los tokens JWT proporcionados.
- **Acceso a datos protegidos**: Proporciona acceso a rutas protegidas que requieren autenticación.
- **Prueba de base de datos**: Realiza una consulta simple a la base de datos para verificar la conexión.

## Tecnologías Utilizadas
- **Lenguaje**: C#
- **Framework**: .NET 8.0
- **Base de datos**: SQL Server
- **Autenticación**: JWT (JSON Web Tokens)
- **ORM**: Entity Framework Core
- **Contenedores**: Docker
- **Documentación de API**: Swagger (Swashbuckle)

## Configuración
El archivo `appsettings.json` contiene configuraciones clave como cadenas de conexión a la base de datos y configuraciones de JWT.

## Dependencias
- BCrypt.Net-Next
- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.AspNetCore.Authorization
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.VisualStudio.Azure.Containers.Tools.Targets
- Swashbuckle.AspNetCore

## Ejemplo de Uso
### Registro de Usuario
```http
POST /api/account/register
{
  "name": "example",
  "email": "example@example.com",
  "password": "password123"
}
```

### Inicio de Sesión
```http
POST /api/account/login
{
  "email": "example@example.com",
  "password": "password123"
}
```

### Validación de Token
```http
GET /api/account/validate-token
Authorization: Bearer <token>
```

### Acceso a Datos Protegidos
```http
GET /api/account/protected
Authorization: Bearer <token>
```

## Despliegue
El despliegue de esta API se realiza mediante archivos ARM templates que configuran recursos en Azure, incluyendo el servicio de API Management y bases de datos SQL.

Para más detalles, puedes consultar los archivos en el repositorio:
- [appsettings.json](https://github.com/nahuk86/mdw-back-ops/blob/a1085aded83104ce3a7da22ba7a952d9579a5381/MDW-Back-ops/appsettings.json)
- [AccountController.cs](https://github.com/nahuk86/mdw-back-ops/blob/a1085aded83104ce3a7da22ba7a952d9579a5381/MDW-Back-ops/Controllers/AccountController.cs)
- [MDW-Back-ops.csproj](https://github.com/nahuk86/mdw-back-ops/blob/a1085aded83104ce3a7da22ba7a952d9579a5381/MDW-Back-ops/MDW-Back-ops.csproj)
