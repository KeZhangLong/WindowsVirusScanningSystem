   M S B A M L   `   `   `  ����U  QWindowsVirusScanningSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:WindowsVirusScanningSystem.ViewWindowsVirusScanningSystem.View  5      H DWPFHexaEditor, Version=2.1.7.0, Culture=neutral, PublicKeyToken=nullD2clr-namespace:WpfHexaEditor;assembly=WPFHexaEditorWpfHexaEditor 5      ]3clr-namespace:WindowsVirusScanningSystem.Converters%WindowsVirusScanningSystem.Converters  5      2    ,WindowsVirusScanningSystem.View.WpfHexEditor   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ?local-clr-namespace:WindowsVirusScanningSystem.View     5      Fcontrol2clr-namespace:WpfHexaEditor;assembly=WPFHexaEditor     5      J
Converters3clr-namespace:WindowsVirusScanningSystem.Converters     5         
Page_Style#��[  5        � 	Resources  5      C   =WindowsVirusScanningSystem.Converters.PathToFilenameConverter  FilenameConverterH   BWindowsVirusScanningSystem.Converters.VisibilityToBooleanConverter  VisibilityToBoolConverter��%:   &	       &	        5   
   6-     5   
   62   5      .��5      � $��15 80 15 20q�6   .+�5   
   ]� .+�5      o� $	��Top=�6   .@�5      m� $��26��6   $
��File��5       J� RenderTransformOrigin$ 0.656,0.769&�5      .@�5      m� -   5         m�Name$ OpenMenu��5      $��Open...��5       m� Icon 5      �� 5      $!��./Images/Open-File-Icon.ico��6$   5      �� 5      m� 5       -   5        $ SaveMenu��5       $
��Save��5"       5#      �� 5$      $��./Images/Save-Icon.ico��6$   5&      m� 5'      -   5        $��
Save as...��5(      $
{�True��5)      �� 5*      m� 5+      -   5        $ CloseFileMenu��5+      $��
Close file��5-      m� 5.      -   5        $ CloseAllFileMenu��5.      $��Close all files��50       51      �� 52      $��./Images/Close-Icon.ico��6$   54      �� 55      57      m� 58      $
��Edit��6   .@�59      m� -   5        $ UndoMenu��59      $
��Undo��5;      m� 5<      -   5        $ RedoMenu��5<      $
��Redo��5>      �� 5?      m� 5@      -   5        $ FindMenu��5@      $��Find...��5B      m� 5C      -	   5        $ ReplaceMenu��5C      $��Find and replace...��5E      m� 5F      -
   5        $ FindAllSelection��5F      $%��Find all occurence of selection��5H      �� 5I      m� 5J      -   5        $ ReverseSelection��5J      $��Reverse selection��5L      �� 5M      m� 5N      -   5        $ SetReadOnlyMenu��5N      $��	Read only��5P       m� IsCheckable$
 True��5Q      �� 5R      m� 5S      -   5        $ CopyHexaMenu��5S      $��Copy hexadecimal string��5U      m� 5V      -   5        $ CopyStringMenu��5V      $��Copy ASCII string��5X      m� 5Y      -   5        $ DeleteSelectionMenu��5Y      $��Delete selection��5[      �� 5\      m� 5]      -   5        $ SelectAllButton��5]      $��
Select all��5_      �� 5`      m� 5a      $��	Bookmarks��6   .@�5b      m� -   5        $ SetBookMarkButton��5b   #   $��Set position as bookmark��5d   #   m� 5e      -   5        $ DeleteBookmark��5e   #   $��Remove all bookmarks��5g   #   5h      �� 5i      m� 5j      $��Character table��6   .@�5k      m� -   5        $ CTableAsciiButton��5k   #   $��ASCII��5m   #   $
 True��5n   #   m� 5o      -   5        $ CTableTblButton��5o   #   $��Load TBL file��5q   #   m� 5r      -   5        $" CTableTblDefaultEbcdicButton��5r   #   $7��1Load Build-in TBL : EBCDIC with special character��5t   #   m� 5u      -   5        $&  CTableTblDefaultEbcdicnoSpButton��5u   #   $5��/Load Build-in TBL : EBCDIC no special character��5w   #   m� 5x      -   5        $! CTableTblDefaultAsciiButton��5x   #   $��Load Build-in TBL : ASCII��5z   #   5{      5|      m� 5}      -   5        $ PositionMenu��5}      $��Position��5~      .@�5      �� $��0q�6!    �� Orientation$ 
Horizontal=�5�   !   .+�5�      �� $	��100��6    $��BytePerline��5�       �� 5�      $��96��6"   $��Center=�5�   "    �� TextWrapping$
 Wrap=�5�   "   ��5�   "   �� *BytePerLine+ �� ElementName$ HexEdit�� �� UpdateSourceTrigger$ 	LostFocus=�5�   "   �� 5�      $��(Lost focus)��6    5�      �� 5�      �� 5�      $ 
Horizontal=�6!   .+�5�      �� $	��100��6    $��ByteShitLeft��5�         WpfHexaEditor.HexBox  5�      -   5        	  Name$	 HbBsl��5�   )   $��96��5�   )   $��22��5�   )   
 � ToolTip$
 ByteShiftLeft test��5�   )   �� 5�      5�      �� 5�      $ 
Horizontal=�6!   .+�5�      �� $	��100��6    $��SetPosition��5�       �� 5�      -   5         ��Name$ PositionText��5�   "   $��75��5�   "   $��Center=�5�   "   5�   "   �� 5�      -   5         ��Name$ GoPositionButton��5�   !   $��GO��5�   !   ${�False��5�   !   5�      �� 5�      $ 
Horizontal=�6!   .+�5�      �� $	��100��6    $��SetPosition��5�         5�      -   5        $	 HbSetPosition��5�   )   $��96��5�   )   $��22��5�   )   5�      5�      m� 5�      -   5        $ ViewMenu��5�      $
��View��5�      .@�5�      m� $��Header��6   $
 True��5�       m� 	IsChecked 5�      �� $ HexEdit�� �� Path$ HeaderVisibility�$ PropertyChanged=� �� 	Converter# [ m� 5�      $��Hex data panel��6   $
 True��5�       5�      �� $ HexEdit��$ HexDataVisibility�$ PropertyChanged=�# [ m� 5�      $��String data panel��6   $
 True��5�       5�      �� $ HexEdit��$ StringDataVisibility�$ PropertyChanged=�# [ m� 5�      $��Line info panel��6   $
 True��5�       5�      �� $ HexEdit��$ LineInfoVisibility�$ PropertyChanged=�# [ m� 5�      $��
Status bar��6   $
 True��5�       5�      �� $ HexEdit��$ StatusBarVisibility�$ PropertyChanged=�# [ 5�      m� 5�      -   5        $ 
OptionMenu��5�      $��Options��5�      .@�5�      m� $��Allow context menu��6   $
 True��5�       5�      �� $ HexEdit��$ AllowContextMenu�$ PropertyChanged=�m� 5�      $��Show byte ToolTip��6   $
 True��5�       5�      �� $ HexEdit��$ ShowByteToolTip�$ PropertyChanged=�m� 5�      $4��.Auto highlight bytes same as the selected byte��6   $
 True��5�       5�      �� $ HexEdit��$% AllowAutoHighLightSelectionByte�$ PropertyChanged=�m� 5�      $I��CAuto select all same byte of SelectionStart in rage at double click��6   $
 True��5�       5�      �� $ HexEdit��$* $AllowAutoSelectSameByteAtDoubleClick�$ PropertyChanged=�m� 5�      $��Allow context menu��6   $
 True��5�       5�      �� $ HexEdit��$ AllowContextMenu�$ PropertyChanged=�m� 5�      $+��%Count the number of the selected byte��6   $
 True��5�       5�      �� $ HexEdit��$ AllowByteCount�$ PropertyChanged=��� 5�      m� 5�      $%��File dropping need confirmation��6   $
 True��5�       5�      �� $ HexEdit��$ FileDroppingConfirmation�$ PropertyChanged=�m� 5�      $��Allow text dropping��6   $
 True��5�       5�      �� $ HexEdit��$ AllowTextDrop�$ PropertyChanged=�m� 5�      $��Allow file dropping��6   $
 True��5�       5�      �� $ HexEdit��$ AllowFileDrop�$ PropertyChanged=��� 5�      m� 5�      $��Hide byte are deleted��6   $
 True��5�       5�      �� $ HexEdit��$ HideByteDeleted�$ PropertyChanged=�m� 5�      $*��$Allow the possibility to delete byte��6   $
 True��5�       5�      �� $ HexEdit��$ AllowDeleteByte�$ PropertyChanged=��� 5�      m� 5�      $��Expend need confirmation��6   $
 True��5�       5�      �� $ HexEdit��$ AppendNeedConfirmation�$ PropertyChanged=�m� 5�      $!��Allow expend at end of file��6   $
 True��5�       5�      �� $ HexEdit��$ AllowExtend�$ PropertyChanged=��� 5�      m� 5�      $��Allow drop over control��6   $
 True��5�       5�      �� $ HexEdit��$ 	AllowDrop�$ PropertyChanged=�m� 5�      $��Allow file drop��6   $
 True��5�       5�      �� $ HexEdit��$ AllowFileDrop�$ PropertyChanged=�m� 5�      $��Allow text drop��6   $
 True��5�       5�      �� $ HexEdit��$ AllowTextDrop�$ PropertyChanged=��� 5�      m� 5�      $��
Allow zoom��6   $
 True��5�       5�      �� $ HexEdit��$ 	AllowZoom�$ PropertyChanged=��� 5�      -   5         ��Name$ 
ZoomSlider��5�      $	��225��5�       � LargeChange$	 0.1Q�5�       � Maximum$ 2Q�5�       � Minimum$	 0.5Q�5�      $
 Zoom editor (50% to 200%)��5�       � Value 5�      �� $ HexEdit��$ 	ZoomScale�5�      5�      � 5      � RowDefinitions 5     �� 5     $
��Auto �6$   �� 5     $��* �6$   .+�5     �� -    5         ��Name$ FileTab��5     $��0��5     $	��2,0q�5	       5      �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius J� Opacity ��Name �� Triggers P� Property  P� Value! �� 
TargetName��%@  '��       �� 5     " ��6    .�5     ��  � �� 5  "   �� 5  &   " ��66   .R�5  *   �� $��Border��61   $	��2,0q�5  1   
�������$��1,1,1,0q�5  1   $ 4,4,0,0��5  1   ._�5  .   �� $ 
Horizontal=�69   .+�5  2   �� -!   5        $��20��5  8   $��16��5  8   $	 0.5Q�5  8   $%��./Resources/Icon/Close-Icon.ico��5  8   $
 Close��5  8   �� 5  2   $ ContentSite��6C   $
��10,2q�5  C   $��Center=�5  C   $��Center=�5  C   $��Header��5   C   5!  /   5"  +    5#  *   P� 5$  .    �@��
IsSelected$
  True��5%  6   . �5&  2   �� $! Border��69    � ��
 ��·�5(  9   5)  /   P� 5*  .    �@��
IsSelected$  False��5+  6   . �5,  2   �� $! Border��69    � ��
 �����5.  9   5/  /   51  '   53     54     55     56       WpfHexaEditor.HexEditor  58     -"   5        "  Name$" HexEdit��58  $   $��1��59  $   $
��Auto��5:  $   $
��Auto��5;  $   $��2q�5<  $   #   AllowByteCount$# False��5=  $   !$   AllowCustomBackgroundBlock$
$ True��5>  $   % J� 	AllowDrop$
% True��5?  $   &   AllowExtend$& False��5@  $   '   AppendNeedConfirmation$
' True��5A  $   $��1q�5B  $   (   ByteGrouping$( FourByte=�5C  $   )   ByteSpacerPositioning$) HexBytePanel=�5E  $   *   ByteSpacerVisualStyle$
* Dash=�5F  $   +   ByteSpacerWidthTickness$+ Normal=�5G  $   ,   DataStringVisual$, Hexadecimal=�5H  $   !-   DefaultCopyToClipboardMode$- 
HexaString=�5I  $   .   ForegroundSecondColor
. ��  �/   OffSetStringVisual$/ Hexadecimal=�5K  $   0   PreloadByteInEditorMode$$0 MaxScreenVisibleLineAtDataLoad=�5L  $   1   VisualCaretMode$1 	Overwrite=�5M  $   2   ByteShiftLeft2 5D  $   �� *	LongValue+$ HbBsl��$ PropertyChanged=�5M  $   5O     5Q     5R     5S     