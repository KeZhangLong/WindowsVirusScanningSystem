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
Select all��5_      �� 5`      5|      5�      � 5     ���#ffffff � RowDefinitions 5     �� 5     $
��Auto �6$   �� 5     $��* �6$   .+�5     �� -   5         ��Name$ FileTab��5     $��0��5     $	��2,0q�5	     ���#f0f0f0  5      �� 
TargetType �� Property	 �� Value
 �� 
TargetType �� CornerRadius �� Orientation J� Opacity � ToolTip ��Name �� Triggers P� Property P� Value �� 
TargetName��%@  '��       �� 5     " ��6    .�5     ��  � ��	 5  "   �� 5  &   "
 ��66   .R�5  *   �� $��Border��61   $	��2,0q�5  1   
�������$��1,1,1,0q�5  1   $ 4,4,0,0��5  1   ._�5  .   �� $ 
Horizontal=�69   .+�5  2   �� -   5        $��20��5  8   $��16��5  8   $	 0.5Q�5  8   $%��./Resources/Icon/Close-Icon.ico��5  8   $ Close��5  8   �� 5  2   $ ContentSite��6C   $
��10,2q�5  C   $��Center=�5  C   $��Center=�5   C   $��Header��5!  C   5"  /   5#  +    5$  *   P� 5%  .    �@��
IsSelected$
 True��5&  6   . �5'  2   �� $ Border��69    � ��
	 ��·�5)  9   5*  /   P� 5+  .    �@��
IsSelected$ False��5,  6   . �5-  2   �� $ Border��69    � ��
	 �����5/  9   50  /   52  '   54     55     56     57       WpfHexaEditor.HexEditor  59     -   5          Name$ HexEdit��59  $   $��1��5:  $   $
��Auto��5;  $   $
��Auto��5<  $   $��2q�5=  $      AllowByteCount$ False��5>  $   !   AllowCustomBackgroundBlock$
 True��5?  $    J� 	AllowDrop$
 True��5@  $      AllowExtend$ False��5A  $      AppendNeedConfirmation$
 True��5B  $   $��1q�5C  $      ByteGrouping$ FourByte=�5D  $      ByteSpacerPositioning$ HexBytePanel=�5F  $      ByteSpacerVisualStyle$
 Dash=�5G  $      ByteSpacerWidthTickness$ Normal=�5H  $      DataStringVisual$ Hexadecimal=�5I  $   !   DefaultCopyToClipboardMode$ 
HexaString=�5J  $       ForegroundSecondColor
  ��  �!   OffSetStringVisual$! Hexadecimal=�5L  $   "   PreloadByteInEditorMode$$" MaxScreenVisibleLineAtDataLoad=�5M  $   #   VisualCaretMode$# 	Overwrite=�5N  $   $   ByteShiftLeft$ 5E  $   �� *	LongValue+% �� ElementName$% HbBsl��& �� UpdateSourceTrigger$& PropertyChanged=�5N  $   5P     5R     5S     5T     