DROP TABLE user;
DROP TABLE diginote;
DROP TABLE purchase_order;
DROP TABLE sales_order;
DROP TABLE quotation;

CREATE TABLE user(
   id INTEGER PRIMARY KEY NOT NULL,
   username TEXT NOT NULL,
   password TEXT NOT NULL,
   nickname TEXT NOT NULL,
   purchase_order INTEGER NOT NULL,
   sales_order INTEGER NOT NULL,
   FOREIGN KEY(purchase_order) REFERENCES purchase_order(id),
   FOREIGN KEY(sales_order) REFERENCES sales_order(id)
);

CREATE TABLE diginote(
   serial_number TEXT PRIMARY KEY NOT NULL,
   owner INTEGER NOT NULL,
   FOREIGN KEY(owner) REFERENCES user(id)
);

CREATE TABLE purchase_order(
   id INTEGER PRIMARY KEY NOT NULL,
   date TEXT DEFAULT CURRENT_TIMESTAMP,
   quantity INTEGER DEFAULT 0,
   is_busy INTEGER DEFAULT 0
);

CREATE TABLE sales_order(
   id INTEGER PRIMARY KEY NOT NULL,
   date TEXT CURRENT_TIMESTAMP,
   quantity INTEGER DEFAULT 0,
   is_busy INTEGER DEFAULT 0
);

CREATE TABLE quotation(
   id INTEGER PRIMARY KEY NOT NULL,
   value REAL NOT NULL,
   date TEXT DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO quotation(id, value, date) VALUES (NULL, 1.0, CURRENT_TIMESTAMP);

INSERT INTO sales_order(id) VALUES(1);
INSERT INTO sales_order(id, quantity) VALUES(2, 2);
INSERT INTO sales_order(id) VALUES(3);
INSERT INTO sales_order(id) VALUES(4);

INSERT INTO purchase_order(id, quantity) VALUES(1, 1);
INSERT INTO purchase_order(id) VALUES(2);
INSERT INTO purchase_order(id) VALUES(3);
INSERT INTO purchase_order(id) VALUES(4);

INSERT INTO user(id, username, password, nickname, purchase_order, sales_order) VALUES (NULL, 'abc', 'abc', 'abc', 1, 1);
INSERT INTO user(id, username, password, nickname, purchase_order, sales_order) VALUES (NULL, 'abcd', 'abcd', 'abcd', 2, 2);
INSERT INTO user(id, username, password, nickname, purchase_order, sales_order) VALUES (NULL, 'abcde', 'abcde', 'abcde', 3, 3);
INSERT INTO user(id, username, password, nickname, purchase_order, sales_order) VALUES (NULL, 'abcdef', 'abcdef', 'abcdef', 4, 4);

INSERT INTO diginote(serial_number, owner) VALUES("123456789", 2);
INSERT INTO diginote(serial_number, owner) VALUES("12345678", 2);
INSERT INTO diginote(serial_number, owner) VALUES("1234567", 2);
INSERT INTO diginote(serial_number, owner) VALUES("123456", 2);
INSERT INTO diginote(serial_number, owner) VALUES("12345", 1);

--select s.id, count(*) from diginote d, sales_order s where d.sales_order = s.id group by s.id order by date desc;