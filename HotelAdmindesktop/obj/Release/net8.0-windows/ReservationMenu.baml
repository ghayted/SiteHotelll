   M S B A M L   `   `   `  ����   
HotelAdmin     HotelAdmin.ReservationMenu   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Réservation��$	��772��$	��660�����#1E1E1E
�������.���  � RowDefinitions �� $
��Auto ��� $��* �.+���  �� Orientation$ 
Horizontal=�$��Center=�$��	0,10,0,10q�$��0��.+��� -    ��Name$ 
BtnAjouter��$��Ajout Réservation��$	��150��$��40��$��10q����#4CAF50
����������#388E3C$��1q��� -   $ BtnAfficher��$��	Affichage��$	��150��$��40��$��10q����#2196F3
����������#1976D2$��1q��� -   $ BtnSupprimer��$��Suppression��$	��150��$��40��$��10q����#F44336
����������#D32F2F$��1q�� $��20q�$��1��.+��� -    ��Name$ AjouterPanel��$y�Visible=�.+��� $��Ajouter une Réservation��$��Center=�$��24(�$��	0,20,0,10q�$
��Bold"�
��������� $��10q�.+��� $��	ID Client��$
��0,10q��� -    ��Name$ ClientIDTextBox��$	��500��$��10q�$��10(����#333333
����������#f8f8f8$��1q��� $��Type de Chambre��$
��0,10q��� -    ��Name$ ChambreTypeComboBox��$	��250��$
��0,05q�$��5q����#333333���#000000���#000000$��1q�.@��� $��Suite��
 � Tag$ 1���� $��Double��$ 2���� $��Date d'Arrivée��$
��0,10q�(  "System.Windows.Controls.DatePicker  -     Name$ DateArriveePicker��$	��250��$
��0,07q����#333333���#000000���#000000$��1q��� $��Date de Départ��$
��0,10q�  -   $ DateDepartPicker��$	��250��$
��0,07q����#333333���#000000���#000000$��1q��� -	   	 ��Name$	 PrixTextBlock��$��Prix : 0 MAD��$
��0,05q�$��16(�$
��Bold"��� $��Chambres Disponibles��$
��0,10q��� -
   $ AvailableChambresComboBox��$	��250��$
��0,10q����#333333
���   �
���   �$��1q��� -   $#��Afficher Chambres Disponibles��$	��250��$��40��$
��0,10q����#FF9800
����������#FB8C00$��1q��� -   $��Ajouter Réservation��$	��250��$��40��$
��0,10q����#4CAF50
����������#388E3C$��1q��� $
��Auto=�$��Disabled=�.���� -   $ AfficherPanel��$y�	Collapsed=�.+��� $��Rechercher Réservation��$��Center=�$��24(�$��	0,20,0,10q�$
��Bold"��� $��20q�.+��� $��	ID Client��$
��0,10q��� -   $ RechercherIDClientTextBox��$	��250��$
��0,10q����#333333
����������#555555$��1q�� $��Center=�$	��Top=�$
��0,20q�.+��� -   $'��!Afficher toutes les réservations��$	��200��$��40��$
��10,0q����#2196F3
����������#1976D2$��1q��� -   $��Trier les réservations��$	��200��$��40��$
��10,0q����#2196F3
����������#1976D2$��1q��� -   $*��$Afficher les réservations non-payer��$	��200��$��40��$
��10,0q����#2196F3
����������#1976D2$��1q��� -   $&�� Afficher les réservations Actif��$	��200��$��40��$
��10,0q����#2196F3
����������#1976D2$��1q��� -   $,��&Afficher les réservations non actives��$	��200��$��40��$
��10,0q����#2196F3
����������#1976D2$��1q��� -   $��Marquer comme Payée��$	��200��$��40��$��10q����#4CAF50
����������#388E3C$��1q��� -   $��Marquer comme Non Payée��$	��200��$��40��$��10q����#FF9800
����������#FB8C00$��1q��� -   $��Afficher Montant Dû��$	��200��$��40��$��10q����#2196F3
����������#1976D2$��1q��� -   $!��Afficher Paiements Réglés��$	��200��$��40��$��10q����#4CAF50
����������#388E3C$��1q��� -   $#��Afficher Paiements en Attente��$	��200��$��40��$��10q����#FF9800
����������#FB8C00$��1q��� -   $!��Afficher Total des Montants��$	��250��$��40��$��10q����#4CAF50
����������#388E3C$��1q��� -   $	 MontantDuTextBlock��$��Montant Dû : 0 MAD��$��Center=�$��16(�$��10,20,10,10q�$
��Bold"�
��������� -   $	 TotalMontantsTextBlock��$ ��Total des Montants : 0 MAD��$��Center=�$��16(�$��10,20,10,10q�$
��Bold"�
��������� -   $��Rechercher par Client ID��$	��250��$��40��$
��0,20q����#2196F3
����������#1976D2$��1q��� -   
 ��Name$
 ReservationsListBox��$	��600��$	��300��$
��0,20q����#333333
����������#555555$��1q��� -   $ SupprimerPanel��$y�	Collapsed=�.+��� $ ��Supprimer une Réservation��$��Center=�$��24(�$��	0,20,0,10q�$
��Bold"��� $��20q�.+��� -   $$��Voir les Réservations Actives��$	��250��$��40��$
��0,10q����#2196F3
����������#1976D2$��1q��� $��Liste des Réservations��$
��0,10q��� -    $
 ActiveReservationsListBox��$	��600��$	��200��$
��0,10q����#333333
����������#555555$��1q��� $1��+Entrez l'ID de la réservation à supprimer��$
��0,10q��� -!   $ ReservationIDTextBox��$	��250��$��30��$
��0,10q����#eeeeee�� -"    ��Name$ MessageLabel��$�� ��
���  ��$
��0,10q��� -#   $��Supprimer la Réservation��$	��250��$��40��$
��0,10q����#F44336
����������#D32F2F$��1q�