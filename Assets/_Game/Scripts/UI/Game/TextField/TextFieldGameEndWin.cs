public class TextFieldGameEndWin : TextFieldShower
{
    private bool _winning = false;

    protected override void Destroy()
    {
        if (_winning)
        {
            return;
        }

        _winning = true;
        ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.Win);
        Destroy(gameObject);
    }
}
