Create database real_state_listing_property;

use real_state_listing_property;



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
	
	

create table users(
	users_id int not null IDENTITY(1,1) primary key ,
	users_name varchar(52) not null,
	users_username varchar(16) not null,
	users_image varchar(16),
	users_address varchar(256) not null,
	users_contact varchar(11) not null,
	users_email varchar(52) unique check (users_email like '%@gmail.com'),
	users_password NVARCHAR(255) not null check (LEN(users_password) >= 8)
    );

	drop table users

    use real_state_listing_property;
	insert into users(users_name, users_username, users_image, users_address, users_contact, users_email, users_password) 
    values ('XYZ','ABC','uv.png','dchvasjdc ikavcjav','017734896','XYZ@gmail.com','123456789');

    use real_state_listing_property;
	select *from users;
-----------------------------------------------------------------------------------------------------------------------

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

    use real_state_listing_property;
	insert into agent(agent_name, agent_username, agent_image, agent_address, agent_contact, agent_email, agent_password, agent_review, agent_star) 
    values ('ABC','XYZ','pq.png','sdhbfvls vsdfvsd','01735322020','XYZ@gmail.com','jfblabfl','Great', 7.00);

    use real_state_listing_property;
	select *from agent;

	

	
------------------------------------------------------------------------------------------------------------------------
use real_state_listing_property;

create table review(
	review_id int not null identity(1,1) primary key,
    agent_id int not null,
	foreign key(agent_id) references agent(agent_id),
    users_id int not null,
	foreign key(users_id) references users(users_id),
    report varchar(256) ,
	);
	
	

  
	insert into review(agent_id, users_id, report) values (1,1,'Agent bhalana, oy ekta batpar');

    use real_state_listing_property;
	select *from review;
----------------------------------------------------------------------------------------------
	CREATE TABLE role (
    role_id int PRIMARY KEY,
    role_name varchar(50) NOT NULL,
    role_color varchar(20) NOT NULL
);

-- Insert fixed roles
INSERT INTO role (role_id, role_name, role_color)
VALUES 
    (1, 'admin', 'red'),
    (2, 'seller', 'green'),
    (3, 'buyer', 'blue'),
    (4, 'agent', 'orange');
	
----------------------------------------------------------------------------------------------------------------------
USE real_state_listing_property;

-- Create the generate table
CREATE TABLE generate (
    users_id INT NOT NULL PRIMARY KEY,
    buyer_id VARCHAR(255) NOT NULL Unique ,
    seller_id VARCHAR(255) NOT NULL Unique 

);

-- Insert user_id and generate buyer_id and seller_id in the format users_id.buyer_id and users_id.seller_id
DECLARE @users_id INT = 1;
DECLARE @buyer_id VARCHAR(255) = CONCAT(CAST(@users_id AS VARCHAR(255)), '.', CAST(@users_id * 2 AS VARCHAR(255)));
DECLARE @seller_id VARCHAR(255) = CONCAT(CAST(@users_id AS VARCHAR(255)), '.', CAST(@users_id * 2 + 1 AS VARCHAR(255)));

INSERT INTO generate (users_id, buyer_id, seller_id)
VALUES (@users_id, @buyer_id, @seller_id);

-- Retrieve all records from the Generate table
SELECT * FROM generate;



-------------------------------------------------------------------------------------
USE real_state_listing_property;

CREATE TABLE property (
    prop_id int not null identity(1,1) primary key, 
    seller_id varchar(255) not null unique,
    foreign key(seller_id) references generate(seller_id),
    prop_location varchar(70) not null,
    porp_type varchar(10) not null,
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
    seller_id varchar(255) not null unique,
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
use real_state_listing_property;
create table amenities(
    prop_id int not null,
	foreign key(prop_id) references property(prop_id),
    bed int not null,
    washroom int not null,
    balcony int not null,
    elevator int not null,
    garage int not null
    );

    use real_state_listing_property;
    insert into amenities(prop_id, bed, washroom, balcony, elevator, garage)
    values(1,5,3,2,2,1);

    use real_state_listing_property;
    select *from amenities;

