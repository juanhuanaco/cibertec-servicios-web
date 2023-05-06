--drop database BdChaskiTravel
create database BdChaskiTravel
use BdChaskiTravel


-- tables


create table tb_tour(
idTour int primary key IDENTITY(1,1),
precioTour decimal not null,
descripcionTour nvarchar(100) not null
)
go

insert into tb_tour values(0,'Sin tour')
insert into tb_tour values(300,'medio dia')
insert into tb_tour values(100,'city tour')
insert into tb_tour values(100,'full day')

go


create  table tb_hotel(
idHotel int primary key IDENTITY(1,1) , 
nomHotel nvarchar(20) not null,
categoriaHotel nvarchar(15) not null,
precioHotel decimal not null,
descripcionHotel nvarchar(50) not null

)
go

insert into tb_hotel values('Xcaret','5 estrellas',0,'Semi completo')
insert into tb_hotel values('Sheraton','5 estrellas',150,'Completo')
insert into tb_hotel values('Marriot','4 estrellas',140,'Semi completo')
insert into tb_hotel values('Rialto','4 estrellas',140,'Semi completo')

create table tb_categorias(
IdCategoria int primary key IDENTITY(1,1),
NombreCategoria varchar(15) not null
)

insert into tb_categorias values('Internacionales')
insert into tb_categorias values('Nacionales')
insert into tb_categorias values('Europa')

create table tb_destino(
idDestino int primary key IDENTITY(1,1),
pais nvarchar(40) not null,
ciudad nvarchar(40) not null,
idHotel int ,
IdCategoria int ,
idTour int ,
UnidadesEnExistencia smallint not null,
foreign key(idHotel) references tb_hotel(idHotel),
foreign key(IdCategoria) references tb_categorias(IdCategoria),
foreign key(idTour) references tb_tour(idTour)
)
insert into tb_destino values('España','Madrid',2,3,4,2)
insert into tb_destino values('Perú','Lima',2,3,4,2)
insert into tb_destino values('España','Barcelona',3,2,4,10)

create table tb_pedidos(
	idpedido int primary key,
	fpedido date default(getdate()),
	nombre varchar(100),
	apePaterno varchar(100),
	apeMaterno varchar(100),
	dni varchar(8),
	telefono int,
	email  varchar(150)
)
go

create table tb_pedidos_deta(
	idpedido int references tb_pedidos,
	idDestino int references tb_destino,
	cantidad int,
	precio decimal
)

go

-- ************ PROCEDURES  DE PEDIDOS**********
create or alter function dbo.autogenera() returns int
As
Begin 
	Declare @n int=(Select top 1 idpedido from tb_pedidos order by 1 desc)
	if(@n is null)
		Set @n=1
	else
		Set @n+=1
	return @n
End
go

create or alter proc usp_agrega_pedido
@idpedido int output,
@nom varchar(100),
@apePat varchar(100),
@apeMat varchar(100),
@dni varchar(8),
@fono int,
@email varchar(150)
As
Begin
	Set @idpedido=dbo.autogenera()
	insert tb_pedidos(idpedido,nombre,apePaterno,apeMaterno,dni,telefono,email) Values(@idpedido,@nom,@apePat,@apeMat, @dni,@fono,@email)
End
go

create or alter procedure usp_agrega_detalle
@idpedido int,
@idDestino int,
@cantidad int,
@precio decimal
As
Insert tb_pedidos_deta Values(@idpedido,@idDestino,@cantidad,@precio)
go

create or alter proc usp_actualiza_stock
@idDestino int,
@cant smallint
As
Update tb_destino Set UnidadesEnExistencia-=@cant Where idDestino=@idDestino

go
						 
--*************************************************************************************


-- ************ PROCEDURES DE TOUR**********

create procedure usp_tour_listar
	as
	select * from tb_tour
	go

create procedure usp_agregar_tour

@pre decimal,
@des varchar(100)
As
Insert tb_tour values (@pre,@des)
go




create or alter procedure usp_actualizar_tour
@idTo int,
@pre decimal,
@des varchar(100)
As
Update tb_tour
Set  precioTour = @pre ,descripcionTour =@des
Where idTour=@idTo
go

	
create procedure usp_eliminar_tour
@idTo int
as
	delete from tb_tour
	where idTour = @idTo
go
-- ************ PROCEDURES DE HOTEL**********
	create procedure usp_hotel_lis
	As
	select * from tb_hotel
	go

create or alter procedure usp_agregar_hotel

@nom varchar(20),
@cate varchar(15),
@pre decimal,
@des varchar(50)
As
Insert tb_hotel values (@nom,@cate,@pre,@des)
go



create or alter procedure usp_actualizar_hotel
@idHo int,
@nom varchar(20),
@cate varchar(15),
@pre decimal,
@des varchar(50)
As
Update tb_hotel
Set nomHotel= @nom , categoriaHotel =@cate,precioHotel =@pre,descripcionHotel =@des
Where idHotel=@idHo  
go


create procedure usp_eliminar_hotel
@idHo int
as
	delete from tb_hotel
	where idHotel= @idHo
go


-- ************ PROCEDURES DE DESTINO**********
create  or alter procedure usp_destino_list
as
select * from tb_destino
go

create or alter procedure usp_agregar_destino
@pais varchar(40),
@ciu varchar(40),
@idHo int,
@idCate int,
@idTo int,
@uni smallint
As
Insert tb_destino values (@pais,@ciu,@idHo,@idCate,@idTo,@uni)
go


create or alter procedure usp_actualizar_destino
@idDes int,
@pais varchar(40),
@ciu varchar(40),
@idHo int,
@idCate int,
@idTo int,
@uni smallint
As
Update tb_destino 
Set pais= @pais,ciudad=@ciu,idHotel= @idHo,
IdCategoria=@idCate,idTour=@idTo, UnidadesEnExistencia=@uni 
Where idDestino=@idDes
go

create procedure usp_eliminar_destino
@idDe int
as
	delete from tb_destino
	where idDestino = @idDe
go

-- ************ PROCEDURES DE CATEGORIAS**********
create procedure usp_categorias_list
as
select*from tb_categorias
go


create procedure usp_agregar_categoria

@nom varchar(15)
As
Insert tb_categorias values (@nom)
go



create or alter procedure usp_actualizar_categoria
@idCa int,
@nom varchar(15)
As
Update tb_categorias
Set NombreCategoria= @nom
Where IdCategoria=@idCa
go

create procedure usp_eliminar_categoria

@idCa int
as
	delete from tb_categorias
	where IdCategoria = @idCa
go
/* PROCEDURES DE CONSULTA PARA LA PAG. ESCOJE TU DESTINO*/

create or alter procedure usp_consultar_destino
@pa varchar(40)
as
select d.idDestino, d.pais, d.ciudad,h.nomHotel, h.categoriaHotel,t.descripcionTour,d.UnidadesEnExistencia 
	from tb_destino d inner join tb_hotel h on d.idHotel = h.idHotel 
		             inner join tb_tour t on d.idTour = t.idTour
					 
	where d.pais=@pa
go

create or alter procedure usp_consultarSin_destino
as
select d.idDestino, d.pais, d.ciudad,h.nomHotel, h.categoriaHotel, h.precioHotel , c.NombreCategoria,t.descripcionTour , t.precioTour, d.UnidadesEnExistencia 
	from tb_destino d inner join tb_hotel h on d.idHotel = h.idHotel 
		             inner join tb_tour t on d.idTour = t.idTour
					  inner join tb_categorias c on d.IdCategoria = c.IdCategoria

go

create or alter procedure usp_consultar_pedido_fecha
@fe date
as
select p.idpedido, p.fpedido, p.nombre, p.apePaterno, p.apeMaterno, p.dni, p.telefono, p.email
	from tb_pedidos p 
					 
	where p.fpedido=@fe
go


--exec usp_consultar_pedido_fecha "2022-12-01"
--go