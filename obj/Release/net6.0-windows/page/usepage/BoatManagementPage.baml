   M S B A M L   `   `   `  ���� B  >WpfApp4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:WpfApp4.page.usepageWpfApp4.page.usepage  7 clr-namespace:WpfApp4.ConvertersWpfApp4.Converters  -    'WpfApp4.page.usepage.BoatManagementPage   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     4local"clr-namespace:WpfApp4.page.usepage     _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  1
converters clr-namespace:WpfApp4.Converters    N� Title$  	舟管理�� � 	Resources -   'WpfApp4.Converters.BoolToColorConverter   BoolToColorConverter4   .WpfApp4.Converters.BoolToSubmitStatusConverter  BoolToSubmitStatusConverter��%   &	        &	          .���  � RowDefinitions �� $
��Auto ��� $��* �.+�� $��0��  MaterialDesignPaper#���  $��0,0,0,10q� � ColumnDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0�� �� Orientation$ 
Horizontal=�$
��10,5q�.+��� $��数据库状态: ��   MaterialDesignBody1TextBlock#��[ $��Center=�A� $��12��$��12��$	��5,0q����� *IsDatabaseConnected+ �� 	Converter# [  �� #��[ $��Center=����� *DatabaseStatus+�� $��1��$
��10,5q�#��[ $��Center=����� *LastOperationStatus+�� $��2��$
��10,5q�  MaterialDesignIconButton#��[ ���� *RefreshDataCommand+.��'  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Refresh=�� $��1�� �� $��2* ��� $��* �.+�� $��0�� �� $
��Auto ��� $��* ��� $
��Auto ��� $
��Auto ��� $��* �.+�� $��0�� �� $��* ��� $
��Auto ��� $
��Auto �.+��� $��舟对象列表�� $  MaterialDesignHeadline6TextBlock#��[ $��10q��� $��1��$��新增舟对象��  MaterialDesignRaisedButton#��[ $y�Hidden=�$��5q����� *AddNewBoatCommand+�� $��2��$��确认修改��#��[ $��5q����� *SaveBoatChangesCommand+&   System.Windows.Controls.DataGrid  $��1��   AutoGenerateColumns$ False��   
IsReadOnly$ False��  MaterialDesignDataGrid#��[ -  'MaterialDesignThemes.Wpf.DataGridAssist	   CellPadding$	 13 8 8 8q�
   ColumnHeaderPadding$
 8q�$��10q�   CanUserAddRows$ False������ *Boats+ �� SelectedItem �� *SelectedBoat+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 当前位置��   
IsReadOnly$
 True��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *
Location+4	  .System.Windows.Controls.DataGridTemplateColumn	  $ 监控对象�� 	  CellTemplate �� .R���  �� DisplayMemberPath$ 
BoatNumber�� �� SelectedValuePath$ 
BoatNumber������ *DataContext.BoatMonitors+ �� RelativeSource ��  �� AncestorType Page �� SelectedValue �� *MonitorBoatNumber+ �� UpdateSourceTrigger$ PropertyChanged=�� $��2�� �� $��* ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��舟号列表��#��[ $��10q��� $��1��$��添加空行��#��[ $��5q����� *AddEmptyRowCommand+�� $��2��$��提交新行��#��[ $��5q����� *SubmitNewRowsCommand+�� $��3��$��删除选中项��#��[ $��5q����� *DeleteSelectedMonitorsCommand+  $��3��$ False��$ False��   SelectionMode$ Extended=�   SelectionUnit$ FullRow=�#��[ $	 13 8 8 8q�$
 8q�$��10q����� *BoatMonitors+ �� *SelectedMonitor+ 4
  .System.Windows.Controls.DataGridCheckBoxColumn
  $ 选择�� - )MaterialDesignDataGridCheckBoxColumnStyle   ElementStyle# [  4	 0MaterialDesignDataGridCheckBoxColumnEditingStyle   EditingElementStyle# [	    Width5  /System.Windows.Controls.DataGridLengthConverter$ 80  �� *
IsSelected+$ PropertyChanged=�  $ 舟号��$	 100  �� *
BoatNumber+$ PropertyChanged=�  $ 工艺次数��$
 True��$	 100  �� *ProcessCount+	  $ 当前工艺��$	 150  �� .R��� $ ProcessName��$ ProcessName�� �� 
IsEditable$
 True������ *DataContext.Processes+ ��  Page �� Text �� *CurrentProcess+$ PropertyChanged=� �� *CurrentProcess+$ PropertyChanged=�  $ 开始时间��$
 True��$	 150  �� *ProcessStartTime+  $ 结束时间��$
 True��$	 150  �� *ProcessEndTime+	  $ 状态��$	 100  �� .R��� ���� *IsSubmitted+# [ ���� *IsSubmitted+# [  � $��1�� �� $��* ��� $��* �.+�� $��0��.+��  �� $
��Auto ��� $��* �.+�� $��0�� �� $
��Auto ��� $��* ��� $
��Auto ��� $
��Auto �.+��� $��0��$��工艺列表��#��[ $��Center=�$��18(�$
��Bold"�$��15q��� $��2�� 
 MaterialDesignFlatButton#��[
 ���#2196F3
�������$��8q����� *SaveProcessFilesCommand+.���� $ 
Horizontal=�.+�  $ ContentSave=�$��18��$��18��$��Center=��� $��保存修改��$��8 0 0 0q�$��Center=��� $��3��#��[
 ���#F44336
�������$��8q����� *" DeleteSelectedProcessFileCommand+.���� $ 
Horizontal=�.+�  $ Delete=�$��18��$��18��$��Center=��� $��删除选中��$��8 0 0 0q�$��Center=�  -     Name$ ProcessFilesDataGrid��$��1��$ False��#��[ $	 13 8 8 8q�$
 8q�
����������#DDDDDD$��1q�    HeadersVisibility$  Column=�!   GridLinesVisibility$! 
Horizontal=�"   HorizontalGridLinesBrush" �#EEEEEE$ False��$��15q�$
��Auto=�$
��Auto=����� *ProcessFiles+ 	  $ 选择��$ 60  �� .R��� $��Center=�$��Center=�# d� 	IsChecked# �� *
IsSelected+$ PropertyChanged=�  $ 	文件名��$ 2* $ False�� �� *
FileName+$ PropertyChanged=� �� $ �� 
TargetType"$ ��.��� % �� Property% � ��& �� Value$
& Wrap=��� % � ��$& 5q� �� "$ ��.��� % �@��TextWrapping$
& Wrap=��� % �@��AcceptsReturn$
& True���� % � ��$& 5q�  $ 描述��$ 2* $ False�� �� *Description+$ PropertyChanged=� �� "$ ��.��� % � ��$
& Wrap=��� % � ��$& 5q� �� "$ ��.��� % �@��TextWrapping$
& Wrap=��� % �@��AcceptsReturn$
& True���� % � ��$& 5q�  $ 创建时间��$ * $
 True�� �� *
CreateTime+' �� StringFormat$' {0:yyyy-MM-dd HH:mm:ss}�� �� "$ ��.��� % � ��$
& Wrap=��� % � ��$& 5q�� $��1�� �� $
��Auto ��� $��* �.+�� $��0�� �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$��工艺预约��#��[ $��10q�$��Center=��� $��2��   MaterialDesignOutlinedButton#��[ $��10q����� *UpdateFurnaceProcessCommand+.���� $ 
Horizontal=�.+�  $ ContentSave=�$��0,0,5,0q��� $��更新修改��  $��1��$ False��#��[ $	 13 8 8 8q�$
 8q�$ False��
����������#DDDDDD$��1q�$  Column=�$! 
Horizontal=�" �#EEEEEE( �� AlternationCount$( 6��$��10q����� *
Furnaces+   $ 炉管编号��$	 100 $
 True�� ��  �� ) �� Mode$) FindAncestor=� DataGridRow* �� Path$%* (ItemsControl.AlternationIndex)�$' {0:D}��	  $ 工艺文件��$ *  �� .R��� $ FileName��$ CollectionName��)  #MaterialDesignThemes.Wpf.HintAssist+   Hint$+ 选择工艺文件�� " MaterialDesignOutlinedComboBox#��[ $��5q����� *DataContext.ProcessFiles+ ��  DataGrid �� *ProcessCollectionName+$ PropertyChanged=�