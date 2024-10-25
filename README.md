# Sistema de Gestión Salarial

El **Sistema de Gestión Salarial** es una aplicación web desarrollada en ASP.NET Core para gestionar los datos de asociados y departamentos en una organización, así como para calcular y registrar aumentos salariales. Esta herramienta permite realizar operaciones CRUD (Crear, Leer, Actualizar y Eliminar) sobre los asociados y departamentos, y facilita el seguimiento de los cambios salariales con una interfaz intuitiva y notificaciones visuales con SweetAlert.

## Funcionalidades
- Gestión de Asociados: Registro, modificación, eliminación y consulta de datos de los asociados.
- Gestión de Departamentos: Operaciones CRUD para los departamentos.
- Cálculo de Aumentos Salariales: Aplicación de aumentos salariales a nivel de departamento o global con un porcentaje configurable.
- Histórico de Aumentos: Consulta de registros históricos de aumentos salariales realizados.

## Requisitos
Para ejecutar el sistema localmente, es necesario instalar:

- **.NET 6.0 SDK** o superior: Para construir y ejecutar la aplicación ASP.NET Core.
- **SQL Server**: Base de datos relacional donde se almacenarán los datos de asociados y departamentos. Configura la cadena de conexión en `appsettings.json`.
- **Visual Studio 2022** o **Visual Studio Code**: Entornos recomendados para el desarrollo y pruebas de la aplicación.
- **Bootstrap** y **SweetAlert**: Ya integrados en el proyecto para un diseño responsivo y alertas visuales.

## Instalación
1. Clona el repositorio en tu máquina local:
   ```bash
   git clone https://github.com/tuusuario/sistema-de-gestion-salarial.git
