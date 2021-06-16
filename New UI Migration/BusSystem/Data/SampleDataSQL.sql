USE [BusSystem]
GO
INSERT [dbo].[AspNetUsers] ([Id], [ClientName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'650cb381-b14f-42d6-a065-4d712429f9f3', N'Client', N'Client@gmail.com', N'CLIENT@GMAIL.COM', N'Client@gmail.com', N'CLIENT@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEJl/dZNssAVgH0MZqWAKvLzxgwNnkgK/ETgoX4hHNIxB4pEhrqP9qaNdrqHfunPVZw==', N'T5GRONWPZWSOIUE5JUR7B33LLZWGW65G', N'5cc1d31f-b3c5-4a08-b6ba-8f7a3dbe302a', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [ClientName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'7ded3fe0-56ee-4596-8b79-7deb920787c1', N'Employee', N'Employee@ByBus.com', N'EMPLOYEE@BYBUS.COM', N'Employee@ByBus.com', N'EMPLOYEE@BYBUS.COM', 1, N'AQAAAAEAACcQAAAAENF78fXdWmBvKJW4WtIL9aZGR89K6xcjF84pxKOHVDIaSlsitimiV4+gr85PohUmAQ==', N'XFIJFILLZMZA4TCCFVEIZLGLUPNQQCGC', N'bff263b4-b795-4b7b-a05b-117892cea00d', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7ded3fe0-56ee-4596-8b79-7deb920787c1', N'82b91c06-e050-431c-9e40-5b3e15286b2a')
GO
SET IDENTITY_INSERT [dbo].[Buses] ON 
GO
INSERT [dbo].[Buses] ([ID], [BusNum], [Category], [Capacity]) VALUES (1, N'ABC123', 0, 30)
GO
INSERT [dbo].[Buses] ([ID], [BusNum], [Category], [Capacity]) VALUES (2, N'MNO456', 1, 20)
GO
INSERT [dbo].[Buses] ([ID], [BusNum], [Category], [Capacity]) VALUES (3, N'XYZ789', 2, 12)
GO
SET IDENTITY_INSERT [dbo].[Buses] OFF
GO
SET IDENTITY_INSERT [dbo].[Stations] ON 
GO
INSERT [dbo].[Stations] ([ID], [City], [Name]) VALUES (1, N'Cairo', N'Ramsis')
GO
INSERT [dbo].[Stations] ([ID], [City], [Name]) VALUES (2, N'Cairo', N'Nasr City')
GO
INSERT [dbo].[Stations] ([ID], [City], [Name]) VALUES (3, N'Giza', N'Haram')
GO
INSERT [dbo].[Stations] ([ID], [City], [Name]) VALUES (4, N'Alex', NULL)
GO
INSERT [dbo].[Stations] ([ID], [City], [Name]) VALUES (5, N'Qina', NULL)
GO
INSERT [dbo].[Stations] ([ID], [City], [Name]) VALUES (6, N'Luxor', NULL)
GO
INSERT [dbo].[Stations] ([ID], [City], [Name]) VALUES (7, N'Aswan', N'Grand Hotel')
GO
SET IDENTITY_INSERT [dbo].[Stations] OFF
GO
SET IDENTITY_INSERT [dbo].[Routes] ON 
GO
INSERT [dbo].[Routes] ([ID], [Duration], [PickUpID], [DropOffID]) VALUES (1, 120, 1, 4)
GO
INSERT [dbo].[Routes] ([ID], [Duration], [PickUpID], [DropOffID]) VALUES (2, 120, 4, 1)
GO
INSERT [dbo].[Routes] ([ID], [Duration], [PickUpID], [DropOffID]) VALUES (3, 600, 1, 5)
GO
INSERT [dbo].[Routes] ([ID], [Duration], [PickUpID], [DropOffID]) VALUES (4, 600, 5, 1)
GO
SET IDENTITY_INSERT [dbo].[Routes] OFF
GO
SET IDENTITY_INSERT [dbo].[Trips] ON 
GO
INSERT [dbo].[Trips] ([ID], [StartDateTime], [Price], [AvailableSeats], [RouteID], [BusID]) VALUES (1, CAST(N'2021-07-01T18:00:00.0000000' AS DateTime2), 49.9900, N'1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30', 1, 1)
GO
INSERT [dbo].[Trips] ([ID], [StartDateTime], [Price], [AvailableSeats], [RouteID], [BusID]) VALUES (2, CAST(N'2021-07-01T19:00:00.0000000' AS DateTime2), 74.9900, N'1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20', 1, 2)
GO
INSERT [dbo].[Trips] ([ID], [StartDateTime], [Price], [AvailableSeats], [RouteID], [BusID]) VALUES (3, CAST(N'2021-07-01T20:21:00.0000000' AS DateTime2), 99.9900, N'1,2,3,4,5,6,7,8,9,10,11,12', 1, 3)
GO
SET IDENTITY_INSERT [dbo].[Trips] OFF
GO
SET IDENTITY_INSERT [dbo].[Tickets] ON 
GO
INSERT [dbo].[Tickets] ([ID], [PassengerCount], [BookedSeats], [TripID], [ClientID]) VALUES (1, 5, N'1,2,3,4,5', 1, N'650cb381-b14f-42d6-a065-4d712429f9f3')
GO
INSERT [dbo].[Tickets] ([ID], [PassengerCount], [BookedSeats], [TripID], [ClientID]) VALUES (2, 4, N'1,2,3,4', 2, N'650cb381-b14f-42d6-a065-4d712429f9f3')
GO
INSERT [dbo].[Tickets] ([ID], [PassengerCount], [BookedSeats], [TripID], [ClientID]) VALUES (3, 3, N'1,2,3', 3, N'650cb381-b14f-42d6-a065-4d712429f9f3')
GO
SET IDENTITY_INSERT [dbo].[Tickets] OFF
GO
