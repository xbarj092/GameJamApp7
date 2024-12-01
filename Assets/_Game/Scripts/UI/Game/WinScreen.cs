public class WinScreen : GameScreen
{
    private void Start()
    {
        TextManager.Instance.ShowText(StringStorageType.PostWin);
        TextManager.Instance.CurrentText.OnTextFinished += OnTextFinished;
    }

    private void OnTextFinished()
    {
        TextManager.Instance.CurrentText.OnTextFinished -= OnTextFinished;
        SceneLoadManager.Instance.GoGameToMenu();
    }
}
