CREATE TABLE clients (
	id INT NOT NULL PRIMARY KEY IDENTITY,
	firstname VARCHAR(100) NOT NULL,
	lastname VARCHAR(100) NOT NULL,
	email VARCHAR(100) NOT NULL,
	address VARCHAR(100) NOT NULL,
	phone VARCHAR(100) NOT NULL,
	created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);


INSERT INTO clients (firstname, lastname, email, address, phone, created_at) VALUES
('John', 'Doe', 'johndoe@example.com', '123 Main St', '123-456-7890', DEFAULT),
('Jane', 'Smith', 'janesmith@example.com', '456 Elm St', '987-654-3210', DEFAULT),
('Alice', 'Johnson', 'alicej@example.com', '789 Oak St', '555-123-4567', DEFAULT),
('Bob', 'Brown', 'bobbrown@example.com', '321 Pine St', '222-333-4444', DEFAULT),
('Charlie', 'Davis', 'charlied@example.com', '654 Maple St', '444-555-6666', DEFAULT),
('Diana', 'Evans', 'dianae@example.com', '987 Cedar St', '777-888-9999', DEFAULT),
('Frank', 'Green', 'frankg@example.com', '159 Birch St', '666-777-8888', DEFAULT),
('Grace', 'Hill', 'graceh@example.com', '753 Walnut St', '111-222-3333', DEFAULT),
('Henry', 'Ivy', 'henryi@example.com', '951 Spruce St', '333-444-5555', DEFAULT),
('Isabel', 'Jones', 'isabelj@example.com', '258 Chestnut St', '888-999-0000', DEFAULT);

