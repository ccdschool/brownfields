namespace Snake;

public class Snake
{
    // GUI components
    private Panel board;
    private Button[] snakeBodyPart;
    private Button bonusFood;
    private TextBox scoreViewer;

    // Constants
    private const int SNAKE_RUNNING_SPEED_FASTEST = 25;
    private const int SNAKE_RUNNING_SPEED_FASTER = 50;
    private const int SNAKE_RUNNING_SPEED_FAST = 100;
    private const int BOARD_WIDTH = 500;
    private const int BOARD_HEIGHT = 250;
    private const int SCORE_BOARD_HEIGHT = 20;
    private const int SNAKE_LENGTH_DEFAULT = 4;
    private const int SNAKE_BODY_PART_SQURE = 10;
    private const int BONUS_FOOD_SQURE = 15;
    private static readonly Point INIT_POINT = new Point(100, 150);

    // Others values
    private enum GAME_TYPE
    {
        NO_MAZE,
        BORDER,
        TUNNEL
    };

    private int selectedSpeed = SNAKE_RUNNING_SPEED_FASTER;
    private GAME_TYPE selectedGameType = GAME_TYPE.NO_MAZE;
    private int totalBodyPart;
    private int directionX;
    private int directionY;
    private int score;
    private Point pointOfBonusFood = new Point();
    private bool isRunningLeft;
    private bool isRunningRight;
    private bool isRunningUp;
    private bool isRunningDown;
    private bool isBonusFoodAvailable;
    private bool isRunning;
    private Random Random = new Random();

    public Snake()
    {
        //initialize all variables.
        ResetDefaultValues();
        // initialize GUI.
        Init();
        // Create Initial body of a snake.
        CreateInitSnake();
        // Initialize Thread.
        isRunning = true;
        createThread();
    }

    public void Init()
    {
        Form form = new Form();
        form.Text = "Snake";
        form.Size = new Size(500, 330);

        //Create Menue bar with functions
        SetMenuStrip(form);
        // Start of UI design
        Panel scorePanel = new Panel();
        scoreViewer = new TextBox();
        scoreViewer.Text = "Score ==>" + score;
        scoreViewer.Enabled = false;
        scoreViewer.BackColor = Color.Black;

        board = new Panel();
        board.Location = new Point(0, 0);
        board.Size = new Size(BOARD_WIDTH, BOARD_HEIGHT);
        board.BackColor = Color.White;
        scorePanel.Location = new Point(0, BOARD_HEIGHT);
        scorePanel.Size = new Size(BOARD_WIDTH, SCORE_BOARD_HEIGHT);
        scorePanel.BackColor = Color.Red;
        scorePanel.Controls.Add(scoreViewer); // will contain score board

        form.Controls.Add(board);
        form.Controls.Add(scorePanel);
        form.Visible = true;
        form.FormClosed += (_, args) => Environment.Exit(0);
        form.KeyDown += (_, args) => SnakeKeyPressed(args);
        form.MaximizeBox = false;
        form.FormBorderStyle = FormBorderStyle.FixedSingle;
        form.KeyPreview = true;
        form.Focus();
    }

    public void SetMenuStrip(Form form)
    {
        MenuStrip menuStrip = new MenuStrip();

        ToolStripMenuItem game = new ToolStripMenuItem("Game");
        ToolStripMenuItem newGame = new ToolStripMenuItem("New Game");
        ToolStripMenuItem exit = new ToolStripMenuItem("Exit");
        newGame.Click += (_, _) => { StartNewGame(); };
        exit.Click += (_, _) => { Application.Exit(); };
        game.DropDownItems.Add(newGame);
        game.DropDownItems.Add(exit);
        menuStrip.Items.Add(game);

        ToolStripMenuItem type = new ToolStripMenuItem("Type");
        ToolStripMenuItem noMaze = new ToolStripMenuItem("No Maze");
        noMaze.Click += (_, _) =>
        {
            selectedGameType = GAME_TYPE.NO_MAZE;
            StartNewGame();
        };

        ToolStripMenuItem border = new ToolStripMenuItem("Border Maze");
        border.Click += (_, _) =>
        {
            selectedGameType = GAME_TYPE.BORDER;
            StartNewGame();
        };

        type.DropDownItems.Add(noMaze);
        type.DropDownItems.Add(border);
        menuStrip.Items.Add(type);

        ToolStripMenuItem level = new ToolStripMenuItem("Level");

        ToolStripMenuItem level1 = new ToolStripMenuItem("Level 1");
        level1.Click += (_, _) =>
        {
            selectedSpeed = SNAKE_RUNNING_SPEED_FAST;
            StartNewGame();
        };

        ToolStripMenuItem level2 = new ToolStripMenuItem("Level 2");
        level2.Click += (_, _) =>
        {
            selectedSpeed = SNAKE_RUNNING_SPEED_FASTER;
            StartNewGame();
        };

        ToolStripMenuItem level3 = new ToolStripMenuItem("Level 3");
        level3.Click += (_, _) =>
        {
            selectedSpeed = SNAKE_RUNNING_SPEED_FASTEST;
            StartNewGame();
        };

        level.DropDownItems.Add(level1);
        level.DropDownItems.Add(level2);
        level.DropDownItems.Add(level3);
        menuStrip.Items.Add(level);

        ToolStripMenuItem help = new ToolStripMenuItem("Help");
        ToolStripMenuItem instruction = new ToolStripMenuItem("Instruction");

        help.DropDownItems.Add(instruction);
        menuStrip.Items.Add(help);

        form.Controls.Add(menuStrip);
    }

    public void ResetDefaultValues()
    {
        snakeBodyPart = new Button[2000];
        totalBodyPart = SNAKE_LENGTH_DEFAULT;
        directionX = SNAKE_BODY_PART_SQURE;
        directionY = 0;
        score = 0;
        isRunningLeft = false;
        isRunningRight = true;
        isRunningUp = true;
        isRunningDown = true;
        isBonusFoodAvailable = false;
    }

    void StartNewGame()
    {
        ResetDefaultValues();
        board.Controls.Clear();
        CreateInitSnake();
        scoreViewer.Text = "Score==>" + score;
        isRunning = true;
    }

    // This method is responsible to initialize the snake with four body part.
    public void CreateInitSnake()
    {
        // Location of the snake's head.
        int x = (int)INIT_POINT.X;
        int y = (int)INIT_POINT.Y;

        // Initially the snake has three body part.
        for (int i = 0; i < totalBodyPart; i++)
        {
            snakeBodyPart[i] = new Button();
            snakeBodyPart[i].SetBounds(x, y, SNAKE_BODY_PART_SQURE, SNAKE_BODY_PART_SQURE);
            snakeBodyPart[i].BackColor = Color.Gray;
            board.Controls.Add(snakeBodyPart[i]);
            // Set location of the next body part of the snake.
            x = x - SNAKE_BODY_PART_SQURE;
        }

        // Create food.
        CreateFood();
    }

    // This method is responsible to create food of a snake.
    // The most last part of this snake is treated as a food, which has not become a body part of the snake yet.
    // This food will be the body part if and only if when snake head will touch it.
    void CreateFood()
    {
        int randomX = SNAKE_BODY_PART_SQURE + (SNAKE_BODY_PART_SQURE * Random.Next(48));
        int randomY = SNAKE_BODY_PART_SQURE + (SNAKE_BODY_PART_SQURE * Random.Next(23));

        snakeBodyPart[totalBodyPart] = new Button();
        snakeBodyPart[totalBodyPart].Enabled = false;
        snakeBodyPart[totalBodyPart].SetBounds(randomX, randomY, SNAKE_BODY_PART_SQURE, SNAKE_BODY_PART_SQURE);
        board.Controls.Add(snakeBodyPart[totalBodyPart]);

        totalBodyPart++;
    }

    private void CreateBonusFood()
    {
        bonusFood = new Button();
        bonusFood.Enabled = false;
        // Set location of the bonus food.
        int bonusFoodLocX = SNAKE_BODY_PART_SQURE * Random.Next(50);
        int bonusFoodLocY = SNAKE_BODY_PART_SQURE * Random.Next(25);

        bonusFood.SetBounds(bonusFoodLocX, bonusFoodLocY, BONUS_FOOD_SQURE, BONUS_FOOD_SQURE);
        pointOfBonusFood = bonusFood.Location;
        board.Controls.Add(bonusFood);
        isBonusFoodAvailable = true;
    }

    // Process next step of the snake.
    // And decide what should be done.
    void ProcessNextStep()
    {
        bool isBorderTouched = false;
        // Generate new location of snake head.
        int newHeadLocX = (int)snakeBodyPart[0].Location.X + directionX;
        int newHeadLocY = (int)snakeBodyPart[0].Location.Y + directionY;

        // Most last part of the snake is food.
        int foodLocX = (int)snakeBodyPart[totalBodyPart - 1].Location.X;
        int foodLocY = (int)snakeBodyPart[totalBodyPart - 1].Location.Y;

        // Check does snake cross the border of the board?
        if (newHeadLocX >= BOARD_WIDTH - SNAKE_BODY_PART_SQURE)
        {
            newHeadLocX = 0;
            isBorderTouched = true;
        }
        else if (newHeadLocX <= 0)
        {
            newHeadLocX = BOARD_WIDTH - SNAKE_BODY_PART_SQURE;
            isBorderTouched = true;
        }
        else if (newHeadLocY >= BOARD_HEIGHT - SNAKE_BODY_PART_SQURE)
        {
            newHeadLocY = 0;
            isBorderTouched = true;
        }
        else if (newHeadLocY <= 0)
        {
            newHeadLocY = BOARD_HEIGHT - SNAKE_BODY_PART_SQURE;
            isBorderTouched = true;
        }

        // Check has snake touched the food?
        if (newHeadLocX == foodLocX && newHeadLocY == foodLocY)
        {
            // Set score.
            score += 5;
            scoreViewer.Text = "Score==>" + score;

            // Check bonus food should be given or not?
            if (score % 50 == 0 && !isBonusFoodAvailable)
            {
                CreateBonusFood();
            }

            // Create new food.
            CreateFood();
        }

        // Check has snake touched the bonus food?
        if (isBonusFoodAvailable &&
            pointOfBonusFood.X <= newHeadLocX &&
            pointOfBonusFood.Y <= newHeadLocY &&
            (pointOfBonusFood.X + SNAKE_BODY_PART_SQURE) >= newHeadLocX &&
            (pointOfBonusFood.Y + SNAKE_BODY_PART_SQURE) >= newHeadLocY)
        {
            board.Controls.Remove(bonusFood);
            score += 100;
            scoreViewer.Text = "Score ==>" + score;
            isBonusFoodAvailable = false;
        }

        // Check is game over?
        if (IsGameOver(isBorderTouched, newHeadLocX, newHeadLocY))
        {
            scoreViewer.Text = "GAME OVER    " + score;
            isRunning = false;
            return;
        }
        else
        {
            // Move the whole snake body to forword.
            MoveSnakeForward(newHeadLocX, newHeadLocY);
        }

        board.Refresh();
    }

    // This method is responsible to detect is game over or not?
    // Game should be over while snake is touched by any maze or by itself.
    // If any one want to add new type just declare new GAME_TYPE enum value and put logic in this method.
    private bool IsGameOver(bool isBorderTouched, int headLocX, int headLocY)
    {
        switch (selectedGameType)
        {
            case GAME_TYPE.BORDER:
                if (isBorderTouched)
                {
                    return true;
                }

                break;
            case GAME_TYPE.TUNNEL:
                // TODO put logic here...
                throw new NotImplementedException();
            default:
                break;
        }

        for (int i = SNAKE_LENGTH_DEFAULT; i < totalBodyPart - 2; i++)
        {
            Point partLoc = snakeBodyPart[i].Location;
            Console.WriteLine("(" + partLoc.X + ", " + partLoc.Y + ")  (" + headLocX + ", " + headLocY + ")");
            if (partLoc.Equals(new Point(headLocX, headLocY)))
            {
                return true;
            }
        }

        return false;
    }
    
    // Every body part should be placed to location of the front part.
    // For example if part:0(100,150) , part: 1(90, 150), part:2(80,150) and new head location (110,150) then,
    // Location of part:2 should be (80,150) to (90,150), part:1 will be (90,150) to (100,150) and part:3 will be (100,150) to (110,150)
    // This movement process should be start from the last part to first part.
    // We must avoid the food that means most last body part of the snake.
    // Notice that we write (totalBodyPart - 2) instead of (totalBodyPart - 1).
    // (totalBodyPart - 1) means food and (totalBodyPart - 2) means tail.
    public void MoveSnakeForward(int headLocX, int headLocY) {
        for (int i = totalBodyPart - 2; i > 0; i--) {
            Point frontBodyPartPoint = snakeBodyPart[i - 1].Location;
            snakeBodyPart[i].Location = frontBodyPartPoint;
        }
        snakeBodyPart[0].Bounds = new Rectangle(headLocX, headLocY, SNAKE_BODY_PART_SQURE, SNAKE_BODY_PART_SQURE);
    }
    
    public void SnakeKeyPressed(KeyEventArgs e) {
        // snake should move to left when player pressed left arrow
        if (isRunningLeft == true && e.KeyCode == Keys.Left) {
            directionX = -SNAKE_BODY_PART_SQURE; // means snake move right to left by 10 pixel
            directionY = 0;
            isRunningRight = false;     // means snake cant move from left to right
            isRunningUp = true;         // means snake can move from down to up
            isRunningDown = true;       // means snake can move from up to down
        }
        // snake should move to up when player pressed up arrow
        if (isRunningUp == true && e.KeyCode == Keys.Up) {
            directionX = 0;
            directionY = -SNAKE_BODY_PART_SQURE; // means snake move from down to up by 10 pixel
            isRunningDown = false;     // means snake can move from up to down
            isRunningRight = true;     // means snake can move from left to right
            isRunningLeft = true;      // means snake can move from right to left
        }
        // snake should move to right when player pressed right arrow
        if (isRunningRight == true && e.KeyCode == Keys.Right) {
            directionX = +SNAKE_BODY_PART_SQURE; // means snake move from left to right by 10 pixel
            directionY = 0;
            isRunningLeft = false;
            isRunningUp = true;
            isRunningDown = true;
        }
        // snake should move to down when player pressed down arrow
        if (isRunningDown == true && e.KeyCode == Keys.Down) {
            directionX = 0;
            directionY = +SNAKE_BODY_PART_SQURE; // means snake move from left to right by 10 pixel
            isRunningUp = false;
            isRunningRight = true;
            isRunningLeft = true;
        }
    }

    private void createThread()
    {
        // start thread
        Thread thread = new Thread(() => { runIt(); });
        thread.Start(); // go to runIt() method
    }

    public void runIt()
    {
        while (true)
        {
            if (isRunning)
            {
                // Process what should be next step of the snake.
                ProcessNextStep();
                try
                {
                    Thread.Sleep(selectedSpeed);
                }
                catch (ThreadInterruptedException ie)
                {
                    Console.WriteLine(ie.StackTrace);
                }
            }
        }
    }
}