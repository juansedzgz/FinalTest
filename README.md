La mayor parte de estre projecto fue realizado de manera local, por ende, explicaré brevemente la manera en la cual podemos replicar mis resultados sus propias máquinas.

1. Hacemos la creación de nuestras 3 DB en SQL Server, debemos crear 1 DB para cada microservicio Person, Appoint y Prescription.
2. Hacemos la instalación de RabbitMQ, y lo configuramos en nuestro sistema. (En mi caso usé Cloud amqp para tener una ejecución en la nube constante y hacer pruebas desde otros equipos).
3. Una vez definidas debemos insertar la tabla para cada una de las bases de datos, en mi caso fueron Table_Person, Table_Appoint y Table_Prescription.
4. Ahora procedemos a insertar algunos datos dummy en nuestra Tabla_Person, para así poder trabajar con las demás.
5. Hecho esto, procedemos a Visual Studio 2022 y creamos una Blank Solution, dentro de ella, creamos una carpeta llamada Microservices en la cual haremos la creación de nuestras APIs.
6. Dentro de nuestra carpeta, añadiremos un nuevo proyecto, ASP.NET Web Application (.NET FRAMEWORK), esta será nuestra primera API, la creamos como queramos en mi caso la nombré AppointAPI, en el Framework seleccionamos .NET Framework 4.8, le damos create para finalmente seleccionar "Web API" y deseleccionamos "Configure for HTTPS" y crear.
7. Esto lo haremos 2 veces más para importar los microservicios faltantes.
8. Luego importaremos los modelos de datos a nuestras carpetas de Models, para cada uno de nuestros microservicios, estos modelos los importaremos de nuestras DB con el formato de "Data", "ADO.NET Entity Data Model" y "Code First from database".
9. Finalmente crearemos los controladores en nuestras carpeta de Controllers.
10. Luego implementaremos la lógica presente en cada uno de los microcontroladores.
11. Haremos una carpeta llamada "Services" en la cual añadiremos la lógica que no sea correspondiente directamente de cada uno de nuestros microservicios, por ejemplo, un servicio que nos permita consultar datos desde el microservicio de citas sobre el microservicio de personas, así como la lógica de RabbitMQ.
12. En la carpeta de "Models" añadiremos también todos los DTOs que necesitemos a lo largo de nuestro desarrollo, como el modelo "Table_AppointFinished", que comparte la misma estructura que nuestra tabla de appointments, pero es necesario para transferir la información de las citas a las prescripciones.
13. Haremos uso de un importante paquete llamado Ninject, la cual nos hace la inyección de dependencias mucho más sencilla, para esto también debemos añadir un archivo llamado "NinjectBindings.cs" a nuestra carpeta App_Start e incluiremos la lógica que nos permitirá configurar dicho paquete.
14. Finalmente implementamos la lógica del archivo llamado Global.asax.cs, la cual dictará la ejecución al momento que iniciemos nuestra aplicación.
15. Con la ayuda de POSTMAN haremos las pruebas correspondientes para cada una de nuestras peticiones y documentaremos nuestros resultados.

16. Gracias.
