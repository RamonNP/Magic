using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPlayerEvent : MonoBehaviour
{
    public Action<MagicEnum> OnplayerCastMagicByAnimation;
    public Action<AnimatorEnum> OnplayerShotArrowByAnimation;

    public void MagicControllerCastFireByAnimation()
    {
        OnplayerCastMagicByAnimation?.Invoke(MagicEnum.Fire);
    }    
    public void ShotArrowByAnimation()
    {
        OnplayerShotArrowByAnimation?.Invoke(AnimatorEnum.Shooting);
    }    
    public void HighShotArrowByAnimation()
    {
        OnplayerShotArrowByAnimation?.Invoke(AnimatorEnum.HighShot);
    }
}
