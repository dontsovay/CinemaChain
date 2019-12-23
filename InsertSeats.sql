use CINEMABASEE;


CREATE PROCEDURE [dbo].[sp_InsertSeats]
    @cinemaid int,
    @seatnumber int
AS
declare @sn int=1;
while(@sn<=@seatnumber)
begin
    INSERT INTO dbo.Seats (CinemaId, SeatNumber)
    VALUES (@cinemaid, @sn);
	set @sn=@sn+1;
	end
GO