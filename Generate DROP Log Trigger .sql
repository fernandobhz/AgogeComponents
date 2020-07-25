DECLARE @sql varchar(8000), @nome_tabela sysname, @nome_chave sysname
SET NOCOUNT ON


SELECT @nome_tabela = MIN(t.name) 
FROM sys.tables t 
where t.is_ms_shipped = 0 
and t.name <> 'alteracoes' 
and t.name in ( select t.name from sys.tables t join sys.columns c on c.object_id = t.object_id where c.name = 'cd_usuario' and t.name <> 'usuario' )
and t.name not like 'extra_%'
and t.name not like 'backup_%'
and t.name not like 'teste_%'

	
select @nome_chave = c.name 
from sys.tables t 
join sys.columns c on c.object_id = t.object_id 
where t.name = @nome_tabela 
and c.column_id = 1




WHILE @nome_tabela is not null
 BEGIN
 

set @sql = '
if object_id(''' + @nome_tabela +'_log'') is not null
	drop trigger ' + @nome_tabela +'_log
GO
'

	print @sql
 
	SELECT @nome_tabela = MIN(t.name) 
	FROM sys.tables t 
	where t.is_ms_shipped = 0 
	and t.name <> 'alteracoes' 
	and t.name in ( select t.name from sys.tables t join sys.columns c on c.object_id = t.object_id where c.name = 'cd_usuario' and t.name <> 'usuario' )
	and t.name not like 'extra_%'
	and t.name not like 'backup_%'
	and t.name not like 'teste_%'

	--NESTA QUERY DEBAIXO TEM QUE TER ESSA CLAUSULA
	and t.name > @nome_tabela
	
	select @nome_chave = c.name 
	from sys.tables t 
	join sys.columns c on c.object_id = t.object_id 
	where t.name = @nome_tabela 
	and c.column_id = 1


	select @nome_chave = c.name from sys.tables t join sys.columns c on c.object_id = t.object_id where t.name = @nome_tabela and c.column_id = 1

  END
SET NOCOUNT OFF
