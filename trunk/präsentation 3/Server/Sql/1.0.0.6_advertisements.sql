ALTER TABLE `rika`.`led_advertisements` ADD COLUMN `name` VARCHAR(255) NOT NULL AFTER `line4`,
 ADD COLUMN `startDate` TIMESTAMP NOT NULL AFTER `name`,
 ADD COLUMN `stopDate` TIMESTAMP NOT NULL AFTER `startDate`,
 ADD COLUMN `startTime` TIMESTAMP NOT NULL AFTER `stopDate`,
 ADD COLUMN `stopTime` TIMESTAMP NOT NULL AFTER `startTime`;
