use CINEMABASE;

CREATE PROCEDURE [dbo].[sp_InsertBusySeats]
    @seanceid int,
    @seatnumber int
AS
declare @sn int=1;
while(@sn<=@seatnumber)
begin
    INSERT INTO dbo.BusySeats(SeanceId, SeatNumber, IsBusy)
    VALUES (@seanceid, @sn, 0);
	set @sn=@sn+1;
	end
GO
select * from BusySeats
delete from BusySeats
delete from seances;