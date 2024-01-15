# WWWineProject

WWWineProject es un proyecto de API en .NET que ofrece información sobre variedades de vinos, incluyendo su origen, color y regiones de cultivo.

## Características

- Consulta información sobre variedades de vinos, colores y regiones.
- Agrega nuevas variedades de vinos junto con sus detalles, como color y origen.
- Actualiza la información de las variedades de vinos existentes.
- Elimina variedades de vinos de la base de datos.

## Endpoints

### 1. Obtener listado de colores de uva y las variedades de cada uno

-   **Endpoint:** `/api/Color/`
-   **Método:** GET

### 2. Obtener listado de países

-   **Endpoint:** `/api/Country/`
-   **Método:** GET

### 3. Obtener listado de variedades según país

-   **Endpoint:** `/api/Country/get-varieties/{countryName}`
-   **Método:** GET

### 4. Obtener listado de regiones

-   **Endpoint:** `/api/Region/
-   **Método:** GET

### 5. Obtener listado de variedades según región

-   **Endpoint:** `/api/Region/get-varieties/{regionName}`
-   **Método:** GET

### 6. Obtener listado de variedades

-   **Endpoint:** `/api/Variety/get-all`
-   **Método:** GET

### 7. Obtener variedad según id

-   **Endpoint:** `/api/Variety/get-id/{id}`
-   **Método:** GET

### 8. Obtener variedad según nombre

-   **Endpoint:** `/api/Variety/get-name/{varietyName}`
-   **Método:** GET

### 9. Obtener regiones según nombre de una variedad

-   **Endpoint:** `/api/Variety/get-regions/{varietyName}`
-   **Método:** GET

### 10. Agregar variedad

-   **Endpoint:** `/api/Variety/add`
-   **Método:** POST

### 11. Actualizar variedad

-   **Endpoint:** `/api/Variety/update`
-   **Método:** PUT

### 12. Eliminar variedad según nombre

-   **Endpoint:** `/api/Variety/delete/{varietyName}`
-   **Método:** DELETE

## Notas

-   Asegúrate de proporcionar los datos requeridos en el cuerpo de las solicitudes POST y PUT.
-   Al añadir o editar una variedad a la base de datos, los campos correspondientes a región y origen se agregarán a la tabla correspondiente en caso de ser nuevos.

## Configuración del Proyecto

Para ejecutar este proyecto en tu entorno local, sigue estos pasos:

1. Clona el repositorio:
2. Configura y realizá las migraciones en tu base de datos