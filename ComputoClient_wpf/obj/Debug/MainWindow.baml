   M S B A M L   `   `   `  ����  ComputoClient_wpf"    ComputoClient_wpf.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   U QMaterialDesignThemes.Wpf, Version=2.3.0.823, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   :d2http://schemas.microsoft.com/expression/blend/2008  5   	   -   5          � Title$  Sistema de Acceso��5   	   $	��600��6#   $	��800��60   $��Regular"�5	   	   $��13(�5
   	    � Topmost . &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	    � 
ResizeMode$ NoResize=�6.    � WindowStartupLocation$ CenterScreen=�6D    � WindowState$ 	Maximized=�6i    � AllowsTransparency .  � WindowStyle$
 None=�6�   	 � Icon$	 web_hi_res_512.ico��6�   
 � 	Resources
 5       �� MergedDictionaries �� Source �� 
TargetType   MaterialDesignComboBox �� BasedOn �� Property �� Value�� 5   
    5      �� 5      $f `pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Button.xaml?�6%   �� 5      $d ^pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Card.xaml?�6%   �� 5      $h bpack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.CheckBox.xaml?�6%   �� 5      $g apack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Flipper.xaml?�6%   �� 5      $h bpack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Popupbox.xaml?�6%   �� 5      $g apack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.TextBox.xaml?�6%   %�   '��       7   �� 5      " ��6   8   .�5      ��  � ��$ Stretch=�68   �� 5       � ��$ 0 8 0 8q�6+   5      5      .��5       � ��5!   
   �� 5"       �� ImageSource$3 -http://geodesia.uas.edu.mx/img/computoapp.jpg��6    j� TileMode$
 Tile=�6U    j� Stretch$ UniformToFill=�6e   .+�5$   
   (  "MaterialDesignThemes.Wpf.ColorZone  $��20q�6#   +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth2=�60      Mode$ 
PrimaryMid=�5%   -   $	��Top=�6?   ���	#FF454C74.��5&      ]� .+�5'      '  !MaterialDesignThemes.Wpf.PackIcon  $
��Left=�6*   �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.3.0.823, Culture=neutral, PublicKeyToken=null]]   Kind$ AccountBoxOutline=�6@   $��24��6Y   $��24��6e   �� 5(      $��Center=�6   $��Center=�69   $��18(�6T   42Sistema de Control de Acceso al Centro de Cómputo6�   5)      5*      �� 5+   
   $��Center=�6   $��Center=�62   .+�5,      #  MaterialDesignThemes.Wpf.Card  $��4 4 0 0q�6"   $	��665��63   $	��447��6?   .��5-      � ��5.      �� 5/      $% Resources/img/login_backpng.png��6%   .+�51        $
��Left=�6/   $��44��6J   $��44��6V   $��
289,82,0,0q�6a   $	��Top=�6u   $ AccountBoxOutline=�6�   
�������6�   �� 52      -   5         ��Name$ 
txt_cuenta��52      $
��Left=�63   $��50��6N   $��
342,76,0,0q�6Z    �� TextWrapping$
 Wrap=�6n   $�� ��6�   $	��Top=�6�   $	��258��6�    �� SelectionBrush
 �����
����������	#89FFFFFF$��Arial Black)�6�   $��36(�6	   �� 	MaxLength$ 10��6   �� MaxLines$ 1��6&   �� MinLines$ 1��63  $ Display=�6@  
������ 6�    53      $
��Left=�6/   $��44��6J   $��44��6V   $��289,203,0,0q�6a   $	��Top=�6v   $
 Lock=�6�   
�������6�   G� 54      -   5         G�Name$ txt_contraseña��54   "   $
��Left=�6:   $��50��6U   $��342,199,0,0q�6a   $	��Top=�6v   $	��258��6�    G� SelectionBrush
 �����
����������	#89FFFFFF$��Arial Black)�6�   $��36(�6�    G� 	MaxLength$ 10��6  $ Display=�6  
������ 6;  �� 55      -   5          ��Name$  label��55      $��Número de cuenta��6+   $
��Left=�6F   $��337,130,0,0q�6a   $	��Top=�6v   
�������$��Italic$�6�   �� 56      -   5        $  
label_Copy��56      $��Contraseña��60   $
��Left=�6E   $��337,252,0,0q�6`   $	��Top=�6u   
�������$��Italic$�6�   �� 57      -   5        ! ��Name$! button��57      " �� 	IsDefault" . $��INGRESAR��6>   $
��Left=�6Q   $��272,324,0,0q�6l   $	��Top=�6�   $	��346��6�   $��54��6�   ���	#FFE8ECF9���	#FF5A6379$
��Bold"�6�   $��20(�6�   6�   �� 58      -   5        $  
lbl_socket��58      $��	Servidor:��60   $
��Left=�6D   $��272,397,0,0q�6_   $	��Top=�6t   
�������$	��370��6�   $��11(�6�   5:      5;      5<      5>      5?      