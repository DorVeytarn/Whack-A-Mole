﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saver : MonoBehaviour
{
    public static void SaveScore(string key, int scoreValue)
    {
        PlayerPrefs.SetInt(key, scoreValue);
    }
}
