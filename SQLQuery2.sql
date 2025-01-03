CREATE TABLE doctor (
	id INT NOT NULL PRIMARY KEY IDENTITY,
	firstname VARCHAR(100) NOT NULL,
	lastname VARCHAR(100) NOT NULL,
	email VARCHAR(100) NOT NULL,
	hospital VARCHAR(100) NOT NULL,
	male BIT NOT NULL,
	created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);


INSERT INTO doctor (firstname, lastname, email, hospital, male, created_at) VALUES
('John', 'Doe', 'johndoe@example.com', '123 Main St', 0, DEFAULT),
('Jane', 'Smith', 'janesmith@example.com', '456 Elm St', 1, DEFAULT),
('Alice', 'Johnson', 'alicej@example.com', '789 Oak St', 0, DEFAULT),
('Bob', 'Brown', 'bobbrown@example.com', '321 Pine St', 0, DEFAULT),
('Charlie', 'Davis', 'charlied@example.com', '654 Maple St', 1, DEFAULT),
('Diana', 'Evans', 'dianae@example.com', '987 Cedar St', 1, DEFAULT),
('Frank', 'Green', 'frankg@example.com', '159 Birch St', 0, DEFAULT),
('Grace', 'Hill', 'graceh@example.com', '753 Walnut St', 0, DEFAULT),
('Henry', 'Ivy', 'henryi@example.com', '951 Spruce St', 1, DEFAULT),
('Isabel', 'Jones', 'isabelj@example.com', '258 Chestnut St',1, DEFAULT);

