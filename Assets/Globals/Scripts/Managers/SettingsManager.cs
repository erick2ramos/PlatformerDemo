using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    Platform,
    Runner
}

public class SettingsManager : MonoBehaviour {
    public GameSettings data;
    public GameMode gameMode;
}
