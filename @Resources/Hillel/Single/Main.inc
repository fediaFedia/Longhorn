[Variables]
TypeW=1
TypeH=1
PaddingW=5
PaddingH=5



[bg]
Meter=Image
ImageName=#@#\Graphics\separator.png
X=(5-#Padding#)
Y=(5-#Padding#)
W=(#Height#+(#Padding#*2))*#ScaleDpi#
H=(#Height#+(#Padding#*2))*#ScaleDpi#
solidcolor=0,0,0,1
MouseOverAction=[!SetOption #CURRENTSECTION# solidcolor "255,255,255,20"][!UpdateMeter bg][!ShowMeter Down][!Redraw]
MouseLeaveAction=[!SetOption #CURRENTSECTION# solidcolor "0,0,0,1"][!UpdateMeter bg][!HideMeter Down][!Redraw]

ScaleMargins=10,10,10,10


[Down]
Meter=Image
ImageName=#@#Graphics\settings.png
LeftMouseUpAction=[!SkinCustomMenu]
X=(#Height#+(#Padding#*2)-25)*#ScaleDpi#
y=0
w=(25)*#scaledpi#
ImageTint=#arrowcolor#
ImageAlpha=180
Hidden=1
MouseOverAction=[!SetOption #CURRENTSECTION# ImageAlpha 255][!Update]
MouseLeaveAction=[!SetOption #CURRENTSECTION# ImageAlpha 180][!Update]
MouseActionCusor=1

[btnstyle]
ImageTint=#colorskin2#,#opacity2#


[overlay]
Meter=Image
ImageName=#@#Common\OverlayBorder\#overlay##padding#.png
X=0
Y=0
W=(#Height#+10)*#ScaleDpi#
H=(#Height#+10)*#ScaleDpi#

[TextStyle]
X=(5+(#Height#/#xposition#))*#ScaleDpi#
Y=((#Height#/(#yposition#))+5)*#ScaleDpi#
StringAlign=#align#
FontFace=#fonttype#
FontColor=#textcolor2#,#PanelTitleOpacity2#
FontSize=#defaultfontsize#*#ScaleDpi#
AntiAlias=1
ClipString=1
w=(#Height#/1.17)*#ScaleDpi#





[FullTextStyle]
X=15
StringAlign=LEFT
Y=15
W=(#Height#-20)
H=(#Height#/1.13-20)
ClipString=1
FontColor=#TextColor2#,#paneltitleopacity#
FontSize=(#Height#/16.4)
FontFace=#fonttype#
Antialias=1
solidcolor=0,0,0,1

[IconStyle]
X=(#Height#/5.8+5)*#ScaleDpi#
Y=(#Height#/7.8+#yposition#+5)*#ScaleDpi#
W=(#Height#/1.5)*#ScaleDpi#
H=(#Height#/1.5)*#ScaleDpi#
AntiAlias=1
LeftMouseUpAction=["#PanelAction#"]
ImageName=#IconLocation#
MouseActionCursor=1
[IconStyle2]
ImageTint=#textcolor2#,#paneltitleopacity#


[FullStyle]
X=(5-#Padding#+2)
Y=(5-#Padding#+2)
W=(#Height#+(#Padding#*2)-2)*#ScaleDpi#
H=(#Height#+(#Padding#*2))*#ScaleDpi#
AntiAlias=1
ImageName=#IconLocation#
MouseActionCursor=1
LeftMouseUpAction=["#PanelAction#"]