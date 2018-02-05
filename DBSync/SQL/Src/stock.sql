select SPBM as drugCode,pfj as stock FROM jc_spxx WHERE (SDYW=0 or SDYW is null) AND
(ABCFL Like 'KS/%' or ABCFL like 'ZH/%' or ABCFL like 'BQ/YSB%' or dl=02) 
--and spbm='00120510'
