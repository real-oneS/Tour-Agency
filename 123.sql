CREATE PROCEDURE [dbo].[sp_CreateDiscount]
    @condition nvarchar(max),
    @count nvarchar(max),
    @Id int out
AS
    INSERT INTO Discounts(Condition, Count)
    VALUES (@condition, @count)
  
    SET @Id=SCOPE_IDENTITY()
GO
CREATE PROCEDURE [dbo].[sp_CreateClient]
    @surname nvarchar(max),
	@firstname nvarchar(max),
	@patronymic nvarchar(max),
	@address nvarchar(max),
	@telephone nvarchar(max),
    @Id int out
AS
    INSERT INTO Clients(Surname,FirstName,Patronymic,Address,Telephone)
    VALUES (@surname,@firstname,@patronymic,@address,@telephone)
  
    SET @Id=SCOPE_IDENTITY()
GO


CREATE PROCEDURE [dbo].[sp_CreateHotel]
    @name nvarchar(max),
	@country nvarchar(max),
	@climate nvarchar(max),
	@cost int,
    @Id int out
AS
    INSERT INTO Hotels(Name,Country,Climate,Cost)
    VALUES (@name,@country,@climate,@cost)
  
    SET @Id=SCOPE_IDENTITY()
GO

CREATE PROCEDURE [dbo].[sp_CreateRoute]
    @hotelid int,
	@duration int,
	@cost int,
    @Id int out
AS
    INSERT INTO Routes(HotelId,Duration,Cost)
    VALUES (@hotelid,@duration,@cost)
  
    SET @Id=SCOPE_IDENTITY()
GO


CREATE PROCEDURE [dbo].[sp_CreateVoucher]
    @routeid int,
	@clientid int,
	@date date,
	@count int,
	@discount nvarchar(max),
	@finalcost int,
    @Id int out
AS
    INSERT INTO Vouchers(RouteId,ClientId,Date,Count,Discount,FinalCost)
    VALUES (@routeid,@clientid,@date,@count,@discount,@finalcost)
  
    SET @Id=SCOPE_IDENTITY()
GO

