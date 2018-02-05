select SPBM as drugCode,PRICE04 as price FROM jc_jgtx where spbm in 
(select spbm FROM jc_spxx WHERE (SDYW=0 or SDYW is null) and 
(ABCFL Like 'KS/%' or ABCFL like 'ZH/%' or ABCFL like 'BQ/YSB%' or dl=02)) --and spbm='00120510'
