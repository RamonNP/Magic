using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private MagicController _magicController;
    public void MagicControllerCastFire()
    {
        _magicController.CastFire();
    }
}
