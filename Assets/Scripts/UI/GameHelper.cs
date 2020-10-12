public static class GameHelper
{
    public static bool isFirstStartMenu = true;
    public static bool isFirstStartUfo = true;
    public static bool isFirstStartPlayer = true;
    public static bool isChangeSetting = true;
    public static bool isPausedGame = false;

    public static double changeAngle = 45;


    public static void restartGame()
    {
        isFirstStartMenu = true;
        isFirstStartUfo = true;
        isFirstStartPlayer = true;
    }
}
