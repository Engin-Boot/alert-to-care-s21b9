   M S B A M L   `   `   `  ����N  JAlertToCareFrontend, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:AlertToCareFrontend.ViewsAlertToCareFrontend.Views  5      O,clr-namespace:AlertToCareFrontend.ViewModelsAlertToCareFrontend.ViewModels  5      0    *AlertToCareFrontend.Views.MonitorOccupancy   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:AlertToCareFrontend.Views     5      ;vm,clr-namespace:AlertToCareFrontend.ViewModels     5        � DataContext  5
      8   2AlertToCareFrontend.ViewModels.MonitoringOccupancy  5   
   6#   .��5      � 
������� � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5       $��* �6   �� 5!      $��* �6   �� 5"      $��* �6   .+�5&   
   �� -   5         ��Name$ 
pageBorder��5&         BorderStyle#��[  6%   $��10��6V   $��0��6h   $��6��6u   �� 5'   
   $��Monitor Occupancy��6   $��0��6-   $��2��6:     HeaderStyle#��[ 6N   �� 5(   
     HorizontalSeparatorStyle#��[ 6   $��0��6R   $��6��6_   $��2��6s   �� 5*   
   #��[ 6   $��1��6R   $��6��6_   $��2��6s   �� 5+   
   $��Occupancy Status��6   $��0��6,   $��1��6<   $��1��6I     ContentStyle#��[ 6]   �� 5-   
   $��Bed no��6   $��2��6"   $��3��6/   #��[ 6?   �� 5.   
   $��2��65   $��4��6B     ComboBoxStyle#��[ 6~   ��6   �� *	BedIdList+ �� SelectedItem 6R   �� *BedId+ �� Mode$ TwoWay=�6~   �� 5/   
   #��[ 6   $��2��6R   $��6��6_   $��2��6s   �� 51   
   $��ICU no��6   $��2��6"   $��1��6/   #��[ 6?   �� 52   
   $��2��65   $��2��6B   #��[ 6�   ��6   �� *	IcuIdList+ 6S   �� *	IcuRoomNo+$ TwoWay=�6�   �� 54   
   $��Admit : ��6   $��0��6$   $��4��64   #��[ 6A   �� 55   
   $��Discharge: ��6   $��0��6'   $��7��67   #��[ 6D   $��10,0,0,0q�6v   	� 57   
   $��4��6   $��1��6$     radioButtonStyle#��[ 6[    d� 	IsChecked 64   ��  �� Path$ AdmitStatus�6[   	� 58   
   $��7��6   $��1��6#   #��[ 6^    63   �� $ DischargeStatus�6^   �� 5:   
   $
��Save��6     ButtonStyle#��[ 6E   $��8��6v   $��4��6�   $��2��6�   $	��200��6�   ��6    �� $ SaveCommand�6�   5<      5=      