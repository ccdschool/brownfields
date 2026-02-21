import org.junit.jupiter.api.Test;
import org.approvaltests.Approvals;
import static org.junit.jupiter.api.Assertions.*;

class SnakeTests {

    @Test
    void approvalTest() {
        Snake snake = new Snake();

        snake.moveSnakeForword(5, 5);

        // How to test this correctly?
        Approvals.verify(snake.toString());
    }

    @Test
    void unitTest() {
        Snake snake = new Snake();

        snake.moveSnakeForword(5, 5);

        // How to test this correctly?
        assertEquals("4", snake.toString());
    }
}