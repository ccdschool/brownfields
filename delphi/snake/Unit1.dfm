object Form1: TForm1
  Left = 876
  Top = 43
  BorderIcons = [biSystemMenu, biMinimize]
  BorderStyle = bsSingle
  Caption = '            || Snake ||||| by Salim (S@) ||'
  ClientHeight = 450
  ClientWidth = 361
  Color = clBlack
  TransparentColorValue = clGray
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  FormStyle = fsStayOnTop
  OldCreateOrder = False
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object PaintBox1: TPaintBox
    Left = 0
    Top = 0
    Width = 353
    Height = 353
    Color = clBlack
    ParentColor = False
  end
  object Panel1: TPanel
    Left = 0
    Top = 365
    Width = 361
    Height = 84
    Color = clBlack
    TabOrder = 0
    object score: TLabel
      Left = 100
      Top = 5
      Width = 4
      Height = 22
      Font.Charset = ARABIC_CHARSET
      Font.Color = clRed
      Font.Height = -13
      Font.Name = 'Andalus'
      Font.Style = [fsBold]
      ParentFont = False
    end
    object speed: TLabel
      Left = 188
      Top = 15
      Width = 55
      Height = 12
      Caption = 'Vitesse: ON '
      Font.Charset = ANSI_CHARSET
      Font.Color = clGreen
      Font.Height = -11
      Font.Name = 'Cambria'
      Font.Style = []
      ParentFont = False
      Transparent = True
    end
    object diffi: TLabel
      Left = 180
      Top = 56
      Width = 63
      Height = 12
      Caption = 'Difficult'#233': ON '
      Color = clBlack
      Font.Charset = ANSI_CHARSET
      Font.Color = clGreen
      Font.Height = -11
      Font.Name = 'Cambria'
      Font.Style = []
      ParentColor = False
      ParentFont = False
    end
    object Label1: TLabel
      Left = 96
      Top = 56
      Width = 3
      Height = 13
      Font.Charset = ANSI_CHARSET
      Font.Color = clAqua
      Font.Height = -11
      Font.Name = 'Times New Roman'
      Font.Style = [fsBold]
      ParentFont = False
    end
    object Button1: TButton
      Left = 8
      Top = 8
      Width = 75
      Height = 25
      Caption = 'Start'
      TabOrder = 0
      OnClick = Button1Click
    end
    object Button2: TButton
      Left = 8
      Top = 48
      Width = 75
      Height = 25
      Caption = 'Pause'
      TabOrder = 1
      OnClick = Button2Click
    end
    object vitesse: TTrackBar
      Left = 240
      Top = 1
      Width = 113
      Height = 35
      Cursor = crSizeWE
      Min = 2
      Position = 10
      TabOrder = 2
      TickMarks = tmBoth
    end
    object diff: TTrackBar
      Left = 240
      Top = 40
      Width = 113
      Height = 41
      Cursor = crSizeWE
      Max = 2
      Min = 1
      ParentShowHint = False
      Position = 1
      ShowHint = False
      TabOrder = 3
      TickMarks = tmBoth
    end
  end
  object Timer1: TTimer
    Enabled = False
    Interval = 50
    OnTimer = Timer1Timer
    Left = 848
    Top = 16
  end
  object Timer2: TTimer
    Enabled = False
    Interval = 15
    OnTimer = Timer2Timer
    Left = 848
    Top = 48
  end
end
