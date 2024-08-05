USE XfsDataDb
GO
DROP TABLE ErrorDatas
CREATE TABLE ErrorDatas (
	Id bigint IDENTITY PRIMARY KEY,
	Code varchar(50),
	[Description] nvarchar(4000),
	Category varchar(100),
	DeviceClassName varchar(200) null,
	Tag varchar(200) null,
	CreatedBy varchar(100) null,
	CreateDate datetime null,
	UpdatedBy varchar(100) null,
	UpdateDate datetime null
)