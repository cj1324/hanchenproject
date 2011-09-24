"VIM模板 插件 
"作者：寒晨
"时间： 2010年 11月 12日 星期五 14:21:51 CST

if exists("g:loaded_hctemplates")
    finish
endif
let g:loaded_hctemplates=1

let s:save_cop=&cpo
set cpo&vim

function s:GetFileType()
    return tolower((strridx(expand("%"),".") == -1) ?
                \"" :
                \strpart(expand("%"),(strridx(expand("%"),".") + 1)))
endf


function g:CreateTemplates()
    let s:type=call s:GetFileType();
   " if(s:)
endf


"结尾部分
let &cpo=s:save_cpo
