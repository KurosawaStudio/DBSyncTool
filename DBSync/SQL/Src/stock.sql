SELECT jc_spxx.spbm as drugCode,pfj as stock
FROM jc_spfl,jc_spxx,jc_pck WHERE fl=flbm and jc_spxx.spbm=jc_pck.spbm and (LDW='FZG' OR LDW='JMGS')
