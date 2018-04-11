-- 获取节点映射设置
 --declare @gv_StoreID nvarchar(50)=539,@ERPNodeIdentityName nvarchar(50)='化学药品-解热镇痛'
 declare @NodeName nvarchar(50),@SubNodeName nvarchar(50),@DrugType nvarchar(2),@ReturnType nvarchar(1),@DistributionType nvarchar(1)
 declare @NodeID int,@dt int,@rt int,@drt int;
 DECLARE @msg NVARCHAR(MAX)

 select @NodeName=NodeName,@SubNodeName=SubNodeName,@DrugType=DrugType,@ReturnType=ReturnType,@DistributionType=DistributionType from PE_ProductNodeMapping
 where ERPNodeIdentityName=@ERPNodeIdentityName and StoreID=@gv_StoreID;

 --select @NodeName,@SubNodeName,@DrugType,@ReturnType,@DistributionType
 select @drt=case ISNULL(@DrugType,'其他') when '中药' then 1 when '西药' then 2 else 0 end,
 @rt= case ISNULL(@ReturnType,'否') when '是' then 1 else 0 end,
 @dt= case ISNULL(@DistributionType,'否') when '是' then 1 else 0 end


 if @NodeName is null and @SubNodeName is null
 begin
	--print ('未指定节点映射，自动跳过')
	SET @msg='FAIL 未指定节点映射，自动跳过 ' + @drugName+' '+@ERPNodeIdentityName
	raiserror(@msg,18,1)
	return
 end
 else
 begin
	if not exists(select 1 from PE_Nodes where NodeName = @NodeName)
	begin
		--print ('未找到大类节点，自动跳过')
		SET @msg='FAIL 未指定大类节点，自动跳过 ' + @drugName + ' '+@NodeName
		raiserror(@msg,18,1)
		return
	end
	else if(select TOP 1 ParentID from PE_Nodes where NodeName = @NodeName) <>521
	begin
		--print ('非大类节点，自动跳过')
		SET @msg='FAIL 非大类节点，自动跳过 ' + @drugName
		raiserror(@msg,18,1)
		return
	end
	else
	begin
		set @NodeID=(select NodeID from PE_Nodes where NodeName = @NodeName and ParentID=521) --大类节点设置完成
		if exists(select 1 from PE_Nodes where NodeName=@SubNodeName and ParentID=@NodeID)
		begin
			set @NodeID=(select NodeID from PE_Nodes where NodeName=@SubNodeName and ParentID=@NodeID) --小类节点设置完成
		end
	end
 end
 

--开始插入数据
declare @id int;
IF EXISTS (SELECT 1 FROM PE_CommonProduct WHERE ProductNum=@drugCode)
BEGIN
	raiserror('SKIP 药品已存在，自动跳过',18,1)
	return
END

insert into PE_CommonModel_b(NodeID,ModelID,TableName,Title,Inputer,Hits,DayHits,WeekHits,MonthHits
,LinkType,UpdateTime,Status,EliteLevel,Priority,CommentAudited,CommentUnAudited,SigninType,
InputTime,PassedTime,LastHitTime,SGType,SGDataId)
values
(@NodeID,140,'PE_U_tongyong',@drugName,@gv_UserName,0,0,0,0,
0,getdate(),99,0,0,0,0,0,
Getdate(),getdate(),getdate(),0,0)
select @id=@@IDENTITY

update PE_CommonModel_b
set ItemID=GeneralID
where GeneralID=@id

insert into PE_CommonModel
select * from PE_CommonModel_b where GeneralID=@id
insert into PE_CommonProduct(ProductID,TableName,ProductName,ProductNum,ProductType,Unit,ServiceTermUnit,ServiceTerm,
Price,Price_Market,StoreID,Factory,BreedName,Batch,Barcode,DistributionType,ReturnType,DrugType)
values
(@ID,'PE_U_tongyong',@drugName,@drugCode,0,@unit,0,0,
@price,@price,539,@factory,@approval,'',@barcode,@dt,@rt,@drt);

insert into PE_U_tongyong(ID,Instruction_book) values(@ID/*ID*/,'');

