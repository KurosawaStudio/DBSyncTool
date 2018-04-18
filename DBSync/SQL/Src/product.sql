SELECT flmc as ERPNodeIdentityName,jc_spxx.spbm as drugCode,spmc as drugName,ypgg as pack,sccj as factory,pfj as stock,dw as unit,pzpj AS price,
jc_pck.ph as ph,scrq as scrq,XQ AS xq,jc_spxx.zhxgsj as zhxgsj,txm as barcode,pzwh as approval,1 as step
FROM jc_spfl,jc_spxx,jc_pck WHERE fl=flbm and jc_spxx.spbm=jc_pck.spbm and (LDW='FZG' OR LDW='JMGS')