using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicStatus : MonoBehaviour
{
    [SerializeField]private MagicEnum _magic;
    [SerializeField]private PlayerStatusController _playerStatusController;
    public Action<MagicStatus, EnemyStatus, TypeItem> OnMagicalCollision;

    public MagicEnum Magic { get => _magic; set => _magic = value; }
    public PlayerStatusController PlayerStatusController { get => _playerStatusController; set => _playerStatusController = value; }
}
