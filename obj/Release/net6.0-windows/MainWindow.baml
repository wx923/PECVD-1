   M S B A M L   `   `   `  ����   WpfApp4    WpfApp4.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    � Title$  
MainWindow��$
��1080��$
��1920�� � WindowState$ 	Maximized=� � WindowStyle$ SingleBorderWindow=�   MaterialDesignWindow#��[   � 	Resources   BorderStyle1 �� 
TargetType �� Property �� Value  BorderStyle2  BorderStyle3  TopNavBarStyle  MaterialDesignPaper  TopNavigationButtonStyle  MaterialDesignFlatButton �� BasedOn �� 
TargetType	 �� CornerRadius
 �� Triggers P� Property P� Value  MaterialDesignSelection &	 "MaterialDesign.Brush.Primary.Light $
  TopNavigationSelectedButtonStyle  StatusTextStyle  
TextMargin��%5  &	       &	 q     &	 �     &	 W    &	 �    7  &	
 R    &	 i    &	 �    �� " ��.���  � ��
 �������  � ��$ 1q���  �@��CornerRadius$ 5����  � ��$ 5q��� " ��.���  � ��
 �������  � ��$ 2q���  �@��CornerRadius$ 10����  � ��$ 10q��� " ��.���  � ��
 ��  ���  � ��$ 3q���  �@��CornerRadius$ 15����  � ��$ 15q��� " ��.���  � ��# �  ��  � ��
 �������  � ��$ 1q���  �@��CornerRadius$ 5����  � ��$ 5q��� " ��8   .���  � ��$	 100����  � ��$ 40����  � ��$ 3,5,5,0q���  � ��
 �������  � ��$ 1q���  �@�Cursor$
 Hand����  � �� �� " ��.R��� #��z��#��z��#��z��$	 5��._��� $��Center=�$��Center=�
 P�  �@J�IsMouseOver$
 True��. ���  � ��# �  P�  � ��$
 True��. ���  � ��# � 	 �� " ��.���  � ��$	 100����  � ��$ 40����  � ��$ 3,5,5,0q���  � ��
 �������  � ��$ 1q���  �@�Cursor$
 Hand����  � ��# � 	 ��  � �� �� " ��.R��� #��z��#��z��#��z��$	 5��._��� $��Center=�$��Center=��� " ��.���  � ��$ Segoe UI)���  � ��$ 14(���  � ��$	 0 4q��� " ��.���  � ��$ 3q�.���  � RowDefinitions �� $��25 ��� $��50 ��� $��* � � ColumnDefinitions �� $��90 ��� $��* �.+�'  !MaterialDesignThemes.Wpf.PackIcon  $��0��$��0��   Kind$
 Home=�$��32��$��32��$��Center=�$��Center=���  �� Orientation$ 
Horizontal=�$��0��$��1��.+��� $��管式PECVD设备��$��	450,0,0,0q�$��Center=�$��20(��� -    ��Name$ TimeTextBlock��$��12:34:56��$��	500,0,0,0q�$��16(�
���   �$��Right=�$��Center=��� $��3��$��0��$��0,0,1,0q�
�������._��� $ Vertical=�$
��Left=�$��Center=�.+��� -    ��Name$ BtnHome��$��主页��#��[ �� -   $ BtnMotionControl��$��运动控制��#��[ �� -   $ BtnParameterSetting��$��参数设定��#��[  � Cursor$
 Hand���� -   $ BtnControlInterface��$��炉管控制��#��[ �� -   $ BtnGlobalMonitoring��$��工艺过程��#��[ �� -   $ BtnProcessMonitoring��$��工艺监控��#��[ �� -   $ BtnMonitoringAlarm��$��监控报警��#��[ �� -	   $ BtnProcessManagement��$��配方管理��#��[ �� -
   $ BtnBoatManagement��$��	舟管理��#��[ �� $��1��$��1��$��2��#��[ ._�� .+��� $ 
Horizontal=�$��Center=�$��Center=�.+�	� -    	�Name$ BtnFurnaceTube1��$��炉管1��$	��5,0q� 	� 	GroupName$ FurnaceTubes��	� -   $ BtnFurnaceTube2��$��炉管2��$	��5,0q�$ FurnaceTubes��	� -   $ BtnFurnaceTube3��$��炉管3��$	��5,0q�$ FurnaceTubes��	� -   $ BtnFurnaceTube4��$��炉管4��$	��5,0q�$ FurnaceTubes��	� -   $ BtnFurnaceTube5��$��炉管5��$	��5,0q�$ FurnaceTubes��	� -   $ BtnFurnaceTube6��$��炉管6��$	��5,0q�$ FurnaceTubes�� � -     �Name$ 	MainFrame��$��2��$��1��  � NavigationUIVisibility$ Hidden=�