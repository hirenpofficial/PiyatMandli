   M S B A M L   `   `   `  ����F  BPiyatMandli, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:PiyatMandliPiyatMandli  5      H DMahApps.Metro, Version=1.6.5.1, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5      #    PiyatMandli.UCFarmerSelection   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:PiyatMandli     5      PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5      -   5          � 	Resources  5	         groupheaderstyle �� 
TargetType �� Property �� Value �� 
TargetType 9�Name 9� 
IsExpanded��%&  &	        �� 5
   
   " ��6)   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      9� $	 exp��6#    . 
�������
���   ���5      �� 5   "   $��DemiBold"�6,   $��16(�6B   ��6P   �� *Name+.��5      �� 5      5      5      5      5      .��5      � .+�5   
   �� -   5         ��Name$ 	TXTFarmer��5      $��16(�6�   ���	#FF300000$��2q�6�   $	��750��6�   $
��Left=�6�   *  $MahApps.Metro.Controls.TextBoxHelper   	Watermark$, &ખેડૂત પસંદ કરો��5      5   T   � 5   
   -   5        	 �Name$	 PPFarmer��5      
 � AllowsTransparency
 . $��Slide=�6n    � PlacementTarget 6"   ��  �� ElementName$ 	TXTFarmer��.��5      �� 
�������$��3,3,0,0q�6)   ._�5       �� 
���   �$��1,1,3,3q�6-   ._�5!      &   System.Windows.Controls.DataGrid  -   5          Name$ 
LSTRecords��5!      $��0��63      CanUserSortColumns .     CanUserAddRows .     CanUserDeleteRows .   �� ClearTypeHint$ Enabled=�5"      &   System.Windows.Media.TextOptions   TextFormattingMode$ Display=�5#         SelectionUnit$ FullRow=�5$      $ �� IsSynchronizedWithCurrentItem$ FalseY�6+     DataGridHeaderFont   ColumnHeaderStyle# [ 5%         AutoGenerateColumns .     
IsReadOnly .    SelectionMode$ Single=�5&   A   $	��750��6X    �� 
GroupStyle 5'      (  "System.Windows.Controls.GroupStyle  5(         ContainerStyle# [  6)      Panel 5)   "   �� 5*   &   .R�5+   *   >  8System.Windows.Controls.Primitives.DataGridRowsPresenter  5,   '   5.         Columns 50      4  .System.Windows.Controls.DataGridCheckBoxColumn  51        MetroDataGridCheckBox1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# �  65      EditingElementStyle# �  52   )   ,  &System.Windows.Controls.DataGridColumn   Header$ પસંદ��53   )       Binding  54   )   �� ! �� RelativeSource! �� " �� AncestorType$" DataGridRowL�# �� Path$# 
IsSelected�$ �� Mode$$ OneWay=�55      0	  *System.Windows.Controls.DataGridTextColumn	  56      $ ID��6H   %   
Visibility$% Hidden=�6T   &   Width5
  /System.Windows.Controls.DataGridLengthConverter$& 0
 6h     61   �� *Id+6h   	  57      $ સભાસદ કોડ��6P   $& *
 6c     61   �� *
FarmerCode+6c   	  58      $ શેર નં.��6M   $& *
 6^     61   �� *	ShareNo+6^   	  59      $ 	નામ��6J   $& *
 6W     61   �� *Name+6W   	  5:      $ 	ગામ��6M   $& *
 6Z     61   �� *	Village+6Z   	  5;      $ ફોન નં.��6N   $& *
 6_     61   �� *
PhoneNo1+6_   	  5<      $  મોબાઇલ નં.��6O   $& *
 6c     61   �� *	MobileNo1+6c   5>      5?      5@      5A      5B      5D      