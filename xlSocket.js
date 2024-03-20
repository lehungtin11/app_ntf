function xlSocket(message)
{
    try{
    
        var xobj = JSON.parse(message);
        console.log(xobj);
        dem_temp_n=dem_temp_n+1;
        
        if(xobj.type==callback)
        {
            getNTF(#currentUser.username#);
            
            var ins_html = $(`section data-id=${xobj.id} data-fkkh=${xobj.fkKH} onclick=readNTF(this,'${xobj.id}','${xobj.time}','${xobj.time}') class=x-alert data-time=10
                div class=alert alert-simple alert-warning style=colorvar(--text-color);background-colorvar(--white);border-radius 5px 5px 0 0;
                border1px solid #ddd;box-shadow0px 0px 2px #ddd; max-width400px
                    div
                        div style=displayflex;justify-contentspace-between;margin-bottom4px
                            p style=font-weight600;colorvar(--blue)Hẹn gọi lạip
                            i class=sap sap-infoi
                        div
                        p class=x-name limit-line style=font-weight600${xobj.thoiGian}p
                        p class=limit-line${xobj.content}p
                    div
                div
                div class=x-load style=background-colorvar(--blue-2-color)div
            section`);
            ins_html.insertBefore(#x-notification #add-notication);
        }
        var rs_json = JSON.parse(encodedMsg)
        $(#dem-notification).text(dem_temp_n);
        endScrollTop();
    }catch(e){}
}