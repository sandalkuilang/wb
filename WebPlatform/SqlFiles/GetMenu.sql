DECLARE @@RoleId int	

SELECT @@RoleId = b.ROLE_ID
FROM USER_PROFILE a LEFT JOIN USER_ROLE b ON a.USERNAME = b.USERNAME 
WHERE (ISNULL(@Username, '') = '' OR (@Username IS NOT NULL AND a.USERNAME = @Username))

SELECT Id,
		NAME AS Name,
		Description,
		Remark,
		PARENT_ID AS ParentId,
		Url
FROM Menu a inner join ROLE_MENU b on a.id = b.MENU_ID
WHERE (b.role_id = @@RoleId) 