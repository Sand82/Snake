using Snake.Contracts;
using Snake.IOC.Attributes;  

namespace Snake.Implementations
{
    public class Game : IGame
    {
        private const string right = "Right";
        private const string left = "Left";
        private const string up = "Up";
        private const string down = "Down";

        private List<Food> foods = new();

        private string direction = right;

        private bool isGameOver = false;

        private IWriter writer;

        private IScore score;

        private IGameSpeed speed;

        private IBoard board;

        private ISnake snake;

        [Inject]
        public Game(ISnake snake, IWriter writer, IScore score, IGameSpeed speed, IBoard board)
        {
            this.snake = snake;
            this.writer = writer;
            this.score = score;
            this.speed = speed;
            this.board = board;
        }

        public void Run()
        {
            board.DrawBorders("test game");

            writer.PrintInConsole('@', snake.GetHead().xPosition, snake.GetHead().yPosition);

            while (isGameOver != true)
            {
                GamePipeLine();
            }

            score.PrintScoreInPosition();
            writer.WriteInPosition(0, 32, "Game Over!");
        }

        private void GamePipeLine()
        {
            var oldDirection = string.Empty;

            while (!isGameOver)
            {
                score.PrintScoreInPosition();

                score.AddScore(score.GetTurnPoints());

                oldDirection = direction;

                while (foods.Count < 10)
                {
                    SetFood();
                }

                var currScore = score.GetScore();

                speed.SetGameSpeed(currScore);

                Thread.Sleep(speed.GetGameSpeed());

                if (writer.KeyAvailable())
                {
                    var key = writer.Key();

                    GetSnakeDirection(key);

                    ValidateDirection(key, oldDirection);
                }

                var (xValue, yValue) = GetNewPosition(direction);

                var nextXPosition = GetNextStep(snake.GetHead().xPosition, xValue);
                var nextYPosition = GetNextStep(snake.GetHead().yPosition, yValue);

                isGameOver = board.ValidateNextStep(nextXPosition, nextYPosition);

                CheckForEatenFood(nextXPosition, nextYPosition);

                var checkSnakeForSelfBite = snake.CheckSnakeForSelfBite();

                if (checkSnakeForSelfBite)
                {
                    isGameOver = true;
                    break;
                }

                snake.MoveSnake(xValue, yValue);
            }
        }

        private bool CheckForFoodOverride(int foodXPosition, int foodYPosition)
        {
            return foods.Any(food => food.xPosition == foodXPosition && food.yPosition == foodYPosition);
        }

        private void ValidateDirection(ConsoleKey key, string oldDirection)
        {
            if (oldDirection == left && key == ConsoleKey.RightArrow)
            {
                direction = left;
            }

            if (oldDirection == right && key == ConsoleKey.LeftArrow)
            {
                direction = right;
            }

            if (oldDirection == up && key == ConsoleKey.DownArrow)
            {
                direction = up;
            }

            if (oldDirection == down && key == ConsoleKey.UpArrow)
            {
                direction = down;
            }
        }

        private void CheckForEatenFood(int nextXPosition, int nextYPosition)
        {
            foreach (var food in foods)
            {
                if (nextXPosition == food.xPosition && nextYPosition == food.yPosition)
                {
                    foods.Remove(food);
                    snake.AddBodyElement(food.xPosition, food.yPosition);
                    score.AddScore(score.GetFoodPoints());
                    break;
                }
            }
        }

        private int GetNextStep(int headPosition, int step)
        {
            return step + headPosition;
        }

        private (int, int) GetNewPosition(string direction)
        {
            Dictionary<string, string> directionValues = new Dictionary<string, string>()
            {
                { up, "-1, 0" },
                { down, "1, 0" },
                { left, "0, -1" },
                { right, "0, 1" },
            };

            var tokens = directionValues[direction]
                .Split(", ")
                .Select(int.Parse)
                .ToArray();

            return (tokens[1], tokens[0]);
        }

        private void GetSnakeDirection(ConsoleKey key)
        {
            if (key == ConsoleKey.UpArrow)
            {
                direction = up;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                direction = down;
            }
            else if (key == ConsoleKey.LeftArrow)
            {
                direction = left;
            }
            else if (key == ConsoleKey.RightArrow)
            {
                direction = right;
            };
        }

        private void SetFood()
        {
            var x = new Random();
            int xValue = x.Next(1, 79);

            var y = new Random();
            int yValue = y.Next(1, 29);

            var food = new Food(xValue, yValue);

            var isOverrideSnakePart = snake.CheckNewFoodOverrideSnakeBody(xValue, yValue);

            var isOverrideFood = CheckForFoodOverride(xValue, yValue);

            if (!isOverrideSnakePart && !isOverrideFood)
            {
                writer.PrintInConsole(food.foodSymbol, xValue, yValue);

                foods.Add(food);
            }
        }
    }
}
