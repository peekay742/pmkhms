-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_GetOperationInstruments
	-- Add the parameters for the stored procedure here
	@OperationTreaterId INT = NULL,
    @InstrumentId INT = NULL,
    @IsFOC BIT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *, I.Name AS [InstrumentName] FROM OperationInstrument OI
    LEFT JOIN [Instrument] I ON OI.InstrumentId=I.Id
	WHERE
		(@OperationTreaterId IS NULL OR OperationTreaterId=@OperationTreaterId) AND
        (@InstrumentId IS NULL OR InstrumentId=@InstrumentId) AND
        (@IsFOC IS NULL OR IsFOC=@IsFOC)
        ORDER BY SortOrder
END
GO
