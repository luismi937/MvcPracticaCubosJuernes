# Proyecto MvcPracticaCubosJuernes

Aplicación ASP.NET Core MVC (.NET 10) para gestión de Cubos de Rubik con carrito de compras y favoritos.

## Características

? **Gestión de Cubos**
- Listar todos los cubos disponibles
- Ver detalles de cada cubo
- Crear nuevos cubos
- Modificar cubos existentes
- Eliminar cubos
- Subir imágenes de cubos

? **Carrito de Compras (Session)**
- Agregar cubos al carrito
- Ver lista de productos en el carrito
- Ver precio total
- Eliminar productos del carrito
- Finalizar compra (guarda en base de datos)

? **Favoritos (Cache)**
- Marcar cubos como favoritos
- Ver lista de favoritos
- Eliminar de favoritos

## Tecnologías Utilizadas

- **Framework**: ASP.NET Core MVC (.NET 10)
- **ORM**: Entity Framework Core 9.0
- **Base de Datos**: MySQL (Pomelo.EntityFrameworkCore.MySql)
- **Frontend**: Bootstrap 5
- **Session**: ASP.NET Core Session (para carrito)
- **Cache**: IMemoryCache (para favoritos)
- **Serialización**: Newtonsoft.Json

## Configuración de Base de Datos

### Conexión MySQL
- **Servidor**: localhost
- **Base de datos**: PracticaCubos
- **Usuario**: root
- **Contraseña**: mysql
- **Puerto**: 3306

### Estructura de Tablas

**CUBOS**
```sql
CREATE TABLE CUBOS (
    id_cubo INT NOT NULL PRIMARY KEY,
    nombre VARCHAR(500) NOT NULL,
    modelo VARCHAR(500) NOT NULL,
    marca VARCHAR(500) NOT NULL,
    imagen VARCHAR(500) NOT NULL,
    precio INT NOT NULL
);
```

**COMPRA**
```sql
CREATE TABLE COMPRA (
    id_compra INT NOT NULL,
    id_cubo INT NOT NULL,
    cantidad INT NOT NULL,
    precio INT NOT NULL,
    fechapedido DATETIME NOT NULL
);
```

## Instalación y Ejecución

1. **Restaurar paquetes NuGet**
   ```bash
   dotnet restore
   ```

2. **Verificar Base de Datos**
   - Asegúrate de que MySQL está ejecutándose
   - La base de datos PracticaCubos debe existir con las tablas CUBOS y COMPRA
   - Verifica la cadena de conexión en `appsettings.json`

3. **Crear carpeta para imágenes**
   - La carpeta `wwwroot/images/cubos/` ya está creada
   - Coloca las imágenes de los cubos existentes allí

4. **Ejecutar la aplicación**
   ```bash
   dotnet run
   ```

5. **Acceder a la aplicación**
   - Navega a: https://localhost:5001
   - O: http://localhost:5000

## Estructura del Proyecto

```
MvcPracticaCubosJuernes/
??? Controllers/
?   ??? CubosController.cs       # Gestión de cubos y favoritos
?   ??? CarritoController.cs     # Gestión del carrito
??? Data/
?   ??? CubosContext.cs          # DbContext de Entity Framework
??? Models/
?   ??? Cubo.cs                  # Modelo de Cubo
?   ??? Compra.cs                # Modelo de Compra
?   ??? CarritoItem.cs           # Modelo para items del carrito
??? Repositories/
?   ??? RepositoryCubos.cs       # Repositorio con Entity Framework Core
??? Extensions/
?   ??? SessionExtension.cs      # Extensiones para Session
??? Views/
?   ??? Cubos/                   # Vistas de cubos
?   ??? Carrito/                 # Vistas del carrito
??? wwwroot/
    ??? images/cubos/            # Imágenes de productos
```

## Funcionalidades Principales

### Gestión de Cubos
- **Index**: Muestra todos los cubos con opciones para ver detalles, editar, agregar al carrito y marcar como favorito
- **Details**: Muestra información detallada de un cubo específico
- **Create**: Formulario para crear un nuevo cubo con subida de imagen
- **Edit**: Formulario para modificar un cubo existente
- **Delete**: Eliminar un cubo de la base de datos

### Carrito (Session)
- Los productos se almacenan en la sesión del usuario usando `SessionExtension`
- Se mantiene la cantidad de cada producto
- Cálculo automático del subtotal y total
- Al finalizar la compra, se insertan los registros en la tabla COMPRA

### Favoritos (Cache)
- Los IDs de cubos favoritos se almacenan en memoria cache
- Persisten durante la sesión de la aplicación
- Se pueden agregar y eliminar favoritos

## Notas Importantes

- **Entity Framework Core**: El proyecto usa EF Core con el proveedor MySQL de Pomelo
- **Generación de IDs**: Los IDs se generan automáticamente usando MAX(id) + 1
- **Imágenes**: Si una imagen no existe, se mostrará una imagen por defecto (configura `onerror` en las vistas)
- **Precios**: Los precios están en euros (€) como valores enteros

## Patrón de Diseño

Este proyecto sigue el patrón del proyecto de referencia MvcCoreSessionEmpleados:
- **DbContext** para configuración de Entity Framework
- **Repository Pattern** para acceso a datos
- **Session Extensions** para manejo de objetos en sesión
- **Dependency Injection** para todos los servicios

## Autor

Proyecto desarrollado para práctica de ASP.NET Core MVC con MySQL y Entity Framework Core
