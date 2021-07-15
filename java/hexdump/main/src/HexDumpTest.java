import java.io.ByteArrayOutputStream;
import java.io.PrintStream;
import java.io.UnsupportedEncodingException;

import static org.junit.jupiter.api.Assertions.*;

class HexDumpTest {

    @org.junit.jupiter.api.Test
    void dumpHexData() throws UnsupportedEncodingException {
        var os = new ByteArrayOutputStream();
        var out = new PrintStream(os);
        var buffer = new byte[16];

        HexDump.dumpHexData(out, "title", buffer, 16);

        var output = os.toString("UTF8");
        var lines = output.split("\n");
        assertEquals("title - 16 bytes.", lines[0]);
        assertEquals("0000: 00 00 00 00  00 00 00 00  00 00 00 00  00 00 00 00  | ................ |", lines[1]);
    }
}