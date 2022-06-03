using System;
using System.IO;
using System.Text;

namespace hexdump
{
    // source: "C# von Kopf bis Fuß", O'Reilly, see also https://app.box.com/s/a9n6kipfdonr5yeo44ha
    public class MainClass
    {
        public static void Main(string[] args) {
            if (args.Length != 1) {
                Console.Error.WriteLine("Usage: hexdump <dateiname>");
                Environment.Exit(1);
            }

            if (!File.Exists(args[0])) {
                Console.Error.WriteLine("No such file: {0}", args[0]);
                Environment.Exit(2);
            }

            using (var input = File.OpenRead(args[0])) {
                int position = 0;
                var buffer = new byte[16];

                while (position < input.Length) {
                    var charsRead = input.Read(buffer, 0, buffer.Length);
                    if (charsRead > 0) {
                        Console.Write("{0}: ", string.Format("{0:x4}", position));
                        position += charsRead;

                        for (int i = 0; i < 16; i++) {
                            if (i < charsRead) {
                                var hex = string.Format("{0:x2}", buffer[i]);
                                Console.Write(hex + " ");
                            }
                            else {
                                Console.Write("   ");
                            }

                            if (i == 7) {
                                Console.Write("-- ");
                            }

                            if (buffer[i] < 32 || buffer[i] > 250) {
                                buffer[i] = (byte)'.';
                            }
                        }

                        var bufferContent = Encoding.ASCII.GetString(buffer);
                        Console.WriteLine("  " + bufferContent.Substring(0, charsRead));
                    }
                }
            }
        }
    }
}