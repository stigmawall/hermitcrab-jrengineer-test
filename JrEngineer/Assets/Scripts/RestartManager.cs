﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartManager : MonoBehaviour {
    public void RestartEvent() {
        ManagerScript.Instance.RestartGame();
    }
}
