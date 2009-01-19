ALTER TABLE `rika`.`led_users` DROP COLUMN `id`,
 MODIFY COLUMN `login` VARCHAR(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL DEFAULT 'none',
 DROP PRIMARY KEY,
 ADD PRIMARY KEY  USING BTREE(`login`)
, DROP INDEX `uniqe_login`;