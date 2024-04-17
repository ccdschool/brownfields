unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ExtCtrls, StdCtrls, Buttons, ComCtrls,shellapi;

type
  TForm1 = class(TForm)
    Timer1: TTimer;
    PaintBox1: TPaintBox;
    Timer2: TTimer;
    Panel1: TPanel;
    Button1: TButton;
    Button2: TButton;
    vitesse: TTrackBar;
    score: TLabel;
    speed: TLabel;
    diff: TTrackBar;
    diffi: TLabel;
    Label1: TLabel;
    procedure Timer1Timer(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Timer2Timer(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
  


  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;

implementation

{$R *.dfm}
var

self1,surface:array[0..50] of array[0..50] of INTEGER;
tposfood:array[0..50] of array[0..50] of byte;
tnbfood,posx,posy,auxxfood,auxyfood:array[0..999] of byte;
tsnakelength:array[0..999] of byte;
nb,nbfood,tailleh,taillev,auxx,auxy,debut:byte;
snakelength,scoree:integer;
direction:string;
olddirection,niveau:string;
snakewidth:integer=20;
nbdown:integer=0;
nbleft:integer=0;
foodx,foody:byte  ;





procedure placefood;
begin  if niveau='difficil' then
begin
repeat
foodx:=random(tailleh-2)+2;
foody:=random(taillev-2)+2;
until surface[foody,foodx]=0;
end
else
begin
repeat
foodx:=random(tailleh)+1;
foody:=random(taillev)+1;
until surface[foody,foodx]=0;
end;
surface[foody,foodx]:=400;
  
end;


function hitfood:boolean;
begin
result:=((posx[1]=foodx)and(posy[1]=foody));
end;

function hitself1:boolean;
begin
 result:=(self1[posy[1],posx[1]]<>0)
end;


procedure initialisation;
var i,j:integer;
begin
taillev:=(form1.Width) div (snakewidth);
tailleh:=(form1.Width) div (snakewidth);
form1.PaintBox1.Width:=  (form1.Width);
form1.PaintBox1.Height:= (form1.Width);

olddirection:='right';
direction:='right';

if form1.diff.Position=2 then
 niveau:='difficil'
else
niveau :='facile';

for i:=1 to 999 do
begin
tnbfood[i]:=0;
end;


nb:=0;
debut:=1;
snakelength:=2;
nbfood:=0;

     form1.PaintBox1.canvas.Brush.Color:=clblack;
     form1.paintbox1.canvas.Pen.Width:=1;
     form1.PaintBox1.canvas.Pen.Color:=clred;
     form1.paintbox1.canvas.Rectangle(1,1,tailleh*snakewidth,taillev*snakewidth);


for i:=1 to taillev do
begin

for j:=1 to tailleh do
begin
  surface[i,j]:=0;
  if niveau='difficil' then
   begin
       surface[1,J]:=-10;
       surface[i,1]:=-10;
       surface[taillev,J]:=-10;
       surface[i,tailleh]:=-10;
   end;
end;
end;

if niveau='difficil' then
   begin
    for i:=4to 15 do
      begin
          surface[i,i]:=-10;
      end;
   end;

for i:=snakelength downto 1 do
begin
surface[2,snakelength-i+2]:=i;
end;
 posx[1]:=3;
 posy[1]:=2;
 posx[2]:=2;
 posy[2]:=2;

  placefood;
  scoree:=scoree+nbfood*5;
  form1.score.Caption:='Score: '+inttostr(scoree);
form1.label1.Caption:='Next Level:'+inttostr(10-nbfood);

  form1.PaintBox1.Canvas.Refresh;
  Form1.Button2.Caption:='Pause';
 


 end;







procedure play;
var i,j:integer;

begin
if (nbfood=10)and(niveau='facile') THEN
begin
form1.timer2.Enabled:=false;
form1.timer1.Enabled:=false;
form1.diff.Position:=2;
form1.Button1.Click;
application.MessageBox('Trés bien ::: STAGE SUIVANT','GoOoOod',0 )   ;
form1.Button1.Click;
end
else if (niveau='difficil')and(nbfood =10) then
 begin
 if form1.vitesse.Position>3 then
 begin
 form1.Timer1.Enabled:=false;
 form1.timer2.Enabled:=false;
  form1.diff.Position:=1;
  form1.Button1.Click;
   application.MessageBox('Trés bien ::: DIFFICULTE SUIVANT','GoOoOod',0 )   ;
   form1.vitesse.Position:=form1.vitesse.Position-1;
  form1.Button1.Click;
 end
 else
 begin
    form1.Timer1.Enabled:=false;
form1.timer2.Enabled:=false;
application.MessageBox('Trés bien ::: Sakart''ha ella3ba','GoOoOod',0 );
 form1.Button1.Click;
 end;


  end;
if direction='right' then
begin
     for i:= 2 to snakelength do
     begin
     auxx:=posx[i];
     auxy:=posy[i];
       surface[posy[1],posx[1]]:=surface[posy[i],posx[i]];
       posx[i]:=posx[1];
       posy[i]:=posy[1];
       surface[auxy,auxx]:=1;
       posx[1]:=auxx ;
       posy[1]:=auxy;
     end;

    surface[posy[2],posx[2]+1]:=1;
    posy[1]:=posy[2];
    posx[1]:=posx[2]+1;
    surface[auxy,auxx]:=0;
     if ((posx[2]=tailleh)and(posx[1]<>1)) then
     begin
          surface[posy[1],posx[1]]:=0;
     surface[posy[2],1]:=1;
     posy[1]:=posy[2];
    posx[1]:=1;
     end;


end;

if direction='left' then
begin


    for i:= 2 to snakelength do
     begin
       auxx:=posx[i];
       auxy:=posy[i];
       surface[posy[1],posx[1]]:=surface[auxy,auxx];
       posx[i]:=posx[1];
       posy[i]:=posy[1];
       surface[auxy,auxx]:=1;
       posx[1]:=auxx ;
       posy[1]:=auxy;
     end;

    surface[posy[2],posx[2]-1]:=1;
    posx[1]:=posx[2]-1;
    posy[1]:=posy[2];
    surface[auxy,auxx]:=0;

     if ((posx[2]=1)and(posx[1]<>tailleh)) then
     begin
            surface[posy[1],posx[1]]:=0;
      surface[posy[2],tailleh]:=1;
      posx[1]:=tailleh;
    posy[1]:=posy[2];

     end  ;
end;



if direction='up' then
begin

 for i:= 2 to snakelength do
     begin
       auxx:=posx[i];
       auxy:=posy[i];
       surface[posy[1],posx[1]]:=surface[auxy,auxx];
        posx[i]:=posx[1];
       posy[i]:=posy[1];
       surface[auxy,auxx]:=1;
       posx[1]:=auxx ;
       posy[1]:=auxy;
     end;

    surface[posy[2]-1,posx[2]]:=1;
    posx[1]:=posx[2] ;
       posy[1]:=posy[2]-1;
    surface[auxy,auxx]:=0;

    if ((posy[2]=1)and(posy[1]<>taillev)) then
     begin
     surface[posy[1],posx[1]]:=0;
     surface[taillev,posx[2]]:=1;
          posy[1]:=taillev;
           posx[1]:=posx[2];

       end ;
end;


if direction='down' then
begin
    for i:= 2 to snakelength do
     begin
       auxx:=posx[i];
       auxy:=posy[i];
       surface[posy[1],posx[1]]:=surface[auxy,auxx];
        posx[i]:=posx[1];
       posy[i]:=posy[1];
       surface[auxy,auxx]:=1;
       posx[1]:=auxx ;
       posy[1]:=auxy;
     end;

    
    surface[posy[2]+1,posx[2]]:=1;
    posx[1]:=posx[2] ;
       posy[1]:=posy[2]+1;
    surface[auxy,auxx]:=0;
    
  if ((posy[2]=taillev)and(posy[1]<>1)) then
     begin
          surface[posy[1],posx[1]]:=0;
      surface[1,posx[2]]:=1;
      posx[1]:=posx[2];
       posy[1]:=1;
     end ;

end;



 //peinture
for i:=1 to taillev do
begin
    for j:=1 to tailleh do
    begin
     if surface[i,j] in [2..snakelength] then
     begin
     form1.paintbox1.canvas.Brush.Color:=clwhite;
     form1.paintbox1.canvas.Pen.Width:=1;
     form1.paintbox1.canvas.Pen.Color:=clblue;
     form1.paintbox1.canvas.Rectangle((j*snakewidth)-snakewidth+1,(i*snakewidth)-snakewidth+1,(j*snakewidth)+1,(i*snakewidth)+1);
     end
     else if surface[i,j]=1 then
     begin
     form1.paintbox1.canvas.Brush.Color:=clskyblue;
     form1.paintbox1.canvas.Pen.Width:=1;
     form1.paintbox1.canvas.Pen.Color:=clblue;
     form1.paintbox1.canvas.Rectangle((j*snakewidth)-snakewidth+1,(i*snakewidth)-snakewidth+1,(j*snakewidth)+1,(i*snakewidth)+1);
     end
     else if  surface[i,j]=400 then
     begin
       form1.paintbox1.canvas.Brush.Color:=clgreen;
     form1.paintbox1.canvas.Pen.Width:=1;
     form1.paintbox1.canvas.Pen.Color:=Clwhite;
     form1.paintbox1.canvas.ellipse((j*snakewidth)-snakewidth+1,(i*snakewidth)-snakewidth+1,(j*snakewidth)+1,(i*snakewidth)+1);
     end
      else if  surface[i,j]=-10 then
     begin
       form1.paintbox1.canvas.Brush.Color:=clRED;
     form1.paintbox1.canvas.Pen.Width:=1;
     form1.paintbox1.canvas.Pen.Color:=Clwhite;
     form1.paintbox1.canvas.RECTANGLE((j*snakewidth)-snakewidth+1,(i*snakewidth)-snakewidth+1,(j*snakewidth)+1,(i*snakewidth)+1);
     end

     else
     begin
     form1.paintbox1.canvas.Brush.Color:=clblack;
     form1.paintbox1.canvas.Pen.Width:=1;
     form1.paintbox1.canvas.Pen.Color:=Clblack;
     form1.paintbox1.canvas.rectangle((j*snakewidth)-snakewidth+1,(i*snakewidth)-snakewidth+1,(j*snakewidth)+1,(i*snakewidth)+1);
     end;
     end;
 end;
      
//fin dessin




//if hitfood//
if hitfood then
begin
nbfood:=nbfood+1;
nb:=1;
auxxfood[nb]:=foodx;
auxyfood[nb]:=foody;
tposfood[foody,foodx]:=nbfood;
tsnakelength[nbfood]:=snakelength+nbfood;
placefood;
scoree:=scoree+ nbfood*5;
form1.score.Caption:='Score: '+inttostr(scoree);
form1.label1.Caption:='Next Level:'+inttostr(10-nbfood);
end
else
begin
//kill

if hitself1 then
begin
windows.Beep(2000,500);
     form1.paintbox1.canvas.Brush.Color:=clolive;
     form1.paintbox1.canvas.Pen.Width:=1;
     form1.paintbox1.canvas.Pen.Color:=Clblue;
     form1.paintbox1.canvas.rectangle((posx[1]*snakewidth)-snakewidth+1,( posy[1]*snakewidth)-snakewidth+1,(posx[1]*snakewidth)+1,( posy[1]*snakewidth)+1);


form1.Button2.Click;
showmessage('EL Marra ejjaya hihihi loOl <5sert>');
 shellexecute(0,'open',pchar(application.ExeName),nil,nil,sw_show);
 application.Terminate;
end;

end;
//end kill

if nb<>0 then
begin
for i:=debut to nbfood do
begin
tnbfood[i]:=tnbfood[i]+1;
end;


for i:=debut to nbfood do
 begin
 if tsnakelength[i]=tnbfood[i] then
 begin
 surface[auxyfood[i],auxxfood[i]]:=snakelength+1;
 posx[snakelength+1]:=auxxfood[i];
 posy[snakelength+1]:=auxyfood[i];
 snakelength:=snakelength+1;
 debut:=debut+1;
 end;
 end;
 


end;
//end hit testing



end;



procedure TForm1.Timer1Timer(Sender: TObject);
begin
form1.SetFocusedControl(self);
self1:=surface;
timer2.Enabled:=false;
play;
timer2.Enabled:=true;


end;

procedure TForm1.Button1Click(Sender: TObject);
begin


if button1.Caption='Start' then
begin
speed.Font.Color:=clred;
speed.Caption:='Vitesse: OFF';


diffi.Font.Color:=clred;
diffi.Caption:='Difficulté OFF';
diff.Enabled:=false;

button2.Enabled:=true;
button1.caption:='Reset';
vitesse.Enabled:=false;
timer1.Interval:=vitesse.Position*35;
timer2.Interval:=vitesse.Position +14 ;

  initialisation;
timer1.Enabled:=true;

paintbox1.Canvas.Refresh;
end
else
begin
paintbox1.Canvas.Refresh;
speed.font.Color:=clgreen;
speed.Caption:='Vitesse: ON';
 diffi.Font.Color:=clgreen;
diffi.Caption:='Difficulté ON';
diff.Enabled:=true;

timer1.Enabled:=false;
timer2.Enabled:=false;

 button2.Enabled:=false;
  score.Caption:='';
    button1.caption:='Start';
  vitesse.Enabled:=true;
  timer1.Enabled:=false;

  initialisation;
  paintbox1.Canvas.Refresh;
 
end;


end;

procedure TForm1.Timer2Timer(Sender: TObject);
begin

   if (timer2.Enabled=true)   then
   begin
if GetAsyncKeyState(vk_down)<>0 then
 if (olddirection<>'up') then begin olddirection:=direction;direction:='down';end;



if GetAsyncKeyState(vk_up)<>0 then
       if(olddirection<>'down') then begin olddirection:=direction;direction:='up';end;


if GetAsyncKeyState(vk_left)<>0 then
      if (olddirection<>'right') then begin olddirection:=direction;direction:='left'; end;


if GetAsyncKeyState(vk_right)<>0 then
      if (olddirection<>'left') then begin olddirection:=direction;direction:='right'; end;



  form1.SetFocusedControl(self);
   
  end; 
end;

procedure TForm1.Button2Click(Sender: TObject);
begin
if button2.Caption='Pause' then
begin
button2.Caption:='Continu';
timer2.Enabled:=false;
timer1.Enabled:=false;
timer2.Enabled:=false;
paintbox1.Canvas.Refresh;
end
else
begin
paintbox1.Canvas.Refresh;
timer1.Enabled:=true;
timer2.Enabled:=true;
 button2.Caption:='Pause';
end;
end;

procedure TForm1.FormCreate(Sender: TObject);
begin

 button2.Enabled:=false;
  score.Caption:='';
  vitesse.position:=10;
   diff.position:=1;
   scoree:=0;

  application.MessageBox('+ Pour augmenter la difficulté:'+#13#10+#13#10+
'        - augmenter la vitess'+#13#10+
'        - Changer la difficulté de FACILE au DIFFICILE'+#13#10+ #13#10+
'+ <ENJOY>','INFORMATION',0);
 
  end;


end.
