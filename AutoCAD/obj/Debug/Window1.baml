   M S B A M L   `   `   `  ����I  EAutoCAD, Version=1.0.8587.18058, Culture=neutral, PublicKeyToken=null!clr-namespace:AutoCADAutoCAD  5   	       AutoCAD.Window1   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:AutoCAD     5   	     � Title$  Window1��5   	   $	��460��6   $	��752��6&    � 
ResizeMode$ NoResize=�62    � ShowInTaskbar .  .��5
      �  � RowDefinitions 5   
   �� 5      $	��29* �6   �� 5      $	��68* �6   �� 5      $	��14* �6   .+�5   
   �� $��1��6   $
��Left=�6"    �� Orientation$ 
Horizontal=�6=   $��752,230,0,0q�6V   $��2��6k   $��0��6|   �� 5   
   -   5        $��	btnCancel��5      $��Cancel��6"   $
��Left=�63   $��	592,0,0,0q�6N   $��2��6a   $��Center=�6n   $��74��6�   $	��160��6�   6�   �� 5   
   -   5        $��btnOk��5      $��OK��6   $
��Left=�6+   $��	432,0,0,0q�6F   $��2��6Y   $��Center=�6f   $��74��6�   $	��160��6�   6�   �� 5   
   -   5        $��	layerName��5      $��Center=�6#   $��0,55,0,0q�6@   $��1��6R    �� TextWrapping$
 Wrap=�6_   $��TextBox��6s   $	��Top=�6�   $	��302��6�    J� RenderTransformOrigin$ 0.485,0.552&�6�   $��81��6�   �� 5   
   -   5        $��btnLayerName��5      $��Lấy layer name��6&   $
��Left=�6?   $��
557,55,0,0q�6Z   $��1��6n   $	��Top=�6{   $��81��6�   $	��161��6�   6�   5      5      