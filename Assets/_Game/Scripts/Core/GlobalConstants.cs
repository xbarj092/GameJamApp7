using UnityEngine;

public static class GlobalConstants
{
    public enum Tags
    {
        Room,
        Hallway,
        Shop,
        Enemy,
        Food,
        Item,
        Player,
        InteractableGhost
    }

    public enum Layers
    {
        Player = 7,
        Map = 8,
        Kitten = 11,
        Interact = 13,
        KittenInteraction = 18
    }

    public static class SavedDataPaths
    {
#if UNITY_EDITOR
        private static readonly string BasePath = Application.dataPath + "/Data";
#else
        private static readonly string BasePath = Application.persistentDataPath;
#endif

        public static string DATA_PATH_PLAYER_TRANSFORM = BasePath + "/transform.gg";
        public static string DATA_PATH_PLAYER_STATISTICS = BasePath + "/statistics.gg";
        public static string DATA_PATH_PLAYER_INVENTORY = BasePath + "/inventory.gg";

        public static string DATA_PATH_GAME_MAP = BasePath + "/map.gg";
        public static string DATA_PATH_GAME_ITEMS = BasePath + "/items.gg";
        public static string DATA_PATH_GAME_KITTENS = BasePath + "/kittens.gg";
        public static string DATA_PATH_GAME_FOOD = BasePath + "/food.gg";
    }
}
