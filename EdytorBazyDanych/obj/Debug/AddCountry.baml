   M S B A M L   `   `   `  ����K  GEdytorBazyDanych, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:EdytorBazyDanychEdytorBazyDanych  5   	   !    EdytorBazyDanych.AddCountry   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:EdytorBazyDanych     5   	   -   5          � Title$  
Dodaj Kraj��5   	   $	��400��6   $	��400��6)    � WindowStartupLocation$ CenterScreen=�66   #��� ��6[    � 
ResizeMode$ NoResize=�6�    � 	Resources 5
         ButtonDesign �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius	 �� Triggers
 P� Property P� Value  TextboxDesign ��Name �� Padding ��Name �� Triggers �� 
TargetName��%'  &	        &	 �    �� 5   
   " ��6%   .�5      ��  � ��
 �����6+   �� 5       � ��
 �����6+   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $ 10��6!   #��z��63   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      	 5      P� 5      
 �@J�IsMouseOver . . �5      ��  � ��
 �   �63   5      5      �� 5    
   " ��6&   .�5!      ��  � �� 5"      �� 5#      " ��6&   .R�5$      �� $ Bd��6!   $ 10��6-   
���   �$��1q�6S   $ 5,2,0,0q�6g   ._�5%      �� $ PART_ContentHost��66   $��0q�6+   6R   5&       5'      P� 5(      
 �@J�	IsFocused . . �5)   "   �� $ Bd��6N    � ��
 �����6`   5*      5,      5.      51      54      .��56      �  � ColumnDefinitions 58   
   �� 59      $	��200 �6   �� 5:      $	��200 �6   .+�5<   
   �� -   5         ��Name$ 
exitButton��5<      $��Anuluj��6%   #��[  66   $
��Left=�6\   $��
10,293,0,0q�6w   $	��Top=�6�   $	��140��6�   $��40��6�   $��1��6�   �� 5=   
   -   5         ��Name$ NameTextBox��5=      #��[ 6'   $
��Left=�6O   $��30��6j   $��
180,30,0,0q�6v    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��160��6�   $��16(�6�   #��� ��6�   #��� ��6  $��2��6n   J� RenderTransformOrigin$ 0.5,0.5&�6�  .��6�  5?      �� 5@   
   -   5        $ okButton��5@      #��[  6#   $��OK��6I   $
��Left=�6V   $��
30,293,0,0q�6q   $	��Top=�6�   $	��140��6�   $��40��6�   �� 5A   
   -   5         ��Name$ label��5A      $��Nazwa Kraju��6   $
��Left=�65   $��	30,32,0,0q�6P   $	��Top=�6c   $��18(�6{   $	��125��6�   $��32��6�   $	��5,0q�6�   �� 5B   
   -   5        $ label1��5B      $��Flaga Kraju��6    $
��Left=�66   $��	30,93,0,0q�6Q   $	��Top=�6d   $��18(�6|   $	��125��6�   $	��5,0q�6�   �� 5C   
   -   5        $ label1_Copy��5C      $��Logo Związku��6%   $
��Left=�6<   $��
30,153,0,0q�6W   $	��Top=�6k   $��18(�6�   $	��125��6�   $	��5,0q�6�   �� 5D   
   -   5        $ FlagTextBox��5D      #��[ 6'   $��2��6N   $
��Left=�6b   $��30��6}   $��
180,90,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��160��6�   #��� ��6�   $��16(�6!  #��� ��6/   � ToolTip$ np(poland.png) 180x180��6�  $ 0.5,0.5&�6�  .��6�  5F      �� 5G   
   -	   5        $ LogoTextBox��5G      #��[ 6'   $��2��6N   $
��Left=�6b   $��30��6}   $��180,150,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��160��6�   #��� ��6�   #��� ��6"  $��16(�6t  $ 0.5,0.5&�6�  $" np(poland_asso.png), 180x180��6�  .��6�  5I      �� 5J   
   -
   5        $ deleteButton��5J      $��Usuń z Bazy��6'   #��[  6=   $
��Left=�6~   $��
65,203,0,0q�6�   $	��Top=�6�   $��75��6�   $��40��6�   $��1��6�   5L      5N      