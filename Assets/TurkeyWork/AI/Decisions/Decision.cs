﻿using UnityEngine;

public abstract class Decision : ScriptableObject {
    public abstract bool Decide(AIController aI);
}
