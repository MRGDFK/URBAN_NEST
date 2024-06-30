Create database real_estate_listing_properties;

use real_estate_listing_properties;



create table admin(
	admin_id int not null Identity(1,1) primary key,
	admin_name varchar (52) not null,
	admin_image varchar(16),
	admin_contact varchar(11) not null,
	admin_email varchar (52) unique check (admin_email like '%@gmail.com'),
	admin_password Nvarchar(255) not null check (LEN(admin_password) >= 8)

	);
use real_state_listing_property;
	insert into admin( admin_name, admin_image, admin_contact, admin_email, admin_password) 
    values ('XYZX','uwv.png','0175664896','XY@gmail.com','123456678345234');

    
	select *from admin
----------------------------------------------------------------------------------------
-- Create the users table
CREATE TABLE users (
    users_id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    users_name VARCHAR(52) NOT NULL,
    users_username VARCHAR(16) NOT NULL,
    users_image VARCHAR(16),
    users_address VARCHAR(256) NOT NULL,
    users_contact VARCHAR(11) NOT NULL,
    users_email VARCHAR(52) UNIQUE CHECK (users_email LIKE '%@gmail.com'),
    users_password NVARCHAR(255) NOT NULL CHECK (LEN(users_password) >= 8)
);


-- Insert initial data into users table
USE real_estate_listing_properties;
INSERT INTO users (users_name, users_username, users_image, users_address, users_contact, users_email, users_password) 
VALUES ('XYZ', 'ABC', 'uv.png', 'dchvasjdc ikavcjav', '017734896', 'XYZ@gmail.com', '123456789');

INSERT INTO users (users_name, users_username, users_image, users_address, users_contact, users_email, users_password) 
VALUES ('AMB', 'ABC', 'uv.png', 'dchvasjdc ikavcjav', '017734896', 'AMB@gmail.com', '123456789');

INSERT INTO users (users_name, users_username, users_image, users_address, users_contact, users_email, users_password) 
VALUES ('GG', 'ABC', 'uv.png', 'dchvasjdc ikavcjav', '017734896', 'AGG@gmail.com', '123556789');
---------------------------------------------------------------------------------------------------------
CREATE TABLE generate (
    users_id INT NOT NULL,
    FOREIGN KEY(users_id) REFERENCES users(users_id),
    buyer_id VARCHAR(255) NOT NULL UNIQUE,
    seller_id VARCHAR(255) NOT NULL UNIQUE
);


----------------------------------------------------------------------------------------------------
CREATE TRIGGER trg_AfterInsertUsers
ON users
AFTER INSERT
AS
BEGIN
    DECLARE @new_users_id INT;
    DECLARE @buyer_id VARCHAR(255);
    DECLARE @seller_id VARCHAR(255);

    SELECT @new_users_id = inserted.users_id FROM inserted;

    SET @buyer_id = CONCAT(CAST(@new_users_id AS VARCHAR(255)), '.2');
    SET @seller_id = CONCAT(CAST(@new_users_id AS VARCHAR(255)), '.1');

    INSERT INTO generate (users_id, buyer_id, seller_id)
    VALUES (@new_users_id, @buyer_id, @seller_id);
END;

select *from users
select *from generate

-------------------------------------------------------------------------------------
USE real_state_listing_property;

CREATE TABLE property (
    prop_id int not null identity(1,1) primary key, 
    seller_id varchar(255) not null,
    foreign key(seller_id) references generate(seller_id),
    prop_location varchar(70) not null,
    prop_type varchar(10) not null,
    prop_description varchar(256) not null,
    prop_size varchar(50) not null,
    prop_price int not null,
    prop_status varchar(10) not null,
    prop_image varchar (100) not null,
    prop_bed int not null,
    prop_bath int not null
);


    use real_state_listing_property;
	insert into property( seller_id, prop_location, porp_type, prop_description, prop_size, prop_price, prop_status, prop_image, prop_bed, prop_bath)
    values('1.3','Badda','Flat','This is description','2200 sqft','475000','on list', 'property1.png',5,4);
	
	
    
	select *from property;

USE real_state_listing_property;

ALTER TABLE property
ADD added_at DATETIME2 DEFAULT CURRENT_TIMESTAMP;

	
----------------------------------------------------------------------------------
use real_state_listing_property;

USE real_state_listing_property;

USE real_state_listing_property;

CREATE TABLE listing (
    listing_id int not null identity(1,1) primary key,
    listing_status varchar(10) not null,
    listing_date DATE DEFAULT GETDATE(),
    listing_comments varchar(256),
    seller_id varchar(255) not null,
    foreign key(seller_id) references Generate(seller_id),
    prop_id int not null,
    foreign key(prop_id) references property(prop_id)
);

    USE real_state_listing_property;

-- Insert data into listing table
INSERT INTO listing (listing_status, listing_date, listing_comments, seller_id, prop_id)
VALUES ('pending', GETDATE(), 'keu kininyen na, vejal ase', '1.3', 1);


   
	select *from listing;

------------------------------------------------------------------------------

