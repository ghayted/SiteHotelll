   M S B A M L   `   `   `  ����  
HotelAdmin    HotelAdmin.ClientView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Gestion des Clients��5   	   $	��600��6%   $
��1000��62   ��5      �� 5   
    �� 
StartPoint$	 0,0&�6    �� EndPoint$	 0,1&�6/   .��5      �  � Color$ #2A2A2A��6    � Offset$ 0Q�6+   � 5	      $ #1E1E1E��6   $ 1Q�6+   5
       � 	Resources 5       �� 
TargetType �� Property �� Value	 �� 
TargetType
 ��Name �� CornerRadius �� Padding J� SnapsToDevicePixels �� Triggers P� Property P� Value �� 
TargetName �� Triggers��%�  '��       '�� �    '��     '�� `    '�� _    �� 5   
   " ��6   .�5      ��  � �� 5      �� 5      "	 ��6&   .R�5      �� $
 border��6!   #��z��5   !   ���#5A5A5A$��1q�5   !   $ 6��5   !   $ 8q�5   !   $
 True��5   !   ._�5      �� $��Center=�6/   $��Center=�6L   5       5      P� 5       �@J�IsMouseOver$
 True��6=   . �5   "   �� $ border��6)    � �� �#3C3C3C6S   �� 5    "   $ border��6)    � �� �#FFFFFF6T   5!      P� 5#       � ��$
 True��6;   . �5$   "   �� $ border��6)    � �� �#6161616S   5%      5'      5)      �� 5*       � �� �#FFFFFF6+   �� 5+       � ��$
 Bold"�6+   �� 5,       � ��$ 14(�6)   5-      �� 50   
   " ��6   .�51      ��  � �� �#3C3C3C6+   �� 52       � �� �#FFFFFF6+   �� 53       � �� �#5A5A5A6,   �� 54       � ��$ 1q�60   �� 55       � ��$ 35��6'   �� 56       � ��$ 10q�6(   �� 57       � ��$ Center=�69   58      �� 5;   
   " ��6   .�5<      ��  � �� �#FFFFFF6+   5=      �� 5@   
   " ��6   .�5A      ��  � �� �#0000006+   �� 5C       � �� �#FFFFFF6+   �� 5E       � �� �#5A5A5A6,   �� 5F       � ��$ 1q�60   �� 5G       �@J�SnapsToDevicePixels$
 True��64   5H      �� 5K   
   " ��6   .�5L      ��  � �� �#0000006+   �� 5N       � �� �#FFFFFF6+   �� 5P       � �� �#3C3C3C6,   �� 5Q       � ��$ 0,0,0,1q�60   �� 5S       �@J�SnapsToDevicePixels$
 True��64    5T      P� 5V       �@J�IsMouseOver$
 True��61   . �5W      ��  � �� �#1E1E1E63   5Y      P� 5[       �@��
IsSelected$
 True��60   . �5\      ��  � �� �#42424263   �� 5^       � �� �#FFFFFF63   5`      5b      5c      .��5e      � .+�5g   
   �� ���#3C3C3C$��80��6&   $	��Top=�62   ._�5h      �� $��Gestion des Clients��6   ���#FFFFFF$��26(�6H   $��Center=�5i      $��Center=�63   $
��Bold"�6P   5j      � 5m   
   $��10,90,10,10q�6    � ColumnDefinitions 5n      �� 5o      $��3* �6#   �� 5p      $��5* �6#   .+�5t      � $��0��6   $��
10,0,20,10q�6#   .+�5u      �� ���#424242$ 10��6.   $��5,5,0,0q�6@   ._�5v      �� $��20q�6!   .+�5w      �� $	��Nom��6$   �� 5x      -   5         ��Name$ 
NomTextBox��5x   "   
 � Tag$ Entrez un nom...��66   �� 5z      $��Email��6$   $��0,15,0,0q�61   �� 5{      -   5        $ EmailTextBox��5{   "   $ Entrez un email...��68   �� 5}      $��Téléphone��6$   $��0,15,0,0q�65   �� 5~      -   5        $ TelephoneTextBox��5~   "   $) #Entrez un numéro de téléphone...��6<   �� 5�      $��Adresse��6$   $��0,15,0,0q�63   �� 5�      -   5        $ AdresseTextBox��5�   "   $ Entrez une adresse...��6:   � 5�      $��0,20,0,0q�6    5�      �� 5�   "   $��* �63   �� 5�   "   $��* �63   �� 5�   "   $��* �63   �� 5�   "   $��* �63   .+�5�      �� -   5        $��Ajouter��5�   %   $��40��67   $��5q�6C   ���#2ECC71���#2ECC71$��0��5�   ;   6K   �� 5�      -   5        $��Modifier��5�   %   $��40��68   $��5q�6D   ���#E67E22���#E67E22$��1��5�   ;   6K   �� 5�      -   5        $��	Supprimer��5�   %   $��40��69   $��5q�6E   ���#E74C3C���#E74C3C$��2��5�   ;   6K   �� 5�      -   5        $��Réinitialiser��5�   %   $��40��6=   $��5q�6I   ���#616161���#616161$��3��5�   ;   6K   5�      5�      5�      5�      � 5�      $��1��6   $��	0,0,10,10q�6#   .+�5�      �� ���#424242$ 10��6.   $ 20q�6@   ._�5�      �� .+�5�      �� $��Liste des Clients��6$   $��20(�6=   $
��Bold"�6K   $��0,0,0,20q�6]   �� 5�       �� Orientation$ 
Horizontal=�6%   $��0,0,0,20q�6>   .+�5�      �� -	   5        $ SearchTextBox��5�   &   $	��300��6=   $ Rechercher un client...��6I   �� 5�      -
   5        $��
Rechercher��5�   %   $	��150��6:   $��10,0,0,0q�6F   ���#616161���#6161615�   P   5�      �� 5�      -   5        $��ClientListView��5�   #   ���#5A5A5A$��1q�6O   $��5q�6c    �� View 5�      �� 5�   "   .P�5�   &   �� $��ID��65    �� Width$ 50��6A    �� DisplayMemberBinding 6L   �� *ID+�� 5�   &   $	��Nom��65   $	 150��6B    6N   �� *Nom+�� 5�   &   $��Email��65   $	 200��6D    6P   �� *Email+�� 5�   &   $��Téléphone��65   $	 120��6H    6T   �� *	Telephone+�� 5�   &   $��Adresse��65   $	 200��6F    6R   �� *	Adresse+5�   #   5�      �� 5�      -   5        $��Voir Clients Archivés��5�   !   $	��250��6A   $��	10,20,0,0q�6M   ���#616161���#6161615�   L   5�      5�      5�      5�      5�      5�      