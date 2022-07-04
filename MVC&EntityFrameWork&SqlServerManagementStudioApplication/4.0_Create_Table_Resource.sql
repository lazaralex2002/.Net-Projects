USE TaskManagement
CREATE TABLE Resource
(
	ResourceId INT IDENTITY(1, 1) PRIMARY KEY,
	ResourceName varchar(50),
	ResourceRate numeric(8,1)
);