ALTER TABLE `rika`.`led_waypoints` CHANGE COLUMN `lamps_id` `regions_id` VARCHAR(4) NOT NULL,
 DROP INDEX `waypoints_FKIndex3`,
 ADD INDEX `waypoints_FKIndex3` USING BTREE(`regions_id`);
