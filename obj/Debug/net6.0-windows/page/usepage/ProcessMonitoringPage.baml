   M S B A M L   `   `   `  ����B  >WpfApp4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:WpfApp4.page.usepageWpfApp4.page.usepage  5      0    *WpfApp4.page.usepage.ProcessMonitoringPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:WpfApp4.page.usepage     5      _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        N� Title$  ProcessMonitoringPage��5
       � 	Resources 5      &   System.Windows.Controls.DataGrid �� 
TargetType �� Property �� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%�  '        '  �    �� 5   
   "  6   .�5      ��   �@ AlternatingRowBackground
 �����69   �� 5       �@ RowBackground
 �����6.   �� 5       �@ GridLinesVisibility$	 All=�64   �� 5        �@ HorizontalGridLinesBrush
 �����69   �� 5       �@ VerticalGridLinesBrush
 �����67   �� 5       � ��
 �����6,   �� 5       � ��$ 1q�60   �� 5       � ��$ 14(�6)   5      �� 5   
   "  6   .�5      ��  � ��
 ��ح�6+   �� 5       � ��
 �   �6+   �� 5       � ��$
 Bold"�6+   �� 5       � ��
 �����6,   �� 5       � ��$ 0,0,1,1q�60   5      5      .��5       �  � RowDefinitions 5!   
   �� 5"      $��50 �6   6*   �� 5#      6   .+�5%   
   #  MaterialDesignThemes.Wpf.Card  $��0��6   .��5&      ��  �� Orientation$ 
Horizontal=�6   $��Right=�62   .+�5'      ��    MaterialDesignFlatButton#��[  6   
����·�$��导出excel文件��6e   $	��130��6y   $��10q�6�   6�   �� 5(      #��[  6   
����·�$��打开excel文件��6e   $	��130��6y   $��10q�6�   6�   �� 5)      #��[  6   
����·�$��回退到当前工艺��6e   $	��130��6w   $��10q�6�   6�   5*      5+        5,   
   -   5          Name$ 
myDataGrid��5,         AutoGenerateColumns$ False��5-      $��1��5.      ��5/      �� *MonitoringData+	   Columns	 50      0  *System.Windows.Controls.DataGridTextColumn  51      ,  &System.Windows.Controls.DataGridColumn
   Header$
 数据采集时间��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6S   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *	Timestamp+6S     52      $
 工艺时间��6%   $	 100 6S    63   �� *ProcessTime+6S     53      $
 剩余时间��6%   $	 100 6U    63   �� *RemainingTime+6U     54      $
 步号��6%   $	 100 6P    61   �� *
StepNumber+6P     55      $
 工艺类型��6%   $	 100 6S    63   �� *ProcessType+6S     56      $
 舟号��6%   $	 100 6P    61   �� *
BoatNumber+6P     57      $
 停止原因��6%   $	 100 6R    63   �� *
StopReason+6R     5:      $
 设定温度区1��6%   $	 100 6V    65   �� *SetTempZone1+6V     5;      $
 设定温度区2��6%   $	 100 6V    65   �� *SetTempZone2+6V     5<      $
 设定温度区3��6%   $	 100 6V    65   �� *SetTempZone3+6V     5=      $
 设定温度区4��6%   $	 100 6V    65   �� *SetTempZone4+6V     5>      $
 设定温度区5��6%   $	 100 6V    65   �� *SetTempZone5+6V     5?      $
 设定温度区6��6%   $	 100 6V    65   �� *SetTempZone6+6V     5B      $
 实际温度区1��6%   $	 100 6W    65   �� *RealTempZone1+6W     5C      $
 实际温度区2��6%   $	 100 6W    65   �� *RealTempZone2+6W     5D      $
 实际温度区3��6%   $	 100 6W    65   �� *RealTempZone3+6W     5E      $
 实际温度区4��6%   $	 100 6W    65   �� *RealTempZone4+6W     5F      $
 实际温度区5��6%   $	 100 6W    65   �� *RealTempZone5+6W     5G      $
 实际温度区6��6%   $	 100 6W    65   �� *RealTempZone6+6W     5J      $
 
MFC1设定��6%   $	 100 6Q    65   �� *	SetMFC1+6Q     5K      $
 
MFC2设定��6%   $	 100 6Q    65   �� *	SetMFC2+6Q     5L      $
 
MFC3设定��6%   $	 100 6Q    65   �� *	SetMFC3+6Q     5M      $
 
MFC4设定��6%   $	 100 6Q    65   �� *	SetMFC4+6Q     5N      $
 
MFC1实际��6%   $	 100 6R    65   �� *
RealMFC1+6R     5O      $
 
MFC2实际��6%   $	 100 6R    65   �� *
RealMFC2+6R     5P      $
 
MFC3实际��6%   $	 100 6R    65   �� *
RealMFC3+6R     5Q      $
 
MFC4实际��6%   $	 100 6R    65   �� *
RealMFC4+6R     5T      $
 射频功率设定��6%   $	 100 6T    65   �� *
RfPowerSet+6T     5U      $
 射频功率实际��6%   $	 100 6W    65   �� *RfPowerActual+6W     5V      $
 射频电流��6%   $	 100 6Q    63   �� *	RfCurrent+6Q     5W      $
 射频电压��6%   $	 100 6Q    63   �� *	RfVoltage+6Q     5X      $
 	占空比��6%   $	 100 6P    62   �� *	DutyCycle+6P     5[      $
 蝶阀角度��6%   $	 100 6[    63   �� *ButterflyValveAngle+6[     5^      $
 腔体压力设定��6%   $	 100 6\    65   �� *ChamberPressureSet+6\     5_      $
 腔体压力实际��6%   $	 100 6_    65   �� *ChamberPressureActual+6_     5b      $
 辅热实际功率��6%   $	 100 6\    65   �� *AuxHeatPowerActual+6\     5c      $
 辅热实际温度��6%   $	 100 6[    65   �� *AuxHeatTempActual+6[     5d      $
 辅热设定温度��6%   $	 100 6X    65   �� *AuxHeatTempSet+6X     5e      $
 辅热A相电流��6%   $	 100 6Y    65   �� *AuxHeatCurrentA+6Y     5f      $
 辅热B相电流��6%   $	 100 6Y    65   �� *AuxHeatCurrentB+6Y     5g      $
 辅热C相电流��6%   $	 100 6Y    65   �� *AuxHeatCurrentC+6Y   5i      5j      5k      