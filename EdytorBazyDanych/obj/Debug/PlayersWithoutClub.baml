   M S B A M L   `   `   `  ����K  GEdytorBazyDanych, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:EdytorBazyDanychEdytorBazyDanych  5   	   )    #EdytorBazyDanych.PlayersWithoutClub   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:EdytorBazyDanych     5   	   -   5          � Title$  Zawodnicy Bez Klubu��5   	   $	��600��6%   $	��600��62    � WindowStartupLocation$ CenterScreen=�6?   #��� ��6d    � 
ResizeMode$ NoResize=�6�    � 	Resources 5	         ButtonDesign �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius	 �� Triggers
 P� Property P� Value��%�  &	        �� 5   
   " ��6%   .�5      ��  � ��
 �����6+   �� 5       � ��
 �����6+   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $ 10��6!   #��z��63   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      	 5      P� 5      
 �@J�IsMouseOver . . �5      ��  � ��
 �   �63   5      5      5       .��5#      � .+�5$   
   �� -   5         ��Name$ playersWithoutClubListBox��5$      $
��Left=�65   $	��440��6P   $��
10,100,0,0q�6]   $	��Top=�6q   $	��370��6�   #��� ��6�   #��� ��6�   $��16(�63   5%      ��%_   '��       �� 5&      " ��6   .�5'      ��  �@��CornerRadius$ 10��65   5(      5)      ��5*      �� 5+      .R�5,      � $	��0,2q�6    � ColumnDefinitions 5-      �� 5.      $	��120 �6/   �� 5/      $	��120 �6/   �� 50      $	��120 �6/   �� 51      $	��115 �6/   .+�53      �� $��0��6?    �� TextAlignment$ Center=�6O   ��6$   �� *	FirstName+6O   �� 54      $��1��6:   $ Center=�6J   ��6$   �� *Name+6J   �� 55      $��2��6$   $ Center=�6N   ��64   �� *
Position+6N   �� 56      $��3��6;   $ Center=�6K   ��6$   �� *Price+6K   57      58      5;      �� 5<   
   -   5         ��Name$ label��5<      $��Nazwisko��6   $
��Left=�62   $��	37,66,0,0q�6M   $	��Top=�6`   $��18(�6x   $��85��6�   $��5q�6�   �� 5=   
   -   5        $ label1��5=      $��Pozycja��6    $
��Left=�62   $��
165,66,0,0q�6M   $	��Top=�6a   $��18(�6y   $��70��6�   �� 5>   
   -   5        $ label2��5>      $��	Wartość��6    $
��Left=�62   $��277.5,66,0,0q�6M   $	��Top=�6c   $��18(�6{   $��75��6�   �� 5?   
   -   5         ��Name$ closeButton��5?      $��Zamknij��6&   #��[  68   $
��Left=�6^   $��493,500,0,0q�6y   $	��Top=�6�   $��75��6�   $��40��6�   6�   �� 5@   
   -   5        $ editPlayerButton��5@      #��[  6+   $��Edytuj Zawodnika��6Q   $
��Left=�6l   $��
408,97,0,0q�6�   $	��Top=�6�   $	��160��6�   $��40��6�   6�   �� 5A   
   -   5        $ transferButton��5A      #��[  6)   $��Szybkie Transfery��6O   $
��Left=�6k   $��408,168,0,0q�6�   $	��Top=�6�   $	��160��6�   $��40��6�   6�   �� 5B   
   -	   5        $ playerNameLabel��5B      $
��Left=�6�   $��
722,12,0,0q�6�   $	��Top=�6�   ��6)   ��  �� ElementName$ playersWithoutClubListBox�� �� Path$ SelectedItem.Name� �� Mode$ TwoWay=�6�   �� 5C   
   -
   5        $ playerFirstNameLabel��5C      $
��Left=�6�   $��
737,12,0,0q�6�   $	��Top=�6�   ��6.   �� $ playersWithoutClubListBox��$ SelectedItem.FirstName�$ TwoWay=�6�   5D      5E      