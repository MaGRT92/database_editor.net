   M S B A M L   `   `   `  ���� K  GEdytorBazyDanych, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:EdytorBazyDanychEdytorBazyDanych  !    EdytorBazyDanych.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:EdytorBazyDanych       � Title$  Edytor Bazy Danych��$��728.902��$��715.346�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ CanMinimize=�#��� �� � 	Resources    ButtonDesign �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius	 �� Triggers
 P� Property P� Value��%�   &	        �� " ��.���  � ��
 �������  � ��
 �������  � �� �� " ��.R��� $ 10��#��z��._��� $��Center=�$��Center=�	 P� 
 �@J�IsMouseOver . . ���  � ��
 �   �.��� $��0,0,4,-3q�.+��� -    ��Name$ CountriesLabel��$��KRAJE��$��20(�$��101,114,526,536q��� -   $ TournamentsLabel��$
��LIGI��$��20(�$��320,114,330,536q��� -   $ label��$
��Left=�$��45(�$
��Bold"�$y�Hidden=�$��
170,19,0,0q�$	��Top=�����  �� ElementName$ ListViewCountries�� �� Path$ SelectedItem.Country_Name� �� Mode$ OneWay=��� -   $ label1��$
��Left=�$��45(�$y�Hidden=�$
��Bold"�$��170,357,0,0q�$	��Top=����� $ ListViewTournaments��$" SelectedItem.Tournament_Name�$ OneWay=��� -   $ IDlabel��$y�Hidden=�$
��Left=�$��45(�$
��Bold"�$��
113,19,0,0q�$	��Top=����� $ ListViewCountries��$ SelectedItem.Country_ID�$ OneWay=��� -   $ IDLigilabel��$y�Hidden=�$
��Left=�$��45(�$
��Bold"�$��
113,19,0,0q�$	��Top=����� $ ListViewTournaments��$  SelectedItem.Tournament_ID�$ OneWay=��� -   $ label2��$��KLUBY��$
��Left=�$��20(�$��524,114,0,0q�$	��Top=��� -    ��Name$ button��$��Zamknij��#��[  $
��Left=�$��587,601,0,0q�$	��Top=�$��75��$��40��
��������� -	    ��Name$ teamsListBox��$
��Left=�$	��251��$��462,152,0,0q�$	��Top=�$	��200��$��16(�#��� ��#��� �� ��%:   '��       �� " ��.���  �@��CornerRadius$ 10���� -
   $ addCountryButton��$��
Dodaj Kraj��#��[  $
��Left=�$��	19,31,0,0q�$	��Top=�$	��160��$��40���� -   $ countriesListBox��$
��Left=�$	��251��$��
34,152,0,0q�$	��Top=�$	��200��$��16(�#��� ��#��� �� ��%:   '��       �� " ��.���  �@��CornerRadius$ 10���� -   $ tournamentsListBox��$
��Left=�$	��251��$��248,152,0,0q�$	��Top=�$	��200��$��16(�#��� ��#��� �� ��%:   '��       �� " ��.���  �@��CornerRadius$ 10���� -   $ button2��$��Edytuj Kraj��#��[  $
��Left=�$��
89,408,0,0q�$	��Top=�$��75��$��40���� -   $ button3��$��Edytuj Ligę��#��[  $
��Left=�$��300,408,0,0q�$	��Top=�$��75��$��40���� -   $ button4��$��Edytuj Klub��#��[  $
��Left=�$��524,408,0,0q�$	��Top=�$��75��$��40���� -   $ addTournamentButton��$��Dodaj Ligę��#��[  $
��Left=�$��
184,31,0,0q�$	��Top=�$	��160��$��40���� -   $ button6��$��
Dodaj Klub��#��[  $
��Left=�$��
349,31,0,0q�$	��Top=�$	��160��$��40�� J� RenderTransformOrigin$ 0.5,0.5&��� -   $ button7��$��Dodaj Zawodnika/Trenera��#��[  $
��Left=�$��
514,31,0,0q�$	��Top=�$	��160��$ 0.322,0.772&�$��40���� -   $ button8��$��Trenerzy Bez Klubu��#��[  $
��Left=�$��
34,601,0,0q�$	��Top=�$	��160��$��40���� -   $ button9��$��Wyszukaj Trenera��#��[  $
��Left=�$��
34,556,0,0q�$	��Top=�$	��160��$��40���� -   $ button10��$��Wyszukaj Zawodnika��#��[  $
��Left=�$��199,556,0,0q�$	��Top=�$	��160��$��40���� -   $ button11��$��Zawodnicy Bez Klubu��#��[  $
��Left=�$��199,601,0,0q�$	��Top=�$	��160��$��40��