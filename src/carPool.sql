DROP DATABASE IF EXISTS `carPool`;
CREATE DATABASE `carPool`;
USE `CarPool`;

CREATE TABLE `user`(
`user_id` int,
`name` varchar(50) NOT NULL,
`surname` varchar(50) NOT NULL,
`phone` int(11) NOT NULL,
`email` varchar(100) NOT NULL,
`password` varchar(100) NOT NULL,
PRIMARY KEY(user_id)
);

CREATE TABLE `carpool_opportunity`(
`carpool_id` int(10) NOT NULL,
`departure_time` int(10) NOT NULL,
`arrival_time` int(10) NOT NULL,
`origin` varchar(50) NOt Null,
`days_avaliable` int(10) NOT NULL,
`destination` varchar(50) NOT NULL,
`avaliable_seats` int(10) NOT NULL,
`owner` varchar(50) NOT NULL,
PRIMARY KEY(carpool_id)
);
