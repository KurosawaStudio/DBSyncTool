SELECT flmc as ERPNodeIdentityName,jc_spxx.spbm as drugCode,spmc as drugName,ypgg as pack,sccj as factory,pfj as stock,dw as unit,pzpj AS price,
jc_pck.ph as 批号,scrq as 生产日期,XQ AS 有效期,jc_spxx.zhxgsj as 最后修改时间,txm as barcode,pzwh as approval,1 as step
FROM jc_spfl,jc_spxx,jc_pck WHERE fl=flbm and jc_spxx.spbm=jc_pck.spbm and (LDW='FZG' OR LDW='JMGS')