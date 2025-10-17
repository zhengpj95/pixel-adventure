public class TheFrogPrinceProxy
{
  private static TheFrogPrinceProxy _instance;

  public int Score = 0;

  public int TempScore = 0; // 每关卡得到的分数

  public static TheFrogPrinceProxy Instance
  {
    get
    {
      if (_instance == null) _instance = new TheFrogPrinceProxy();

      return _instance;
    }
  }
}