   M S B A M L   `   `   `  ����   
HotelAdmin    HotelAdmin.ChambreMenu   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Gestion des Chambres��$	��700��$	��800�����#1E1E1E
�������.��� $��20q� � RowDefinitions �� $
��Auto ��� $��* �.+��� $��Gérer les Chambres��$��28(�$
��Bold"�$��Center=�$��0,0,0,20q���  �� Orientation$ Vertical=�$��0��.+��� $��
ID Chambre��$	��0,5q��� -    ��Name$ ChambreIDTextBox��$��30�����#333333
�������
��������� $��Type de Chambre��$��0,10,0,5q��� -    ��Name$ TypeChambreComboBox��$��30�����#DDDDDD
���   �
�������.@��� $��Suite��
 � Tag$ 1���� $��Double��$ 2���� $��Statut Chambre��$��0,10,0,5q��� -   $ StatutChambreComboBox��$��30�����#DDDDDD
���   �
�������.@��� $��
Disponible��$
 true���� $��Occupée��$ false���� $ 
Horizontal=�$��Center=�$
��0,20q�.+��� -   $��Ajouter Chambre��$	��200��$��40��$��10q����#4CAF50
����������#388E3C�� -   $��Afficher Chambres��$	��200��$��40��$��10q����#FF9800
����������#FB8C00�� -   $��Supprimer Chambre��$	��200��$��40��$��10q����#F44336
����������#D32F2F�� -    ��Name$ ChambresListBox��$��1��$
��0,20q����#333333
����������#555555$��1q�$	��250��