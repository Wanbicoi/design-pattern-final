-- PostgreSQL
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_tables WHERE schemaname = 'public' AND tablename = 'users') THEN
        CREATE TABLE users (
            id SERIAL PRIMARY KEY,
            username VARCHAR(50) NOT NULL,
            email VARCHAR(50) NOT NULL
        );

        INSERT INTO users (username, email) VALUES
        ('john_doe', 'john.doe@example.com'),
        ('jane_smith', 'jane.smith@example.com');
    END IF;
END $$;

-- PostgreSQL
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_tables WHERE schemaname = 'public' AND tablename = 'products') THEN
        CREATE TABLE products (
            id SERIAL PRIMARY KEY,
            product_name VARCHAR(100) NOT NULL,
            price DECIMAL(10, 2) NOT NULL
        );

        -- Insert sample data into "products" table
        INSERT INTO products (product_name, price) VALUES
        ('Laptop', 999.99),
        ('Smartphone', 499.50),
        ('Headphones', 49.99);
    END IF;
END $$;