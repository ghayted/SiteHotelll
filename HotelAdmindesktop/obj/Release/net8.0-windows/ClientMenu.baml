   M S B A M L   `   `   `  ����   
HotelAdmin    HotelAdmin.ClientView   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Gestion des Clients��$	��600��$
��1000������  �� 
StartPoint$	 0,0&� �� EndPoint$	 0,1&�.���  � Color$ #2A2A2A�� � Offset$ 0Q�� $ #1E1E1E��$ 1Q� � 	Resources  �� 
TargetType �� Property �� Value	 �� 
TargetType
 ��Name �� CornerRadius �� Padding J� SnapsToDevicePixels �� Triggers P� Property P� Value �� 
TargetName �� Triggers��%�  '��       '�� �    '�� _    '�� �    '�� *    �� " ��.���  � �� �� "	 ��.R��� $
 border��#��z�����#5A5A5A$��1q�$ 6��$ 8q�$
 True��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
 True��. ��� $ border�� � �� �#3C3C3C�� $ border�� � �� �#FFFFFFP�  � ��$
 True��. ��� $ border�� � �� �#616161��  � �� �#FFFFFF��  � ��$
 Bold"���  � ��$ 14(��� " ��.���  � �� �#3C3C3C��  � �� �#FFFFFF��  � �� �#5A5A5A��  � ��$ 1q���  � ��$ 35����  � ��$ 10q���  � ��$ Center=��� " ��.���  � �� �#FFFFFF�� " ��.���  � �� �#000000��  � �� �#FFFFFF��  � �� �#5A5A5A��  � ��$ 1q���  �@J�SnapsToDevicePixels$
 True���� " ��.���  � �� �#000000��  � �� �#FFFFFF��  � �� �#3C3C3C��  � ��$ 0,0,0,1q���  �@J�SnapsToDevicePixels$
 True�� P�  �@J�IsMouseOver$
 True��. ���  � �� �#1E1E1EP�  �@��
IsSelected$
 True��. ���  � �� �#424242��  � �� �#FFFFFF.��� .+��� ���#3C3C3C$��80��$	��Top=�._��� $��Gestion des Clients�����#FFFFFF$��26(�$��Center=�$��Center=�$
��Bold"�� $��10,90,10,10q� � ColumnDefinitions �� $��3* ��� $��5* �.+�� $��0��$��
10,0,20,10q�.+��� ���#424242$ 10��$��5,5,0,0q�._��� $��20q�.+��� $	��Nom���� -    ��Name$ 
NomTextBox��
 � Tag$ Entrez un nom...���� $��Email��$��0,15,0,0q��� -   $ EmailTextBox��$ Entrez un email...���� $��Téléphone��$��0,15,0,0q��� -   $ TelephoneTextBox��$) #Entrez un numéro de téléphone...���� $��Adresse��$��0,15,0,0q��� -   $ AdresseTextBox��$ Entrez une adresse...��� $��0,20,0,0q� �� $��* ��� $��* ��� $��* ��� $��* �.+��� -   $��Ajouter��$��40��$��5q����#2ECC71���#2ECC71$��0���� -   $��Modifier��$��40��$��5q����#E67E22���#E67E22$��1���� -   $��	Supprimer��$��40��$��5q����#E74C3C���#E74C3C$��2���� -   $��Réinitialiser��$��40��$��5q����#616161���#616161$��3��� $��1��$��	0,0,10,10q�.+��� ���#424242$ 10��$ 20q�._��� .+��� $��Liste des Clients��$��20(�$
��Bold"�$��0,0,0,20q���  �� Orientation$ 
Horizontal=�$��0,0,0,20q�.+��� -	   $ SearchTextBox��$	��300��$ Rechercher un client...���� -
   $��
Rechercher��$	��150��$��10,0,0,0q����#616161���#616161�� -   $��ClientListView�����#5A5A5A$��1q�$��5q� �� View �� .P��� $��ID�� �� Width$ 50�� �� DisplayMemberBinding �� *ID+�� $	��Nom��$	 150�� �� *Nom+�� $��Email��$	 200�� �� *Email+�� $��Téléphone��$	 120�� �� *	Telephone+�� $��Adresse��$	 200�� �� *	Adresse+�� -   $��Voir Clients Archivés��$	��250��$��	10,20,0,0q����#616161���#616161