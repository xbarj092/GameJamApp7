public class WinScreen : GameScreen
{
    private void Start()
    {
        TextManager.Instance.ShowText(StringStorageType.PostWin, backToMenu: true);
    }
}
