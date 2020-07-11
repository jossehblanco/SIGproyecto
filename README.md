# SIGproyecto
Aplicacion movil para deteccion de posibles focos de infeccion de casos de COVID-19.

Desarrollada en Xamarin Forms se comunica con una REST-Api desarrollada en Flask donde se maneja la base de datos de POSTGIS.
No cuenta con base de datos local se maneja todo en linea, tiene un control basico de usuarios y todo se maneja en base a llamadas asincronas.

# Seguridad
Utiliza autorizacion de token con el Api. 

# Uso
- Si deseas utilizar esta aplicacion para cualquier desarrollo es necesario que agregues en el AndroidManifest.xml tu propio API-Key.

- Puedes conectar el app con un Api desarrollada en otro entorno pero es importante retornar los mismos modelos.

- Repositorio donde esta alojado el REST-Api utilizado: https://github.com/jossehblanco/SIGproyectoAPI
