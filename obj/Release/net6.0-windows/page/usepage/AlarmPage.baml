   M S B A M L   `   `   `  ���� B  >WpfApp4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:WpfApp4.page.usepageWpfApp4.page.usepage  5clr-namespace:WpfApp4.ViewModelWpfApp4.ViewModel  7 clr-namespace:WpfApp4.ConvertersWpfApp4.Converters  $    WpfApp4.page.usepage.AlarmPage   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     4local"clr-namespace:WpfApp4.page.usepage     _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  /	viewmodelclr-namespace:WpfApp4.ViewModel  1
converters clr-namespace:WpfApp4.Converters    N� Title$  Page8�� � 	Resources 2  ,WpfApp4.Converters.BoolToAlarmColorConverter   BoolToAlarmColorConverter��%   &	          .���  � ColumnDefinitions �� $	��800 ��� $��* �.+�� $��1�� � RowDefinitions �� $	��550 ��� .+�� $��0�� �� $	��550 ��� .+�#  MaterialDesignThemes.Wpf.Card  $��0��.���� $��0�� �� TabStripPlacement$	 Top=�.@��� $��温度��.���� $��5q����� .R�A�  A� Columns$ 2��$	��500��$	��480��.@��� ${�False��$��38�� J� Opacity$ 1Q�.����  �� Orientation$ 
Horizontal=�.+��� $��控制温度极限超温��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *.,CurrentAlarm.ControlTemperatureLimitOverheat+ �� 	Converter# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $ ��Profile 热偶极限超温��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� */-CurrentAlarm.ProfileThermocoupleLimitOverheat+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��	profile-1��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.Profile1+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��	profile-2��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.Profile2+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��	profile-3��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.Profile3+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��	profile-4��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.Profile4+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��	profile-5��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.Profile5+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��	profile-6��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.Profile6+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��	profile-7��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.Profile7+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��	profile-8��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.Profile8+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��	profile-9��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.Profile9+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��spike-1��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.Spike1+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��spike-2��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.Spike2+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��spike-3��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.Spike3+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��spike-4��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.Spike4+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��spike-5��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.Spike5+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��spike-6��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.Spike6+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��spike-7��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.Spike7+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��spike-8��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.Spike8+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��spike-9��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.Spike9+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $!��固态继电器超温报警��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *&$CurrentAlarm.SolidStateRelayOverheat+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��变压器超温报警��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *" CurrentAlarm.TransformerOverheat+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��辅助加热报警��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.AuxiliaryHeating+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��辅助超温报警��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� * CurrentAlarm.AuxiliaryOverheat+# [  �� $��流量/水流��.���� $��10q�$	��Top=����� .R�A� $ 2��$	��500��$	��200��.@��� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $!��水流报警（前法兰）��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *#!CurrentAlarm.WaterFlowFrontFlange+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $!��水流报警（后法兰）��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *" CurrentAlarm.WaterFlowRearFlange+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $!��水温报警（前法兰）��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *#!CurrentAlarm.WaterTempFrontFlange+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $!��水温报警（后法兰）��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *" CurrentAlarm.WaterTempRearFlange+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��N2流量计报警��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.N2FlowMeter+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��SIH4流量计报警��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.Sih4FlowMeter+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��NH3流量计报警��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.Nh3FlowMeter+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��N2O流量计报警��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.N2oFlowMeter+# [  �� $��舟/泵��.���� $��10q�$	��Top=����� .R�A� $ 2��$	��500��$	��200��.@��� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��前极限位报警��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *!CurrentAlarm.FrontLimitPosition+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��后极限位报警��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� * CurrentAlarm.RearLimitPosition+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��上极限位位报警��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *!CurrentAlarm.UpperLimitPosition+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��下极限位报警��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *!CurrentAlarm.LowerLimitPosition+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��舟同步报警��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *" CurrentAlarm.BoatSynchronization+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��压强超差报警��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� * CurrentAlarm.PressureDeviation+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��伺服异常��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.ServoAnomaly+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��伺服通讯��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *!CurrentAlarm.ServoCommunication+# [  �� $��其他��.���� $��10q�$	��Top=����� .R�A� $ 2��$	��500��$	��400��.@��� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��N2 压力报警��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.N2Pressure+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��风压检测（炉口）��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *&$CurrentAlarm.AirPressureFurnaceMouth+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��风压检测（炉室）��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *(&CurrentAlarm.AirPressureFurnaceChamber+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��CDA 报警（机柜）��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.CdaCabinet+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��CDA 报警（马达）��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.CdaMotor+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��motion stop 报警��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.MotionStop+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��射频短路��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.RfShortCircuit+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��设备断电报警��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *$"CurrentAlarm.EquipmentPowerFailure+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��射频异常��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.RfAnomaly+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��射频温差报警��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.RfTempDifference+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��SIH4泄漏报警��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.Sih4Leak+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��NH3泄漏报警��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.Nh3Leak+# [  �� ${�False��$��38��$ 1Q�.���� $ 
Horizontal=�.+��� $��N2O泄漏报警��$
��Bold"�$��Center=�A� $��1��
���   �$��20��$��20��$��20,0,0,0q����� *CurrentAlarm.N2oLeak+# [    $��0��$��5q�.���� $ Vertical=�.+�� $��5q� �� $��* ��� $
��Auto �.+��� $ 
Horizontal=�$��0��.+�(  "System.Windows.Controls.DatePicker  $	��4,0q�$	��120��)  #MaterialDesignThemes.Wpf.HintAssist	   Hint$	 起始日期��
   SelectedDate
 �� *	StartDate+�� $�� 至 ��$��Center=�$	��4,0q�  $	��4,0q�$	��120��$	 结束日期��
 �� *	EndDate+��    MaterialDesignOutlinedButton#��[ $	��4,0q� � ToolTip$ 查询日志������ *QueryLogsByDateRangeCommand+.���� $ 
Horizontal=�.+�'  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Magnify=�$��0,0,4,0q��� $��查询���� #��[ $	��4,0q�$ 生成测试记录������ *GenerateTestRecordsCommand+.���� $ 
Horizontal=�.+�  $
 Plus=�$��0,0,4,0q��� $��生成测试���� $��5q�$	��780��.@��� $��报警记录��.��&   System.Windows.Controls.DataGrid  $	��760��   AutoGenerateColumns$ False��   
IsReadOnly$
 True��   CanUserAddRows$ False��   CanUserDeleteRows$ False��$
��Auto=�$��5q����� *
AlarmrLogs+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 报警触发时间��   Width5	  /System.Windows.Controls.DataGridLengthConverter$	 170	 1
  +System.Windows.Controls.DataGridBoundColumn 
  Binding �� *	Timestamp+ �� StringFormat$ {0:yyyy-MM-dd HH:mm:ss}��  $ 触发报警的用户��$	 100	  �� *User+  $ 报警等级��$ 80	  �� *Level+  $ 报警恢复时间��$	 170	  �� *RecoveryTime+$ {0:yyyy-MM-dd HH:mm:ss}�� �� TargetNullValue$ 	未恢复��  $ 报警详情��$ *	  �� *	Details+  $ 报警的描述信息��$	 120	  �� *Description+�� $��操作记录��.��  $	��760��$ False��$
 True��$ False��$ False��$
��Auto=�$��5q����� *OperationRecords+   $ 操作触发时间��$	 170	  �� *	Timestamp+$ {0:yyyy-MM-dd HH:mm:ss}��  $ 执行操作的用户��$	 100	  �� *User+  $ 操作恢复时间��$	 170	  �� *RecoveryTime+$ {0:yyyy-MM-dd HH:mm:ss}��$ 	未恢复��  $ 操作详情��$ *	  �� *	Details+  $ 操作的描述信息��$	 120	  �� *Description+�� $��运行记录��.��  $	��760��$ False��$
 True��$ False��$ False��$
��Auto=�$��5q����� *RunningRecords+   $ 操作发生的时间��$	 170	  �� *OperationTime+$ {0:yyyy-MM-dd HH:mm:ss}��  $ 执行操作的用户��$	 100	  �� *User+  $ 操作模式��$ 80	  �� *OperationMode+  $ 设备名称��$	 100	  �� *
DeviceName+  $ 舟号��$ 80	  �� *
BoatNumber+  $ 	舟状态��$ 80	  �� *
BoatStatus+  $! 运行记录的描述信息��$ *	  �� *Description+