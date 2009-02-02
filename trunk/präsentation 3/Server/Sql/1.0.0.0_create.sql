CREATE TABLE led_advertisements (
  id INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
  regions_id INTEGER UNSIGNED NOT NULL,
  text VARCHAR(255) NULL,
  PRIMARY KEY(id),
  INDEX advertisements_FKIndex1(regions_id)
);

CREATE TABLE led_lamps (
  id INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
  regions_id INTEGER UNSIGNED NOT NULL,
  PRIMARY KEY(id),
  INDEX lamps_FKIndex1(regions_id)
);

CREATE TABLE led_regions (
  id INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
  name VARCHAR(255) NULL,
  PRIMARY KEY(id)
);

CREATE TABLE led_roles (
  id INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
  name INTEGER UNSIGNED NULL,
  create_advertisement BOOL NULL,
  edit_sign BOOL NULL,
  add_sign BOOL NULL,
  add_user BOOL NULL,
  edit_user BOOL NULL,
  add_lamp BOOL NULL,
  add_region BOOL NULL,
  PRIMARY KEY(id)
);

CREATE TABLE led_signs (
  id INTEGER UNSIGNED NOT NULL,
  regions_id INTEGER UNSIGNED NULL,
  text VARCHAR(255) NULL,
  type INTEGER UNSIGNED NULL,
  PRIMARY KEY(id),
  INDEX signs_FKIndex1(regions_id)
);

CREATE TABLE led_traces (
  id INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
  PRIMARY KEY(id)
);

CREATE TABLE led_users (
  id INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
  roles_id INTEGER UNSIGNED NOT NULL,
  login VARCHAR(255) NULL,
  password VARCHAR(255) NULL,
  PRIMARY KEY(id),
  INDEX users_FKIndex1(roles_id)
);

CREATE TABLE led_waypoints (
  id INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
  lamps_id INTEGER UNSIGNED NOT NULL,
  traces_id INTEGER UNSIGNED NOT NULL,
  time TIMESTAMP NULL,
  PRIMARY KEY(id),
  INDEX waypoints_FKIndex2(traces_id),
  INDEX waypoints_FKIndex3(lamps_id)
);

