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
   Header$
 时间��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 80 6J   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Time+6J     52      $
 内温1��6%   $ 80 6I    62   �� *T1+6I     53      $
 内温2��6%   $ 80 6I    62   �� *T2+6I     54      $
 内温3��6%   $ 80 6I    62   �� *T3+6I     55      $
 内温4��6%   $ 80 6I    62   �� *T4+6I     56      $
 内温5��6%   $ 80 6I    62   �� *T5+6I     57      $
 内温6��6%   $ 80 6I    62   �� *T6+6I     58      $
 内温7��6%   $ 80 6I    62   �� *T7+6I     59      $
 内温8��6%   $ 80 6I    62   �� *T8+6I     5:      $
 内温9��6%   $ 80 6I    62   �� *T9+6I     5;      $

 SiH4��6%   $ 80 6L    63   �� *SiH4+6L     5<      $
 N2��6%   $ 80 6H    61   �� *N2+6H     5=      $	
 N2O��6%   $ 80 6J    62   �� *N2O+6J     5>      $
 H2��6%   $ 80 6H    61   �� *H2+6H     5?      $	
 PH3��6%   $ 80 6J    62   �� *Ph3+6J     5@      $
 压力��6%   $ 80 6N    61   �� *
Pressure+6N     5A      $
 功率1��6%   $ 80 6M    62   �� *Power1+6M     5B      $
 功率2��6%   $ 80 6M    62   �� *Power2+6M     5C      $
 电流��6%   $ 80 6V    61   �� *CurrentReference+6V     5D      $
 电压��6%   $ 80 6V    61   �� *VoltageReference+6V     5E      $
 脉冲频率��6%   $ 80 6V    63   �� *PulseFrequency+6V     5F      $
 脉冲电压��6%   $ 80 6T    63   �� *PulseVoltage+6T   5H      5I      5J      