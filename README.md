# WWWineProject

WWWineProject es un proyecto de API en .NET que ofrece informaci�n sobre variedades de vinos, incluyendo su origen, color y regiones de cultivo.

## Caracter�sticas

- Consulta informaci�n sobre variedades de vinos, colores y regiones.
- Agrega nuevas variedades de vinos junto con sus detalles, como color y origen.
- Actualiza la informaci�n de las variedades de vinos existentes.
- Elimina variedades de vinos de la base de datos.

## Endpoints

### 1. Obtener listado de colores de uva y las variedades de cada uno

-   **Endpoint:** `/api/Color/`
-   **M�todo:** GET

### 2. Obtener listado de pa�ses

-   **Endpoint:** `/api/Country/`
-   **M�todo:** GET

### 3. Obtener listado de variedades seg�n pa�s

-   **Endpoint:** `/api/Country/get-varieties/{countryName}`
-   **M�todo:** GET

### 4. Obtener listado de regiones

-   **Endpoint:** `/api/Region/
-   **M�todo:** GET

### 5. Obtener listado de variedades seg�n regi�n

-   **Endpoint:** `/api/Region/get-varieties/{regionName}`
-   **M�todo:** GET

### 6. Obtener listado de variedades

-   **Endpoint:** `/api/Variety/get-all`
-   **M�todo:** GET

### 7. Obtener variedad seg�n id

-   **Endpoint:** `/api/Variety/get-id/{id}`
-   **M�todo:** GET

### 8. Obtener variedad seg�n nombre

-   **Endpoint:** `/api/Variety/get-name/{varietyName}`
-   **M�todo:** GET

### 9. Obtener regiones seg�n nombre de una variedad

-   **Endpoint:** `/api/Variety/get-regions/{varietyName}`
-   **M�todo:** GET

### 10. Agregar variedad

-   **Endpoint:** `/api/Variety/add`
-   **M�todo:** POST

### 11. Actualizar variedad

-   **Endpoint:** `/api/Variety/update`
-   **M�todo:** PUT

### 12. Eliminar variedad seg�n nombre

-   **Endpoint:** `/api/Variety/delete/{varietyName}`
-   **M�todo:** DELETE

## Notas

-   Aseg�rate de proporcionar los datos requeridos en el cuerpo de las solicitudes POST y PUT.
-   Al a�adir o editar una variedad a la base de datos, los campos correspondientes a regi�n y origen se agregar�n a la tabla correspondiente en caso de ser nuevos.

## Configuraci�n del Proyecto

Para ejecutar este proyecto en tu entorno local, sigue estos pasos:

1. Clona el repositorio:
2. Configura y realiz� las migraciones en tu base de datos