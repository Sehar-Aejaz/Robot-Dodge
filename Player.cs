using System;
using SplashKitSDK;

namespace RobotDodgeGame
{
    public class Player
    {

        private const int SPEED = 4; // Constant speed
        private const int GAP = 10;  // Least gap between window edge and player
        private const int SCORE_INCREMENT_PER_SECOND = 1; // Score increment per second

        private Bitmap _PlayerBitmap;

        private Bitmap _LivesBitmap;

        private SplashKitSDK.Timer _ScoreTimer;
        private double _lastUpdateTime = 0;
        // x co-ordinate of player
        public int X 
        { 
            get; 
            set; 
        }
        // y co-ordinate of player
        public int Y 
        { 
            get; 
            set; 
        }
        // Property set to quit the game
        public bool Quit
        {
            get; 
            private set; 
        }
        // Number of lives
        public int Lives 
        {
            get; 
            private set; 
        }

         public int Score { get; private set; } // Player's score

        //Player class constructor
        public Player()
        {
            _PlayerBitmap = new Bitmap("Player", "Player.png");
            _LivesBitmap = new Bitmap("LIves", "heart.png");
            Lives = 10; // Set initial number of lives
            Score = 0; // Initialize score to 0

            X = 360;
            Y = 260;
            Quit = false;
            // Initialize and start the score timer
            _ScoreTimer = new SplashKitSDK.Timer("ScoreTimer");
            _ScoreTimer.Start();

        }

        // Method to increment score
        public void IncrementScore()
        {
            double currentTime = SplashKit.TimerTicks(_ScoreTimer);

            // Check if one second has elapsed since the last update
            if (currentTime - _lastUpdateTime >= 1000)
            {
                Score += SCORE_INCREMENT_PER_SECOND;
                _lastUpdateTime = currentTime; // Update the last update time
            }
        }

        //Adding a width property to Player class
         public int Width
         {
            get
            {
                return _PlayerBitmap.Width;
            }
         }

         public void DecreaseLife()
         {
            Lives--;
         }

        
        // Method to draw the player bitmap
        public void Draw()
        {
            _PlayerBitmap.Draw(X, Y);
        }
        // Method to draw lives of the player
        public void DrawLives()
        {
            for (int i = 0; i < Lives; i++)
            {
                // Draw hearts of remaining lives on the screen
                _LivesBitmap.Draw(i*10, 50);
            }
        }

        // Method to draw the score of the player
        public void DrawScore()
        {
            SplashKit.DrawText($"Score: {Score}", Color.Black, "Arial", 20, 20, 40);
        }

        // Method to move player based on user input
        public void HandleInput()
        {

            if(SplashKit.KeyDown(KeyCode.RightKey)) Move(SPEED, 0);
            if(SplashKit.KeyDown(KeyCode.LeftKey)) Move(-SPEED, 0);
            if(SplashKit.KeyDown(KeyCode.UpKey)) Move(0, -SPEED);
            if(SplashKit.KeyDown(KeyCode.DownKey)) Move(0, SPEED);

            if (SplashKit.KeyTyped(KeyCode.EscapeKey)) Quit = true;
           
        }

        // Method to move the player
        public void Move(int amountForward, int amountStrafe)
        {
            X += amountForward;
            Y += amountStrafe;
        }

        // Method to keep the player within the window bounds
        public void StayOnWindow(Window gameWindow)
        {
            // Check for left edge
            if (X < GAP)
            {
                X = GAP; 
            }
            // Check for right edge
            else if (X + _PlayerBitmap.Width > gameWindow.Width - GAP)
            {
                X = gameWindow.Width - GAP - _PlayerBitmap.Width; 
            }

            // Check for top edge
            if (Y < GAP)
            {
                Y = GAP; 
            }
            // Check for bottom edge
            else if (Y + _PlayerBitmap.Height > gameWindow.Height - GAP)
            {
                Y = gameWindow.Height - GAP - _PlayerBitmap.Height; 
            }
        }
        
        // Method to check for collision
        public bool CollidedWith(Robot other)
        {
            Rectangle playerBoundingBox = SplashKit.RectangleFrom(X, Y, _PlayerBitmap.Width, _PlayerBitmap.Height);
            Rectangle robotBoundingBox = SplashKit.RectangleFrom(other.X, other.Y, other.Width, other.Height);

            return SplashKit.RectanglesIntersect(playerBoundingBox, robotBoundingBox);
        }

        
    }
}
