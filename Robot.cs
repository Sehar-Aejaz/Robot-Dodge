using SplashKitSDK;

namespace RobotDodgeGame
{
    public abstract class Robot
    {
        public double X // x co-ordinate of robot
        { 
            get; 
            private set; 
        }
        public double Y // y co-ordinate of robot
        { 
            get; 
            private set; 
        }
        public Color MainColor // Color of robot
        { 
            get; 
            private set; 
        }
        public Circle CollisionCircle // Collision status of robot
        { 
            get; 
        }

        public int Width // Width of robot
        {
            get 
            { 
                return 50; 
            }
        }
        public int Height // Height of robot
        {
            get 
            { 
                return 50; 
            }
        }
        private Vector2D Velocity
        {
            get;
            set;
        }

        // Constructor for the Robot class
        public Robot(Window gameWindow, Player player)
        {

            if (SplashKit.Rnd() < 0.5)
            {
                X = SplashKit.Rnd(gameWindow.Width);

                if(SplashKit.Rnd() < 0.5)
                    Y = -Height;
                else
                    Y = gameWindow.Height;
            }
            else
            {
                Y = SplashKit.Rnd(gameWindow.Height);

                if(SplashKit.Rnd() < 0.5)
                    X = -Width;
                else
                    X = gameWindow.Width;
            }

            const double SPEED = 0.3;

            Point2D fromPt = new Point2D()
            {X = X, Y = Y};
            Point2D toPt = new Point2D()
            {X = player.X, Y = player.Y};

            Vector2D dir; // Velovity vector for robot
            dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));

            Velocity = SplashKit.VectorMultiply(dir, SPEED);
            MainColor = Color.RandomRGB(200);
            CollisionCircle = SplashKit.CircleAt(X + Width / 2, Y + Height / 2, 20);
        }

        // Method to move the Robot
        public void Update()
        {
            X += Velocity.X;
            Y += Velocity.Y; 
        }

        // Method to draw the robot
        public abstract void Draw();
        
        // Method to check if robot is offscreen
        public bool IsOffScreen(Window screen)
        {
            if((X < -Width) || (X > screen.Width) || (Y < -Height) || (Y > screen.Height))
            {
                return true;
            }
                
            
            return false;
        }
    }

    public class Boxy : Robot
    {
        public Boxy (Window gameWindow, Player player) : base(gameWindow, player)
        {
            //Constructor
        }
        // Method to draw the Boxy robot
        public override void Draw()
        {
            double leftX = X + 12;
            double rightX = X + 27;
            double eyeY = Y + 10;
            double mouthY = Y + 30;

            SplashKit.FillRectangle(Color.Gray, X, Y, Width, Height);
            SplashKit.FillRectangle(MainColor, leftX, eyeY, 10, 10);
            SplashKit.FillRectangle(MainColor, rightX, eyeY, 10, 10);
            SplashKit.FillRectangle(MainColor, leftX, mouthY, 25, 10);
            SplashKit.FillRectangle(MainColor, leftX + 2, mouthY + 2, 21, 6);
        }

    }

    public class Roundy : Robot
    {
        public Roundy (Window gameWindow, Player player) : base(gameWindow, player)
        {
            //Constructor
        }
        // Method to draw the Roundy robot
        public override void Draw() 
        {
            double leftX, midX, rightX; 
            double midY, eyeY, mouthY;

            leftX = X + 17; 
            midX = X + 25; 
            rightX = X + 33;

            midY = Y + 25; 
            eyeY = Y + 20; 
            mouthY = Y + 35;

            SplashKit.FillCircle(Color.White, midX, midY, 25); 
            SplashKit.DrawCircle(Color.Gray, midX, midY, 25); 
            SplashKit.FillCircle(MainColor, leftX, eyeY, 5); 
            SplashKit.FillCircle(MainColor, rightX, eyeY, 5); 
            SplashKit.FillEllipse(Color.Gray, X, eyeY, 50, 30); 
            SplashKit.DrawLine(Color.Black, X, mouthY, X + 50, Y + 35);
        }

    }
   
    public class Houry : Robot
    {
        private Bitmap _robotImage; // The image for the robot

        public Houry(Window gameWindow, Player player) : base(gameWindow, player)
        {
            _robotImage = SplashKit.BitmapNamed("robot.jpg"); // Load the image
        }

        // Method to draw the robot
        public override void Draw()
        {
            // Draw the robot image at the specified position
            SplashKit.DrawBitmap(_robotImage, X, Y);
        }
    }

    
}
