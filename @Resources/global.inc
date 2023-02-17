[Variables]
Version=2.1
MainLanguage=English
Language=en-GB
globalblurenable=0
ScaleDpi=1
ThemeFont=Segoe UI
SubstituteFeed="&apos;":"'","&quot;":"","&Quot;":"","&amp;":"&","&lt;br&gt;":"","![CDATA[":"","]]":"","...":"","<":"",">":"","/PRE&gt;":"","PRE&gt;":"","&lt;":"","&#39;":"'","&#228;":"ä","&#246;":"ö","&#8211;":"–"
SubstituteLink="&amp;":"&","<![CDATA[":"","]]>":""

CodePage=0
textcolor2=255,255,255
colorskin2=0,0,0
DisplayAMPM=1
TimeFormat=%H:%M
Latitude=8.684170
Padding=5
Structure=Hillel


[Default]
FontColor=#textcolor2#,225
FontFace=#ThemeFont#
FontSize=(9)*#scaledpi#
StringAlign=LEFT
AntiAlias=1
InlineSetting=Shadow | 1 | 1 | 2 | 0,0,0,255
MouseOverAction=[!SetOption #CURRENTSECTION# FontColor #textcolor2#,255][!SetOption #CURRENTSECTION# InlineSetting "Shadow | 0 | 0 | 4 | 255,255,255,255"][!Update]
MouseLeaveAction=[!SetOption #CURRENTSECTION# FontColor #textcolor2#,225][!SetOption #CURRENTSECTION# InlineSetting "Shadow | 1 | 1 | 2 | 0,0,0,255"][!Update]

[Small]
FontColor=#textcolor2#,100
FontSize=(7)*#scaledpi#
InlineSetting=Shadow | 1 | 1 | 2 | 0,0,0,255
MouseOverAction=[!SetOption #CURRENTSECTION# FontColor #textcolor2#,200][!Update]
MouseLeaveAction=[!SetOption #CURRENTSECTION# FontColor #textcolor2#,100][!Update]


[SmallIcon]
MouseActionCursor=1
w=(16)*#scaledpi#
h=(16)*#scaledpi#
x=(10)*#scaledpi#
y=(12)*#scaledpi#
AntiAlias=1