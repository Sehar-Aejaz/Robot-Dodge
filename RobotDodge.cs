using System;
using SplashKitSDK;
using System.Collections.Generic;
using System.Diagnostics;


namespace RobotDodgeGame
{
    public class RobotDodge
    {

        private Player _Player; // Player for the game
        private Window _GameWindow; // Game window creation
        private SoundEffect death; // Sound effect for collision

        private List<Robot> _Robots; // List to store robots

        private List<Bullet> _Bullets; // List to store bullets

        public int COUNT = 0; // Count to add robots

        // Property to quit game
        public bool Quit 
        {
            get 
            {
                return _Player.Lives <= 0;
                
            }
        }


        // RobotDodge Class Constructor
        public RobotDodge(Window gameWindow)
        {
            _GameWindow = gameWindow;
            _Player = new Player(); 
            _Robots = new List<Robot>(); // Initialize the list of robots
            _Bullets = new List<Bullet>();
            death = new SoundEffect("Death", "sound.mp3");
        }

        // Methos to move player according to the user input
        public void HandleInput()
        {
            _Player.HandleInput();
            _Player.StayOnWindow(_GameWindow);
            HandleShooting();
        }

        // Method to handle shooting bullets
        public void HandleShooting()
        {
            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                // Create a new bullet and add it to the list
                Bullet bullet = new Bullet(_Player.X, _Player.Y); 
                _Bullets.Add(bullet);
            }
        }

        // Method to draw the game elements
        public void Draw()
        {
            // Clear the window
            _GameWindow.Clear(Color.White);

            // Draw the robots
            foreach (var robot in _Robots)
            {
                robot.Draw();
            }

            // Draw the bullets
            foreach (var bullet in _Bullets)
            {
                bullet.Draw();
            }

            // Draw the player
            _Player.Draw();

            // Increment Score
            _Player.IncrementScore();

            // Draw lives
            _Player.DrawLives();

            // Draw Scpore
            _Player.DrawScore();

            // Refresh the window
            _GameWindow.Refresh();
        }


        

        // Method to update robots and bullets if the collision happens
        public void Update() 
        {
            COUNT++;

            foreach (var robot in _Robots)
            {
                robot.Update();
            }

            foreach (Bullet bullet in _Bullets.ToArray()) 
            {
                bullet.Update();

                // Remove the bullet if it's off-screen
                if (bullet.IsOffScreen(_GameWindow))
                {
                    _Bullets.Remove(bullet);
                }
            }

            if ((COUNT % 200) == 0) _Robots.Add(RandomRobot(_Player));

            CheckCollisions();

        }

        // Method to create robots at random posotions
        private Robot RandomRobot(Player player)
        {
            if (COUNT % 400 == 0) return new Boxy(_GameWindow, player);
            else if (COUNT % 600 == 0) return new Houry(_GameWindow, player);
            
            return new Roundy(_GameWindow, player);
        }
        // Method to check for all collisions
        public void CheckCollisions()
        {
            foreach (var robot in _Robots.ToList()) 
            {
                if (_Player.CollidedWith(robot))// Player Robot Collision
                {
                    death.Play();
                    _Robots.Remove(robot);
                    _Player.DecreaseLife();
                    _Robots.Add(RandomRobot(_Player));
                }

                if (robot.IsOffScreen(_GameWindow))// Robot Offscreen
                {
                    _Robots.Remove(robot);
                }
                foreach (var bullet in _Bullets.ToList())// Robot Bullet Collision
                {
                    if (bullet.CollidedWith(robot))
                    {
                        _Bullets.Remove(bullet);
                        _Robots.Remove(robot);
                    }
                }

            }

        }


        
    }
}
