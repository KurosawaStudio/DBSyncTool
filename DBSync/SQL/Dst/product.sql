--开始插入数据
declare @id int;
insert into PE_CommonModel_b(NodeID,ModelID,TableName,Title,Inputer,Hits,DayHits,WeekHits,MonthHits
,LinkType,UpdateTime,Status,EliteLevel,Priority,CommentAudited,CommentUnAudited,SigninType,
InputTime,PassedTime,LastHitTime,SGType,SGDataId)
values
(760,140,'PE_U_tongyong',@drugName,'Inputer',0,0,0,0,
0,getdate(),99,0,0,0,0,0,
Getdate(),getdate(),getdate(),0,0)
select @id=@@IDENTITY

update PE_CommonModel_b
set ItemID=GeneralID
where GeneralID=@id

insert into PE_CommonModel
select * from PE_CommonModel_b where GeneralID=@id
insert into PE_CommonProduct(ProductID,TableName,ProductName,ProductNum,ProductType,Unit,ServiceTermUnit,ServiceTerm,
Price,Price_Market,StoreID,Factory,BreedName,Batch,Barcode)
values
(@ID,'PE_U_tongyong',@drugName,@drugCode,0,@unit,0,0,
@price,@price,539,@factory,@approval,'',@barcode);

insert into PE_U_tongyong(ID,Instruction_book) values(@ID/*ID*/,'');

