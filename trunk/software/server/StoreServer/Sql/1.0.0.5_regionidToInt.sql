ALTER TABLE `rika`.`led_regions` MODIFY COLUMN `id` VARCHAR(4) NOT NULL DEFAULT 0;
ALTER TABLE `rika`.`led_products` MODIFY COLUMN `regions_id` VARCHAR(4) NOT NULL DEFAULT 0;
ALTER TABLE `rika`.`led_advertisements` MODIFY COLUMN `regions_id` VARCHAR(4) NOT NULL DEFAULT 0;
