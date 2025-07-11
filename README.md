# 🛒 TuPedido

**TuPedido** es una aplicación integral que fusiona la experiencia de usuario de apps tipo *PedidosYa* o *Rappi* con herramientas de gestión para comercios locales. Desde la carga de productos, control de stock y producción, hasta la administración de repartidores, horarios, cupones y cierre de caja. Todo en una plataforma escalable, moderna y lista para producción.

---

## 🚀 Tecnologías Utilizadas

### 📊 Base de Datos
- ![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=flat&logo=microsoft-sql-server&logoColor=white)  
  **SQL Server**: Base de datos relacional con estructuras normalizadas y relaciones bien definidas.

### 🖥️ Backend
- ![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-512BD4?style=flat&logo=dotnet&logoColor=white)  
  **Framework**: ASP.NET Core 7 en C#, siguiendo buenas prácticas de arquitectura limpia.
- **Estructura del Proyecto**:
  - **Controladores**: Endpoints RESTful organizados por entidad.
  - **DTOs**: Separación de lógica de negocio y presentación.
  - **Servicios**: Lógica central desacoplada del acceso a datos.
  - **EF Core + FluentAPI**: Mapeo de entidades y migraciones automáticas.
- **Seguridad y Autenticación**:
  - ![JWT](https://img.shields.io/badge/JWT-000000?style=flat&logo=json-web-tokens&logoColor=white)  
    Autenticación con JWT y control de acceso por roles (`admin`, `vendedor`, `repartidor`).
  - ![BCrypt](https://img.shields.io/badge/BCrypt-228B22?style=flat&logo=lock&logoColor=white)  
    Hash de contraseñas seguro.

### 🌐 Frontend
- ![React](https://img.shields.io/badge/React-61DAFB?style=flat&logo=react&logoColor=black)  
  **Framework**: React + Vite + TypeScript.
- **UI**:  
  - TailwindCSS.
  - Shadcn UI.
  - Iconografía Lucide.
  - React Router DOM para navegación con rutas protegidas.
  - Axios para conexión a API REST.
  - React Joyride para instructivos interactivos.

---

## 🧩 Módulos del Sistema

### 🏪 Información del Local
- Datos del negocio, horarios de apertura/cierre, logo, y ubicación GPS.
- Visibilidad automática del negocio según horario.

### 🍽 Productos / Menú
- Carga de productos con unidad (por unidad, kg, 100gr, etc.).
- Precio dinámico según producción y consumo de insumos.
- Gestión de stock automático según producción.
- Visibilidad automática de productos con stock.

### 🧾 Proveedores / Compras
- Alta, edición y eliminación de proveedores.
- Registro de compras de insumos, con fecha, proveedor, cantidad y precio unitario.

### 🏭 Producción / Insumos
- Gestión de insumos (harina, grasa, etc.).
- Registro diario de producción: insumo utilizado, cantidad producida, cálculo automático de costo unitario.
- Stock actualizado en tiempo real.

### 💰 Caja / Finanzas
- Apertura y cierre de caja (3 turnos por día).
- Registro de ventas, gastos, ingresos adicionales.
- Cálculo de utilidad diaria.
- Módulo de estadísticas de rentabilidad por producto, ventas y gastos.

### 🚴‍♂️ Repartidores
- Alta de repartidores con ubicación y credenciales.
- Asignación automática de pedidos según zona.

### 🎟️ Cupones de Descuento
- Generación de cupones por local.
- Lógica de uso: por porcentaje o monto fijo.
- Control de usos disponibles y validez por fecha.

---

## 📱 Funcionalidades para el Cliente

- Registro/login.
- Ver negocios disponibles según horario.
- Ver productos por categoría.
- Agregar al carrito.
- Aplicar cupones.
- Realizar pedido.
- Ver estado del pedido en tiempo real.
- Recibir notificaciones.

---

## 🔐 Seguridad

- Acceso con roles (Admin, Vendedor, Repartidor, Cliente).
- Rutas protegidas.
- JWT para autenticación.
- Cifrado de contraseñas con BCrypt.
- Protección de endpoints sensibles.

---

## 📖 Instalación y Configuración

**Requisitos Previos:**
- .NET 7 SDK.
- SQL Server.
- Node.js + npm.
- Vite.
- Visual Studio y/o VSCode.

### 🧪 Instrucciones

1. Clonar repositorio:
```bash
git clone https://github.com/Gonzawk/TuPedido.git
cd TuPedido

- 2.Cambia al directorio del proyecto. 
```bash
cd TuPedido
```

 
- 3.Crear base de datos en SQL Server (usar el script TuPedidoDB.sql).


- 4.Abrir la solucion del proyecto API en **Visual Studio** (`AppPedidos.API.sln`).


- 5.Modificar en este el archivo `appsettings.json` para configurar la cadena de conexión con tu base de datos MySQL. Ejemplo:
```json
{
  "ConnectionStrings": {
     "DefaultConnection": "Servidor=localhost;Base de datos=PedidosAppDB;Id de usuario=<usuario>;Contraseña=<contraseña>;" 
    }
}
```


- 6.Restaura las dependencias de la API ejecutando el siguiente comando en la consola del administrador de paquetes de Visual Studio.
```bash
dotnet restore
```


- 7.Ejecuta la API (presiona **F5** o selecciona la opcion **Iniciar sin depuración** para ejecutar el servidor.


- 8.Abra la carpeta del proyecto de la aplicacion web en **Visual Studio Code**.


- 9.Instala las dependencias de npm necesarias ejecutando:
```bash
npm install 
```


- 10.En este punto solo queda verificar que el frontend tenga configurada correctamente la URL de la API (http://localhost:5173/api/).


**Contribuir**

1.Realiza un fork del repositorio. 

2.Crea una nueva rama para tus cambios: 
```bash 
git checkout -b feature/nueva-funcionalidad
```

3.Realiza tus cambios y subelos a tu repositorio fork.

4.Envia un pull request para revision.

---
---

## 📌 Autor

**👨‍💻 [Gonzalo Daniel Paz]**  _@GonzaDev_
_Full Stack Developer |_  

💼 **Portafolio:** [Portafoliowebgonzalopaz.com](https://portafoliowebgonzalopaz.netlify.app)  
📧 **Correo Electrónico:** [Gonzalopaz@gmail.com](mailto:gdp43191989@gmail.com)  
🌐 **LinkedIn:** [linkedin.com/in/gonzalodpaz](https://linkedin.com/in/gonzalodpaz/)  
🐦 **Twitter:** [@GonzaPaz]([https://x.com/Gonza77])  
📂 **GitHub:** [github.com/Gonzawk](https://github.com/Gonzawk)  

---

> "El desarrollo es más que código" 🚀
