select [table],'ALTER TABLE '+[table]+' ALTER column '+column_name+' datetime2' actn from (
select schema_name(t.schema_id) + '.' + t.name as [table],
       c.column_id,
       c.name as column_name,
       type_name(user_type_id) as data_type,
       scale as second_scale
from sys.columns c
join sys.tables t
     on t.object_id = c.object_id
where type_name(user_type_id) in ( 'datetimeoffset', 
       'smalldatetime', 'datetime', 'time'))x
	   where [table] not in ('dbo.ActionRegister','dbo.Facility','dbo.FacilityManifest','dbo.MasterFacility','dbo.tmp_stg_Patients') 
order by [table],column_name