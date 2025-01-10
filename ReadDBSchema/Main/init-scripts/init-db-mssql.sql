-- MSSQL
CREATE TABLE users (
    id INT IDENTITY(1,1) PRIMARY KEY,
    username VARCHAR(50) NOT NULL,
    email VARCHAR(50) NOT NULL
);

INSERT INTO users (username, email) VALUES
('john_doe', 'john.doe@example.com'),
('jane_smith', 'jane.smith@example.com');

-- MSSQL
CREATE TABLE products (
    id INT IDENTITY(1,1) PRIMARY KEY,
    product_name VARCHAR(100) NOT NULL,
    price DECIMAL(10, 2) NOT NULL
);


-- Insert sample data into "products" table
INSERT INTO products (product_name, price) VALUES
('Laptop', 999.99),
('Smartphone', 499.50),
('Headphones', 49.99);