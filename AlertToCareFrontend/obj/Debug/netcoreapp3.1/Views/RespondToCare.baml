   M S B A M L   `   `   `  ����N  JAlertToCareFrontend, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:AlertToCareFrontend.ViewsAlertToCareFrontend.Views  5      O,clr-namespace:AlertToCareFrontend.ViewModelsAlertToCareFrontend.ViewModels  5      b ^Microsoft.VisualBasic.Core, Version=10.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3aaGclr-namespace:Microsoft.VisualBasic;assembly=Microsoft.VisualBasic.CoreMicrosoft.VisualBasic 5	      -    'AlertToCareFrontend.Views.RespondToCare   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:AlertToCareFrontend.Views     5      ;vm,clr-namespace:AlertToCareFrontend.ViewModels     5      Hi:http://schemas.microsoft.com/expression/2010/interactivity     5      _visualBasicGclr-namespace:Microsoft.VisualBasic;assembly=Microsoft.VisualBasic.Core     5	      .��5      � -   5        $��Respond��5      
�������  � RowDefinitions  5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5       $��* �6   �� 5!      $��* �6   �� 5"      $��* �6   �� 5#      $��* �6   �� 5$      $��* �6   .+�5'   
   �� -   5         ��Name$ 
pageBorder��5'         BorderStyle#��[  6%   $��0��6V   $��7��6f   $��7��6z   $��0��6�   �� 5(   
   $��Respond To Care��6   $��0��6-   $��0��6=   $��2��6J     HeadingStyle#��[ 6^   $
��Bold"�6�   �� 5*   
   $��Personal Information��6   #��[ 60   $��0��6b   $��2��6r   $��1��6�   �� 5,   
   $��
Patient ID��6   $��2��6&   $��2��63     ContentStyle#��[ 6C   $
��Bold"�6u   �� 5-   
   -   5        $��PatientList��5-       �� DisplayMemberPath$ 	PatientId��6L   $��2��6�   $��3��6�   $��2��6�     ComboBoxStyle#��[ 6�   ��6&   �� *PatientIdList+ �� Text 6j   ��  �� Path$ 	PatientId� �� Mode$ TwoWay=�6�   �� 5/   
     personalInfoTemplate#��[ 6W   $��0��6�   $��4��6�   $��7��6�   $��3��6�   ��6   ��  �� ElementName$ PatientList��$ SelectedItem�6�   �� 51   
   $��Vitals Information��6   $��0��6.   $��7��6>   #��[ 6K   $��3��6}   �� 53   
   $��Spo2: ��6   $��0��6"   $��9��62   #��[ 6?   �� 54   
   -   5         ��Name$ Spo2Rate��54      $��9��6$   $��1��61   $��1��6A     TextBoxStyle#��[ 6�   ��6U   �� *
Spo2Rate+{�6o   �� *	Spo2Alarm+6�   d� 55   
   -   5        $��	Spo2Alarm��55      ${�False��6(   $��9��6:   $��3��6G     ToggleButtonStyle#��[ 6�   	 d� 	IsChecked	 6W   �� $ Spo2Rate��$ TwoWay=�$
 Text� 	 spo2c
 �� 	Converter#
 [ 6�   �� 57   
   $��Blood Pressure: ��6   $��10��6,   $��0��6:   #��[ 6J   $��2��6|   �� 58   
   -   5        $ BpRate��58      $��10��6"   $��1��60   $��1��6D   #��[ 6�   ��6T   �� *BpRate+{�6l   �� *	BpAlarm+6�   d� 59   
   -   5        $��BpAlarm��59      ${�False��6&   $��10��68   $��3��6F   #��[ 6�   	 6V   �� $ BpRate��$ TwoWay=�$
 Text�  bpc#
 [ 6�   �� 5;   
   $��Respiration: ��6   $��11��6)   #��[ 67   �� 5<   
   -   5        $ RespRate��5<      $��11��6$   $��1��62   $��1��6F   #��[ 6�   ��6V   �� *
RespRate+{�6p   �� *RespRateAlarm+6�   d� 5=   
   -	   5        $��	RespAlarm��5=      ${�False��6(   $��11��6:   $��3��6H   #��[ 6�   	 6X   �� $ RespRate��$ TwoWay=�$
 Text� 		 respc#
 [	 6�   �� 5?   
   $
��Save��6   $��12��6E   $��5��6S   $��2��6c    
 ButtonStyle#��[
 6w   $��2��6�   ��6    �� $ SaveCommand�6�   �� 5@   
   $��Reset��6   $��12��6G   $��4��6U   $��2��6e   #��[
 6y   $��2��6�   ��6!   �� $ ResetCommand�6�   �� 5C   
     HorizontalSeparatorStyle#��[ 6   $��0��6R   $��7��6_   $��2��6s   �� 5D   
   #��[ 6   $��1��6R   $��7��6_   $��2��6s   �� 5E   
   #��[ 6   $��7��6R   $��	0,56,0,19q�6f   5J      5K      