program hexdump;

{$APPTYPE CONSOLE}

uses
  System.SysUtils,
  System.Classes;

procedure Main(Args: array of string);
var
  input: TFileStream;
  position, charsRead, i: Integer;
  buffer: array of Byte;
  hex: string;
  bufferContent: AnsiString;
begin
  if Length(Args) <> 1 then
  begin
    Writeln(ErrOutput, 'Usage: hexdump <filename>');
    Halt(1);
  end;

  if not FileExists(Args[0]) then
  begin
    Writeln(ErrOutput, Format('No such file: %s', [Args[0]]));
    Halt(2);
  end;

  input := TFileStream.Create(Args[0], fmOpenRead or fmShareDenyWrite);
  try
    position := 0;
    SetLength(buffer, 16);

    while position < input.Size do
    begin
      charsRead := input.Read(buffer[0], Length(buffer));
      if charsRead > 0 then
      begin
        Write(Format('%0:.4x: ', [position]));
        Inc(position, charsRead);

        for i := 0 to 15 do
        begin
          if i < charsRead then
          begin
            hex := IntToHex(buffer[i], 2);
            Write(hex + ' ');
          end
          else
            Write('   ');

          if i = 7 then
            Write('-- ');

          if (buffer[i] < 32) or (buffer[i] > 250) then
            buffer[i] := Ord('.');
        end;

        SetString(bufferContent, PAnsiChar(@buffer[0]), charsRead);
        Writeln('  ' + bufferContent);
      end;
    end;
  finally
    input.Free;
  end;
end;

begin
  try
    Main(ParamStr(1).Split([' ']));
  except
    on E: Exception do
      Writeln(E.ClassName, ': ', E.Message);
  end;
end.
