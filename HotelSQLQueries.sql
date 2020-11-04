create database HotelReservation;
use HotelReservation;

create table Hotels
(
	 Location varchar(20) NOT NULL ,
	 Rating int NOT NULL DEFAULT(0),
	 WeekdayRegular int,
	 WeekdayReward int,
	 WeekdendRegular int,
	 WeekdendReward int
);

EXEC sp_RENAME 'Hotels.WeekdendReward', 'WeekendReward' ,'COLUMN';

drop table Hotels;

select * from HotelReservation.sys.tables;

select * from Hotels;

delete from Hotels where Rating=5;