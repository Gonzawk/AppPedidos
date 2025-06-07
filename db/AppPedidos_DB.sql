USE [AppPedidos]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 06/06/2025 22:37:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 06/06/2025 22:37:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorias](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[LocalId] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Categorias] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 06/06/2025 22:37:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[Telefono] [nvarchar](max) NOT NULL,
	[Activo] [bit] NOT NULL,
	[Apellido] [nvarchar](max) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompraDetalles]    Script Date: 06/06/2025 22:37:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompraDetalles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompraId] [int] NOT NULL,
	[InsumoId] [int] NOT NULL,
	[Cantidad] [decimal](18, 2) NOT NULL,
	[PrecioUnitario] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_CompraDetalles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Compras]    Script Date: 06/06/2025 22:37:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Compras](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LocalId] [int] NOT NULL,
	[ProveedorId] [int] NOT NULL,
	[Fecha] [datetime2](7) NOT NULL,
	[Total] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_Compras] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Direcciones]    Script Date: 06/06/2025 22:37:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Direcciones](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClienteId] [int] NOT NULL,
	[Calle] [nvarchar](max) NOT NULL,
	[Numero] [nvarchar](max) NOT NULL,
	[Ciudad] [nvarchar](max) NOT NULL,
	[Coordenadas] [nvarchar](max) NOT NULL,
	[Principal] [bit] NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Direcciones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Horarios]    Script Date: 06/06/2025 22:37:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Horarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LocalId] [int] NOT NULL,
	[DiaSemana] [int] NOT NULL,
	[HoraApertura] [time](7) NOT NULL,
	[HoraCierre] [time](7) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Horarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Insumos]    Script Date: 06/06/2025 22:37:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Insumos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LocalId] [int] NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[Unidad] [nvarchar](max) NOT NULL,
	[PrecioUnitario] [decimal](10, 2) NOT NULL,
	[Stock] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Insumos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Locales]    Script Date: 06/06/2025 22:37:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Locales](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[Slug] [nvarchar](max) NOT NULL,
	[Telefono] [nvarchar](max) NOT NULL,
	[Direccion] [nvarchar](max) NOT NULL,
	[Coordenadas] [nvarchar](max) NOT NULL,
	[LogoUrl] [nvarchar](max) NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaAlta] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Locales] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Modificadores]    Script Date: 06/06/2025 22:37:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Modificadores](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[PrecioExtra] [decimal](10, 2) NOT NULL,
	[LocalId] [int] NOT NULL,
 CONSTRAINT [PK_Modificadores] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProduccionDetalles]    Script Date: 06/06/2025 22:37:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProduccionDetalles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProduccionId] [int] NOT NULL,
	[InsumoId] [int] NOT NULL,
	[CantidadUtilizada] [decimal](18, 2) NOT NULL,
	[PrecioUnitario] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_ProduccionDetalles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producciones]    Script Date: 06/06/2025 22:37:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producciones](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductoId] [int] NOT NULL,
	[CantidadProducida] [int] NOT NULL,
	[Fecha] [datetime2](7) NOT NULL,
	[LocalId] [int] NOT NULL,
 CONSTRAINT [PK_Producciones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductoInsumos]    Script Date: 06/06/2025 22:37:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductoInsumos](
	[ProductoId] [int] NOT NULL,
	[InsumoId] [int] NOT NULL,
	[CantidadPorUnidad] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_ProductoInsumos] PRIMARY KEY CLUSTERED 
(
	[ProductoId] ASC,
	[InsumoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductoModificadores]    Script Date: 06/06/2025 22:37:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductoModificadores](
	[ProductoId] [int] NOT NULL,
	[ModificadorId] [int] NOT NULL,
 CONSTRAINT [PK_ProductoModificadores] PRIMARY KEY CLUSTERED 
(
	[ProductoId] ASC,
	[ModificadorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 06/06/2025 22:37:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[Descripcion] [nvarchar](max) NOT NULL,
	[Precio] [decimal](10, 2) NOT NULL,
	[ImagenUrl] [nvarchar](max) NOT NULL,
	[Stock] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
	[CategoriaId] [int] NOT NULL,
 CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proveedores]    Script Date: 06/06/2025 22:37:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedores](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LocalId] [int] NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[Telefono] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Direccion] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Proveedores] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 06/06/2025 22:37:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Turnos]    Script Date: 06/06/2025 22:37:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Turnos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LocalId] [int] NOT NULL,
	[DiaSemana] [int] NOT NULL,
	[NumeroTurno] [int] NOT NULL,
	[HoraApertura] [time](7) NOT NULL,
	[HoraCierre] [time](7) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Turnos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 06/06/2025 22:37:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[RolId] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaCreacion] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250527203525_InitFull', N'9.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250527210111_InitLocales', N'9.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250530063815_AgregarEntidadTurno', N'9.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250601035005_AddLocalToModificador', N'9.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250601051511_AddCamposCliente', N'9.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250601052658_AjustesModelo', N'9.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250604121347_AddComprasYProveedores', N'9.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250605083539_UpdateComprasConDetalles', N'9.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250606110717_FixCompraDetalleInsumoRelation', N'9.0.5')
GO
SET IDENTITY_INSERT [dbo].[Categorias] ON 

INSERT [dbo].[Categorias] ([Id], [Nombre], [LocalId], [Activo]) VALUES (1, N'Pizzas', 1, 1)
INSERT [dbo].[Categorias] ([Id], [Nombre], [LocalId], [Activo]) VALUES (2, N'Bebidas', 1, 1)
INSERT [dbo].[Categorias] ([Id], [Nombre], [LocalId], [Activo]) VALUES (3, N'Postres', 1, 1)
INSERT [dbo].[Categorias] ([Id], [Nombre], [LocalId], [Activo]) VALUES (4, N'Hamburguesas', 1, 1)
INSERT [dbo].[Categorias] ([Id], [Nombre], [LocalId], [Activo]) VALUES (5, N'Sandwuiches', 1, 1)
SET IDENTITY_INSERT [dbo].[Categorias] OFF
GO
SET IDENTITY_INSERT [dbo].[Clientes] ON 

INSERT [dbo].[Clientes] ([Id], [UsuarioId], [Telefono], [Activo], [Apellido], [Nombre]) VALUES (1, 4, N'3415551234', 1, N'Pérez', N'Juan')
INSERT [dbo].[Clientes] ([Id], [UsuarioId], [Telefono], [Activo], [Apellido], [Nombre]) VALUES (2, 5, N'3415555678', 1, N'Gómez', N'Laura')
SET IDENTITY_INSERT [dbo].[Clientes] OFF
GO
SET IDENTITY_INSERT [dbo].[CompraDetalles] ON 

INSERT [dbo].[CompraDetalles] ([Id], [CompraId], [InsumoId], [Cantidad], [PrecioUnitario]) VALUES (1, 3, 2, CAST(10.00 AS Decimal(18, 2)), CAST(3000.00 AS Decimal(10, 2)))
INSERT [dbo].[CompraDetalles] ([Id], [CompraId], [InsumoId], [Cantidad], [PrecioUnitario]) VALUES (2, 4, 3, CAST(2.00 AS Decimal(18, 2)), CAST(1000.00 AS Decimal(10, 2)))
INSERT [dbo].[CompraDetalles] ([Id], [CompraId], [InsumoId], [Cantidad], [PrecioUnitario]) VALUES (3, 5, 3, CAST(1.00 AS Decimal(18, 2)), CAST(1000.00 AS Decimal(10, 2)))
INSERT [dbo].[CompraDetalles] ([Id], [CompraId], [InsumoId], [Cantidad], [PrecioUnitario]) VALUES (4, 5, 4, CAST(2.00 AS Decimal(18, 2)), CAST(600.00 AS Decimal(10, 2)))
INSERT [dbo].[CompraDetalles] ([Id], [CompraId], [InsumoId], [Cantidad], [PrecioUnitario]) VALUES (5, 6, 1, CAST(10.00 AS Decimal(18, 2)), CAST(4500.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[CompraDetalles] OFF
GO
SET IDENTITY_INSERT [dbo].[Compras] ON 

INSERT [dbo].[Compras] ([Id], [LocalId], [ProveedorId], [Fecha], [Total]) VALUES (3, 1, 2, CAST(N'2025-06-06T11:13:26.0311651' AS DateTime2), CAST(30000.00 AS Decimal(10, 2)))
INSERT [dbo].[Compras] ([Id], [LocalId], [ProveedorId], [Fecha], [Total]) VALUES (4, 1, 3, CAST(N'2025-06-06T11:15:50.8325540' AS DateTime2), CAST(2000.00 AS Decimal(10, 2)))
INSERT [dbo].[Compras] ([Id], [LocalId], [ProveedorId], [Fecha], [Total]) VALUES (5, 1, 3, CAST(N'2025-06-06T11:16:30.0515396' AS DateTime2), CAST(2200.00 AS Decimal(10, 2)))
INSERT [dbo].[Compras] ([Id], [LocalId], [ProveedorId], [Fecha], [Total]) VALUES (6, 1, 1, CAST(N'2025-06-06T11:17:06.4832926' AS DateTime2), CAST(45000.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Compras] OFF
GO
SET IDENTITY_INSERT [dbo].[Horarios] ON 

INSERT [dbo].[Horarios] ([Id], [LocalId], [DiaSemana], [HoraApertura], [HoraCierre], [Activo]) VALUES (1, 1, 1, CAST(N'08:00:00' AS Time), CAST(N'13:00:00' AS Time), 1)
SET IDENTITY_INSERT [dbo].[Horarios] OFF
GO
SET IDENTITY_INSERT [dbo].[Insumos] ON 

INSERT [dbo].[Insumos] ([Id], [LocalId], [Nombre], [Unidad], [PrecioUnitario], [Stock]) VALUES (1, 1, N'Carne para lomo', N'Kg', CAST(4500.00 AS Decimal(10, 2)), CAST(8.50 AS Decimal(18, 2)))
INSERT [dbo].[Insumos] ([Id], [LocalId], [Nombre], [Unidad], [PrecioUnitario], [Stock]) VALUES (2, 1, N'Pan de lomo', N'Un', CAST(3000.00 AS Decimal(10, 2)), CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[Insumos] ([Id], [LocalId], [Nombre], [Unidad], [PrecioUnitario], [Stock]) VALUES (3, 1, N'Tomates', N'Kg', CAST(1000.00 AS Decimal(10, 2)), CAST(1.50 AS Decimal(18, 2)))
INSERT [dbo].[Insumos] ([Id], [LocalId], [Nombre], [Unidad], [PrecioUnitario], [Stock]) VALUES (4, 1, N'Lechuga', N'Kg', CAST(600.00 AS Decimal(10, 2)), CAST(0.50 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Insumos] OFF
GO
SET IDENTITY_INSERT [dbo].[Locales] ON 

INSERT [dbo].[Locales] ([Id], [UsuarioId], [Nombre], [Slug], [Telefono], [Direccion], [Coordenadas], [LogoUrl], [Activo], [FechaAlta]) VALUES (1, 2, N'Rostiseria Uno', N'rostiseria-uno', N'3511234567', N'Av. Siempre Viva 742', N'-31.4135,-64.1810', N'https://i.ibb.co/B2XKqs1S/Logo-Perfumeriafondoazul.png', 1, CAST(N'2025-05-27T21:02:31.2979180' AS DateTime2))
INSERT [dbo].[Locales] ([Id], [UsuarioId], [Nombre], [Slug], [Telefono], [Direccion], [Coordenadas], [LogoUrl], [Activo], [FechaAlta]) VALUES (2, 6, N'Rostiseria ', N'rostiseria-', N'3832406542', N'Av. Siempre Viva 742', N'-31.4135,-64.1810', N'https://i.ibb.co/B2XKqs1S/Logo-Perfumeriafondoazul.png', 0, CAST(N'2025-06-01T07:16:30.8993839' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Locales] OFF
GO
SET IDENTITY_INSERT [dbo].[Modificadores] ON 

INSERT [dbo].[Modificadores] ([Id], [Nombre], [PrecioExtra], [LocalId]) VALUES (1, N'Extra Carne', CAST(1500.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[Modificadores] ([Id], [Nombre], [PrecioExtra], [LocalId]) VALUES (2, N'Mayonesa ', CAST(0.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[Modificadores] ([Id], [Nombre], [PrecioExtra], [LocalId]) VALUES (3, N'Savora', CAST(0.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[Modificadores] ([Id], [Nombre], [PrecioExtra], [LocalId]) VALUES (4, N'Ketchup', CAST(0.00 AS Decimal(10, 2)), 1)
SET IDENTITY_INSERT [dbo].[Modificadores] OFF
GO
SET IDENTITY_INSERT [dbo].[ProduccionDetalles] ON 

INSERT [dbo].[ProduccionDetalles] ([Id], [ProduccionId], [InsumoId], [CantidadUtilizada], [PrecioUnitario]) VALUES (1, 1, 1, CAST(1.50 AS Decimal(18, 2)), CAST(4500.00 AS Decimal(18, 2)))
INSERT [dbo].[ProduccionDetalles] ([Id], [ProduccionId], [InsumoId], [CantidadUtilizada], [PrecioUnitario]) VALUES (2, 1, 2, CAST(5.00 AS Decimal(18, 2)), CAST(3000.00 AS Decimal(18, 2)))
INSERT [dbo].[ProduccionDetalles] ([Id], [ProduccionId], [InsumoId], [CantidadUtilizada], [PrecioUnitario]) VALUES (3, 1, 3, CAST(1.50 AS Decimal(18, 2)), CAST(1000.00 AS Decimal(18, 2)))
INSERT [dbo].[ProduccionDetalles] ([Id], [ProduccionId], [InsumoId], [CantidadUtilizada], [PrecioUnitario]) VALUES (4, 1, 4, CAST(1.50 AS Decimal(18, 2)), CAST(600.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[ProduccionDetalles] OFF
GO
SET IDENTITY_INSERT [dbo].[Producciones] ON 

INSERT [dbo].[Producciones] ([Id], [ProductoId], [CantidadProducida], [Fecha], [LocalId]) VALUES (1, 11, 5, CAST(N'2025-06-06T12:27:33.7016157' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[Producciones] OFF
GO
INSERT [dbo].[ProductoModificadores] ([ProductoId], [ModificadorId]) VALUES (8, 1)
INSERT [dbo].[ProductoModificadores] ([ProductoId], [ModificadorId]) VALUES (10, 1)
INSERT [dbo].[ProductoModificadores] ([ProductoId], [ModificadorId]) VALUES (8, 2)
INSERT [dbo].[ProductoModificadores] ([ProductoId], [ModificadorId]) VALUES (10, 2)
INSERT [dbo].[ProductoModificadores] ([ProductoId], [ModificadorId]) VALUES (11, 2)
INSERT [dbo].[ProductoModificadores] ([ProductoId], [ModificadorId]) VALUES (8, 3)
INSERT [dbo].[ProductoModificadores] ([ProductoId], [ModificadorId]) VALUES (10, 3)
INSERT [dbo].[ProductoModificadores] ([ProductoId], [ModificadorId]) VALUES (11, 3)
INSERT [dbo].[ProductoModificadores] ([ProductoId], [ModificadorId]) VALUES (8, 4)
INSERT [dbo].[ProductoModificadores] ([ProductoId], [ModificadorId]) VALUES (10, 4)
INSERT [dbo].[ProductoModificadores] ([ProductoId], [ModificadorId]) VALUES (11, 4)
GO
SET IDENTITY_INSERT [dbo].[Productos] ON 

INSERT [dbo].[Productos] ([Id], [Nombre], [Descripcion], [Precio], [ImagenUrl], [Stock], [Activo], [CategoriaId]) VALUES (5, N'Muzzarella', N'Clásica con mucho queso', CAST(1500.00 AS Decimal(10, 2)), N'', 20, 1, 1)
INSERT [dbo].[Productos] ([Id], [Nombre], [Descripcion], [Precio], [ImagenUrl], [Stock], [Activo], [CategoriaId]) VALUES (6, N'Napolitana Especial', N'Con tomate y ajo', CAST(1800.00 AS Decimal(10, 2)), N'', 15, 1, 1)
INSERT [dbo].[Productos] ([Id], [Nombre], [Descripcion], [Precio], [ImagenUrl], [Stock], [Activo], [CategoriaId]) VALUES (7, N'Calabresa', N'Con calabresa y aceitunas', CAST(1500.00 AS Decimal(10, 2)), N'', 0, 1, 1)
INSERT [dbo].[Productos] ([Id], [Nombre], [Descripcion], [Precio], [ImagenUrl], [Stock], [Activo], [CategoriaId]) VALUES (8, N'Doble Especial', N'Pan, Doble Carne, Huevo, Jamon, Lechuga, Tomate y Papas Fritas.', CAST(8000.00 AS Decimal(10, 2)), N'', 0, 1, 4)
INSERT [dbo].[Productos] ([Id], [Nombre], [Descripcion], [Precio], [ImagenUrl], [Stock], [Activo], [CategoriaId]) VALUES (9, N'Coca 500cc', N'Bebida Coca Cola de 500cc.', CAST(1000.00 AS Decimal(10, 2)), N'', 0, 1, 2)
INSERT [dbo].[Productos] ([Id], [Nombre], [Descripcion], [Precio], [ImagenUrl], [Stock], [Activo], [CategoriaId]) VALUES (10, N'Simple Especial', N'Pan, Carne, Huevo, Jamon, Lechuga, Tomate y Papas Fritas.', CAST(7000.00 AS Decimal(10, 2)), N'', 0, 1, 4)
INSERT [dbo].[Productos] ([Id], [Nombre], [Descripcion], [Precio], [ImagenUrl], [Stock], [Activo], [CategoriaId]) VALUES (11, N'Lomo Simple', N'Pan, Carne de lomo, Lechuga ', CAST(5000.00 AS Decimal(10, 2)), N'', 5, 1, 5)
SET IDENTITY_INSERT [dbo].[Productos] OFF
GO
SET IDENTITY_INSERT [dbo].[Proveedores] ON 

INSERT [dbo].[Proveedores] ([Id], [LocalId], [Nombre], [Telefono], [Email], [Direccion]) VALUES (1, 1, N'Distribuidora Inline', N'3832406542', N'gdp43191989@gmail.com', N'Ramon S. Castillo Barrio Obrero')
INSERT [dbo].[Proveedores] ([Id], [LocalId], [Nombre], [Telefono], [Email], [Direccion]) VALUES (2, 1, N'Panaderia ', N'3832406542', N'-', N'Av. Belgrano')
INSERT [dbo].[Proveedores] ([Id], [LocalId], [Nombre], [Telefono], [Email], [Direccion]) VALUES (3, 1, N'Verduleria', N'3832406542', N'-', N'Bv. Nestor Kirchner')
SET IDENTITY_INSERT [dbo].[Proveedores] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Nombre], [Activo]) VALUES (1, N'admin', 1)
INSERT [dbo].[Roles] ([Id], [Nombre], [Activo]) VALUES (2, N'local', 1)
INSERT [dbo].[Roles] ([Id], [Nombre], [Activo]) VALUES (3, N'cliente', 1)
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Turnos] ON 

INSERT [dbo].[Turnos] ([Id], [LocalId], [DiaSemana], [NumeroTurno], [HoraApertura], [HoraCierre], [Activo]) VALUES (1, 1, 1, 0, CAST(N'08:00:00' AS Time), CAST(N'13:00:00' AS Time), 1)
INSERT [dbo].[Turnos] ([Id], [LocalId], [DiaSemana], [NumeroTurno], [HoraApertura], [HoraCierre], [Activo]) VALUES (2, 1, 1, 1, CAST(N'20:30:00' AS Time), CAST(N'02:30:00' AS Time), 1)
SET IDENTITY_INSERT [dbo].[Turnos] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([Id], [Email], [PasswordHash], [RolId], [Activo], [FechaCreacion]) VALUES (1, N'Admin@gmail.com', N'$2a$11$DP/Bx6pqg5Ei4FK1zOopeu9T7OVW.9aDnQvYc1/Fm0JJ5ZUVBCFby', 1, 1, CAST(N'2025-05-27T20:36:18.0895323' AS DateTime2))
INSERT [dbo].[Usuarios] ([Id], [Email], [PasswordHash], [RolId], [Activo], [FechaCreacion]) VALUES (2, N'local1@gmail.com', N'$2a$11$L1BU5As9bjaDfpRl1TEgK.zzLIsqQ1MR7SP/78JXv5lkzdIJVVSd6', 2, 1, CAST(N'2025-05-27T20:54:22.7805781' AS DateTime2))
INSERT [dbo].[Usuarios] ([Id], [Email], [PasswordHash], [RolId], [Activo], [FechaCreacion]) VALUES (4, N'gdp43191989@gmail.com', N'', 3, 1, CAST(N'2025-05-28T06:08:38.7914949' AS DateTime2))
INSERT [dbo].[Usuarios] ([Id], [Email], [PasswordHash], [RolId], [Activo], [FechaCreacion]) VALUES (5, N'cliente1@gmail.com', N'$2a$11$dGYaalK7BMxb/D4aTKNAIeeKYqIxdkw0nMkSBIptlS4qOuksCxVjK', 3, 1, CAST(N'2025-05-31T20:37:55.3682064' AS DateTime2))
INSERT [dbo].[Usuarios] ([Id], [Email], [PasswordHash], [RolId], [Activo], [FechaCreacion]) VALUES (6, N'local2@gmail.com', N'$2a$11$cnNiG1ucqGfx0hHjuCHqsOBFNzqYUKLYdmcQEgHy..VEVwy5o/hOG', 2, 1, CAST(N'2025-06-01T07:16:30.7791662' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
/****** Object:  Index [IX_Categorias_LocalId]    Script Date: 06/06/2025 22:37:47 ******/
CREATE NONCLUSTERED INDEX [IX_Categorias_LocalId] ON [dbo].[Categorias]
(
	[LocalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Clientes_UsuarioId]    Script Date: 06/06/2025 22:37:47 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Clientes_UsuarioId] ON [dbo].[Clientes]
(
	[UsuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CompraDetalles_CompraId]    Script Date: 06/06/2025 22:37:47 ******/
CREATE NONCLUSTERED INDEX [IX_CompraDetalles_CompraId] ON [dbo].[CompraDetalles]
(
	[CompraId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CompraDetalles_InsumoId]    Script Date: 06/06/2025 22:37:47 ******/
CREATE NONCLUSTERED INDEX [IX_CompraDetalles_InsumoId] ON [dbo].[CompraDetalles]
(
	[InsumoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Compras_LocalId]    Script Date: 06/06/2025 22:37:47 ******/
CREATE NONCLUSTERED INDEX [IX_Compras_LocalId] ON [dbo].[Compras]
(
	[LocalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Compras_ProveedorId]    Script Date: 06/06/2025 22:37:47 ******/
CREATE NONCLUSTERED INDEX [IX_Compras_ProveedorId] ON [dbo].[Compras]
(
	[ProveedorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Direcciones_ClienteId]    Script Date: 06/06/2025 22:37:47 ******/
CREATE NONCLUSTERED INDEX [IX_Direcciones_ClienteId] ON [dbo].[Direcciones]
(
	[ClienteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Horarios_LocalId]    Script Date: 06/06/2025 22:37:47 ******/
CREATE NONCLUSTERED INDEX [IX_Horarios_LocalId] ON [dbo].[Horarios]
(
	[LocalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Insumos_LocalId]    Script Date: 06/06/2025 22:37:47 ******/
CREATE NONCLUSTERED INDEX [IX_Insumos_LocalId] ON [dbo].[Insumos]
(
	[LocalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Locales_UsuarioId]    Script Date: 06/06/2025 22:37:47 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Locales_UsuarioId] ON [dbo].[Locales]
(
	[UsuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Modificadores_LocalId]    Script Date: 06/06/2025 22:37:47 ******/
CREATE NONCLUSTERED INDEX [IX_Modificadores_LocalId] ON [dbo].[Modificadores]
(
	[LocalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProduccionDetalles_InsumoId]    Script Date: 06/06/2025 22:37:47 ******/
CREATE NONCLUSTERED INDEX [IX_ProduccionDetalles_InsumoId] ON [dbo].[ProduccionDetalles]
(
	[InsumoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProduccionDetalles_ProduccionId]    Script Date: 06/06/2025 22:37:47 ******/
CREATE NONCLUSTERED INDEX [IX_ProduccionDetalles_ProduccionId] ON [dbo].[ProduccionDetalles]
(
	[ProduccionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Producciones_LocalId]    Script Date: 06/06/2025 22:37:47 ******/
CREATE NONCLUSTERED INDEX [IX_Producciones_LocalId] ON [dbo].[Producciones]
(
	[LocalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Producciones_ProductoId]    Script Date: 06/06/2025 22:37:47 ******/
CREATE NONCLUSTERED INDEX [IX_Producciones_ProductoId] ON [dbo].[Producciones]
(
	[ProductoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductoInsumos_InsumoId]    Script Date: 06/06/2025 22:37:47 ******/
CREATE NONCLUSTERED INDEX [IX_ProductoInsumos_InsumoId] ON [dbo].[ProductoInsumos]
(
	[InsumoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductoModificadores_ModificadorId]    Script Date: 06/06/2025 22:37:47 ******/
CREATE NONCLUSTERED INDEX [IX_ProductoModificadores_ModificadorId] ON [dbo].[ProductoModificadores]
(
	[ModificadorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Productos_CategoriaId]    Script Date: 06/06/2025 22:37:47 ******/
CREATE NONCLUSTERED INDEX [IX_Productos_CategoriaId] ON [dbo].[Productos]
(
	[CategoriaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Proveedores_LocalId]    Script Date: 06/06/2025 22:37:47 ******/
CREATE NONCLUSTERED INDEX [IX_Proveedores_LocalId] ON [dbo].[Proveedores]
(
	[LocalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Turnos_LocalId]    Script Date: 06/06/2025 22:37:47 ******/
CREATE NONCLUSTERED INDEX [IX_Turnos_LocalId] ON [dbo].[Turnos]
(
	[LocalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Usuarios_RolId]    Script Date: 06/06/2025 22:37:47 ******/
CREATE NONCLUSTERED INDEX [IX_Usuarios_RolId] ON [dbo].[Usuarios]
(
	[RolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Clientes] ADD  DEFAULT (N'') FOR [Apellido]
GO
ALTER TABLE [dbo].[Clientes] ADD  DEFAULT (N'') FOR [Nombre]
GO
ALTER TABLE [dbo].[ProduccionDetalles] ADD  DEFAULT ((0.0)) FOR [PrecioUnitario]
GO
ALTER TABLE [dbo].[Categorias]  WITH CHECK ADD  CONSTRAINT [FK_Categorias_Locales_LocalId] FOREIGN KEY([LocalId])
REFERENCES [dbo].[Locales] ([Id])
GO
ALTER TABLE [dbo].[Categorias] CHECK CONSTRAINT [FK_Categorias_Locales_LocalId]
GO
ALTER TABLE [dbo].[Clientes]  WITH CHECK ADD  CONSTRAINT [FK_Clientes_Usuarios_UsuarioId] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Clientes] CHECK CONSTRAINT [FK_Clientes_Usuarios_UsuarioId]
GO
ALTER TABLE [dbo].[CompraDetalles]  WITH CHECK ADD  CONSTRAINT [FK_CompraDetalles_Compras_CompraId] FOREIGN KEY([CompraId])
REFERENCES [dbo].[Compras] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CompraDetalles] CHECK CONSTRAINT [FK_CompraDetalles_Compras_CompraId]
GO
ALTER TABLE [dbo].[CompraDetalles]  WITH CHECK ADD  CONSTRAINT [FK_CompraDetalles_Insumos_InsumoId] FOREIGN KEY([InsumoId])
REFERENCES [dbo].[Insumos] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CompraDetalles] CHECK CONSTRAINT [FK_CompraDetalles_Insumos_InsumoId]
GO
ALTER TABLE [dbo].[Compras]  WITH CHECK ADD  CONSTRAINT [FK_Compras_Locales_LocalId] FOREIGN KEY([LocalId])
REFERENCES [dbo].[Locales] ([Id])
GO
ALTER TABLE [dbo].[Compras] CHECK CONSTRAINT [FK_Compras_Locales_LocalId]
GO
ALTER TABLE [dbo].[Compras]  WITH CHECK ADD  CONSTRAINT [FK_Compras_Proveedores_ProveedorId] FOREIGN KEY([ProveedorId])
REFERENCES [dbo].[Proveedores] ([Id])
GO
ALTER TABLE [dbo].[Compras] CHECK CONSTRAINT [FK_Compras_Proveedores_ProveedorId]
GO
ALTER TABLE [dbo].[Direcciones]  WITH CHECK ADD  CONSTRAINT [FK_Direcciones_Clientes_ClienteId] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Clientes] ([Id])
GO
ALTER TABLE [dbo].[Direcciones] CHECK CONSTRAINT [FK_Direcciones_Clientes_ClienteId]
GO
ALTER TABLE [dbo].[Horarios]  WITH CHECK ADD  CONSTRAINT [FK_Horarios_Locales_LocalId] FOREIGN KEY([LocalId])
REFERENCES [dbo].[Locales] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Horarios] CHECK CONSTRAINT [FK_Horarios_Locales_LocalId]
GO
ALTER TABLE [dbo].[Insumos]  WITH CHECK ADD  CONSTRAINT [FK_Insumos_Locales_LocalId] FOREIGN KEY([LocalId])
REFERENCES [dbo].[Locales] ([Id])
GO
ALTER TABLE [dbo].[Insumos] CHECK CONSTRAINT [FK_Insumos_Locales_LocalId]
GO
ALTER TABLE [dbo].[Locales]  WITH CHECK ADD  CONSTRAINT [FK_Locales_Usuarios_UsuarioId] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Locales] CHECK CONSTRAINT [FK_Locales_Usuarios_UsuarioId]
GO
ALTER TABLE [dbo].[Modificadores]  WITH CHECK ADD  CONSTRAINT [FK_Modificadores_Locales_LocalId] FOREIGN KEY([LocalId])
REFERENCES [dbo].[Locales] ([Id])
GO
ALTER TABLE [dbo].[Modificadores] CHECK CONSTRAINT [FK_Modificadores_Locales_LocalId]
GO
ALTER TABLE [dbo].[ProduccionDetalles]  WITH CHECK ADD  CONSTRAINT [FK_ProduccionDetalles_Insumos_InsumoId] FOREIGN KEY([InsumoId])
REFERENCES [dbo].[Insumos] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProduccionDetalles] CHECK CONSTRAINT [FK_ProduccionDetalles_Insumos_InsumoId]
GO
ALTER TABLE [dbo].[ProduccionDetalles]  WITH CHECK ADD  CONSTRAINT [FK_ProduccionDetalles_Producciones_ProduccionId] FOREIGN KEY([ProduccionId])
REFERENCES [dbo].[Producciones] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProduccionDetalles] CHECK CONSTRAINT [FK_ProduccionDetalles_Producciones_ProduccionId]
GO
ALTER TABLE [dbo].[Producciones]  WITH CHECK ADD  CONSTRAINT [FK_Producciones_Locales_LocalId] FOREIGN KEY([LocalId])
REFERENCES [dbo].[Locales] ([Id])
GO
ALTER TABLE [dbo].[Producciones] CHECK CONSTRAINT [FK_Producciones_Locales_LocalId]
GO
ALTER TABLE [dbo].[Producciones]  WITH CHECK ADD  CONSTRAINT [FK_Producciones_Productos_ProductoId] FOREIGN KEY([ProductoId])
REFERENCES [dbo].[Productos] ([Id])
GO
ALTER TABLE [dbo].[Producciones] CHECK CONSTRAINT [FK_Producciones_Productos_ProductoId]
GO
ALTER TABLE [dbo].[ProductoInsumos]  WITH CHECK ADD  CONSTRAINT [FK_ProductoInsumos_Insumos_InsumoId] FOREIGN KEY([InsumoId])
REFERENCES [dbo].[Insumos] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductoInsumos] CHECK CONSTRAINT [FK_ProductoInsumos_Insumos_InsumoId]
GO
ALTER TABLE [dbo].[ProductoInsumos]  WITH CHECK ADD  CONSTRAINT [FK_ProductoInsumos_Productos_ProductoId] FOREIGN KEY([ProductoId])
REFERENCES [dbo].[Productos] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductoInsumos] CHECK CONSTRAINT [FK_ProductoInsumos_Productos_ProductoId]
GO
ALTER TABLE [dbo].[ProductoModificadores]  WITH CHECK ADD  CONSTRAINT [FK_ProductoModificadores_Modificadores_ModificadorId] FOREIGN KEY([ModificadorId])
REFERENCES [dbo].[Modificadores] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductoModificadores] CHECK CONSTRAINT [FK_ProductoModificadores_Modificadores_ModificadorId]
GO
ALTER TABLE [dbo].[ProductoModificadores]  WITH CHECK ADD  CONSTRAINT [FK_ProductoModificadores_Productos_ProductoId] FOREIGN KEY([ProductoId])
REFERENCES [dbo].[Productos] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductoModificadores] CHECK CONSTRAINT [FK_ProductoModificadores_Productos_ProductoId]
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD  CONSTRAINT [FK_Productos_Categorias_CategoriaId] FOREIGN KEY([CategoriaId])
REFERENCES [dbo].[Categorias] ([Id])
GO
ALTER TABLE [dbo].[Productos] CHECK CONSTRAINT [FK_Productos_Categorias_CategoriaId]
GO
ALTER TABLE [dbo].[Proveedores]  WITH CHECK ADD  CONSTRAINT [FK_Proveedores_Locales_LocalId] FOREIGN KEY([LocalId])
REFERENCES [dbo].[Locales] ([Id])
GO
ALTER TABLE [dbo].[Proveedores] CHECK CONSTRAINT [FK_Proveedores_Locales_LocalId]
GO
ALTER TABLE [dbo].[Turnos]  WITH CHECK ADD  CONSTRAINT [FK_Turnos_Locales_LocalId] FOREIGN KEY([LocalId])
REFERENCES [dbo].[Locales] ([Id])
GO
ALTER TABLE [dbo].[Turnos] CHECK CONSTRAINT [FK_Turnos_Locales_LocalId]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Roles_RolId] FOREIGN KEY([RolId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Roles_RolId]
GO
USE [master]
GO
ALTER DATABASE [AppPedidos] SET  READ_WRITE 
GO
