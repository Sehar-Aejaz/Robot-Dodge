using System;
using SplashKitSDK;

namespace RobotDodgeGame
{

public class Program
{
    public static void Main()
    {
        Window gameWindow = new Window("Game Window", 800, 600);

        RobotDodge robotDodge = new RobotDodge(gameWindow);        

        while (!gameWindow.CloseRequested && !robotDodge.Quit)
        {
            SplashKit.ProcessEvents();
            
            robotDodge.HandleInput();
            robotDodge.Update();  
            robotDodge.Draw();  
        }
        

        gameWindow.Close();


    }
}
}

