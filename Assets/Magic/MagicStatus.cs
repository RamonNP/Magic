using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicStatus : MonoBehaviour
{
    [SerializeField]private MagicEnum _magic;
    public Action<MagicStatus, EnemyStatus> OnMagicalCollision;

    public MagicEnum Magic { get => _magic; set => _magic = value; }
}
