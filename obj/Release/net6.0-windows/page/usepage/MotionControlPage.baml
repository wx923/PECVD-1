   M S B A M L   `   `   `  ���� B  >WpfApp4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:WpfApp4.page.usepageWpfApp4.page.usepage  7 clr-namespace:WpfApp4.ConvertersWpfApp4.Converters  ,    &WpfApp4.page.usepage.MotionControlPage   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     4local"clr-namespace:WpfApp4.page.usepage     _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  1
converters clr-namespace:WpfApp4.Converters  -     N� Title$  Page1�� � 	Resources    BorderStyle1 �� 
TargetType �� Property �� Value  BorderStyle2  BorderStyle3  TopNavBarStyle  MaterialDesignPaper  TopNavigationButtonStyle  MaterialDesignFlatButton �� BasedOn �� 
TargetType �� CornerRadius �� Triggers	 P� Property
 P� Value  MaterialDesignSelection  SelectedButtonStyle 	 MaterialDesignBody 
 LeftNavigationButtonStyle  StatusTextStyle  
TextMargin1   +WpfApp4.Converters.BoolToOpenCloseConverter  BoolToOpenCloseConverter-   'WpfApp4.Converters.RadioButtonConverter  RadioButtonConverter-   'WpfApp4.Converters.BoolToYesNoConverter  BoolToYesNoConverter-   'WpfApp4.Converters.BoolToOnOffConverter  BoolToOnOffConverter5   /WpfApp4.Converters.BooleanToBackgroundConverter   BooleanToBackgroundConverter9   3WpfApp4.Converters.BooleanToInverseBooleanConverter $  BooleanToInverseBooleanConverter3   -WpfApp4.Converters.BoatStatusToColorConverter  BoatStatusToColorConverter+   %WpfApp4.Converters.BoolToIntConverter  BoolToIntConverter��%�  &	        &	 q     &	 �     &	 W    &	 �    7  &	 L    &	
 :    7  &	 �    &	 �    &	     &	     &	     &	     &	     &	     &	 $    &	 )    �� " ��.���  � ��
 �������  � ��$ 1q���  �@��CornerRadius$ 5����  � ��$ 5q��� " ��.���  � ��
 �������  � ��$ 2q���  �@��CornerRadius$ 10����  � ��$ 10q��� " ��.���  � ��
 ��  ���  � ��$ 3q���  �@��CornerRadius$ 15����  � ��$ 15q��� " ��.���  � ��# �  ��  � ��
 �������  � ��$ 1q���  �@��CornerRadius$ 5����  � ��$ 5q��� " ��8   .���  � ��$	 100����  � ��$ 40����  � ��$ 5q���  � ��
 �������  � ��$ 1q���  �@�Cursor$
 Hand����  � �� �� " ��.R��� #��z��#��z��#��z��$ 5��._��� $��Center=�$��Center=� P� 	 �@J�IsMouseOver$

 True��. ���  � ��# �  P� 	 � ��$

 True��. ���  � ��# �  �� " ��.���  � ��# �  ��  � ��# � 	 ��  � ��$ 10q���  � ��$ 5q���  � ��$ 0q���  �@�Cursor$
 Hand����  � �� �� " ��.R��� #��z��#��z��#��z��._��� $��Center=�$��Center=��� " ��8   .���  � ��$ 8q���  � ��
 �������  � ��$ 1q��� " ��.���  � ��$ Segoe UI)���  � ��$ 14(���  � ��$	 0 4q��� " ��.���  � ��$ 3q�                .���  � RowDefinitions �� $	��320 ��� .+�� $��0��$��
20,10,20,0q� � ColumnDefinitions �� $	��150 ��� $	��450 ��� $	��360 ��� $	��380 ��� $	��150 �.+�#	  MaterialDesignThemes.Wpf.Card	  $��0��$��5,5,5,5q�.����  �� Orientation$ Vertical=�$��Center=�.+��� $ 
Horizontal=�$��3q�.+��� $��进出料门锁1:��$��12(�$
��Bold"�$��0 0 0 1q��� $��12(�$
��Bold"�$��0 0 0 1q����� *MotionPlcData.InOutDoor1Lock+ �� 	Converter# [ �� $ 
Horizontal=�$��3q�.+��� $��进出料门锁2:��$��12(�$
��Bold"�$��0 0 0 1q��� $��12(�$
��Bold"�$��0 0 0 1q����� *MotionPlcData.InOutDoor2Lock+# [ �� $ 
Horizontal=�$��3q�.+��� $��维修门锁1:��$��12(�$
��Bold"�$��0 0 0 1q��� $��12(�$
��Bold"�$��0 0 0 1q����� *$"MotionPlcData.MaintenanceDoor1Lock+# [ �� $ 
Horizontal=�$��3q�.+��� $��维修门锁2:��$��12(�$
��Bold"�$��0 0 0 1q��� $��12(�$
��Bold"�$��0 0 0 1q����� *$"MotionPlcData.MaintenanceDoor2Lock+# [ �� $ Vertical=�$��3,10,3,3q�.+���  " MaterialDesignFlatDarkBgButton#��[ $	��140��$��30�� � ToolTip$ 点击关闭蜂鸣器������ *ToggleBuzzerCommand+.���� $ 
Horizontal=�.+�'
  !MaterialDesignThemes.Wpf.PackIcon
   
  Kind$ 
VolumeHigh=�$��0,0,8,0q��� $��关闭蜂鸣器���� -   $��0,4,0,0q�$��空间数据下发���� $��0,4,0,0q�$��上料������ *MaterialLoadingCommand+�� $��0,4,0,0q�$��出料������ *MaterialMovingCommand+	  $��1��$��5q�$��8,0,8,0q�.���  �� $��* ��� $��* �.+�	  $��0��.���� .+�A�  A� Columns$ 3��$��1q�.+��� 
�������$��1q�$��2q�$ 3��._��� $��3q�.+��� $��
暂存区1��$
��Bold"� �� TextAlignment$ Center=�$��0,0,0,1q�$��12(��� $ Center=�$	��0,1q�$��11(����� *StorageAreas[0].BoatNumber+�� $ Center=�$	��0,1q�$��11(����� *StorageAreas[0].Status+# [ ���� *StorageAreas[0].Status+# [ �� 
�������$��1q�$��2q�$ 3��._��� $��3q�.+��� $��
暂存区2��$
��Bold"�$ Center=�$��0,0,0,1q�$��12(��� $ Center=�$	��0,1q�$��11(����� *StorageAreas[1].BoatNumber+�� $ Center=�$	��0,1q�$��11(����� *StorageAreas[1].Status+# [ ���� *StorageAreas[1].Status+# [ �� 
�������$��1q�$��2q�$ 3��._��� $��3q�.+��� $��
暂存区3��$
��Bold"�$ Center=�$��0,0,0,1q�$��12(��� $ Center=�$	��0,1q�$��11(����� *StorageAreas[2].BoatNumber+�� $ Center=�$	��0,1q�$��11(����� *StorageAreas[2].Status+# [ ���� *StorageAreas[2].Status+# [ �� 
�������$��1q�$��2q�$ 3��._��� $��3q�.+��� $��
暂存区4��$
��Bold"�$ Center=�$��0,0,0,1q�$��12(��� $ Center=�$	��0,1q�$��11(����� *StorageAreas[3].BoatNumber+�� $ Center=�$	��0,1q�$��11(����� *StorageAreas[3].Status+# [ ���� *StorageAreas[3].Status+# [ �� 
�������$��1q�$��2q�$ 3��._��� $��3q�.+��� $��
暂存区5��$
��Bold"�$ Center=�$��0,0,0,1q�$��12(��� $ Center=�$	��0,1q�$��11(����� *StorageAreas[4].BoatNumber+�� $ Center=�$	��0,1q�$��11(����� *StorageAreas[4].Status+# [ ���� *StorageAreas[4].Status+# [ �� 
�������$��1q�$��2q�$ 3��._��� $��3q�.+��� $��
暂存区6��$
��Bold"�$ Center=�$��0,0,0,1q�$��12(��� $ Center=�$	��0,1q�$��11(����� *StorageAreas[5].BoatNumber+�� $ Center=�$	��0,1q�$��11(����� *StorageAreas[5].Status+# [ ���� *StorageAreas[5].Status+# [ �� 
�������$��1q�$��2q�$ 3��._��� $��3q�.+��� $��
暂存区6��$
��Bold"�$ Center=�$��0,0,0,1q�$��12(��� $ Center=�$	��0,1q�$��11(����� *StorageAreas[6].BoatNumber+�� $ Center=�$	��0,1q�$��11(����� *StorageAreas[6].Status+# [ ���� *StorageAreas[5].Status+# [ �� 
�������$��1q�$��2q�$ 3��._��� $��3q�.+��� $��桨区1��$
��Bold"�$ Center=�$��0,0,0,1q�$��12(��� $ Center=�$	��0,1q�$��11(����� *StorageAreas[7].BoatNumber+�� $ Center=�$	��0,1q�$��11(����� *StorageAreas[7].Status+# [ ���� *StorageAreas[7].Status+# [ �� 
�������$��1q�$��2q�$ 3��._��� $��3q�.+��� $��桨区2��$
��Bold"�$ Center=�$��0,0,0,1q�$��12(��� $ Center=�$	��0,1q�$��11(����� *StorageAreas[8].BoatNumber+�� $ Center=�$	��0,1q�$��11(����� *StorageAreas[8].Status+# [ ���� *StorageAreas[8].Status+# [ �� 
�������$��1q�$��2q�$ 3��._��� $��3q�.+��� $��桨区3��$
��Bold"�$ Center=�$��0,0,0,1q�$��12(��� $ Center=�$	��0,1q�$��11(����� *StorageAreas[9].BoatNumber+�� $ Center=�$	��0,1q�$��11(����� *StorageAreas[9].Status+# [ ���� *StorageAreas[9].Status+# [ �� 
�������$��1q�$��2q�$ 3��._��� $��3q�.+��� $��桨区4��$
��Bold"�$ Center=�$��0,0,0,1q�$��12(��� $ Center=�$	��0,1q�$��11(����� *StorageAreas[10].BoatNumber+�� $ Center=�$	��0,1q�$��11(����� *StorageAreas[10].Status+# [ ���� *StorageAreas[3].Status+# [ �� 
�������$��1q�$��2q�$ 3��._��� $��3q�.+��� $��桨区5��$
��Bold"�$ Center=�$��0,0,0,1q�$��12(��� $ Center=�$	��0,1q�$��11(����� *StorageAreas[11].BoatNumber+�� $ Center=�$	��0,1q�$��11(����� *StorageAreas[11].Status+# [ ���� *StorageAreas[11].Status+# [ �� 
�������$��1q�$��2q�$ 3��._��� $��3q�.+��� $��桨区6��$
��Bold"�$ Center=�$��0,0,0,1q�$��12(��� $ Center=�$	��0,1q�$��11(����� *StorageAreas[12].BoatNumber+�� $ Center=�$	��0,1q�$��11(����� *StorageAreas[12].Status+# [ ���� *StorageAreas[12].Status+# [ 	  $��1��$��2q�.���� 	  $��2��$��5,5,10,5q�.���� $ 
Horizontal=�$��10q�.+��� $
��Auto=�$	��250��.���� $ Vertical=�$��5q�.+��� $��源位选择��$
��Bold"�$��0,0,0,5q�	� $��
暂存区1��$��0,2,0,5q� 	� 	GroupName$ SourcePosition��{��� *" MotionPlcData.Storage1BoatSensor+ d� 	IsChecked �� *SelectedSourcePosition+# [  �� ConverterParameter$ 
暂存区1��	� $��
暂存区2��$��0,2,0,5q�$ SourcePosition��{��� *" MotionPlcData.Storage2BoatSensor+ �� *SelectedSourcePosition+# [ $ 
暂存区2��	� $��
暂存区3��$��0,2,0,5q�$ SourcePosition��{��� *" MotionPlcData.Storage3BoatSensor+ �� *SelectedSourcePosition+# [ $ 
暂存区3��	� $��
暂存区4��$��0,2,0,5q�$ SourcePosition��{��� *" MotionPlcData.Storage4BoatSensor+ �� *SelectedSourcePosition+# [ $ 
暂存区4��	� $��
暂存区5��$��0,2,0,5q�$ SourcePosition��{��� *" MotionPlcData.Storage5BoatSensor+ �� *SelectedSourcePosition+# [ $ 
暂存区5��	� $��
暂存区6��$��0,2,0,5q�$ SourcePosition��{��� *" MotionPlcData.Storage6BoatSensor+ �� *SelectedSourcePosition+# [ $ 
暂存区6��	� $��桨区1��$��0,2,0,5q�$ SourcePosition��{��� *(&FurnacePlcDataDict[0].PaddleBoatSensor+ �� *SelectedSourcePosition+# [ $ 桨区1��	� $��桨区2��$��0,2,0,5q�$ SourcePosition��{��� *(&FurnacePlcDataDict[1].PaddleBoatSensor+ �� *SelectedSourcePosition+# [ $ 桨区2��	� $��桨区3��$��0,2,0,5q�$ SourcePosition��{��� *(&FurnacePlcDataDict[2].PaddleBoatSensor+ �� *SelectedSourcePosition+# [ $ 桨区3��	� $��桨区4��$��0,2,0,5q�$ SourcePosition��{��� *(&FurnacePlcDataDict[3].PaddleBoatSensor+ �� *SelectedSourcePosition+# [ $ 桨区4��	� $��桨区5��$��0,2,0,5q�$ SourcePosition��{��� *(&FurnacePlcDataDict[4].PaddleBoatSensor+ �� *SelectedSourcePosition+# [ $ 桨区5��	� $��桨区6��$��0,2,0,5q�$ SourcePosition��{��� *(&FurnacePlcDataDict[5].PaddleBoatSensor+ �� *SelectedSourcePosition+# [ $ 桨区6���� $	��100��$��	10,2,10,5q�$��Center=�.+��� $��启动��#��[ $��30��$��80��$��14(�$��0,2,0,5q�$��Center=����� *AutoModeStartCommand+{��� *IsAutoModeSelected+�� $��暂停��#��[ $��30��$��80��$��14(�$��0,2,0,5q�$y�Hidden=����� *PauseCommand+{��� *IsPauseEnabled+�� $��恢复��#��[ $��30��$��80��$��14(�$��0,2,0,5q�$y�Hidden=����� *ResumeCommand+{��� *IsResumeEnabled+�� $��	上一步��#��[ $��30��$��80��$��14(�$��0,2,0,5q�$y�Hidden=����� *PreviousStepCommand+{��� *IsStepEnabled+�� $��	下一步��#��[ $��30��$��80��$��14(�$��0,2,0,5q�$y�Hidden=����� *NextStepCommand+{��� *IsStepEnabled+�� $
��Auto=�$	��250��.���� $��5q�.+��� $��目标位置��$
��Bold"�$��0,0,0,5q�	� $��
暂存区1��$��0,2,0,5q�$ TargetPosition��{��� *" MotionPlcData.Storage1BoatSensor+# [  �� *SelectedTargetPosition+# [ $ 
暂存区1��	� $��
暂存区2��$��0,2,0,5q�$ TargetPosition��{��� *" MotionPlcData.Storage2BoatSensor+# [  �� *SelectedTargetPosition+# [ $ 
暂存区2��	� $��
暂存区3��$��0,2,0,5q�$ TargetPosition��{��� *" MotionPlcData.Storage3BoatSensor+# [  �� *SelectedTargetPosition+# [ $ 
暂存区3��	� $��
暂存区4��$��0,2,0,5q�$ TargetPosition��{��� *" MotionPlcData.Storage4BoatSensor+# [  �� *SelectedTargetPosition+# [ $ 
暂存区4��	� $��
暂存区5��$��0,2,0,5q�$ TargetPosition��{��� *" MotionPlcData.Storage5BoatSensor+# [  �� *SelectedTargetPosition+# [ $ 
暂存区5��	� $��
暂存区6��$��0,2,0,5q�$ TargetPosition��{��� *" MotionPlcData.Storage6BoatSensor+# [  �� *SelectedTargetPosition+# [ $ 
暂存区6��	� $��桨区1��$��0,2,0,5q�$ TargetPosition��{��� *(&FurnacePlcDataDict[0].PaddleBoatSensor+# [  �� *SelectedTargetPosition+# [ $ 桨区1��	� $��桨区2��$��0,2,0,5q�$ TargetPosition��{��� *(&FurnacePlcDataDict[1].PaddleBoatSensor+# [  �� *SelectedTargetPosition+# [ $ 桨区2��	� $��桨区3��$��0,2,0,5q�$ TargetPosition��{��� *(&FurnacePlcDataDict[2].PaddleBoatSensor+# [  �� *SelectedTargetPosition+# [ $ 桨区3��	� $��桨区4��$��0,2,0,5q�$ TargetPosition��{��� *(&FurnacePlcDataDict[3].PaddleBoatSensor+# [  �� *SelectedTargetPosition+# [ $ 桨区4��	� $��桨区5��$��0,2,0,5q�$ TargetPosition��{��� *(&FurnacePlcDataDict[4].PaddleBoatSensor+# [  �� *SelectedTargetPosition+# [ $ 桨区5��	� $��桨区6��$��0,2,0,5q�$ TargetPosition��{��� *(&FurnacePlcDataDict[5].PaddleBoatSensor+# [  �� *SelectedTargetPosition+# [ $ 桨区6��	  $��3��$��8,5,10,5q�  PrimaryHueLightBrush#���  .���� $ 
Horizontal=�$��10,10,10,10q�.+��� $ Vertical=�.+��� $��机械手移动��$��0,0,0,8q��� $ Vertical=�.+��� $��水平上轴回原点��#��[ $	��150��$��24��$��0,0,0,4q�$��11(����� *%#MoveRobotHorizontal1ToOriginCommand+{��� *IsJogModeSelected+�� $$��水平上轴移动到左限位��#��[ $	��150��$��24��$��0,0,0,4q�$��11(����� *+)MoveRobotHorizontal1ToForwardLimitCommand+{��� *IsJogModeSelected+�� $��水平下轴回原点��#��[ $	��150��$��24��$��0,0,0,4q�$��11(����� *%#MoveRobotHorizontal2ToOriginCommand+{��� *IsJogModeSelected+�� $$��水平下轴移动到右限位��#��[ $	��150��$��24��$��0,0,0,4q�$��11(����� *,*MoveRobotHorizontal2ToBackwardLimitCommand+{��� *IsJogModeSelected+�� $��垂直轴回原点��#��[ $	��150��$��24��$��0,0,0,4q�$��11(����� *" MoveRobotVerticalToOriginCommand+{��� *IsJogModeSelected+�� $!��垂直轴移动到上限位��#��[ $	��150��$��24��$��0,0,0,4q�$��11(����� *&$MoveRobotVerticalToUpperLimitCommand+{��� *IsJogModeSelected+�� $!��垂直轴移动到下限位��#��[ $	��150��$��24��$��0,0,0,4q�$��11(����� *&$MoveRobotVerticalToLowerLimitCommand+{��� *IsJogModeSelected+�� $��整体回原点��#��[ $	��150��$��24��$��0,0,0,4q�$��11(����� *MoveAllToOriginCommand+{��� *IsJogModeSelected+�� $ Vertical=�$��15,0,0,0q�.+��� $��	桨移动��$��0,0,0,8q��� $ Vertical=�.+��� $ 
Horizontal=�$��0,0,0,4q�.+��� $��桨1:��$��25��$��Center=��� $��进入炉内��#��[ $��75��$��24��$��0,4,4,0q�$��11(�$��1������ *MoveIntoFurnaceCommand+{��� *IsJogModeSelected+�� $��移动出炉��#��[ $��75��$��24��$��11(�$��1������ *MoveOutFurnaceCommand+{��� *IsJogModeSelected+�� $ 
Horizontal=�$��0,0,0,4q�.+��� $��桨2:��$��25��$��Center=��� $��进入炉内��#��[ $��75��$��24��$��0,4,4,0q�$��11(�$��2������ *MoveIntoFurnaceCommand+{��� *IsJogModeSelected+�� $��移动出炉��#��[ $��75��$��24��$��11(�$��2������ *MoveOutFurnaceCommand+{��� *IsJogModeSelected+�� $ 
Horizontal=�$��0,0,0,4q�.+��� $��桨3:��$��25��$��Center=��� $��进入炉内��#��[ $��75��$��24��$��0,4,4,0q�$��11(�$��3������ *MoveIntoFurnaceCommand+{��� *IsJogModeSelected+�� $��移动出炉��#��[ $��75��$��24��$��11(�$��3������ *MoveOutFurnaceCommand+{��� *IsJogModeSelected+�� $ 
Horizontal=�$��0,0,0,4q�.+��� $��桨4:��$��25��$��Center=��� $��进入炉内��#��[ $��75��$��24��$��0,4,4,0q�$��11(�$��4������ *MoveIntoFurnaceCommand+{��� *IsJogModeSelected+�� $��移动出炉��#��[ $��75��$��24��$��11(�$��4������ *MoveOutFurnaceCommand+{��� *IsJogModeSelected+�� $ 
Horizontal=�$��0,0,0,4q�.+��� $��桨5:��$��25��$��Center=��� $��进入炉内��#��[ $��75��$��24��$��0,4,4,0q�$��11(�$��5������ *MoveIntoFurnaceCommand+{��� *IsJogModeSelected+�� $��移动出炉��#��[ $��75��$��24��$��11(�$��5������ *MoveOutFurnaceCommand+{��� *IsJogModeSelected+�� $ 
Horizontal=�$��0,0,0,4q�.+��� $��桨6:��$��25��$��Center=��� $��进入炉内��#��[ $��75��$��24��$��0,4,4,0q�$��11(�$��6������ *MoveIntoFurnaceCommand+{��� *IsJogModeSelected+�� $��移动出炉��#��[ $��75��$��24��$��11(�$��6������ *MoveOutFurnaceCommand+{��� *IsJogModeSelected+	  $��4��$��5q�.���� $ Vertical=�.+��� $��模式切换��$��14(�$
��Bold"�$��5,5,5,10q��� #��[ $��80��$��30��$��2q����� *AutoModeCommand+���� *IsAutoModeSelected+# [ .���� $ 
Horizontal=�.+�
  $ Robot=�$��25��$��16��$��0,0,4,0q�$��Center=��� $��移舟模式��$��11(�$��Center=��� #��[ $��80��$��30��$��2q����� *JogModeCommand+���� *IsJogModeSelected+# [ .���� $ 
Horizontal=�.+�
  $ HandBackRight=�$��25��$��16��$��0,0,4,0q�$��Center=��� $��点动模式��$��11(�$��Center=��� #��[ $��80��$��30��$��2q����� *ValueModeCommand+���� *IsValueModeSelected+# [ .���� $ 
Horizontal=�.+�
  $ 
Calculator=�$��25��$��16��$��0,0,4,0q�$��Center=��� $��数值模式��$��11(�$��Center=�� $��1��$��
20,10,20,0q� �� $	��350 ��� $	��300 ��� $	��300 ��� $	��200 ��� $	��400 �.+��� $ Vertical=�$��0��$��
5,10,10,10q�.+��� $ Vertical=�.+��� $!��机械手传感器检测区��$��14(�$
��Bold"�$��5,5,5,5q�	  $��0,0,0,10q�.���� $ 
Horizontal=�.+��� $ 
Horizontal=�.+��� .+��� $��水平上轴前限位:��$��2,5,2,5q��� $��水平上轴后限位:��$��2,5,2,5q��� $��水平上轴原点:��$��2,5,2,5q��� $��水平下轴前限位:��$��2,5,2,5q��� $��水平下轴后限位:��$��2,5,2,5q��� $��水平下轴原点:��$��2,5,2,5q��� .+��� $��0,5,15,5q����� *,*MotionPlcData.RobotHorizontal1ForwardLimit+# [ �� $��0,5,15,5q����� *-+MotionPlcData.RobotHorizontal1BackwardLimit+# [ �� $��0,5,15,5q����� *+)MotionPlcData.RobotHorizontal1OriginLimit+# [ �� $��0,5,15,5q����� *,*MotionPlcData.RobotHorizontal2ForwardLimit+# [ �� $��0,5,15,5q����� *-+MotionPlcData.RobotHorizontal2BackwardLimit+# [ �� $��0,5,15,5q����� *+)MotionPlcData.RobotHorizontal2OriginLimit+# [ �� $ 
Horizontal=�.+��� .+��� $��垂直轴上限位:��$��2,5,2,5q��� $��垂直轴下限位:��$��2,5,2,5q��� $��垂直轴原点:��$��2,5,2,5q��� $��水平上轴运动:��$��2,5,2,5q��� $��水平下轴运动:��$��2,5,2,5q��� $��垂直轴运动:��$��2,5,2,5q��� .+��� $��0,5,15,5q����� *'%MotionPlcData.RobotVerticalUpperLimit+# [ �� $��0,5,15,5q����� *'%MotionPlcData.RobotVerticalLowerLimit+# [ �� $��0,5,15,5q����� *(&MotionPlcData.RobotVerticalOriginLimit+# [ �� $��0,5,15,5q����� *!MotionPlcData.Horizontal1Moving+# [ �� $��0,5,15,5q����� *!MotionPlcData.Horizontal2Moving+# [ �� $��0,5,15,5q����� *MotionPlcData.VerticalMoving+# [ �� $��机械手数值显示区��$��14(�$
��Bold"�$��5,5,5,5q�	  .���� $ 
Horizontal=�$��5q�.+��� $ Vertical=�$��0,0,10,0q�.+��� $ Vertical=�.+��� $��当前位置��$��12(�$
��Bold"�$��0,0,0,5q��� $ 
Horizontal=�$	��0,2q�.+��� $��水平上轴：��$��Center=��� $��Center=�$	��5,0q����� *(&MotionPlcData.RobotHorizontal1Position+�� $��mm��$��Center=��� $ 
Horizontal=�$	��0,2q�.+��� $��水平下轴：��$��Center=��� $��Center=�$	��5,0q����� *(&MotionPlcData.RobotHorizontal2Position+�� $��mm��$��Center=��� $ 
Horizontal=�$	��0,2q�.+��� $��垂直轴：��$��Center=��� $��Center=�$	��5,0q����� *%#MotionPlcData.RobotVerticalPosition+�� $��mm��$��Center=��� $ Vertical=�$��0,0,10,0q�.+��� $��垂直轴安全距离��$��12(�$
��Bold"�$��0,0,0,5q��� $ 
Horizontal=�$	��0,2q�.+��� $��
位置1：��$��Center=�$��45���� $
��-400��$��Center=�$	��5,0q��� $��mm��$��Center=�$��0,0,10,0q��� $��~��$��Center=�$��0,0,10,0q��� $
��-200��$��Center=�$��0,0,5,0q��� $��mm��$��Center=��� $ 
Horizontal=�$	��0,2q�.+��� $��
位置2：��$��Center=�$��45���� $��0��$��Center=�$	��5,0q��� $��mm��$��Center=�$��0,0,10,0q��� $��~��$��Center=�$��0,0,10,0q��� $	��200��$��Center=�$��0,0,5,0q��� $��mm��$��Center=��� $ 
Horizontal=�$	��0,2q�.+��� $��
位置3：��$��Center=�$��45���� $	��400��$��Center=�$	��5,0q��� $��mm��$��Center=�$��0,0,10,0q��� $��~��$��Center=�$��0,0,10,0q��� $	��600��$��Center=�$��0,0,5,0q��� $��mm��$��Center=��� $ 
Horizontal=�$	��0,2q�.+��� $��
位置4：��$��Center=�$��45���� $	��800��$��Center=�$	��5,0q��� $��mm��$��Center=�$��0,0,10,0q��� $��~��$��Center=�$��0,0,10,0q��� $
��1000��$��Center=�$��0,0,5,0q��� $��mm��$��Center=��� $ 
Horizontal=�$	��0,2q�.+��� $��
位置5：��$��Center=�$��45���� $
��1200��$��Center=�$	��5,0q��� $��mm��$��Center=�$��0,0,10,0q��� $��~��$��Center=�$��0,0,10,0q��� $
��1400��$��Center=�$��0,0,5,0q��� $��mm��$��Center=��� $ 
Horizontal=�$	��0,2q�.+��� $��
位置6：��$��Center=�$��45���� $
��1600��$��Center=�$	��5,0q��� $��mm��$��Center=�$��0,0,10,0q��� $��~��$��Center=�$��0,0,10,0q��� $
��1800��$��Center=�$��0,0,5,0q��� $��mm��$��Center=��� $ Vertical=�$��0,0,20,0q�.+��� $��限位和原点数值��$��12(�$
��Bold"�$��0,0,0,5q��� $��水平上轴��$
��Bold"�$��0,5,0,2q��� $ 
Horizontal=�$	��0,2q�.+��� $��前限位：��$��Center=�$��60���� $��0��$��Center=�$	��5,0q��� $��mm��$��Center=��� $ 
Horizontal=�$	��0,2q�.+��� $��后限位：��$��Center=�$��60���� $��0��$��Center=�$	��5,0q��� $��mm��$��Center=��� $ 
Horizontal=�$	��0,2q�.+��� $��	原点：��$��Center=�$��60���� $��0��$��Center=�$	��5,0q��� $��mm��$��Center=��� $��水平下轴��$
��Bold"�$��0,5,0,2q��� $ 
Horizontal=�$	��0,2q�.+��� $��前限位：��$��Center=�$��60���� $��0��$��Center=�$	��5,0q��� $��mm��$��Center=��� $ 
Horizontal=�$	��0,2q�.+��� $��后限位：��$��Center=�$��60���� $��0��$��Center=�$	��5,0q��� $��mm��$��Center=��� $ 
Horizontal=�$	��0,2q�.+��� $��	原点：��$��Center=�$��60���� $��0��$��Center=�$	��5,0q��� $��mm��$��Center=��� $��	垂直轴��$
��Bold"�$��0,5,0,2q��� $ 
Horizontal=�$	��0,2q�.+��� $��上限位：��$��Center=�$��60���� $��0��$��Center=�$	��5,0q��� $��mm��$��Center=��� $ 
Horizontal=�$	��0,2q�.+��� $��下限位：��$��Center=�$��60���� $��0��$��Center=�$	��5,0q��� $��mm��$��Center=��� $ 
Horizontal=�$	��0,2q�.+��� $��	原点：��$��Center=�$��60���� $��0��$��Center=�$	��5,0q��� $��mm��$��Center=��� $ Vertical=�$��1��.+��� $��桨台数值显示区��$��14(�$
��Bold"�$��5,5,5,5q�	  $��5,0,5,5q�.���� $ Vertical=�$��10q�.+��� $ Vertical=�$	��0,5q�.+��� $��桨台1��$��12(�$
��Bold"�$��0,0,0,5q��� $ 
Horizontal=�$	��0,2q�.+��� $��水平轴：��$��50���� $	��5,0q����� **(FurnacePlcDataDict[0].HorizontalPosition+�� $��mm��$��0,0,10,0q��� $��	限位：��$��40���� $��上：��$��20���� $��15������ *,*FurnacePlcDataDict[0].HorizontalUpperLimit+# [ �� $��下：��$��20���� $��15������ *,*FurnacePlcDataDict[0].HorizontalLowerLimit+# [ �� $��原：��$��20���� $��15������ *-+FurnacePlcDataDict[0].HorizontalOriginLimit+# [ �� $ 
Horizontal=�$	��0,2q�.+��� $��垂直轴：��$��50���� $	��5,0q����� *(&FurnacePlcDataDict[0].VerticalPosition+�� $��mm��$��0,0,10,0q��� $��	限位：��$��40���� $��上：��$��20���� $��15������ **(FurnacePlcDataDict[0].VerticalUpperLimit+# [ �� $��下：��$��20���� $��15������ **(FurnacePlcDataDict[0].VerticalLowerLimit+# [ �� $��原：��$��20���� $��15������ *+)FurnacePlcDataDict[0].VerticalOriginLimit+# [ �� $ Vertical=�$	��0,5q�.+��� $��桨台2��$��12(�$
��Bold"�$��0,0,0,5q��� $ 
Horizontal=�$	��0,2q�.+��� $��水平轴：��$��50���� $	��5,0q����� **(FurnacePlcDataDict[1].HorizontalPosition+�� $��mm��$��0,0,10,0q��� $��	限位：��$��40���� $��上：��$��20���� $��15������ *,*FurnacePlcDataDict[1].HorizontalUpperLimit+# [ �� $��下：��$��20���� $��15������ *,*FurnacePlcDataDict[1].HorizontalLowerLimit+# [ �� $��原：��$��20���� $��15������ *-+FurnacePlcDataDict[1].HorizontalOriginLimit+# [ �� $ 
Horizontal=�$	��0,2q�.+��� $��垂直轴：��$��50���� $	��5,0q����� *(&FurnacePlcDataDict[1].VerticalPosition+�� $��mm��$��0,0,10,0q��� $��	限位：��$��40���� $��上：��$��20���� $��15������ **(FurnacePlcDataDict[1].VerticalUpperLimit+# [ �� $��下：��$��20���� $��15������ **(FurnacePlcDataDict[1].VerticalLowerLimit+# [ �� $��原：��$��20���� $��15������ *+)FurnacePlcDataDict[1].VerticalOriginLimit+# [ �� $ Vertical=�$	��0,5q�.+��� $��桨台3��$��12(�$
��Bold"�$��0,0,0,5q��� $ 
Horizontal=�$	��0,2q�.+��� $��水平轴：��$��50���� $	��5,0q����� **(FurnacePlcDataDict[2].HorizontalPosition+�� $��mm��$��0,0,10,0q��� $��	限位：��$��40���� $��上：��$��20���� $��15������ *,*FurnacePlcDataDict[2].HorizontalUpperLimit+# [ �� $��下：��$��20���� $��15������ *,*FurnacePlcDataDict[2].HorizontalLowerLimit+# [ �� $��原：��$��20���� $��15������ *-+FurnacePlcDataDict[2].HorizontalOriginLimit+# [ �� $ 
Horizontal=�$	��0,2q�.+��� $��垂直轴：��$��50���� $	��5,0q����� *(&FurnacePlcDataDict[2].VerticalPosition+�� $��mm��$��0,0,10,0q��� $��	限位：��$��40���� $��上：��$��20���� $��15������ **(FurnacePlcDataDict[2].VerticalUpperLimit+# [ �� $��下：��$��20���� $��15������ **(FurnacePlcDataDict[2].VerticalLowerLimit+# [ �� $��原：��$��20���� $��15������ *+)FurnacePlcDataDict[2].VerticalOriginLimit+# [ �� $ Vertical=�$	��0,5q�.+��� $��桨台4��$��12(�$
��Bold"�$��0,0,0,5q��� $ 
Horizontal=�$	��0,2q�.+��� $��水平轴：��$��50���� $	��5,0q����� **(FurnacePlcDataDict[3].HorizontalPosition+�� $��mm��$��0,0,10,0q��� $��	限位：��$��40���� $��上：��$��20���� $��15������ *,*FurnacePlcDataDict[3].HorizontalUpperLimit+# [ �� $��下：��$��20���� $��15������ *,*FurnacePlcDataDict[3].HorizontalLowerLimit+# [ �� $��原：��$��20���� $��15������ *-+FurnacePlcDataDict[3].HorizontalOriginLimit+# [ �� $ 
Horizontal=�$	��0,2q�.+��� $��垂直轴：��$��50���� $	��5,0q����� *(&FurnacePlcDataDict[3].VerticalPosition+�� $��mm��$��0,0,10,0q��� $��	限位：��$��40���� $��上：��$��20���� $��15������ **(FurnacePlcDataDict[3].VerticalUpperLimit+# [ �� $��下：��$��20���� $��15������ **(FurnacePlcDataDict[3].VerticalLowerLimit+# [ �� $��原：��$��20���� $��15������ *+)FurnacePlcDataDict[3].VerticalOriginLimit+# [ �� $ Vertical=�$	��0,5q�.+��� $��桨台5��$��12(�$
��Bold"�$��0,0,0,5q��� $ 
Horizontal=�$	��0,2q�.+��� $��水平轴：��$��50���� $	��5,0q����� **(FurnacePlcDataDict[4].HorizontalPosition+�� $��mm��$��0,0,10,0q��� $��	限位：��$��40���� $��上：��$��20���� $��15������ *,*FurnacePlcDataDict[4].HorizontalUpperLimit+# [ �� $��下：��$��20���� $��15������ *,*FurnacePlcDataDict[4].HorizontalLowerLimit+# [ �� $��原：��$��20���� $��15������ *-+FurnacePlcDataDict[4].HorizontalOriginLimit+# [ �� $ 
Horizontal=�$	��0,2q�.+��� $��垂直轴：��$��50���� $	��5,0q����� *(&FurnacePlcDataDict[4].VerticalPosition+�� $��mm��$��0,0,10,0q��� $��	限位：��$��40���� $��上：��$��20���� $��15������ **(FurnacePlcDataDict[4].VerticalUpperLimit+# [ �� $��下：��$��20���� $��15������ **(FurnacePlcDataDict[4].VerticalLowerLimit+# [ �� $��原：��$��20���� $��15������ *+)FurnacePlcDataDict[4].VerticalOriginLimit+# [ �� $ Vertical=�$	��0,5q�.+��� $��桨台6��$��12(�$
��Bold"�$��0,0,0,5q��� $ 
Horizontal=�$	��0,2q�.+��� $��水平轴：��$��50���� $	��5,0q����� **(FurnacePlcDataDict[5].HorizontalPosition+�� $��mm��$��0,0,10,0q��� $��	限位：��$��40���� $��上：��$��20���� $��15������ *,*FurnacePlcDataDict[5].HorizontalUpperLimit+# [ �� $��下：��$��20���� $��15������ *,*FurnacePlcDataDict[5].HorizontalLowerLimit+# [ �� $��原：��$��20���� $��15������ *-+FurnacePlcDataDict[5].HorizontalOriginLimit+# [ �� $ 
Horizontal=�$	��0,2q�.+��� $��垂直轴：��$��50���� $	��5,0q����� *(&FurnacePlcDataDict[5].VerticalPosition+�� $��mm��$��0,0,10,0q��� $��	限位：��$��40���� $��上：��$��20���� $��15������ **(FurnacePlcDataDict[5].VerticalUpperLimit+# [ �� $��下：��$��20���� $��15������ **(FurnacePlcDataDict[5].VerticalLowerLimit+# [ �� $��原：��$��20���� $��15������ *+)FurnacePlcDataDict[5].VerticalOriginLimit+# [ �� $��2��$��3,3,10,3q�.+��� $!��桨台垂直轴安全距离��$��14(�$
��Bold"�$��5,5,5,5q�	  .���� $ Vertical=�$��10q�.+��� $ Vertical=�$	��0,5q�.+��� $��桨台1��$��12(�$
��Bold"�$��0,0,0,5q��� $ 
Horizontal=�$	��0,2q�.+��� $��垂直轴安全距离：��$	��100���� $
��-400��$	��5,0q��� $��mm��$��0,0,10,0q��� $��~��$��0,0,10,0q��� $
��-200��$��0,0,5,0q��� $��mm���� $ Vertical=�$	��0,5q�.+��� $��桨台2��$��12(�$
��Bold"�$��0,0,0,5q��� $ 
Horizontal=�$	��0,2q�.+��� $��垂直轴安全距离：��$	��100���� $��0��$	��5,0q��� $��mm��$��0,0,10,0q��� $��~��$��0,0,10,0q��� $	��200��$��0,0,5,0q��� $��mm���� $ Vertical=�$	��0,5q�.+��� $��桨台3��$��12(�$
��Bold"�$��0,0,0,5q��� $ 
Horizontal=�$	��0,2q�.+��� $��垂直轴安全距离：��$	��100���� $	��400��$	��5,0q��� $��mm��$��0,0,10,0q��� $��~��$��0,0,10,0q��� $	��600��$��0,0,5,0q��� $��mm���� $ Vertical=�$	��0,5q�.+��� $��桨台4��$��12(�$
��Bold"�$��0,0,0,5q��� $ 
Horizontal=�$	��0,2q�.+��� $��垂直轴安全距离：��$	��100���� $	��800��$	��5,0q��� $��mm��$��0,0,10,0q��� $��~��$��0,0,10,0q��� $
��1000��$��0,0,5,0q��� $��mm���� $ Vertical=�$	��0,5q�.+��� $��桨台5��$��12(�$
��Bold"�$��0,0,0,5q��� $ 
Horizontal=�$	��0,2q�.+��� $��垂直轴安全距离：��$	��100���� $
��1200��$	��5,0q��� $��mm��$��0,0,10,0q��� $��~��$��0,0,10,0q��� $
��1400��$��0,0,5,0q��� $��mm���� $ Vertical=�$	��0,5q�.+��� $��桨台6��$��12(�$
��Bold"�$��0,0,0,5q��� $ 
Horizontal=�$	��0,2q�.+��� $��垂直轴安全距离：��$	��100���� $
��1600��$	��5,0q��� $��mm��$��0,0,10,0q��� $��~��$��0,0,10,0q��� $
��1800��$��0,0,5,0q��� $��mm���� $��3��$��3,3,10,3q�.+��� $��机械手精确控制��$��14(�$
��Bold"�$��5q�	  $��0,0,0,10q�.���� $ Vertical=�$��10q�.+��� $ Vertical=�$��0,0,0,5q�.+��� $��	轴选择��$��12(�$��0,0,0,5q��� $ Vertical=�$��10,0,0,0q�.+��� $ 
Horizontal=�.+�	� $ 
AxisSelect��$��水平一轴��$��0,0,15,0q� �� *IsHorizontal1Selected+{��� *IsValueModeSelected+	� $ 
AxisSelect��$��水平二轴��$��0q� �� *IsHorizontal2Selected+{��� *IsValueModeSelected+�� $ 
Horizontal=�$��0,5,0,0q�.+�	� $ 
AxisSelect��$��	垂直轴��$��0q� �� *IsVerticalSelected+{��� *IsValueModeSelected+�� $ Vertical=�$��0,0,0,5q�.+��� $��速度选择��$��12(�$��0,0,0,5q��� $ 
Horizontal=�$��10,0,0,0q�.+�	� $ SpeedSelect��$��快速��$��0,0,15,0q� �� *IsHighSpeedSelected+{��� *IsValueModeSelected+	� $ SpeedSelect��$��慢速��$��0q� �� *IsLowSpeedSelected+{��� *IsValueModeSelected+�� $ 
Horizontal=�$��0,5,0,10q�.+��� $��位置值：��$��Center=��� $��60��$��25��$��5,0,0,0q����� *
InputValue+{��� *IsValueModeSelected+�� $��mm��$��Center=�$��5,0,0,0q���  ! MaterialDesignFlatMidBgButton#��[ $��启动��$��30��$��80������ *StartMotionCommand+{��� *IsValueModeSelected+�� $��桨精确控制��$��14(�$
��Bold"�$��5q�	  .���� $ Vertical=�$��10q�.+��� $ Vertical=�$��0,0,0,5q�.+��� $��桨台选择��$��12(�$��0,0,0,5q��� $	��120��$��10,0,0,0q� �� SelectedIndex �� *SelectedPaddleIndex+{��� *IsValueModeSelected+.@��� $��桨台1���� $��桨台2���� $��桨台3���� $��桨台4���� $��桨台5���� $��桨台6���� $ Vertical=�$��0,0,0,5q�.+��� $��	轴选择��$��12(�$��0,0,0,5q��� $ 
Horizontal=�$��10,0,0,0q�.+�	� $ ClampAxisSelect��$��	水平轴��$��0,0,15,0q� �� *IsClampHorizontalSelected+{��� *IsValueModeSelected+	� $ ClampAxisSelect��$��	垂直轴��$��0q� �� *IsClampVerticalSelected+{��� *IsValueModeSelected+�� $ Vertical=�$��0,0,0,5q�.+��� $��速度选择��$��12(�$��0,0,0,5q��� $ 
Horizontal=�$��10,0,0,0q�.+�	� $ ClampSpeedSelect��$��快速��$��0,0,15,0q� �� *IsClampHighSpeedSelected+{��� *IsValueModeSelected+	� $ ClampSpeedSelect��$��慢速��$��0q� �� *IsClampLowSpeedSelected+{��� *IsValueModeSelected+�� $ 
Horizontal=�$��0,5,0,10q�.+��� $��位置值：��$��Center=��� $��60��$��25��$��5,0,0,0q����� *ClampInputValue+{��� *IsValueModeSelected+�� $��mm��$��Center=�$��5,0,0,0q��� #��[ $��启动��$��30��$��80������ *StartClampMotionCommand+{��� *IsValueModeSelected+�� $��4��$��3,3,3,3q�.+��� $ 
Horizontal=�$��5,5,5,5q�.+��� $��事件记录��$��14(�$
��Bold"��� $��清空��#��[ $��20,0,0,0q�$��25��$��60��$��12(����� *ClearEventLogsCommand+�� ���	#FFD0D0D0$��1q�$ 3��$	��400��
�������._��� $
��Auto=�.���� $��5q����� *	EventLogs+���� .R��� $ 
Horizontal=�$	��0,2q�.+��� $��0,0,10,0q����	#FF666666���� *Time+ �� StringFormat$ HH:mm:ss���� $
��Wrap=����	#FF333333���� *	Message+