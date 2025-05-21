//using static UnityEditor.PlayerSettings.SplashScreen;

public class Diana : BaseFighter
{
    public static Diana instance;

    private void Awake()
    {
        instance = this;
    }
}