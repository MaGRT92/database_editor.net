   M S B A M L   `   `   `  ����K  GEdytorBazyDanych, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:EdytorBazyDanychEdytorBazyDanych  5   	   )    #EdytorBazyDanych.CoachesWithoutClub   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:EdytorBazyDanych     5   	   -   5          � Title$  Trenerzy Bez Klubu��5      $	��600��6#   $	��450��60    � WindowStartupLocation$ CenterScreen=�6=   #��� ��6b    � 
ResizeMode$ NoResize=�6�    � 	Resources 5	         ButtonDesign �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius	 �� Triggers
 P� Property P� Value��%�  &	        �� 5   
   " ��6%   .�5      ��  � ��
 �����6+   �� 5       � ��
 �����6+   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $ 10��6!   #��z��63   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      	 5      P� 5      
 �@J�IsMouseOver . . �5      ��  � ��
 �   �63   5      5      5       .��5#      � .+�5$   
   �� -   5         ��Name$ coachesWithoutClubListBox��5$      $
��Left=�65   $	��440��6P   $��
37,100,0,0q�6]   $	��Top=�6q   $	��215��6�   #��� ��6�   #��� ��6�   $��16(�63   5%      ��%_   '��       �� 5&      " ��6   .�5'      ��  �@��CornerRadius$ 10��65   5(      5)      ��5*      �� 5+      .R�5,      � $	��0,2q�6   $��Center=�6(    � ColumnDefinitions 5-      �� 5.      $	��100 �6/   �� 5/      $	��100 �6/   .+�52      �� $��0��6?    �� TextAlignment$ Center=�6O   $��Center=�6f   ��6$   �� *	FirstName+6f   �� 53      $��1��6:   $ Center=�6J   $��Center=�6a   ��6$   �� *Name+6a   54      55      58      �� 59   
   -   5         ��Name$ label��59      $��Imię��6   $
��Left=�6.   $��	42,66,0,0q�6I   $	��Top=�6\   $��18(�6t   $	��100��6�   $��5q�6�   $��Center=�6�   �� 5:   
   -   5        $ label1��5:      $��Nazwisko��6    $
��Left=�63   $��
142,66,0,0q�6N   $	��Top=�6b   $��18(�6z   $	��100��6�   $��Center=�6�   �� 5;   
   -   5         ��Name$ closeButton��5;      $��Zamknij��6&   #��[  68   $
��Left=�6^   $��326,500,0,0q�6y   $	��Top=�6�   $��75��6�   $��40��6�   6�   �� 5<   
   -   5        $ editCoachButton��5<      #��[  6*   $��Edytuj Trenera��6P   $
��Left=�6i   $��281,100,0,0q�6�   $	��Top=�6�   $	��120��6�   $��40��6�   6�   �� 5=   
   -   5        $ coachNameLabel��5=      $
��Left=�6�   $��
722,12,0,0q�6�   $	��Top=�6�   $y�Hidden=�6�   ��6(   ��  �� ElementName$ coachesWithoutClubListBox�� �� Path$ SelectedItem.Name� �� Mode$ TwoWay=�6�   �� 5>   
   -   5        $ coachFirstNameLabel��5>      $
��Left=�6�   $��
737,12,0,0q�6�   $	��Top=�6�   $y�Hidden=�6�   ��6-   �� $ coachesWithoutClubListBox��$ SelectedItem.FirstName�$ TwoWay=�6�   5@      5A      