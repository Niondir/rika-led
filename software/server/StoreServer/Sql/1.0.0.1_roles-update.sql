ALTER TABLE `rika`.`led_roles` 
 MODIFY COLUMN `name` VARCHAR(255) NOT NULL,
 ADD PRIMARY KEY (`name`),
 DROP COLUMN `id`,
 DROP COLUMN `create_advertisement`,
 DROP COLUMN `edit_sign`,
 DROP COLUMN `add_sign`,
 DROP COLUMN `add_user`,
 DROP COLUMN `edit_user`,
 DROP COLUMN `add_lamp`,
 DROP COLUMN `add_region`,
 ADD COLUMN `flags` INTEGER UNSIGNED NOT NULL AFTER `name`;

ALTER TABLE `rika`.`led_users` 
 CHANGE COLUMN `roles_id` `roles_name` VARCHAR(255) NOT NULL,
 DROP INDEX `users_FKIndex1`,
 ADD INDEX `users_FKIndex1` USING BTREE(`roles_name`);
