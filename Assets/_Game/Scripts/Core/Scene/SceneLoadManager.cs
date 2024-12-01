using System;

public class SceneLoadManager : MonoSingleton<SceneLoadManager>
{
    protected override void Init()
    {
        base.Init();
        GoBootToMenu();
    }

    public void GoBootToMenu()
    {
        SceneLoader.OnSceneLoadDone += OnBootToMenuLoadDone;
        SceneLoader.LoadScene(SceneLoader.Scenes.MenuScene);
    }

    private void OnBootToMenuLoadDone(SceneLoader.Scenes scene)
    {
        PlayMenuMusic();
        GameManager.Instance.Unpause();
        SceneLoader.OnSceneLoadDone -= OnBootToMenuLoadDone;
    }

    public void GoMenuToGame()
    {
        EventManager.Instance.ResetScript();
        GameManager.Instance.ResetScript();
        ScreenManager.Instance.ResetScript();
        TextManager.Instance.ResetScript();
        GameManager.Instance.Unpause();
        AudioManager.Instance.StopAllSounds();
        SceneLoader.OnSceneLoadDone += OnMenuToGameLoadDone;
        SceneLoader.LoadScene(SceneLoader.Scenes.GameScene, toUnload: SceneLoader.Scenes.MenuScene);
    }

    private void OnMenuToGameLoadDone(SceneLoader.Scenes scenes)
    {
        PlayGameMusic();
        TextManager.Instance.ShowText(StringStorageType.IntroGood, true);
        TextManager.Instance.CurrentText.OnTextFinished += OnTextFinished;
        SceneLoader.OnSceneLoadDone -= OnMenuToGameLoadDone;
    }

    public void GoGameToGame2D()
    {
        SceneLoader.OnSceneLoadDone += OnGameToGame2DLoadDone;
        SceneLoader.LoadScene(SceneLoader.Scenes.GameScene2D, toUnload: SceneLoader.Scenes.GameScene);
    }

    private void OnGameToGame2DLoadDone(SceneLoader.Scenes scenes)
    {
        TextManager.Instance.ShowText(StringStorageType.DimensionChange);
        SceneLoader.OnSceneLoadDone -= OnGameToGame2DLoadDone;
    }

    public void GoGame2DToGame1D()
    {
        SceneLoader.OnSceneLoadDone += OnGame2DToGame1DLoadDone;
        SceneLoader.LoadScene(SceneLoader.Scenes.GameScene1D, toUnload: SceneLoader.Scenes.GameScene2D);
    }

    private void OnGame2DToGame1DLoadDone(SceneLoader.Scenes scenes)
    {
        TextManager.Instance.ShowText(StringStorageType.DimensionChange);
        SceneLoader.OnSceneLoadDone -= OnGame2DToGame1DLoadDone;
    }

    public void GoGame1DToGame0D()
    {
        SceneLoader.OnSceneLoadDone += OnGame1DToGame0DLoadDone;
        SceneLoader.LoadScene(SceneLoader.Scenes.GameScene0D, toUnload: SceneLoader.Scenes.GameScene1D);
    }

    private void OnGame1DToGame0DLoadDone(SceneLoader.Scenes scenes)
    {
        SceneLoader.OnSceneLoadDone -= OnGame1DToGame0DLoadDone;
    }

    public void GoGame1DToGame2D()
    {
        SceneLoader.OnSceneLoadDone += OnGame1DToGame2DLoadDone;
        SceneLoader.LoadScene(SceneLoader.Scenes.GameScene2D, toUnload: SceneLoader.Scenes.GameScene1D);
    }

    private void OnGame1DToGame2DLoadDone(SceneLoader.Scenes scenes)
    {
        SceneLoader.OnSceneLoadDone -= OnGame1DToGame2DLoadDone;
    }

    public void GoGame2DToGame()
    {
        SceneLoader.OnSceneLoadDone += OnGame2DToGameLoadDone;
        SceneLoader.LoadScene(SceneLoader.Scenes.GameScene, toUnload: SceneLoader.Scenes.GameScene2D);
    }

    public void OnGame2DToGameLoadDone(SceneLoader.Scenes scenes)
    {
        SceneLoader.OnSceneLoadDone -= OnGame2DToGameLoadDone;
    }

    public void GoGameToMenu()
    {
        SceneLoader.OnSceneLoadDone += OnGameToMenuLoadDone;
        SceneLoader.LoadScene(SceneLoader.Scenes.MenuScene, toUnload: SceneLoader.GetActiveScene());
    }

    private void OnGameToMenuLoadDone(SceneLoader.Scenes scenes)
    {
        PlayMenuMusic();
        GameManager.Instance.Unpause();
        SceneLoader.OnSceneLoadDone -= OnGameToMenuLoadDone;
    }

    public void RestartGame()
    {
        EventManager.Instance.ResetScript();
        GameManager.Instance.ResetScript();
        ScreenManager.Instance.ResetScript();
        TextManager.Instance.ResetScript();
        GameManager.Instance.Unpause();
        AudioManager.Instance.StopAllSounds();
        SceneLoader.OnSceneLoadDone += OnRestartGameDone;
        SceneLoader.LoadScene(SceneLoader.Scenes.GameScene, toUnload: SceneLoader.GetActiveScene());
    }

    private void OnRestartGameDone(SceneLoader.Scenes scenes)
    {
        PlayGameMusic();
        TextManager.Instance.ShowText(StringStorageType.IntroGood, true);
        TextManager.Instance.CurrentText.OnTextFinished += OnTextFinished;
        SceneLoader.OnSceneLoadDone -= OnRestartGameDone;
    }

    private void PlayGameMusic()
    {
        if (AudioManager.Instance.IsPlaying(SoundType.MenuMusic))
        {
            AudioManager.Instance.Stop(SoundType.MenuMusic);
        }

        if (!AudioManager.Instance.IsPlaying(SoundType.GameMusic))
        {
            AudioManager.Instance.Play(SoundType.GameMusic);
        }
    }

    private void PlayMenuMusic()
    {
        if (AudioManager.Instance.IsPlaying(SoundType.GameMusic))
        {
            AudioManager.Instance.Stop(SoundType.GameMusic);
        }

        if (!AudioManager.Instance.IsPlaying(SoundType.MenuMusic))
        {
            AudioManager.Instance.Play(SoundType.MenuMusic);
        }
    }

    private void OnTextFinished()
    {
        TextManager.Instance.CurrentText.OnTextFinished -= OnTextFinished;
        TextManager.Instance.ShowText(StringStorageType.Intro);
    }

    public bool IsSceneLoaded(SceneLoader.Scenes sceneToCheck)
    {
        return SceneLoader.IsSceneLoaded(sceneToCheck);
    }
}
