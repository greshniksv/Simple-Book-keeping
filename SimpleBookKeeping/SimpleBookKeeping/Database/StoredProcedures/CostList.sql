USE [DB_A0991A_greshniksv]
GO
/****** Object:  StoredProcedure [dbo].[CostList]    Script Date: 18.09.2017 18:37:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[CostList] @Plan uniqueidentifier 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @cost table (Id uniqueidentifier, Name nvarchar(50), Balance int);
	DECLARE @id uniqueidentifier
	DECLARE @name nvarchar(255)
	declare @cost_cur cursor 
	set @cost_cur = cursor scroll
	for 
	select id, name from [DB_A0991A_greshniksv].[dbo].[Costs] c where plan_id=@Plan and deleted=0
	OPEN @cost_cur;
	FETCH NEXT FROM @cost_cur INTO @id, @name
	WHILE @@FETCH_STATUS = 0
	BEGIN

		DECLARE @spend int
		DECLARE @balance int
		DECLARE @cost_balance int
		set @cost_balance = 0
		declare @costdet_cur cursor 
		set @costdet_cur = cursor scroll
		for 
		SELECT 
		  (SELECT ISNULL(sum(ISNULL(s.value, 0)), 0) FROM [dbo].[Spends] s where cost_detail_id = cd.[id]) as spend,
		  cd.[value] - (SELECT  ISNULL(sum(ISNULL(s.value, 0)), 0)  FROM [dbo].[Spends] s where cost_detail_id = cd.[id]) as balance
		  FROM [DB_A0991A_greshniksv].[dbo].[CostDetails] cd
		  where cost_id = @id
		  and datetime <= GETDATE() 
		  and [deleted] = 0
		  order by datetime
		OPEN @costdet_cur;
		FETCH NEXT FROM @costdet_cur INTO @spend, @balance
		WHILE @@FETCH_STATUS = 0
		BEGIN

		set @cost_balance = @cost_balance + @balance;

		FETCH NEXT FROM @costdet_cur INTO @spend, @balance
		END
		CLOSE @costdet_cur

		insert into @cost (Id, Name, Balance) values (@id, @name, @cost_balance)

	FETCH NEXT FROM @cost_cur INTO @id, @name
	END
	CLOSE @cost_cur

	select * from @cost
END
