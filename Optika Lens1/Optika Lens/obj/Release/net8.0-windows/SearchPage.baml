   M S B A M L   `   `   `  ����   Optika Lens    Optika_Lens.SearchPage   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       N� Title$  Search Page��$	��900�� � 	Resources    ButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius �� Triggers P� Property	 P� Value  
TitleStyle  SearchTextBoxStyle
 ��Name  ListViewStyle��%G  &	        &	 �    &	     &	 3    '�� �    '�� U    �� " ��.���  � �� �#005F99��  � ��
 �������  � �� �#005F99��  � ��$ 1q���  � ��$ 18(���  � ��$
 Bold"���  � ��$	 150����  � ��$ 40����  � ��$ 10, 5q���  � ��$ 10, 0q���  � �� �� " ��.R��� #��z��#��z��#��z��$ 8��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
	 True��. ���  � �� �#004C7D�� " ��.���  � ��$ 28(���  � �� �#333333��  � ��$
 Bold"���  � ��$ 	0,10,0,20q��� " ��.���  � �� �#555555��  � ��$ 16(���  � ��$ 1q���  � �� �#CCCCCC��  � ��$ 8q���  � ��$	 250����  � ��$ 0,0,10,0q���  � �� �� " ��.R��� #��z��#��z��#��z��$ 5��._��� $
 PART_ContentHost���� " ��.���  � ��$ 10q���  � ��$ 1q���  � �� �#005F99��  � �� �#333333��  � ��
 �������  � ��$ 16(���  � ��$	 400���� " ��.���  � ��$ 18(���  � ��$
 Bold"���  � �� �#D3EAF9��  � ��$ 10q��� " ��.���  � ��$ 10q���  � ��$ 16(���  � ��$ Normal"���  � �� �#333333 P�  �@J�IsMouseOver$
	 True��. ���  � �� �#E0F0FF.��� 
�������$��Center=�$��Center=�$��20q� � RowDefinitions �� $
��Auto ��� $��* �.+��� $��0�� �� Orientation$ 
Horizontal=�$��10q�.+��� -   $��	txtSearch��$��Enter user name...��
�������#��[ �� -   $��Search��#��[  �� -   $��Prikaži sve��#��[  �� -   $��lvUsers��$��1��#��[ $��Disabled=� �� View �� .P��� $�� �� �� Width$ 50�� �� DisplayMemberBinding �� *Index+�� $��Ime i Prezime��$	 300�� �� *Ime_Prezime+�� $��Broj Telefona��$	 300�� �� *Broj_Telefona+