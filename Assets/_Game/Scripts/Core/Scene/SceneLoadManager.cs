using UnityEngine;

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
        Time.timeScale = 1;
        SceneLoader.OnSceneLoadDone -= OnBootToMenuLoadDone;
    }

    public void GoMenuToGame()
    {
        SceneLoader.OnSceneLoadDone += OnMenuToGameLoadDone;
        SceneLoader.LoadScene(SceneLoader.Scenes.GameScene, toUnload: SceneLoader.Scenes.MenuScene);
    }

    private void OnMenuToGameLoadDone(SceneLoader.Scenes scenes)
    {
        TextManager.Instance.ShowText(StringStorageType.Intro);
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
        SceneLoader.LoadScene(SceneLoader.Scenes.MenuScene, toUnload: SceneLoader.Scenes.GameScene);
    }

    private void OnGameToMenuLoadDone(SceneLoader.Scenes scenes)
    {
        Time.timeScale = 1;
        SceneLoader.OnSceneLoadDone -= OnGameToMenuLoadDone;
    }

    public void RestartGame()
    {
        SceneLoader.OnSceneLoadDone += OnRestartGameDone;
        SceneLoader.LoadScene(SceneLoader.Scenes.GameScene, toUnload: SceneLoader.Scenes.GameScene);
    }

    private void OnRestartGameDone(SceneLoader.Scenes scenes)
    {
        SceneLoader.OnSceneLoadDone -= OnRestartGameDone;
    }

    public bool IsSceneLoaded(SceneLoader.Scenes sceneToCheck)
    {
        return SceneLoader.IsSceneLoaded(sceneToCheck);
    }
}
