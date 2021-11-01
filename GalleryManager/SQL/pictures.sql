CREATE TABLE `pictures` (
  `id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `path` varchar(256) NOT NULL,
  `size` bigint(20) unsigned NOT NULL,
  `format` varchar(10) DEFAULT NULL,
  `hash` varchar(128) NOT NULL,
  `file` varchar(256) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;