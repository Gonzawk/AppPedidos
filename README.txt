# 🛍️ TuPedido - Plataforma de Gestión Gastronómica

**TuPedido** es un sistema completo de autogestión para locales gastronómicos. Permite a comercios crear su cuenta, configurar su local, gestionar el menú, pedidos, caja, insumos, producción, finanzas, y más. Pensado para pequeñas y medianas empresas gastronómicas que buscan optimizar sus procesos sin depender de plataformas externas.

---

## 🚀 Características Principales

- ✅ Registro de locales autogestionado  
- ✅ Gestión de menú, productos, stock e insumos  
- ✅ Administración de horarios y atención  
- ✅ Control de pedidos en tiempo real  
- ✅ Módulo de compras y proveedores  
- ✅ Producción a partir de insumos  
- ✅ Caja diaria, egresos y reportes financieros  
- ✅ Asignación de repartidores  
- ✅ Cupones de descuento personalizados  
- ✅ Cliente con seguimiento de pedidos  
- ✅ Panel de administración por roles  

---

## 🛠️ Tecnologías Utilizadas

### Frontend
- React + Vite + TypeScript
- React Router DOM v6
- Tailwind CSS
- Axios con interceptor JWT
- Framer Motion
- Google OAuth (`@react-oauth/google`)

### Backend
- ASP.NET Core 8
- Entity Framework Core + SQL Server
- JWT + OAuth2 (login propio + Google)
- Claims personalizados + Soft Delete + DTOs

---

## 📂 Estructura de Carpetas (Frontend)

```plaintext
src/
├── api/                  # Axios con configuración global
├── auth/                 # Login, AuthProvider, SessionGuard
├── components/           # UI generales (modales, loaders, botones)
├── layouts/              # Layouts para local, cliente, admin
├── pages/
│   ├── cliente/          # Rutas privadas para el cliente
│   ├── local/            # Rutas privadas para el local
│   ├── public/           # Landing y registro público
│   └── Home.tsx          # RoleRedirector
├── App.tsx               # Definición de rutas
└── main.tsx              # Entrada principal
```

---

## 🧪 Instalación y Ejecución

```bash
git clone https://github.com/tuusuario/tu-pedido.git
cd tu-pedido
npm install
npm run dev
```

Accedé desde `http://localhost:5173/`

---

## 🔐 Accesos según Rol

- **Cliente**: `/inicio`  
- **Local**: `/local/dashboard`  
- **Admin**: `/admin` (próximamente)  

---

## 📡 API RESTful

- Backend con ASP.NET Core 8
- Protegido con JWT (claim: `role`)
- Token requerido vía `Authorization: Bearer <token>`
- Login expone `token` y `rol` en respuesta

---

## 📜 Licencia

MIT © [TuPedido](https://github.com/tuusuario/tu-pedido)

---

## 🙋 Contribuciones

¡Tus ideas y mejoras son bienvenidas!  
Abrí un issue o PR si querés colaborar con el desarrollo.

---

## 🌐 Demo

> Agregá aquí el enlace si está desplegado online.
