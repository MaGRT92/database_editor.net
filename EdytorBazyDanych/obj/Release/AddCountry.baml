   M S B A M L   `   `   `  ���� K  GEdytorBazyDanych, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:EdytorBazyDanychEdytorBazyDanych  !    EdytorBazyDanych.AddCountry   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:EdytorBazyDanych     -     � Title$  
Dodaj Kraj��$	��400��$	��400�� � WindowStartupLocation$ CenterScreen=�#��� �� � 
ResizeMode$ NoResize=� � 	Resources    ButtonDesign �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius	 �� Triggers
 P� Property P� Value��%�   &	        �� " ��.���  � ��
 �������  � ��
 �������  � �� �� " ��.R��� $ 10��#��z��._��� $��Center=�$��Center=�	 P� 
 �@J�IsMouseOver . . ���  � ��
 �   �.���  � ColumnDefinitions �� $	��200 ��� $	��200 �.+��� -    ��Name$ 
exitButton��$��Anuluj��#��[  $
��Left=�$��
10,293,0,0q�$	��Top=�$	��140��$��40��$��1���� -    ��Name$ NameTextBox��$
��Left=�$��30��$��
180,30,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��160��$��16(�#��� ��#��� ��$��2�� J� RenderTransformOrigin$ 0.5,0.5&�.���� -   $ okButton��#��[  $��OK��$
��Left=�$��
30,293,0,0q�$	��Top=�$	��140��$��40���� -    ��Name$ label��$��Nazwa Kraju��$
��Left=�$��	30,30,0,0q�$	��Top=�$��18(�$	��125��$��32��$	��5,0q��� -   $ label1��$��Flaga Kraju��$
��Left=�$��	30,91,0,0q�$	��Top=�$��18(�$	��125��$	��5,0q��� -   $ label1_Copy��$��Logo Związku��$
��Left=�$��
30,152,0,0q�$	��Top=�$��18(�$	��125��$	��5,0q��� -   $ FlagTextBox��$��2��$
��Left=�$��30��$��
180,90,0,0q�$
 Wrap=�$	��Top=�$	��160��#��� ��$��16(�#��� �� � ToolTip$ np(polska.png) 180x180��$ 0.5,0.5&�.���� -	   $ LogoTextBox��$��2��$
��Left=�$��30��$��180,150,0,0q�$
 Wrap=�$	��Top=�$	��160��#��� ��#��� ��$��16(�$ 0.5,0.5&�$* $np(związek_piłkarski.png), 180x180��.��