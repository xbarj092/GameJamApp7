public class TextFieldGameEnd : TextFieldShower
{
    private bool _destroying = false;

    protected override void Destroy()
    {
        if (_destroying)
        {
            return;
        }

        _destroying = true;
        ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.Death);
        Destroy(gameObject);
    }
}
