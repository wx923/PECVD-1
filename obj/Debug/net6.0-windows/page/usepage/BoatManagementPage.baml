   M S B A M L   `   `   `  ����B  >WpfApp4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:WpfApp4.page.usepageWpfApp4.page.usepage  5      7 clr-namespace:WpfApp4.ConvertersWpfApp4.Converters  5      -    'WpfApp4.page.usepage.BoatManagementPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:WpfApp4.page.usepage     5      _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      1
converters clr-namespace:WpfApp4.Converters  5        N� Title$  	舟管理��5       � 	Resources 5      -   'WpfApp4.Converters.BoolToColorConverter   BoolToColorConverter4   .WpfApp4.Converters.BoolToSubmitStatusConverter  BoolToSubmitStatusConverter��%:   &	        &	        5   
   6*     5   
   61   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   � $��0��6     MaterialDesignPaper#���  6   $��0,0,0,10q�6O    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��* �6#   �� 5      $
��Auto �6#   .+�5!      �� $��0��6    �� Orientation$ 
Horizontal=�6)   $
��10,5q�6B   .+�5"      �� $��数据库状态: ��6      MaterialDesignBody1TextBlock#��[ 6+   $��Center=�6a   A� 5#      $��12��6   $��12��6%   $	��5,0q�61   ��5$      �� *IsDatabaseConnected+ �� 	Converter# [  �� 5%      #��[ 6<   $��Center=�6r   ��6   �� *DatabaseStatus+6r   5&      �� 5)      $��1��6   $
��10,5q�6(   #��[ 5+      $��Center=�5,      ��5*      �� *LastOperationStatus+5,      �� 5/      $��2��6   $
��10,5q�6%     MaterialDesignIconButton#��[ 51      ��50      �� *RefreshDataCommand+.��52      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Refresh=�6*   53      54      � 57   
   $��1��6    58      �� 59      $��2* �6#   �� 5:      $��* �6#   .+�5>      � $��0��6    5?      �� 5@      $
��Auto �6$   �� 5A      $��* �6$   �� 5B      $
��Auto �6$   �� 5C      $
��Auto �6$   �� 5D      $��* �6$   .+�5H      � $��0��6    5I      �� 5J      $��* �6+   �� 5K      $
��Auto �6+   �� 5L      $
��Auto �6+   .+�5O      �� $��舟对象列表��6     $  MaterialDesignHeadline6TextBlock#��[ 5P      $��10q�5Q      �� 5S      $��1��6   $��新增舟对象��5T        MaterialDesignRaisedButton#��[ 5V      $y�Hidden=�5W      $��5q�5X      ��5U      �� *AddNewBoatCommand+5X      �� 5Z      $��2��6   $��确认修改��5[      #��[ 5]      $��5q�5^      ��5\      �� *SaveBoatChangesCommand+5^      5_      &   System.Windows.Controls.DataGrid  5a      $��1��6      AutoGenerateColumns$ False��5d         
IsReadOnly$ False��5e        MaterialDesignDataGrid#��[ 5f      -  'MaterialDesignThemes.Wpf.DataGridAssist	   CellPadding$	 13 8 8 8q�5g      
   ColumnHeaderPadding$
 8q�5h      $��10q�5i         CanUserAddRows$ False��5j      ��5b      �� *Boats+ �� SelectedItem 5c      �� *SelectedBoat+   Columns 5k      0  *System.Windows.Controls.DataGridTextColumn  5l      ,  &System.Windows.Controls.DataGridColumn   Header$ 当前位置��6-      
IsReadOnly$
 True��5n   +   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5m   +   �� *
Location+5n   +   4	  .System.Windows.Controls.DataGridTemplateColumn	  5q      $ 监控对象��61    	  CellTemplate 5r      �� 5s   "   .R�5t   &   ��  �� DisplayMemberPath$ 
BoatNumber��5w   -    �� SelectedValuePath$ 
BoatNumber��5x   -   ��5t   /   �� *DataContext.BoatMonitors+ �� RelativeSource ��  �� AncestorType Page �� SelectedValue 5v   -   �� *MonitorBoatNumber+ �� UpdateSourceTrigger$ PropertyChanged=�5y   '   5z   #   5|      5~      � 5�      $��2��6    5�      �� 5�      $��* �6+   �� 5�      $
��Auto �6+   �� 5�      $
��Auto �6+   �� 5�      $
��Auto �6+   .+�5�      �� $��舟号列表��6    #��[ 5�      $��10q�5�      �� 5�      $��1��6   $��添加空行��5�      #��[ 5�      $��5q�5�      ��5�      �� *AddEmptyRowCommand+5�      �� 5�      $��2��6   $��提交新行��5�      #��[ 5�      $��5q�5�      ��5�      �� *SubmitNewRowsCommand+5�      �� 5�      $��3��6   $��删除选中项��5�      #��[ 5�      $��5q�5�      ��5�      �� *DeleteSelectedMonitorsCommand+5�      5�        5�      $��3��6   $ False��5�      $ False��5�         SelectionMode$ Extended=�5�         SelectionUnit$ FullRow=�5�      #��[ 5�      $	 13 8 8 8q�5�      $
 8q�5�      $��10q�5�      ��5�      �� *BoatMonitors+ 5�      �� *SelectedMonitor+ 5�      4
  .System.Windows.Controls.DataGridCheckBoxColumn
  5�      $ 选择��61    - )MaterialDesignDataGridCheckBoxColumnStyle   ElementStyle# [ 5�   /    4	 0MaterialDesignDataGridCheckBoxColumnEditingStyle   EditingElementStyle# [	 5�   /      Width5  /System.Windows.Controls.DataGridLengthConverter$ 80 5�   /    5�   /   �� *
IsSelected+$ PropertyChanged=�5�   /     5�      $ 舟号��6-   $	 100 5�   +    5�   +   �� *
BoatNumber+$ PropertyChanged=�5�   +     5�      $ 工艺次数��6-   $
 True��5�   +   $	 100 5�   +    5�   +   �� *ProcessCount+5�   +   	  5�      $ 当前工艺��61   $	 150 6?    5�      �� 5�   "   .R�5�   &   �� $ ProcessName��5�   -   $ ProcessName��5�   -    �� 
IsEditable$
 True��5�   -   ��5�   /   �� *DataContext.Processes+ ��  Page �� Text 5�   -   �� *CurrentProcess+$ PropertyChanged=� 5�   -   �� *CurrentProcess+$ PropertyChanged=�5�   '   5�   #   5�        5�      $ 开始时间��6-   $
 True��5�   +   $	 150 5�   +    5�   +   �� *ProcessStartTime+5�   +     5�      $ 结束时间��6-   $
 True��5�   +   $	 150 5�   +    5�   +   �� *ProcessEndTime+5�   +   	  5�      $ 状态��61   $	 100 6=    5�      �� 5�   "   .R�5�   &   �� ��60   �� *IsSubmitted+# [ ��5�   .   �� *IsSubmitted+# [  5�   #   5�      5�      5�      � 5�      $��1��6    5�      �� 5�      $��* �6$   �� 5�      $��* �6$   .+�5�      � $��0��6   .+�5�      �  5�      �� 5�      $
��Auto �6,   �� 5�      $��* �6,   .+�5�      � $��0��6    5�      �� 5�   "   $
��Auto �63   �� 5�   "   $��* �63   �� 5�   "   $
��Auto �63   �� 5�   "   $
��Auto �63   .+�5�      �� $��0��6(   $��工艺列表��5�   '   #��[ 5�   '   $��Center=�5�   '   $��18(�5�   '   $
��Bold"�5�   '   $��15q�5�   '   �� 5      $��2��6%    
 MaterialDesignFlatButton#��[
 5  %   ���#2196F3
�������$��8q�5  %   ��5  %   �� *SaveProcessFilesCommand+.��5  "   �� $ 
Horizontal=�6-   .+�5  &     $ ContentSave=�6>   $��18��5  <   $��18��5	  <   $��Center=�5
  <   �� 5  &   $��保存修改��60   $��8 0 0 0q�5  /   $��Center=�5  /   5  #   5     �� 5     $��3��6%   #��[
 5  %   ���#F44336
�������$��8q�5  %   ��5  %   �� *" DeleteSelectedProcessFileCommand+.��5  "   �� $ 
Horizontal=�6-   .+�5  &     $ Delete=�6>   $��18��5  <   $��18��5  <   $��Center=�5  <   �� 5  &   $��删除选中��60   $��8 0 0 0q�5  /   $��Center=�5  /   5  #   5      5!       5$     -   5          Name$ ProcessFilesDataGrid��5$  #   $��1��5%  "   $ False��5'  "   #��[ 5(  "   $	 13 8 8 8q�5)  "   $
 8q�5*  "   
����������#DDDDDD$��1q�5-  "       HeadersVisibility$  Column=�5.  "   !   GridLinesVisibility$! 
Horizontal=�5/  "   "   HorizontalGridLinesBrush" �#EEEEEE$ False��51  "   $��15q�52  "   $
��Auto=�53  "   $
��Auto=�54  "   ��5&  "   �� *ProcessFiles+ 55     	  56  "   $ 选择��69   $ 60 6E    57  &   �� 58  *   .R�59  .   �� $��Center=�5:  6   $��Center=�5;  6   # d� 	IsChecked# 59  7   �� *
IsSelected+$ PropertyChanged=�5;  6   5<  +   5>  #     5?  "   $ 	文件名��65   $ 2* 5A  3   $ False��5B  3    5@  3   �� *
FileName+$ PropertyChanged=� 5C  &   �� 5D  *   $ �� 
TargetType"$ ��60   .�5E  .   �� % �� Property% � ��& �� Value$
& Wrap=�6M   �� 5F  .   % � ��$& 5q�6G   5G  +    5I  &   �� 5J  *   "$ ��60   .�5K  .   �� % �@��TextWrapping$
& Wrap=�6M   �� 5L  .   % �@��AcceptsReturn$
& True��6N   �� 5M  .   % � ��$& 5q�6G   5N  +   5P  #     5Q  "   $ 描述��65   $ 2* 5S  3   $ False��5T  3    5R  3   �� *Description+$ PropertyChanged=� 5U  &   �� 5V  *   "$ ��60   .�5W  .   �� % � ��$
& Wrap=�6M   �� 5X  .   % � ��$& 5q�6G   5Y  +    5[  &   �� 5\  *   "$ ��60   .�5]  .   �� % �@��TextWrapping$
& Wrap=�6M   �� 5^  .   % �@��AcceptsReturn$
& True��6N   �� 5_  .   % � ��$& 5q�6G   5`  +   5b  #     5c  "   $ 创建时间��65   $ * 5e  3   $
 True��5f  3    5d  3   �� *
CreateTime+' �� StringFormat$' {0:yyyy-MM-dd HH:mm:ss}�� 5g  &   �� 5h  *   "$ ��60   .�5i  .   �� % � ��$
& Wrap=�6M   �� 5j  .   % � ��$& 5q�6G   5k  +   5m  #   5o     5p     5q     � 5t     $��1��6    5u     �� 5v     $
��Auto �6(   �� 5w     $��* �6(   .+�5{     � $��0��6    5|     �� 5}     $
��Auto �6/   �� 5~     $��* �6/   �� 5     $
��Auto �6/   .+�5�     �� $��0��6$   $��工艺预约��5�  "   #��[ 5�  "   $��10q�5�  "   $��Center=�5�  "   �� 5�     $��2��6!      MaterialDesignOutlinedButton#��[ 5�  !   $��10q�5�  !   ��5�  !   �� *UpdateFurnaceProcessCommand+.��5�     �� $ 
Horizontal=�6)   .+�5�  "     $ ContentSave=�6:   $��0,0,5,0q�6M   �� 5�  "   $��更新修改��6,   5�     5�     5�       5�     $��1��6   $ False��5�     #��[ 5�     $	 13 8 8 8q�5�     $
 8q�5�     $ False��5�     
����������#DDDDDD$��1q�5�     $  Column=�5�     $! 
Horizontal=�5�     " �#EEEEEE( �� AlternationCount$( 6��5�     $��10q�5�     ��5�     �� *
Furnaces+ 5�       5�     $ 炉管编号��61   $	 100 5�  /   $
 True��5�  /    5�  /   ��  �� ) �� Mode$) FindAncestor=� DataGridRow* �� Path$%* (ItemsControl.AlternationIndex)�$' {0:D}��5�  /   	  5�     $ 工艺文件��65   $ * 6C    5�  "   �� 5�  &   .R�5�  *   �� $ FileName��5�  3   $ CollectionName��5�  3   )  #MaterialDesignThemes.Wpf.HintAssist+   Hint$+ 选择工艺文件��5�  3    " MaterialDesignOutlinedComboBox#��[ 5�  3   $��5q�5�  3   ��5�  3   �� *DataContext.ProcessFiles+ ��  DataGrid 5�  3   �� *ProcessCollectionName+$ PropertyChanged=�5�  3   5�  '   5�     5�     5�     5�     5�     5�     5�     