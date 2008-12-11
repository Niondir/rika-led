DROP TABLE IF EXISTS `rika`.`led_products`;
CREATE TABLE  `rika`.`led_products` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `name` varchar(255) NOT NULL,
  `price` double NOT NULL,
  `regions_id` int(10) unsigned NOT NULL,
  PRIMARY KEY  (`id`)
)

