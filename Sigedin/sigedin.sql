-- tem que definir um nome para MULTI PROJETO, MULTI EMPRESA, FILIAL/SEDE, um nome que contemple todas estas possibilidades
-- trocar cd_projeto por este nome
use master
go


if object_id('mm') is not null
	drop function mm
go

create function mm(@s as varchar(1000)) returns varchar(1000) as
begin
	return upper(substring(@s, 1, 1)) + replace(substring(@s, 2, len(@s) -1), '_', ' ')
end
go




if object_id('arrumar_xml') is not null
	drop function arrumar_xml
go

create function arrumar_xml(@texto varchar(max)) returns varchar(max)
begin
	declare @ret varchar(max)
	
	set @ret = @texto
	set @ret =  replace(@ret, '&#x0d;', char(13))
	set @ret =  replace(@ret, '&gt;', '>')
	set @ret =  replace(@ret, '&lt;', '<')
	
	return @ret	
end
go



if object_id('amigavel') is not null
	drop function amigavel
go

create function amigavel(@s as varchar(1000)) returns varchar(1000) as
begin
	declare @ret varchar(1000)

	set @ret = 
	case 
		when (substring(@s, 3, 1) = '_') and (len(@s) > 4) then upper(substring(@s, 4, 1)) + replace(substring(@s, 5, len(@s)-4), '_', ' ')
		else master.dbo.mm(replace(@s, '_', ' '))
	end

	set @ret = replace(@ret, 'preco', 'preço')

	return @ret
end
go



if object_id('nome_amigavel') is not null
	drop function nome_amigavel
go

create function nome_amigavel(@coluna sysname, @tipo sysname, @identity bit) returns sysname as
begin
	return case 
		--codigo
		when @identity = 1 then 'Código ' + master.dbo.amigavel(@coluna)
		
		when (substring(@coluna, 1, 3) = 'co_') then 'Código ' + master.dbo.amigavel(@coluna)

		--email
		when (substring(@coluna, 1, 3) = 'em_') then 'E-mail ' + master.dbo.amigavel(@coluna)
		when @coluna = 'ed_eletronico' then 'E-mail'
		when @coluna = 'ed_email' then 'E-mail'
		when @coluna like 'ed_eletronico_%' then master.dbo.mm('E-mail' + substring(@coluna, 14, len(@coluna) - 13))
		when @coluna like 'ed_email_%' then master.dbo.mm('E-mail' + substring(@coluna, 8, len(@coluna) - 7))
		
		--uf
		when @coluna = 'ed_uf' then upper('uf')
		
		--cnpj
		when @coluna = 'dc_cnpj' then upper('cnpj')
		
		--cpf
		when @coluna = 'dc_cpf' then upper('cpf')

		--rg
		when @coluna = 'dc_rg' then upper('RG')

		--identificacao
		when (substring(@coluna, 1, 3) = 'id_') then 'Id ' + master.dbo.amigavel(@coluna)		
				
		--valor monetario
		when (substring(@coluna, 1, 3) = 'vm_') then 'Valor (R$) ' + master.dbo.amigavel(@coluna)
						
		--telefone
		when (substring(@coluna, 1, 3) = 'te_') and (@tipo in ('char', 'varchar')) then 'Tel ' + master.dbo.amigavel(@coluna)
		
		--nome
		when (substring(@coluna, 1, 3) = 'nm_') and (@tipo = 'varchar') then 'Nome ' + master.dbo.amigavel(@coluna)
		
		--número int
		when (substring(@coluna, 1, 3) = 'nr_') and (@tipo = 'int') then 'Nº ' + master.dbo.amigavel(@coluna)

		--número varchar, char
		when (substring(@coluna, 1, 3) = 'nr_') and (@tipo in ('varchar', 'char')) then 'Nº ' + master.dbo.amigavel(@coluna)

		--descrição
		when (substring(@coluna, 1, 3) = 'ds_') and (@tipo = 'varchar') then 'Descrição ' + master.dbo.amigavel(@coluna)
										
		--texto
		when (substring(@coluna, 1, 3) = 'tx_') and (@tipo = 'varchar') then 'Texto ' + master.dbo.amigavel(@coluna)
						
		--tipo
		when (substring(@coluna, 1, 3) = 'tp_') then 'Tipo de ' + master.dbo.amigavel(@coluna)
									
		--hora
		when (@tipo = 'time') then 'Hora de ' + master.dbo.amigavel(@coluna)
				
		--data
		when (@tipo in ('date', 'datetime')) then 'Data de ' + master.dbo.amigavel(@coluna)
		
		--qtd
		when ((@tipo = 'int') and (substring(@coluna, 1, 3) = 'qt_'))  then 'Qtd ' + master.dbo.amigavel(@coluna)
		
		--geral		
		else master.dbo.amigavel(@coluna)
	end	
end
go

















if OBJECT_ID('sp_parcol') is not null
	drop procedure sp_parcol
go

create procedure sp_parcol @banco sysname, @tabela sysname, @coluna sysname as
with a as
(
	select 
	ordem = 1
	, tabela
	, coluna
	, nome_amigavel
	, tamanho_maximo
	
	, grd_controle 
	, grd_campo_ligacao
	, grd_largura_controle

	, cb_source
	, cb_display
	, cb_value

	from master..sigedin_coluna 

	where banco = @banco
	
	and tabela = @tabela 

	and coluna = @coluna
	
	and grd_controle <> 'DataGridViewComboBoxColumn'

	union

	select
	ordem = 2
	, tabela = @tabela
	, coluna = @coluna
	, nome_amigavel = master.dbo.nome_amigavel(@coluna, 'varchar', 0)
	, tamanho_maximo = 80
	
	, grd_controle = 'DataGridViewTextBoxColumn'
	, grd_campo_ligacao = 'Text'
	, grd_largura_controle = 200

	, cb_source = null
	, cb_display = null
	, cb_value = null
)
select top 1 * from a order by ordem

go


EXEC sys.sp_MS_marksystemobject sp_parcol






























--PARA SER USADO NO BANCO DE DADOS DE DESTINO

--if object_id('extra_coluna') is null
--create table [dbo].[extra_coluna](
--	[tabela] [sysname] not null,
--	[coluna] [sysname] not null,
--	[extra] [varchar](100) not null,
--	[valor] [sql_variant] null,
-- constraint [pk_extra_coluna] primary key clustered 
--(
--	[tabela] asc,
--	[coluna] asc,
--	[extra] asc
--)with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on) on [primary]
--) on [primary]

--go




--if object_id('extra_tabela') is null
--create table [dbo].[extra_tabela](
--	[tabela] [sysname] not null,
--	[extra] [varchar](100) null,
--	[valor] [sql_variant] null,
-- constraint [pk_extra_tabela] primary key clustered 
--(
--	[tabela] asc
--)with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on) on [primary]
--) on [primary]

--go





if object_id('sigedin_projeto') is not null
	drop table sigedin_projeto
go


create table sigedin_projeto
(
	banco sysname not null,
	pasta_raiz varchar(1000) null,
	
	primary key clustered 
	(
		banco asc
	)
)
go

--truncate table sigedin_projeto

insert into master..sigedin_projeto
select name, 'Z:\' + name from sys.databases where name not in ('master', 'tempdb', 'model', 'msdb', 'ReportServer', 'ReportServerTempDB') and name not like '%[_]%'




if object_id('sigedin_tabela') is not null
	drop table sigedin_tabela
go

if object_id('sigedin_tabela') is null
create table sigedin_tabela
(
	banco sysname not null,
	tabela sysname not null,
	nome_cadastro varchar(50) null, 
	sub_pasta varchar(50) null, 
	tipo_cad char(3) null, 
	tabela_master nvarchar(128) null, 
	fk_name nvarchar(128) null,
	sub_ordem int null, 
	
	 constraint pk_sigedin_tabela primary key clustered 
	(
		banco asc
		, tabela asc
	)
)
go



if object_id('sigedin_coluna') is not null
	drop table sigedin_coluna
go

create table sigedin_coluna
(
	banco sysname not null,
	tabela sysname not null,
	coluna sysname not null,
	no_ordem int not null,
	nome_amigavel varchar(50) null,
	tamanho_maximo int null,
	identificador bit null,
	chave_primaria bit null,
	numeracao_automatica bit null,
	tipo_dado nvarchar(128) null,
	permite_nulo bit null,
	cad_controle varchar(50) null,
	cad_campo_ligacao varchar(50) null,
	cad_largura_controle int null,
	grd_controle varchar(50) null,
	grd_campo_ligacao varchar(50) null,
	grd_largura_controle int null,
	cb_table nvarchar(128) null,
	cb_source nvarchar(128) null,
	cb_param_name nvarchar(128) null,
	cb_display nvarchar(128) null,
	cb_value nvarchar(128) null,
	cb_nullable bit null,
	
	 constraint pk_sigedin_coluna primary key clustered 
	(
		banco asc,
		tabela asc,
		coluna asc
	)
)
go




if object_id('sigedin_params') is not null
	drop table sigedin_params
go

create table sigedin_params
(
	banco sysname not null,
	procedimento sysname not null,
	parametro sysname not null,
	tipo_dado sysname not null,
	parameter_id int not null,
	nome_amigavel varchar(50) null,
	tamanho_maximo int null,
	cad_controle varchar(50) null,
	cad_campo_ligacao varchar(50) null,
	cad_largura_controle int null,
	cb_table nvarchar(128) null,
	cb_source nvarchar(128) null,
	cb_display nvarchar(128) null,
	cb_value nvarchar(128) null,
	cb_nullable bit null,
	
	 constraint pk_sigedin_params primary key clustered 
	(
		banco asc,
		procedimento asc,
		parametro asc
	)
)
go
























if object_id('sigedin_controle') is not null
	drop table sigedin_controle
go

create table sigedin_controle
(
	tp_controle char(2) not null,
	nm_controle varchar(50) null,
	cad_controle varchar(50) null,
	cad_campo_ligacao varchar(50) null,
	grd_controle varchar(50) null,
	grd_campo_ligacao varchar(50) null,
	
	 constraint pk_sigedin_controle primary key clustered 
	(
		tp_controle asc
	)
)
go

; with controle as
(
	select tp_controle = 'ck', nm_controle = 'checkbox', cad_controle = 'checkbox', cad_campo_ligacao = 'checkstate', grd_controle = 'datagridviewcheckboxcolumn', grd_campo_ligacao = 'checkstate'
	union
	select tp_controle = 'cb', nm_controle  = 'combobox', cad_controle = 'combobox', cad_campo_ligacao = 'selectedvalue', grd_controle = 'datagridviewcomboboxcolumn', grd_campo_ligacao = 'selectedvalue'
	union
	select tp_controle = 'dt', nm_controle  = 'data somente', cad_controle = 'frgdatetimepicker', cad_campo_ligacao = 'value', grd_controle = 'datagridviewtextboxcolumn', grd_campo_ligacao = 'text'
	union
	select tp_controle = 'hr', nm_controle  = 'hora somente', cad_controle = 'textbox', cad_campo_ligacao = 'text', grd_controle = 'datagridviewtextboxcolumn', grd_campo_ligacao = 'text'
	union
	select tp_controle = 'dh', nm_controle  = 'data + hora', cad_controle = 'textbox', cad_campo_ligacao = 'text', grd_controle = 'datagridviewtextboxcolumn', grd_campo_ligacao = 'text'
	union
	select tp_controle = 'nr', nm_controle  = 'nº textbox', cad_controle = 'textbox', cad_campo_ligacao = 'text', grd_controle = 'datagridviewtextboxcolumn', grd_campo_ligacao = 'text'
	union
	select tp_controle = 'no', nm_controle  = 'nº updown', cad_controle = 'numericupdown', cad_campo_ligacao = 'value', grd_controle = 'datagridviewtextboxcolumn', grd_campo_ligacao = 'text'
	union
	select tp_controle = 'tb', nm_controle  = 'textbox', cad_controle = 'textbox', cad_campo_ligacao = 'text', grd_controle = 'datagridviewtextboxcolumn', grd_campo_ligacao = 'text'
	union
	select tp_controle = 'te', nm_controle  = 'telefone', cad_controle = 'frgmaskedtelefone', cad_campo_ligacao = 'text', grd_controle = 'datagridviewtextboxcolumn', grd_campo_ligacao = 'text'	
) 
insert into master..sigedin_controle
(
	tp_controle
	, nm_controle
	, cad_controle
	, cad_campo_ligacao
	, grd_controle
	, grd_campo_ligacao
)
select 
	tp_controle
	, nm_controle
	, cad_controle
	, cad_campo_ligacao
	, grd_controle
	, grd_campo_ligacao	
from controle




if object_id('sigedin_prefixo') is not null
	drop table sigedin_prefixo
go

create table sigedin_prefixo
(
	tp_prefixo char(2) not null,
	nm_prefixo varchar(50) null,
	tp_controle char(2) null,
	
	 constraint pk_sigedin_prefixo primary key clustered 
	(
		tp_prefixo asc
	)
)
go

; with prefixo as
(
	select tp_prefixo = 'cd', nm_prefixo = 'codigo', tp_controle = 'tb'
	union
	select tp_prefixo = 'co', nm_prefixo = 'codigo', tp_controle = 'tb'
	union	
	select tp_prefixo = 'dc', nm_prefixo  = 'documento', tp_controle = 'tb'
	union
	select tp_prefixo = 'dh', nm_prefixo  = 'data + tp_controle', tp_controle = 'dh'
	union
	select tp_prefixo = 'ds', nm_prefixo  = 'descrição', tp_controle = 'tb'
	union
	select tp_prefixo = 'dt', nm_prefixo  = 'data', tp_controle = 'dt'
	union
	select tp_prefixo = 'ed', nm_prefixo  = 'endereço', tp_controle = 'tb'
	union
	select tp_prefixo = 'hr', nm_prefixo  = 'hora', tp_controle = 'hr'
	union
	select tp_prefixo = 'lg', nm_prefixo  = 'lógico sim/nãp', tp_controle = 'ck'
	union
	select tp_prefixo = 'nm', nm_prefixo  = 'nome', tp_controle = 'tb'
	union
	select tp_prefixo = 'nr', nm_prefixo  = 'número textbox', tp_controle = 'nr'
	union
	select tp_prefixo = 'no', nm_prefixo  = 'número numericupdown', tp_controle = 'no'
	union
	select tp_prefixo = 'pe', nm_prefixo  = 'peso', tp_controle = 'tb'
	union
	select tp_prefixo = 'ph', nm_prefixo  = 'path', tp_controle = 'tb'
	union
	select tp_prefixo = 'qt', nm_prefixo  = 'quantidade', tp_controle = 'tb'
	union
	select tp_prefixo = 'sg', nm_prefixo  = 'sigla', tp_controle = 'tb'
	union
	select tp_prefixo = 'te', nm_prefixo  = 'telefone', tp_controle = 'te'
	union
	select tp_prefixo = 'tp', nm_prefixo  = 'tipo', tp_controle = 'cb'
	union
	select tp_prefixo = 'tx', nm_prefixo  = 'texto', tp_controle = 'tb'
	union
	select tp_prefixo = 'vl', nm_prefixo  = 'valor', tp_controle = 'tb'
	union
	select tp_prefixo = 'vm', nm_prefixo  = 'valor monetário', tp_controle = 'tb'	
)
insert into master..sigedin_prefixo
(
	tp_prefixo
	, nm_prefixo
	, tp_controle
)
select 
	tp_prefixo
	, nm_prefixo
	, tp_controle
from prefixo
























-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N
-- S I G E D I N


if object_id('sp_sigedin') is not null
	drop procedure sp_sigedin
go

create procedure sp_sigedin as
begin

print ''
print ''
print 'Tabelas'
delete master..sigedin_tabela where banco = db_name()
; with tm as
(
	select 
		tabela = p.name
		, tabela_master = t.name	
		, ordem = row_number() over(partition by t.name order by p.create_date)
		, fk_name = fn.name

	from sys.foreign_key_columns fk

	join sys.foreign_keys fn on fn.object_id = fk.constraint_object_id

	join sys.tables as p on p.object_id = fk.parent_object_id
	join sys.columns as pc on pc.object_id = fk.parent_object_id 
	and pc.column_id = fk.parent_column_id

	join sys.tables as t on t.object_id = fk.referenced_object_id
	join sys.columns as tc on tc.object_id = fk.referenced_object_id 
	and tc.column_id = fk.referenced_column_id 

	where pc.name like 'cd_%'

	and p.name like t.name + '_%'

	and pc.name <> 'cd_projeto'
)
insert into master..sigedin_tabela
(
	banco
	, tabela
	, nome_cadastro
	, sub_pasta
	, tipo_cad
	, tabela_master
	, fk_name
	, sub_ordem
)
select 
	banco = db_name()
	, tabela = t.name
	, nome_cadastro = master.dbo.mm(t.name)
	, sub_pasta = coalesce(convert(varchar, esub.valor), 'Cadastros')
	, tipo_cad = coalesce(convert(varchar, ecad.valor), case when tm.tabela is null then 'cad' else 'sub' end)
	, tabela_master = tm.tabela_master
	, tm.fk_name
	, sub_ordem = coalesce(eordem.valor, tm.ordem)

from sys.tables t

left join extra_tabela esub on esub.tabela = t.name
and esub.extra = 'sub_pasta'

left join extra_tabela ecad on ecad.tabela = t.name
and ecad.extra = 'tipo_cad'

left join extra_tabela eordem on eordem.tabela = t.name
and eordem.extra = 'ordem'

left join tm on tm.tabela = t.name

where t.name not like 'sigedin_%'

and t.name not in (select tabela from extra_tabela where extra = 'excluir')









print ''
print ''
delete master..sigedin_coluna where banco = db_name()
print 'Colunas'
; with ix as 
(
	select 
		ic.object_id
		, ic.column_id
		
		, tabela = t.name
		, coluna = c.name
					
		, i.is_primary_key
		, c.is_identity

	from sys.tables t

	join sys.columns c on c.object_id = t.object_id

	join sys.index_columns ic on ic.object_id = c.object_id
	and ic.column_id = c.column_id
			
	join sys.indexes i on i.object_id = ic.object_id
	and i.index_id = ic.index_id

	and t.name not like 'sigedin%'
)
, pk as
(
	select * from ix where is_primary_key = 1
)
, id as
(
	-- tabelas que contem somente uma chave primaria
	select pk.*, identificador = convert(bit, 1) from pk where object_id in (select object_id from pk group by object_id having count(*) = 1)

	union

	-- tabelas que contenham mais de uma chave primaria mas contem um coluna identity
	select pk.*, identificador = convert(bit, 1) from pk where object_id in (select object_id from pk group by object_id having count(*) > 1)
	and is_identity = 1 and is_primary_key = 1

	union

	-- tabela que contenham duas chaves primarias, pegar a segunda chave que não seja identity
	select pk.*, identificador = convert(bit, 1) from pk where object_id in (select object_id from pk group by object_id having count(*) = 2) 
	and is_identity = 0 and column_id = 2
)
, cb_cd as 
(
	-- combobox de códigos de outras entidades, não de tipos
	-- exemplo, cd_empresa, cd_funcionario
	select 
		  tb.object_id
		, cb.column_id
		, tabela = tb.name
		, coluna = cb.name
		
		, cb_table = tc.name
		, cb_source = 
		case 
			--quando a chave estrangeira tiver nome diferenete da chave primaria
			--cb.name = chave estrangeira
			--cc.name = cha primaria na outra tabela
			--por exemplo, no projeto da eye, na instrução temos o cd_indireto que aponta para o cd_cliente: o nome do procedimento que atenderá a esta requisição será cb_indireto
			when cb.name  <> + cc.name then 
				'cb_' + substring(cb.name, 4, len(cb.name) - 3) + '_' + tb.name
			else
				'cb_' + tc.name + '_' + tb.name
		end
		, cb_param_name = 
			case 
				--Se for do tipo sub, não incluir o não da sub tabela, buscar nome da tabela pai.
				when (select tipo_cad from master..sigedin_tabela where tabela = tb.name) = 'sub' then
					'cd_' + (select tabela_master from master..sigedin_tabela where tabela = tb.name)
				else
					'cd_' + tb.name
			end
			--case 
			--	--Se for do tipo sub, não incluir o não da sub tabela, buscar nome da tabela pai.
			--	when (select tipo_cad from master..sigedin_tabela where tabela COLLATE Latin1_General_CI_AI  = tb.name COLLATE Latin1_General_CI_AI ) = 'sub' then
			--		'cd_' + (select tabela_master from master..sigedin_tabela where tabela COLLATE Latin1_General_CI_AI  = tb.name COLLATE Latin1_General_CI_AI )
			--	else
			--		'cd_' + tb.name
			--end	
		, cb_display = ds.name	
		, cb_value = cc.name
		, cb_nullable = cb.is_nullable
	  
	from sys.foreign_key_columns fk

	-- tabela que tem a chave estrangeira
	join sys.tables as tb on tb.object_id = fk.parent_object_id
	join sys.columns as cb on cb.object_id = fk.parent_object_id 
	and cb.column_id = fk.parent_column_id

	-- tabela referenciada
	join sys.tables as tc on tc.object_id = fk.referenced_object_id
	join sys.columns as cc on cc.object_id = fk.referenced_object_id 
	and cc.column_id = fk.referenced_column_id 

	-- cross = join
	-- outer = left join
	cross apply
	(
		select top 1 name
		
		from sys.columns
		
		where object_id = fk.referenced_object_id 
		
		and column_id > fk.referenced_column_id 
		
		and name in ('nm_' + substring(cc.name, 4, len(cc.name) - 3), 'ds_' +  substring(cc.name, 4, len(cc.name) - 3))
				
		order by column_id
	) ds
	
	left join pk on pk.object_id = cb.object_id
	and pk.column_id = cb.column_id
	
	where tc.name not like '%_tipo'

	and coalesce(pk.is_primary_key, 0) = 0

	-- remover tabelas filhas como cliente > cliente_solicitante
	and tb.name not like '%' + tc.name + '%'
)
, cb_tipo as 
(
	-- combobox de tipo de entidades, 
	-- tabela: entidade_tipo, tipo de instrução, tipo de carro, tipo de funcionario
	-- instrucao.tp_instrucao >> instrucao_tipo.tp_instrucao
	select 
		  tb.object_id
		, cb.column_id
		, tabela = tb.name
		, coluna = cb.name
		
		, cb_table = tc.name
		, cb_source = 'cb_' + tc.name + '_' + tb.name
		, cb_param_name = 'cd_' + tb.name
		, cb_display = 'ds_tipo'	
		, cb_value = cc.name
		, cb_nullable = cb.is_nullable
	  
	from sys.foreign_key_columns fk

	join sys.tables as tb on tb.object_id = fk.parent_object_id
	join sys.columns as cb on cb.object_id = fk.parent_object_id 
	and cb.column_id = fk.parent_column_id

	join sys.tables as tc on tc.object_id = fk.referenced_object_id
	join sys.columns as cc on cc.object_id = fk.referenced_object_id 
	and cc.column_id = fk.referenced_column_id 
	
	left join ix on ix.object_id = cb.object_id
	and ix.column_id = cb.column_id
	
	where tc.name like '%_tipo'
		
	and coalesce(ix.is_primary_key, 0) = 0
)
, cb as 
(
	select * 
	
	from cb_cd
	
	union
	
	select t.* 
	
	from cb_tipo t
	
	-- previnir que selecione duas CB SOURCE para a mesma coluna.
	left join cb_cd c on c.object_id = t.object_id
	and c.column_id = t.column_id
	
	where c.cb_source is null	
)
, extra_tp_controle as 
(
	select
		  e.tabela
		, e.coluna
		, tp_controle = convert(varchar, e.valor)

	from extra_coluna e

	where extra = 'tp_controle'
)
, extra_cad_largura_controle as 
(
	select
		  e.tabela
		, e.coluna
		, cad_largura_controle = convert(varchar, e.valor)

	from extra_coluna e

	where extra = 'cad_largura_controle'
)
, extra_grd_largura_controle as 
(
	select
		  e.tabela
		, e.coluna
		, grd_largura_controle = convert(varchar, e.valor)

	from extra_coluna e

	where extra = 'grd_largura_controle'
)
insert into master..sigedin_coluna
(
	banco
	, tabela
	, coluna
	, no_ordem
	, nome_amigavel
	
	, tamanho_maximo 
	, identificador
	, chave_primaria 
	, numeracao_automatica
	, tipo_dado
	, permite_nulo 
			
	, cad_controle 
	, cad_campo_ligacao 
	, cad_largura_controle 

	, grd_controle 
	, grd_campo_ligacao 
	, grd_largura_controle 

	, cb_table
	, cb_source
	, cb_param_name
	, cb_display 
	, cb_value 
	, cb_nullable 
)

select 
	db_name()
	, tabela = t.name
	, coluna = c.name
	, no_ordem = c.column_id
	, nome_amigavel = master.dbo.nome_amigavel(c.name, st.name, c.is_identity)
			
	, tamanho_maximo = case when c.system_type_id = 56 then 9 else c.max_length end
	, identificador = id.identificador
	, chave_primaria = pk.is_primary_key
	, numeracao_automatica = c.is_identity
	, tipo_dado = st.name
	, permite_nulo = c.is_nullable
	
	, cad_controle = case 	
		--controle prioritario: column description
		when (pc.tp_controle is not null) then pc.cad_controle

		when (c.name in ('dc_cpf', 'cpf')) then 'CpfMascara'
	
		--codigo de outras tabelas, combobox
		when (coalesce(pk.is_primary_key, 0) = 0) and (substring(c.name, 1, 3) = 'cd_') then 'combobox'
				
		--tipo sem combobox, char(1)
		when (st.name = 'char') and (c.max_length = 1) and (substring(c.name, 1, 3) = 'tp_') then 'textbox'
		
		--outros
		else coalesce(cc.cad_controle, 'textbox')
	end
	
	, cad_campo_ligacao = case 
		--controle prioritario: column description
		when (pc.tp_controle is not null) then pc.cad_campo_ligacao
	
		--codigo de outras tabelas, combobox
		when (coalesce(pk.is_primary_key, 0) = 0) and (substring(c.name, 1, 3) = 'cd_') then 'selectedvalue'
				
		--tipo sem combobox, char(1)
		when (st.name = 'char') and (c.max_length = 1) and (substring(c.name, 1, 3) = 'tp_') then 'text'
		
		--outros
		else coalesce(cc.cad_campo_ligacao, 'text')
	end
		
	, cad_largura_controle = case 
		when (eclc.cad_largura_controle is not null) then eclc.cad_largura_controle

		--varchar(max)
		when c.max_length = -1 then 800
		
		--codigo
		when(substring(c.name, 1, 3) = 'cd_') and (coalesce(pk.is_primary_key, 0) = 1) then 50

		--codigo
		when(substring(c.name, 1, 3) = 'co_') and (coalesce(pk.is_primary_key, 0) = 1) then 200
		
		--combobox fk
		when (substring(c.name, 1, 3) = 'cd_') and (coalesce(pk.is_primary_key, 0) = 0) then 300			

		--tipo fk
		when (substring(c.name, 1, 3) = 'tp_') and (st.name = 'int') and (t.name like '%tipo') then 300

		--tipo fk
		when (substring(c.name, 1, 3) = 'tp_') and (st.name = 'int') and (c.name like 'tp%') then 300
				
		--qtd
		when (st.name = 'int') then 50
		
		--hora
		when (st.name = 'time') then 100
		
		--data
		when (st.name = 'date') then 100		
		
		--datetime 
		when (st.name = 'datetime') then 200		
		
		--text em geral 8px por caracter + 20px de espaço
		when ((c.max_length * 8) + 20) < 50 then 50 	
		when ((c.max_length * 8) + 20) > 500 then 500
		else ((c.max_length * 8) + 20) 
	
	end 

	, grd_controle = case 
		--controle prioritario: column description
		when (pc.tp_controle is not null) then pc.grd_controle	
	
		--codigo de outras tabelas, combobox
		when (coalesce(pk.is_primary_key, 0) = 0) and (substring(c.name, 1, 3) = 'cd_') then 'datagridviewcomboboxcolumn'
				
		--tipo sem combobox, char(1)
		when (st.name = 'char') and (c.max_length = 1) and (substring(c.name, 1, 3) = 'tp_') then 'datagridviewtextboxcolumn'
		
		--outros
		else coalesce(cc.grd_controle, 'datagridviewtextboxcolumn')
	end
	
	, grd_campo_ligacao = case 
		--controle prioritario: column description
		when (pc.tp_controle is not null) then pc.grd_campo_ligacao		
	
		--codigo de outras tabelas, combobox
		when (coalesce(pk.is_primary_key, 0) = 0) and (substring(c.name, 1, 3) = 'cd_') then 'selectedvalue'
				
		--tipo sem combobox, char(1)
		when (st.name = 'char') and (c.max_length = 1) and (substring(c.name, 1, 3) = 'tp_') then 'text'
		
		--outros
		else coalesce(cc.grd_campo_ligacao, 'text')
	end
		
	, grd_largura_controle = case 
		when (eglc.grd_largura_controle is not null) then eglc.grd_largura_controle

		--varchar(max)
		when c.max_length = -1 then 400
		
		--codigo
		when(substring(c.name, 1, 3) = 'cd_') and (coalesce(pk.is_primary_key, 0) = 1) then 100
		
		--codigo
		when(substring(c.name, 1, 3) = 'co_') and (coalesce(pk.is_primary_key, 0) = 1) then 100

		--combobox fk
		when (substring(c.name, 1, 3) = 'cd_') and (coalesce(pk.is_primary_key, 0) = 0) then 150			
		
		--qtd
		when (st.name = 'int') and (len(c.name) > 10) then len(c.name) * 8
		when (st.name = 'int') then 50
		
		--hora
		when (st.name = 'time') then 100
		
		--data
		when (st.name = 'date') then 100		
		
		--datetime 
		when (st.name = 'datetime') then 200		
		
		--text em geral 8px por caracter + 20px de espaço
		when ((c.max_length * 8) + 20) < 50 then 50 	
		when ((c.max_length * 8) + 20) > 300 then 300
		else ((c.max_length * 8) + 20) 
	
	end 
	
	, cb.cb_table
	, cb.cb_source
	, cb_param_name
	, cb.cb_display 
	, cb.cb_value 
	, cb.cb_nullable 	

from sys.tables t

join sys.columns c on c.object_id = t.object_id

join sys.types st on st.user_type_id = c.user_type_id

left join id on id.object_id = t.object_id
and id.column_id = c.column_id

left join pk on pk.object_id = c.object_id
and pk.column_id = c.column_id

left join extra_tp_controle etc on etc.tabela = t.name
and etc.coluna = c.name

left join extra_cad_largura_controle eclc on eclc.tabela = t.name
and eclc.coluna = c.name

left join extra_grd_largura_controle eglc on eglc.tabela = t.name
and eglc.coluna = c.name

left join master..sigedin_controle pc on pc.tp_controle = etc.tp_controle

left join master..sigedin_prefixo pr on pr.tp_prefixo = substring(c.name, 1, 2)
--para ser prefixo te que ter um caracter _ na terceira posição
--do contrário um campo chamado TEmpo_de_almoco por exemplo seria considerado como TElefone
and substring(c.name, 3, 1) = '_'

left join master..sigedin_controle cc on cc.tp_controle = pr.tp_controle

left join cb on cb.object_id = c.object_id
and cb.column_id = c.column_id

left join extra_coluna excluir on excluir.tabela = t.name
and excluir.coluna = c.name
and excluir.extra = 'excluir'

where t.is_ms_shipped = 0

and t.name not like 'sigedin_%'

and c.name not in ('dt_registro', 'cd_usuario')

and c.is_computed = 0 

-- valor é TEXTO
and (excluir.extra is null)

order by t.name, c.column_id













print ''
print ''
print 'Parametros de Pesquisa'
delete master..sigedin_params where banco = db_name()
insert into master..sigedin_params
(
	banco
	, procedimento 
	, parametro
	, tipo_dado
	, parameter_id
	, nome_amigavel 
	, tamanho_maximo
	, cad_controle 
	, cad_campo_ligacao 
	, cad_largura_controle 
	, cb_table 
	, cb_source 
	, cb_display 
	, cb_value 
	, cb_nullable 
)
select 
	db_name()
	, procedimento = p.name
	, parametro = case when len(m.name) > 2 then substring(m.name, 2, len(m.name) - 1) end
	, tipo_dado = t.name
	, m.parameter_id
	
	, nome_amigavel = coalesce(c.nome_amigavel, master.dbo.nome_amigavel(substring(m.name, 2, len(m.name) -1), 'varchar', 0))

	, tamanho_maximo = 
			case 
				when c.tamanho_maximo is not null then c.tamanho_maximo
				when case when len(m.name) >= 4 then substring(m.name, 2, 2) end = 'lg' then 2
				else 80
			end

	, cad_controle = 
			case 
				when c.cad_controle is not null then c.cad_controle
				when case when len(m.name) >= 4 then substring(m.name, 2, 2) end = 'lg' then 'CheckBox'
				else 'textbox'
			end

	, cad_campo_ligacao =
			case 
				when c.cad_campo_ligacao is not null then c.cad_campo_ligacao
				when case when len(m.name) >= 4 then substring(m.name, 2, 2) end = 'lg' then 'Checked'
				else 'text'
			end

	, cad_largura_controle =
			case 
				when c.cad_largura_controle is not null then c.cad_largura_controle
				when case when len(m.name) >= 4 then substring(m.name, 2, 2) end = 'lg' then 15
				else 200
			end

	, c.cb_table
	, c.cb_source
	, c.cb_display
	, c.cb_value
	, c.cb_nullable

from sys.parameters m

join sys.procedures p on p.object_id = m.object_id

join sys.types t on t.user_type_id = m.user_type_id

left join master..sigedin_coluna c on c.tabela = case when len(p.name) > 4 then substring(p.name, 4, len(p.name) - 3) end
and c.coluna = case when len(m.name) > 2 then substring(m.name, 2, len(m.name) - 1) end
and c.banco = db_name()

-- must set that procedure as system object
-- http://weblogs.sqlteam.com/mladenp/archive/2007/01/18/58287.aspx
-- EXEC sys.sp_MS_marksystemobject sp_sigedin

end
go

EXEC sys.sp_MS_marksystemobject sp_sigedin


