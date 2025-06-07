# 📦 AppPedidos

Una aplicación de gestión de pedidos para locales gastronómicos. Permite manejar productos, insumos y registrar producciones con control de costos.

---

## 🚀 Clonación y ejecución

### 1. Clonar el repositorio

```bash
git clone https://github.com/tu-usuario/AppPedidos.git
cd AppPedidos
```

---

### 2. Backend (.NET 8)

📁 Ir a:

```bash
cd backend/AppPedidos.API
```

✅ Restaurar paquetes y ejecutar:

```bash
dotnet restore
dotnet run
```

---

### 3. Frontend (React + Vite + Tailwind)

📁 Ir a:

```bash
cd frontend/app-pedidos
```

📦 Instalar dependencias:

```bash
npm install
```

🚀 Ejecutar servidor de desarrollo:

```bash
npm run dev
```

---

## 🛠 Configuración de Base de Datos

1. Crear una base de datos en SQL Server llamada:

```
AppPedidos
```

2. Ejecutar el script SQL que se encuentra en:

```
/db/AppPedidos_DB.sql
```

3. Modificar la cadena de conexión en:

```
backend/AppPedidos.API/appsettings.Local.json
```

🔗 Ejemplo:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=TU_SERVIDOR;Database=AppPedidos;Trusted_Connection=True;TrustServerCertificate=True"
}
```

> ⚠️ Asegúrate de que el nombre del servidor coincida con tu configuración local.

---

## 🤝 Colaboradores

Para colaborar:

* Crea una rama: `git checkout -b nueva-funcionalidad`
* Haz tus cambios y commitea: `git commit -m "Agrega nueva funcionalidad"`
* Sube la rama: `git push origin nueva-funcionalidad`
* Abre un Pull Request en GitHub

---

## 📂 Estructura del proyecto

```
AppPedidos/
├── backend/
│   └── AppPedidos.API/
├── frontend/
│   └── app-pedidos/
├── db/
│   └── dump.sql
└── README.md
```

---

## 📄 Licencia

Este proyecto está bajo la licencia MIT. Puedes modificarlo y usarlo libremente.

---

¡Listo! Ya puedes empezar a colaborar o probar la aplicación 🚀
