using Snake.Contracts;

namespace Snake
{  
    public class Game 
    {
        private const string right = "Right";
        private const string left = "Left";
        private const string up = "Up";
        private const string down = "Down";       

        private List<Food> foods = new();

        private string direction = right;

        private bool isGameOver = false;

        private IWriter writer;

        private Score score;

        private GameSpeed speed;

        private Board board;

        public Game(Snake snake, IWriter writer, Score score, GameSpeed speed, Board board)
        {
            this.Snake = snake;
            this.writer = writer;
            this.score = score;
            this.speed = speed;
            this.board = board;
        }

        public Snake Snake { get; set; }

        public void Run()
        {
            board.DrawBorders("test test");

            writer.PrintInConsole('@', Snake.Head.xPosition, Snake.Head.yPosition);                    

            while (isGameOver != true)
            {
                GamePipeLine();
            }            

            writer.WriteInPosition(score.xPosition, score.yPosition, String.Format("Final Score is: {0}", score.GetScore()));
            writer.WriteInPosition(0,32, "Game Over!");            
        }

        private void GamePipeLine()
        {
            var oldDirection = string.Empty;           

            while (isGameOver != true)
            {
                writer.WriteInPosition(score.xPosition, score.yPosition,String.Format("Score is: {0}", score.GetScore()));

                score.AddScore(score.TurnPoints);

                oldDirection = direction;                

                while (foods.Count < 10)
                {                    
                    SetFood();
                }

                var currScore = score.GetScore();

                speed.SetGameSpeed(currScore);

                Thread.Sleep(speed.Speed);

                if (writer.KeyAvailable())
                {
                    var key = writer.Key();

                    GetSnakeDirection(key);

                    ValidateDirection(key, oldDirection);
                }

                var (xValue, yValue) = GetNewPosition(direction);

                var nextXPosition = GetNextStep(Snake.Head.xPosition, xValue);
                var nextYPosition = GetNextStep(Snake.Head.yPosition, yValue);

                isGameOver = ValidateNextStep(nextXPosition, nextYPosition);
                
                CheckForEatenFood(nextXPosition, nextYPosition);

                CheckSnakeForSelfBite();                

                Snake.MoveSnake(xValue, yValue);
            }
        }

        private bool CheckForFoodOverride(int foodXPosition, int foodYPosition)
        {
            return foods.Any(food => food.xPosition == foodXPosition && food.yPosition == foodYPosition);            
        }

        private void ValidateDirection(ConsoleKey key , string oldDirection)
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

        private void CheckSnakeForSelfBite()
        {
            foreach (var snakePart in Snake.Body.Skip(1))
            {
                if (Snake.Head.xPosition == snakePart.Value.xPosition && Snake.Head.yPosition == snakePart.Value.yPosition)
                {
                    isGameOver = true;
                    break;
                }
            }
        }

        private void CheckForEatenFood(int nextXPosition, int nextYPosition)
        {
            foreach (var food in foods)
            {
                if (nextXPosition == food.xPosition && nextYPosition == food.yPosition)
                {
                    foods.Remove(food);
                    Snake.AddBodyElemen(food.xPosition, food.yPosition);                    
                    score.AddScore(score.FoodPoints);
                    break;
                }
            }            
        }

        private bool CheckNewFoodOverrideSnakeBody(int foodXPosition, int foodYPosition)
        {
            return Snake.Body.Any(food => food.Value.xPosition == foodXPosition && food.Value.yPosition == foodYPosition);
        }

        private int GetNextStep(int headPosition, int step)
        {
            return step + headPosition;
        }

        private bool ValidateNextStep(int nextXPosition, int nextYPosition)
        {
            return nextXPosition == board.BoardStartPossitionX ||
                        nextXPosition == board.BoardWidth ||
                        nextYPosition == board.BoardStartPossitionY ||
                        nextYPosition == board.BoardHeight;
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

        private void GetSnakeDirection( ConsoleKey key)
        {
            if (key == ConsoleKey.UpArrow )
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

            var isOvveridSnakePart = CheckNewFoodOverrideSnakeBody(xValue, yValue);

            var isOvveridFood = CheckForFoodOverride(xValue, yValue);

            if (!isOvveridSnakePart && !isOvveridFood)
            {
                writer.PrintInConsole(food.foodSymbol, xValue, yValue);

                foods.Add(food);
            }           
        }
    }
}
