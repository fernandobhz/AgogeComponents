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
go

CREATE TRIGGER ' + @nome_tabela +'_log ON ' + @nome_tabela +' WITH ENCRYPTION FOR INSERT, UPDATE, DELETE AS 
begin
	set nocount on
	
	declare @tabela varchar(255)
	set @tabela = ''' + @nome_tabela +'''

	declare @operacao varchar(10)
	IF EXISTS (SELECT * FROM inserted) AND NOT EXISTS (SELECT * FROM deleted) 
	set @operacao = ''INSERIDO''
	
	IF EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted) 
	set @operacao = ''ATUALIZADO''
	
	IF NOT EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted) 
	set @operacao = ''DELETADO''
	
	if (@operacao = ''DELETADO'')
	BEGIN

		insert into alteracoes(usuario, usuario_codigo, tabela, chave, operacao, data, registro, cd_locacao)
		select
			coalesce(u.nm_login, ''?'')
			, coalesce(u.cd_usuario, -1)
			, tabela = @tabela
			, x.' + @nome_chave +'
			, operacao = @operacao
			, data = getdate()

			, registro =
			replace
			(
				(
					select *
					from deleted xs
					where xs.' + @nome_chave +' = x.' + @nome_chave +'
					for xml path('''')
				)
				, ''><'', ''>'' + char(13) + char(10) + ''<''
			)

			, cd_locacao = 
			convert(xml,
			(
				select *
				from deleted xs
				where xs.' + @nome_chave +' = x.' + @nome_chave +'
				for xml path('''')
			)).value(''(/cd_locacao//node())[1]'', ''int'')

		from deleted x

		left join usuario u on u.cd_usuario = x.cd_usuario 

	END
	ELSE
	BEGIN		
		
		insert into alteracoes(usuario, usuario_codigo, tabela, chave, operacao, data, registro, cd_locacao)
		select
			coalesce(u.nm_login, ''?'')
			, coalesce(u.cd_usuario, -1)
			, tabela = @tabela
			, x.' + @nome_chave +'
			, operacao = @operacao
			, data = getdate()

			, registro =
			replace
			(
				(
					select *
					from inserted xs
					where xs.' + @nome_chave +' = x.' + @nome_chave +'
					for xml path('''')
				)
				, ''><'', ''>'' + char(13) + char(10) + ''<''
			)

			, cd_locacao = 
			convert(xml,
			(
				select *
				from inserted xs
				where xs.' + @nome_chave +' = x.' + @nome_chave +'
				for xml path('''')
			)).value(''(/cd_locacao//node())[1]'', ''int'')

		from inserted x

		left join usuario u on u.cd_usuario = x.cd_usuario 

	END

end
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
