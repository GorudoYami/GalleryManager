CREATE TABLE `albums` (
  `id` bigint(20) unsigned NOT NULL,
  `name` varchar(256) NOT NULL,
  `files` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin DEFAULT NULL CHECK (json_valid(`files`))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;