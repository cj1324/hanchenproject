#!usr/bin/python
#-*- coding:utf-8 -*-
#filename hcpj/imgpj/form.py  
#    基本的窗体控件 
#
#
'''  窗体控件的代码 '''

__author__="寒晨"	 	#作者
__version__="1.0" 		#版本
__date__="2010.9.1"	#日期
__copyright__="寒晨所有&2010"#授权
__license__="Python"	#证书

import os
import wx
import coreFun


class MainFrame(wx.Frame):
    def __init__(self,parent,id,title):
        ''' 创建窗体及其控件'''
        wx.Frame.__init__(self,parent,-1,title,pos=(200,200),size=(350,250),style=wx.DEFAULT_FRAME_STYLE|wx.NO_FULL_REPAINT_ON_RESIZE)

        self.lbl_Info1=wx.StaticText(self, -1,u"请选择水印文件：", pos=(10,20),size=(150,40), style=0, name="lbl_Info1")
        self.txt_WatchFile=wx.TextCtrl(self,-1,u"请单击’打开‘选择水印图片",pos=(10,50),size=(300,30),style=wx.TE_READONLY)
        self.btn_OpenWatch=wx.Button(self,-1,u"打开", pos=(160,10),size=(70,30))
        self.Bind(wx.EVT_BUTTON,self.OnClick_btn_OpenWatch,self.btn_OpenWatch)


        self.lbl_Info2=wx.StaticText(self, -1,u"请选择目标目录：",pos=(10,110),size  = (150,40),style=0,name="lbl_Info2")
        self.txt_TargDir=wx.TextCtrl(self,-1,u"请单击’目标目录‘选择：",pos=(10,140),size=(300,30),style= wx.TE_READONLY)
        self.btn_TargDir=wx.Button(self,-1,u"目标目录", pos=(160,100),size=(70,30))
        self.Bind(wx.EVT_BUTTON,self.OnClick_btn_TargDir,self.btn_TargDir)

        self.btn_Submit=wx.Button(self,-1,u"开始", pos=(145,180),size=(85,30))
        self.Bind(wx.EVT_BUTTON,self.OnClick_btn_Submit,self.btn_Submit)
        self.btn_Submit.Enable(False)
        
        self.btn_CheckFileNum=wx.Button(self,-1,u"扫描目标目录",pos=(240,180),size=(90,30))
        self.Bind(wx.EVT_BUTTON,self.OnClick_btn_CheckFileNum,self.btn_CheckFileNum)

        self.lbl_CheckInfo=wx.StaticText(self,-1,u"Info:",pos=(10,185),size=(130,60),name="lbl_Check_INfo",style=wx.ST_NO_AUTORESIZE)


        return

    def OnClick_btn_OpenWatch(self,event):
        ''' 选择水平图片按钮事件'''
        self.dlg_OpenWatch=wx.FileDialog(self,u"请选择一张水印的\r\n图片","","","*.jpg;*.png;*.gif",wx.OPEN)
        if self.dlg_OpenWatch.ShowModal() == wx.ID_OK:
            filename=self.dlg_OpenWatch.GetFilename()
            dirname=self.dlg_OpenWatch.GetDirectory()

            self.txt_WatchFile.SetValue(dirname+os.sep+filename)
        else:
            self.txt_WatchFile.SetValue(u"没有选择文件！")


    def OnClick_btn_TargDir(self,event):
        ''' 选择需要打水印的目录按钮事件 '''
        self.dlg_TargDir=wx.DirDialog(self,u"请选择目标目录")

        if  self.dlg_TargDir.ShowModal() == wx.ID_OK:
             dirname=self.dlg_TargDir.GetPath()
             self.txt_TargDir.SetValue(dirname)
             self.btn_CheckFileNum.Enable(True)
        else:
            self.txt_TargDir.SetValue(u"没有选择目录！")
        
        return




    def OnClick_btn_CheckFileNum(self,event):
        '''  检查目标目录的图片数量按钮事件  '''
        
        if not os.path.isfile(self.txt_WatchFile.GetValue()):
            #msgbox=wx.MessageBox("必须选择水印图片！","标题",style=wx.OK)
            self.lbl_CheckInfo.SetLabel(u"必须选择水印图片！")
            self.lbl_CheckInfo.SetForegroundColour("red")
            return
        
        if not os.path.isdir(self.txt_TargDir.GetValue()):
            #msgbox=wx.MessageBox("必须选择目标目录！","标题",style=wx.OK)
            self.lbl_CheckInfo.SetLabel(u"必须选择目标目录！")
            self.lbl_CheckInfo.SetForegroundColour("red")
            return
        #self.btn_OpenWatch.Enable(False)
        #self.btn_TargDir.Enable(False)
        self.btn_CheckFileNum.Enable(False)
        
        self.imglist=coreFun.CheckFileNum(self.txt_TargDir.GetValue())
        self.lbl_CheckInfo.SetLabel(u"目录中的图片%s张，请单击开始！"%len(self.imglist))
        self.lbl_CheckInfo.SetForegroundColour("black")

        self.btn_Submit.Enable(True)
        return

    def OnClick_btn_Submit(self,event):
        '''  确认图片后打水印的方法 '''
        ErrorNum=0
        OkNum=0
        self.btn_Submit.Enable(False)
        self.lbl_CheckInfo.SetForegroundColour("black")
        self.lbl_CheckInfo.SetLabel(u"")
        for i in range(len(self.imglist)):
            if coreFun.RunPrint(self.txt_WatchFile.GetValue(),self.imglist[i],0.2):   #加水印的核心调用
                OkNum+=1
                self.lbl_CheckInfo.SetLabel(self.lbl_CheckInfo.GetLabel()+str(i)+u",")
            else:
                ErrorNum+=1
                self.lbl_CheckInfo.SetForegroundColour("red")
        self.lbl_CheckInfo.SetLabel(u"共%s张图片,成功打水印%s张，失败%s张"%(str(len(self.imglist)),str(OkNum),str(ErrorNum)));
        #self.CoreFun(self.txt_WatchFile.GetValue(),self.txt_TargDir.GetValue())
        pass

    
def main():
    ''' 创建应用程序，并启动程序 '''
    app=wx.PySimpleApp()
    frame=MainFrame(None,-1,u"水印添加   By:寒晨")
    frame.Show(True)
    app.SetTopWindow(frame)
    app.MainLoop()

if __name__=="__main__":
    main()

