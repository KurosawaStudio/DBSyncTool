SELECT jc_spxx.spbm drugcode,isnull(case dw when 'kg' then jc_spxx.yhs/1000 else 1 end,1) step,dw
from jc_spxx where ldw LIKE 'FZG'