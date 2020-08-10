USE [ImportManagementSystem]
GO

ALTER TABLE CustomerList
	ADD CheckHeader BIT NOT NULL DEFAULT 0

UPDATE CustomerList
	SET CheckHeader = 1
WHERE CustomerName = 'Burlington'

