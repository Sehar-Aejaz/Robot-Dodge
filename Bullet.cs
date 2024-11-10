using SplashKitSDK;

namespace RobotDodgeGame
{
    public class Bullet
    {private const int SPEED = 5; // Bullet speed
        private const int RADIUS = 10; // Bullet radius

        public double X { get; private set; } // Bullet x-coordinate
        public double Y { get; private set; } // Bullet y-coordinate
        

        public Bullet(double x, double y)
        {
            X = x;
            Y = y;
        }

        // Method to update the bullet's position
        public void Update()
        {
            // Move the bullet towards the mouse position
            Point2D mousePosition = SplashKit.MousePosition();
            Vector2D direction = SplashKit.VectorPointToPoint(SplashKit.PointAt(X, Y), mousePosition);

            direction = SplashKit.UnitVector(direction);
            X += direction.X * SPEED;
            Y += direction.Y * SPEED;

        }

        public void Draw()
        {
            // Draw the bullet on the screen
            SplashKit.FillCircle(Color.Red, X, Y, RADIUS);
        }

        // Method to check collision with a robot
        public bool CollidedWith(Robot robot)
        {
            Rectangle bulletBoundingBox = SplashKit.RectangleFrom(X - RADIUS, Y - RADIUS, RADIUS * 2, RADIUS * 2);
            Rectangle robotBoundingBox = SplashKit.RectangleFrom(robot.X, robot.Y, robot.Width, robot.Height);

            return SplashKit.RectanglesIntersect(bulletBoundingBox, robotBoundingBox);
        }


        // Method to check if the bullet is off-screen
        public bool IsOffScreen(Window screen)
        {
            return X < 0 || X > screen.Width || Y < 0 || Y > screen.Height;
        }
    }

}

