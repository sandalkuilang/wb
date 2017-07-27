﻿DECLARE @@salt UNIQUEIDENTIFIER=NEWID()
BEGIN TRY

    INSERT INTO dbo.[USER_PROFILE] (FIRSTNAME, LASTNAME, USERNAME, EMAIL, PASSWORDHASH, SALT, ISACTIVE, CREATEDBY, CREATEDDATE)
    VALUES(@Firstname, @Lastname, @Username, @Email, HASHBYTES('SHA2_512', @Password+CAST(@@salt AS NVARCHAR(36))), @@salt, 1, @CreatedBy, @CreatedDate)

    SELECT 'Success'

END TRY
BEGIN CATCH
    SELECT ERROR_MESSAGE() 
END CATCH