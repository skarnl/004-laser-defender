﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : Singleton
{
    int score = 0;
    int health = 200;

    public void AddPointsToScore(int pointsToAdd) {
        score += pointsToAdd;
    }

    public int GetScore() {
        return score;
    }

    public void SubstractHealth(int healthToSubstract) {
        health -= healthToSubstract;
    }

    public int GetHealth() {
        return health;
    }

    public void Reset() {
        Destroy(gameObject);
    }
}