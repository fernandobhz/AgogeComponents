DECLARE @sql varchar(8000), @nome_tabela sysname, @nome_chave sysname
SET NOCOUNT ON


SELECT @nome_tabela = MIN(t.name) 
FROM sys.tables t 
where t.is_ms_shipped = 0 
and t.name <> 'alteracoes' 
-- NOT IN 
and t.name NOT in ( select t.name from sys.tables t join sys.columns c on c.object_id = t.object_id where c.name = 'cd_usuario' and t.name <> 'usuario' )
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
-----------------' + @nome_tabela + '------------------------------
alter table ' + @nome_tabela + ' add
	cd_usuario int NULL,
	dt_registro datetime NULL
GO
ALTER TABLE dbo.' + @nome_tabela + ' ADD CONSTRAINT
	DF_' + @nome_tabela + '_dt_registro DEFAULT getdate() FOR dt_registro
GO
ALTER TABLE dbo.' + @nome_tabela + ' ADD CONSTRAINT
	FK_' + @nome_tabela + '_usuario FOREIGN KEY
	(
	cd_usuario
	) REFERENCES dbo.usuario
	(
	cd_usuario
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO




'

	print @sql
 
	SELECT @nome_tabela = MIN(t.name) 
	FROM sys.tables t 
	where t.is_ms_shipped = 0 
	and t.name <> 'alteracoes' 
	--- NOT IN
	and t.name NOT in ( select t.name from sys.tables t join sys.columns c on c.object_id = t.object_id where c.name = 'cd_usuario' and t.name <> 'usuario' )
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
