public class TextFieldBackToMenu : TextFieldShower
{
    private bool _winning = false;

    protected override void Destroy()
    {
        if (_winning)
        {
            return;
        }

        _winning = true;
        SceneLoadManager.Instance.GoGameToMenu();
        Destroy(gameObject);
    }
}
