
using System;
using UnityEngine;

public enum KeyStroke
{
    SpawnHayTower,
    SpawnSolidTower,
    Attack
}

public enum PlayerPrefKey
{
    SpawnHayTowerKeyStroke,
    SpawnSolidTowerKeyStroke,
    AttackKeyStroke
}

public enum DefaultKeyStroke
{
    SpawnHayTower = KeyCode.D,
    SpawnSolidTower = KeyCode.S,
    Attack = KeyCode.F
}

public static class KeyStrokeUtil
{
    public static Char GetKeyStrokeChar(PlayerPrefKey playerPrefKey, DefaultKeyStroke defaultKeyStroke)
    {
        return Convert.ToChar(GetKeyStrokeInt(playerPrefKey, defaultKeyStroke));
    }
    
    private static int GetKeyStrokeInt(PlayerPrefKey playerPrefKey, DefaultKeyStroke defaultKeyStroke)
    {
        if (!PlayerPrefs.HasKey(playerPrefKey.ToString()))
            return Convert.ToInt32(defaultKeyStroke);
        return PlayerPrefs.GetInt(playerPrefKey.ToString());
    }
    
    public static KeyCode GetKeyStroke(PlayerPrefKey playerPrefKey, DefaultKeyStroke defaultKeyStroke)
    {
        return (KeyCode) Enum.Parse(typeof(KeyCode), Convert.ToString(GetKeyStrokeInt(playerPrefKey, defaultKeyStroke))) ;
    }


}