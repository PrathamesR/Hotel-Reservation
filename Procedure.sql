create procedure [dbo].[AddNewHotel]
@Location varchar(20),
@Rating int,
@WeekdayRegular int,
@WeekdayReward int,
@WeekendRegular int,
@WeekendReward int
as
insert into Hotels values(@Location,@Rating,@WeekdayRegular,@WeekdayReward,@WeekendRegular,@WeekendReward);