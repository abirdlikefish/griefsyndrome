using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IHomuraAnimationEvent
{
    public Action Fire{get; set; }
    public Action ReShoot{get; set; }
}
