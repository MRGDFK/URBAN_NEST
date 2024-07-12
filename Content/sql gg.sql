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
use real_estate_listing_properties;
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
------------------------------------------------------------------------------------------------------
USE real_estate_listing_properties;
create table agent(
	agent_id int not null identity(1,1) primary key ,
	agent_name varchar(52) not null,
	agent_username varchar(16) not null,
	agent_image varchar(16),
	agent_address varchar(256) not null,
	agent_contact varchar(11) not null,
	agent_email varchar(52) unique check (agent_email like '%@gmail.com'),
	agent_password NVARCHAR(255) check (LEN(agent_password) >= 8),
	agent_review varchar(255),
	
	agent_star float

	);

	drop table agent

	
  USE real_estate_listing_properties;
USE real_estate_listing_properties;

INSERT INTO agent (agent_name, agent_username, agent_image, agent_address, agent_contact, agent_email, agent_password, agent_review, agent_star)
VALUES ('John Doe', 'john123', 'john.png', '123 Elm St, Springfield', '0123456789', 'john@gmail.com', 'password123', 'Very professional', 8.5);

INSERT INTO agent (agent_name, agent_username, agent_image, agent_address, agent_contact, agent_email, agent_password, agent_review, agent_star)
VALUES ('Jane Smith', 'jane456', 'jane.png', '456 Oak St, Springfield', '0987654321', 'jane@gmail.com', 'securepass', 'Highly recommended', 9.0);

INSERT INTO agent (agent_name, agent_username, agent_image, agent_address, agent_contact, agent_email, agent_password, agent_review, agent_star)
VALUES ('Alice Johnson', 'alice789', 'alice.png', '789 Pine St, Springfield', '0192837465', 'alice@gmail.com', 'mypassword', 'Great service', 7.0);

INSERT INTO agent (agent_name, agent_username, agent_image, agent_address, agent_contact, agent_email, agent_password, agent_review, agent_star)
VALUES ('Bob Brown', 'bob987', 'bob.png', '987 Maple St, Springfield', '0213465789', 'bob@gmail.com', 'bobspass', 'Friendly and helpful', 6.5);

INSERT INTO agent (agent_name, agent_username, agent_image, agent_address, agent_contact, agent_email, agent_password, agent_review, agent_star)
VALUES ('Charlie Davis', 'charlie654', 'charlie.png', '654 Cedar St, Springfield', '0321654987', 'charlie@gmail.com', 'charliesecure', 'Knowledgeable', 7.5);

INSERT INTO agent (agent_name, agent_username, agent_image, agent_address, agent_contact, agent_email, agent_password, agent_review, agent_star)
VALUES ('Diane Evans', 'diane321', 'diane.png', '321 Birch St, Springfield', '0432165498', 'diane@gmail.com', 'dianepass', 'Excellent communicator', 9.5);

INSERT INTO agent (agent_name, agent_username, agent_image, agent_address, agent_contact, agent_email, agent_password, agent_review, agent_star)
VALUES ('Eve Foster', 'eve213', 'eve.png', '213 Redwood St, Springfield', '0543216987', 'eve@gmail.com', 'evepass123', 'Highly skilled', 8.0);

INSERT INTO agent (agent_name, agent_username, agent_image, agent_address, agent_contact, agent_email, agent_password, agent_review, agent_star)
VALUES ('Frank Green', 'frank432', 'frank.png', '432 Chestnut St, Springfield', '0654321879', 'frank@gmail.com', 'franksecure', 'Very reliable', 7.0);

USE real_estate_listing_properties;
SELECT * FROM agent;


USE real_estate_listing_properties;
SELECT * FROM agent;

	select *from agent;


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
	insert into property(prop_location, porp_type, prop_description, prop_size, prop_price, prop_status, prop_image, prop_bed, prop_bath)
    values('Badda','Flat','This is description','2200 sqft','475000','on list', 'property1.png',5,4);
	
	
    select *from users
select *from generate
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

SELECT seller_id FROM generate