use winformdb;

-- Create the School table
CREATE TABLE School (
    SchoolID INT IDENTITY(1,1) PRIMARY KEY,  -- Primary key with auto-increment
    SchoolName NVARCHAR(255) NOT NULL,      -- Name of the school
    Address NVARCHAR(255),                  -- Address of the school
    City NVARCHAR(100),                     -- City where the school is located
    State NVARCHAR(100),                    -- State where the school is located
    ZipCode NVARCHAR(20),                   -- ZIP code
    EstablishedYear INT,                    -- Year the school was established
    IsPublic BIT                            -- Indicates if the school is public or private
);

-- Insert mock data into the School table
INSERT INTO School (SchoolName, Address, City, State, ZipCode, EstablishedYear, IsPublic)
VALUES 
(N'Greenwood High', N'123 Elm St', N'Springfield', N'Illinois', N'62704', 1985, 1),
(N'Riverside Academy', N'456 River Rd', N'Austin', N'Texas', N'73301', 1995, 0),
(N'Maple Leaf International', N'789 Maple Ave', N'Toronto', N'Ontario', N'M4B 1B3', 2005, 0),
(N'Horizon Public School', N'1010 Horizon Blvd', N'Phoenix', N'Arizona', N'85001', 1970, 1),
(N'Summit School of Excellence', N'202 Summit Dr', N'Denver', N'Colorado', N'80202', 2010, 1);
