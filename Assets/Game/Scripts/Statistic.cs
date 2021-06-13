using System;
using UnityEngine;

public static class Statistic
{
    private static int currentLevel = 1;
    private static Vector3 startPosition;
    private static int enemyKilled;
    private static DateTime playStartTime;
    
    
    public static void IncrementCurrentLevel()
    {
        currentLevel++;
        if (currentLevel > 100) currentLevel = 100;
    }

    public static void IncrementEnemyKilled()
    {
        enemyKilled++;
    }


    public static void Reset(Player player)
    {
        currentLevel = 1;
        startPosition = player.gameObject.transform.position;
        enemyKilled = 0;
        playStartTime = DateTime.Now;
    }
    public static int CurrentLevel => currentLevel;

    public static Vector3 StartPosition => startPosition;

    public static int EnemyKilled => enemyKilled;

    public static DateTime PlayStartTime => playStartTime;
}