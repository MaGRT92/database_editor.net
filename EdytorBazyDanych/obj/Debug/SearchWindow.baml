   M S B A M L   `   `   `  ����K  GEdytorBazyDanych, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:EdytorBazyDanychEdytorBazyDanych  5   	   #    EdytorBazyDanych.SearchWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:EdytorBazyDanych     5   	   -   5          � Title$  Wyszukaj��5      $	��250��6   $	��250��6&    � WindowStartupLocation$ CenterScreen=�63   #��� ��6X    � 
ResizeMode$ NoResize=�6�    � 	Resources 5	         ButtonDesign �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius	 �� Triggers
 P� Property P� Value  TextboxDesign ��Name �� Padding ��Name �� Triggers �� 
TargetName��%'  &	        &	 �    �� 5   
   " ��6%   .�5      ��  � ��
 �����6+   �� 5       � ��
 �����6+   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $ 10��6!   #��z��63   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      	 5      P� 5      
 �@J�IsMouseOver . . �5      ��  � ��
 �   �63   5      5      �� 5   
   " ��6&   .�5       ��  � �� 5!      �� 5"      " ��6&   .R�5#      �� $ Bd��6!   $ 10��6-   
���   �$��1q�6S   $ 5,2,0,0q�6g   ._�5$      �� $ PART_ContentHost��66   $��0q�6+   6R   5%       5&      P� 5'      
 �@J�	IsFocused . . �5(   "   �� $ Bd��6N    � ��
 �����6`   5)      5+      5-      5.      50      .��52      � $��0,0,0,3q�6   .+�53   
   �� -   5         ��Name$ searchButton��53      $��Wyszukaj��6'   #��[  6:   $
��Left=�6`   $��
41,117,0,0q�6{   $	��Top=�6�   $	��160��6�   $��30��6�   6�   �� 54   
   -   5         ��Name$ searchTextBox��54      #��[ 6)   $
��Left=�6P   $��30��6k   $��	41,69,0,0q�6w    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��160��6�   $��14(�6�   $��Center=�6�   �� 55   
   -   5         ��Name$ label��55      $��Nazwisko��6   $
��Left=�62   $��	41,38,0,0q�6M   $	��Top=�6`   $��Center=�6x   $��16(�6�   $	��160��6�   57      58      