   M S B A M L   `   `   `  ����  AlertToCareFrontend6    0AlertToCareFrontend.Views.IcuLayoutConfiguration   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5	      � 
�������  � RowDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ LoadExistngIcuPage��5      $��Update Existing ICU��60   $��0��6K   $��0��6X      ContentStyle#��[  6h   
����� �6�   �� 5   
   $��Floor Number: ��6   $��2��6*   $��0��67   #��[  6G   �� 5   
   -   5        $��FloorNumber��5      $��2��6�   $��1��6�     ComboBoxStyle#��[ 6�   ��6&   �� *FloorIdList+ �� Text 6J   ��  �� Path$! SelectedFloorForExistingIcu� �� Mode$ TwoWay=�6�   �� 5    
   $��Icu Number: ��6   $��3��6(   $��0��65   #��[  6E   �� 5!   
   -   5        $��IcuList��5!       �� DisplayMemberPath$ IcuNo��6D   $��3��6�   $��1��6�   #��[ 6�   ��6"   �� *	IcuIdList+ 6^   �� $ SelectedIcuNoExisting�$ TwoWay=�6�   �� 5#   
   $��Current Layout Type: ��6   #��[  61   $��4��6c   $��0��6p   �� 5$   
   -   5         ��Name$ CurrentLayout��5$      ${�False��6R   $��4��6d   $��1��6q     TextBoxStyle#��[ 6�   ��6)   �� $ LayoutTypeExisting�6�   �� 5&   
   $��Change Layout To: ��6   $��5��6.   $��0��6;   #��[  6K   �� 5'   
   -   5        $��LayoutsList��5'      $ 
LayoutType��6M   $��5��6�   $��1��6�   #��[ 6�   ��6&   �� *LayoutTypeList+ 6l   �� $! SelectedLayoutIdForExisting�$ TwoWay=�6�   �� 5)   
   $��No Of Beds In Layout: ��6   $��6��62   $��0��6?   #��[  6O   �� 5*   
   ${�False��68   $��6��6J   $��1��6W   #��[ 6g   ��6   �� *BedsInLayoutExisting+6g   �� 5.   
   -   5        $ LoadNewIcuPage��5.      $��Add New ICU��6,   $��0��6?   $��2��6L   $��3��6\   #��[  6p   
����� �6�   �� 50   
   $��Floor Number: ��6   $��2��6*   $��2��67   #��[  6G   �� 51   
   -   5        $��FloorNo��51      $��2��6z   $��3��6�   #��[ 6�   ��6"   �� *FloorIdList+ 6F   �� $ SelectedFloorNew�$ TwoWay=�6�   �� 53   
   $��Layout Id: ��6   $��3��6'   $��2��64   #��[  6D   �� 54   
   -   5        $��LayoutsListNew��54      $ 
LayoutType��6P   $��3��6�   $��3��6�   #��[ 6�   ��6)   �� *LayoutTypeList+ 6o   �� $ SelectedLayoutIdNew�$ TwoWay=�6�   �� 56   
   $��New Icu Number: ��6   #��[  6,   $��4��6^   $��2��6k   �� 57   
   -	   5        $ IcuNo��57      ${�False��6@   $��4��6R   $��3��6_   #��[ 6o   ��6!   �� $ NewIcuNo�6o   �� 59   
   $��Bed Number: ��6   $��5��6(   $��2��65   #��[  6E   �� 5:   
   ${�False��63   $��5��6E   $��3��6R   #��[ 6b   ��6   �� *BedsInLayoutNew+6b   �� 5;   
   $
��Save��6   $��3��6    $��7��60   $	��Top=�6=   $	��108��6U   $��28��6a     ButtonStyle#��[ 6m    J� RenderTransformOrigin$ 0.633,1.354&�6�   ��6�   �� $ saveCommand�6�   5<      5=      