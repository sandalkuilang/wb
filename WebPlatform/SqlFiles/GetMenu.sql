DECLARE @@RoleId int	

SELECT @@RoleId = b.ROLE_ID
FROM USER_PROFILE a LEFT JOIN USER_ROLE b ON a.NETWORK_ID = b.NETWORK_ID AND a.APPLICATION = b.APPLICATION
WHERE (ISNULL(@Username, '') = '' OR (@Username IS NOT NULL AND a.NETWORK_ID = @Username))
	AND a.APPLICATION = @Application

SELECT Id,
		NAME AS Name,
		Description,
		Remark,
		a.Application, 
		PARENT_ID AS ParentId,
		Url
FROM Menu a inner join ROLE_MENU b on a.id = b.MENU_ID AND a.APPLICATION = b.APPLICATION
WHERE (b.role_id = @@RoleId)
	AND a.APPLICATION = @Application