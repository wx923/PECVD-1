   M S B A M L   `   `   `  ����B  >WpfApp4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:WpfApp4.ConvertersWpfApp4.Converters  5      '    !WpfApp4.page.usepage.DataShowPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      R NOxyPlot.Wpf, Version=2.2.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0Y UOxyPlot.Wpf.Shared, Version=2.2.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0"oxyhttp://oxyplot.org/wpf   5      2	Converter clr-namespace:WpfApp4.Converters   5        N� Title$  DataShowPage��5         MaterialDesignPaper N� 
Background# �   5        MaterialDesignBody#���  5       N� 
FontFamily 5   	   :  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension   � 	Resources 5      "   WpfApp4.Converters.BoolToVis  	BoolToVis)   #WpfApp4.Converters.InverseBoolToVis  InverseBoolToVis��%:   &	       &	        5   
   6     5   
   6%   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��Center=�6.   $��0,0,0,20q�6K   .+�5      �� )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Select Collection��6   $	��200��5       & "MaterialDesignFloatingHintComboBox#��[ 5!       �� MaxDropDownHeight$	 300��5"      ��5      �� *Collections+ �� SelectedItem 5       �� *SelectedCollection+5"      �� 5#      $��加载数据��6   $��20,0,0,0q�5$        MaterialDesignRaisedButton#��[ 5%      ��5&      �� *LoadDataCommand+�� 5'      $��Export to Excel��6   $��20,0,0,0q�5(      #��[ 5)      ��5*      �� *ExportToExcelCommand+�� 5+      $��Toggle View��6   $��20,0,0,0q�5,      #��[ 5-      ��5.      �� *ToggleViewCommand+5/      � 52   
   $��1��6   .+�54      �� $
��Auto=�6   $
��Auto=�6@   y�6c   �� *IsTableView+	 �� 	Converter#	 [ .��55      &   System.Windows.Controls.DataGrid  $y�Visible=�6   
   AutoGenerateColumns$
 False��57         
IsReadOnly$
 True��58        MaterialDesignDataGrid#��[ 59      ��56      �� *CollectionData+   Columns 5:      0  *System.Windows.Controls.DataGridTextColumn  5;      ,  &System.Windows.Controls.DataGridColumn   Header$ T1��6-   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *T1+  5<      $ T2��6-    69   �� *T2+  5=      $ T3��6-    69   �� *T3+  5>      $ T4��6-    69   �� *T4+  5?      $ T5��6-    69   �� *T5+  5@      $ T6��6-    69   �� *T6+  5A      $ T7��6-    69   �� *T7+  5B      $ T8��6-    69   �� *T8+  5C      $ T9��6-    69   �� *T9+  5D      $ N2��6-    69   �� *N2+  5E      $
 SiH4��6-    6;   �� *SiH4+  5F      $	 N2O��6-    6:   �� *N2O+  5G      $ H2��6-    69   �� *H2+  5H      $	 PH3��6-    6:   �� *PH3+  5I      $ 压力��6-    69   �� *
Pressure+  5J      $ 功率1��6-    6:   �� *Power1+  5K      $ 功率2��6-    6:   �� *Power2+  5L      $ 进/出��6-    6:   �� *InOut+  5M      $ 平移速度��6-    6;   �� *	MoveSpeed+  5N      $ 	上下速��6-    6:   �� *UpDownSpeed+  5O      $ 辅热时间��6-    6;   �� *TypicalHeatTime+  5P      $ 辅热温度��6-    6;   �� *
AssistTemp+  5Q      $ 
脉冲开1��6-    6;   �� *
PulseOn1+  5R      $ 
脉冲关1��6-    6;   �� *	PulseOff1+  5S      $ 
脉冲开2��6-    6;   �� *
PulseOn2+  5T      $ 
脉冲关2��6-    6;   �� *	PulseOff2+  5U      $ 射频电流��6-    6;   �� *	RFCurrent+  5V      $ 电流参考值（A）��6-    6?   �� *
CurrentRef+  5W      $ 电流卡控值（A）��6-    6?   �� *CurrentLimit+  5X      $ 射频电压��6-    6;   �� *	RFVoltage+  5Y      $ 电压参考值（V）��6-    6?   �� *
VoltageRef+  5Z      $ 电压卡控值（V）��6-    6?   �� *VoltageLimit+5\      5]      	  OxyPlot.Wpf.PlotView	  5_      $��Stretch=�5a      $��Stretch=�67   y�5_      �� *IsTableView+#	 [ 
  OxyPlot.Wpf.PlotViewBase 
  Model 5`      �� *	PlotModel+5a   7   5b      5c      5d      