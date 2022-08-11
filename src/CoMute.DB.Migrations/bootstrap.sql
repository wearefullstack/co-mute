if not exists (select name from master..sysdatabases where name = 'CoMute')
begin
create database CoMute
end;
GO
