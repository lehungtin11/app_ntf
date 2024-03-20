CREATE DEFINER=`root`@`%` PROCEDURE `sys_get_ntf`()
BEGIN

	drop TABLE IF  EXISTS api.tmp_ntf;
	CREATE TEMPORARY TABLE IF NOT EXISTS api.tmp_ntf as
	select c.id, c.c_thoiGian, c.c_agent, c.c_fkKH, c.c_ghiChu as c_content
	from jwdb.app_fd_tls_calback c
	left join jwdb.app_fd_tls_ntf n on c.id = n.id
	where DATE_ADD(NOW(), INTERVAL 15 MINUTE)  STR_TO_DATE(c.c_thoiGian, '%d%m%Y %H%i') and n.id is null
	order by c.dateCreated desc
	;    
   
    insert into jwdb.app_fd_tls_ntf (id,dateCreated,c_time,c_agent,c_fkKH,c_content)
    select c.id,now(), c.c_thoiGian, c.c_agent,c.c_fkKH,c_content
	from api.tmp_ntf c
    ;
     
    select 
    JSON_ARRAYAGG(
				JSON_OBJECT(
					 'id', id
					 ,'thoiGian', c_thoiGian
					 ,'agent', c_agent
					 ,'fkKH', c_fkKH
					 ,'content', c_content
					 ,'type', 'callback'
		))  as rs 
	from api.tmp_ntf 
    ;
END