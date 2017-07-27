SELECT [CAI_ID]  AS CAI
	  ,BADGE 
      ,[NETWORK_ID] AS NetworkId 
      ,[Badge] 
      ,[NAME] AS Name
      ,Email 
      ,[STATUS] AS Status
      ,[COMPANY] AS Company
      ,ISBACKUP
      ,[OPG] AS OPG
      ,[DIVISION] AS Division
      ,[SUBDIVISION] AS SubDivision
      ,[DEPARTMENT] AS Department
      ,[SUBDEPARTMENT] AS SubDepartment
      ,[ISACTIVE] AS IsActive 
  FROM [USER_PROFILE]
  WHERE NETWORK_ID = @Username
	AND APPLICATION = @Application