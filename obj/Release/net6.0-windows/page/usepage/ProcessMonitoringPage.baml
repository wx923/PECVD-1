   M S B A M L   `   `   `  ���� B  >WpfApp4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:WpfApp4.page.usepageWpfApp4.page.usepage  0    *WpfApp4.page.usepage.ProcessMonitoringPage   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     4local"clr-namespace:WpfApp4.page.usepage     _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    N� Title$  ProcessMonitoringPage�� � 	Resources &   System.Windows.Controls.DataGrid �� 
TargetType �� Property �� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%�  '        '  2    �� "  .���   �@ AlternatingRowBackground
 �������  �@ RowBackground
 �������  �@ GridLinesVisibility$	 All=���   �@ HorizontalGridLinesBrush
 �������  �@ VerticalGridLinesBrush
 �������  � ��
 �������  � ��$ 1q���  � ��$ 14(��� "  .���  � ��
 ��ح���  � ��
 �   ���  � ��$
 Bold"���  � ��
 �������  � ��$ 0,0,1,1q�.���  � RowDefinitions �� $��50 ��� .+�#  MaterialDesignThemes.Wpf.Card  $��0��.����  �� Orientation$ 
Horizontal=�$��Right=�.+���    MaterialDesignFlatButton#��[  
����·�$��导出excel文件��$	��130��$��10q��� #��[  
����·�$��打开excel文件��$	��130��$��10q��� #��[  
����·�$��回退到当前工艺��$	��130��$��10q�  -     Name$ 
myDataGrid��   AutoGenerateColumns$ False��$��1������ *MonitoringData+	   Columns	 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn
   Header$
 数据采集时间��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	Timestamp+  $
 工艺时间��$	 100  �� *ProcessTime+  $
 剩余时间��$	 100  �� *RemainingTime+  $
 步号��$	 100  �� *
StepNumber+  $
 工艺类型��$	 100  �� *ProcessType+  $
 舟号��$	 100  �� *
BoatNumber+  $
 停止原因��$	 100  �� *
StopReason+  $
 设定温度区1��$	 100  �� *SetTempZone1+  $
 设定温度区2��$	 100  �� *SetTempZone2+  $
 设定温度区3��$	 100  �� *SetTempZone3+  $
 设定温度区4��$	 100  �� *SetTempZone4+  $
 设定温度区5��$	 100  �� *SetTempZone5+  $
 设定温度区6��$	 100  �� *SetTempZone6+  $
 实际温度区1��$	 100  �� *RealTempZone1+  $
 实际温度区2��$	 100  �� *RealTempZone2+  $
 实际温度区3��$	 100  �� *RealTempZone3+  $
 实际温度区4��$	 100  �� *RealTempZone4+  $
 实际温度区5��$	 100  �� *RealTempZone5+  $
 实际温度区6��$	 100  �� *RealTempZone6+  $
 
MFC1设定��$	 100  �� *	SetMFC1+  $
 
MFC2设定��$	 100  �� *	SetMFC2+  $
 
MFC3设定��$	 100  �� *	SetMFC3+  $
 
MFC4设定��$	 100  �� *	SetMFC4+  $
 
MFC1实际��$	 100  �� *
RealMFC1+  $
 
MFC2实际��$	 100  �� *
RealMFC2+  $
 
MFC3实际��$	 100  �� *
RealMFC3+  $
 
MFC4实际��$	 100  �� *
RealMFC4+  $
 射频功率设定��$	 100  �� *
RfPowerSet+  $
 射频功率实际��$	 100  �� *RfPowerActual+  $
 射频电流��$	 100  �� *	RfCurrent+  $
 射频电压��$	 100  �� *	RfVoltage+  $
 	占空比��$	 100  �� *	DutyCycle+  $
 蝶阀角度��$	 100  �� *ButterflyValveAngle+  $
 腔体压力设定��$	 100  �� *ChamberPressureSet+  $
 腔体压力实际��$	 100  �� *ChamberPressureActual+  $
 辅热实际功率��$	 100  �� *AuxHeatPowerActual+  $
 辅热实际温度��$	 100  �� *AuxHeatTempActual+  $
 辅热设定温度��$	 100  �� *AuxHeatTempSet+  $
 辅热A相电流��$	 100  �� *AuxHeatCurrentA+  $
 辅热B相电流��$	 100  �� *AuxHeatCurrentB+  $
 辅热C相电流��$	 100  �� *AuxHeatCurrentC+