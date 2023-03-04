using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPlayerEvent : MonoBehaviour
{
    public Action<MagicEnum> OnplayerCastMagicByAnimation;

    public void MagicControllerCastFireByAnimation()
    {
        OnplayerCastMagicByAnimation?.Invoke(MagicEnum.Fire);
    }
}
