declare @uu int=0,@ss int =@gv_StoreID
declare @bt datetime='2000-01-01 00:00:00'

SET NOCOUNT ON;

declare @ud int,@pd int,@ut datetime,@t nvarchar(max),@un nvarchar(max),@st nvarchar(max);
declare @new bit=0;

declare uc cursor for
select UserID,UserName from PE_Users where UserID=@uu or @uu=0;
declare pc cursor for 
select GeneralID,UpdateTime,Title from PE_CommonModel where ItemID in 
(select ProductID from PE_CommonProduct where StoreID=@ss or @ss=0) and InputTime >=@bt and Status=99;
open uc;
open pc;
fetch next from uc into @ud,@un;
while @@FETCH_STATUS = 0
begin
	fetch next from pc into @pd,@ut,@t;
	while @@FETCH_STATUS = 0
	begin
		set @st='';
		if exists (select 1 from PE_ProductControl where UserID=@ud and ProductID=@pd)
		begin
			set @st='允许(跳过)';
		end
		else
		begin
			if exists (select 1 from PE_ProductControlLog where ProductID=@pd)
			begin
				if (select LastControlTime from PE_ProductControlLog where ProductID=@pd)<=(select JoinTime From PE_Users where UserID=@ud)
				begin
					set @st='允许(新用户)';
					insert into PE_ProductControl values(@pd,@ud,10)
				end
				else
				begin
					set @st='禁止(跳过)';
				end
			end
			else
			begin
				set @st='新商品(自动允许)';
				set @new=1;
				insert into PE_ProductControl values(@pd,@ud,10) 
			end
		end
		print '正在控销用户'''+@un+'(UID:'+convert(nvarchar,@ud)+')'' 产品'''+@t+'(PID:'+convert(nvarchar,@pd)+')'' ... '+@st;
		if not exists (select 1 from PE_ProductControlLog where ProductID=@pd)
		begin
			if @new <> 1
			begin
				insert into PE_ProductControlLog(ProductID,LastControlTime) values(@pd,'2000-01-01');
			end
		end
		
		fetch next from pc into @pd,@ut,@t;
	end
	close pc;
	open pc;
	fetch next from uc into @ud,@un;
end

close uc;
close pc;
deallocate uc;
deallocate pc;

insert into PE_ProductControlLog(ProductID)
select GeneralID from PE_CommonModel where GeneralID not in (select distinct ProductID from PE_ProductControlLog) and ItemID in 
(select ProductID from PE_CommonProduct where StoreID=@ss or @ss=0) 
update PE_ProductControlLog set LastControlTime=getdate();

set NOCOUNT OFF
