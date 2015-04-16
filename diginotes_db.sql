DROP TABLE user;
DROP TABLE diginote;
DROP TABLE purchase_order;
DROP TABLE sales_order;
DROP TABLE quotation;

CREATE TABLE user(
   id INTEGER PRIMARY KEY NOT NULL,
   username TEXT UNIQUE NOT NULL,
   password TEXT NOT NULL,
   nickname TEXT UNIQUE NOT NULL,
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
   date TEXT DEFAULT CURRENT_TIMESTAMP,
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

INSERT INTO purchase_order(id) VALUES(1);
INSERT INTO purchase_order(id) VALUES(2);
INSERT INTO purchase_order(id) VALUES(3);
INSERT INTO purchase_order(id,quantity) VALUES(4,1);

INSERT INTO user(id, username, password, nickname, purchase_order, sales_order) VALUES (NULL, 'teste1', 'teste1', 'teste1', 1, 1);
INSERT INTO user(id, username, password, nickname, purchase_order, sales_order) VALUES (NULL, 'teste2', 'teste2', 'teste2', 2, 2);
INSERT INTO user(id, username, password, nickname, purchase_order, sales_order) VALUES (NULL, 'teste3', 'teste3', 'teste3', 3, 3);
INSERT INTO user(id, username, password, nickname, purchase_order, sales_order) VALUES (NULL, 'teste4', 'teste4', 'teste4', 4, 4);

INSERT INTO diginote(serial_number, owner) VALUES("QREWCAWTE", 1);
INSERT INTO diginote(serial_number, owner) VALUES("5AGS52AFG", 1);
INSERT INTO diginote(serial_number, owner) VALUES("G34GREW6A", 1);
INSERT INTO diginote(serial_number, owner) VALUES("ADFGYW5TY", 1);

INSERT INTO diginote(serial_number, owner) VALUES("534T23DAF", 2);
INSERT INTO diginote(serial_number, owner) VALUES("54DSFY5DF", 2);

INSERT INTO diginote(serial_number, owner) VALUES("PFKRTRE45", 3);
INSERT INTO diginote(serial_number, owner) VALUES("SAFG45ADD", 3);
INSERT INTO diginote(serial_number, owner) VALUES("AFSDFA332", 3);
INSERT INTO diginote(serial_number, owner) VALUES("YDSF34234", 3);
INSERT INTO diginote(serial_number, owner) VALUES("AITYURIA3", 3);
INSERT INTO diginote(serial_number, owner) VALUES("O797GSFQT", 3);



--select s.id, count(*) from diginote d, sales_order s where d.sales_order = s.id group by s.id order by date desc;