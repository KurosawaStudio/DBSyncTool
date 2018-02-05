select top 1
a.spbm as drugCode,a.spmc as drugName,a.ypgg as pack,a.sccj as factory,a.dw as unit,a.txm as barcode,
a.pzwh as approval,isnull(t.stock,0) as stock,isnull(b.price04,9999) as price,1 as step
from jc_jgtx b,jc_spxx a left join
(select SPBM as spbm,pfj as stock FROM jc_spxx WHERE (SDYW=0 or SDYW is null) and 
(ABCFL Like 'KS/%' or ABCFL like 'ZH/%' or ABCFL like 'BQ/YSB%' or dl=02)) t on a.spbm=t.spbm
where a.spbm=b.spbm and a.spbm is not null and a.spbm !='' and a.sdyw=0 order by drugCode