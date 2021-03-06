﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class hEnemy1 : hEnemy {
    // 攻撃しているかどうか
    public bool isAttacking = false;

    // 移動速度
    [SerializeField, Header("移動速度")]
    float MoveSpeed = 1.0f;

    //移動範囲
    [SerializeField, Header("移動可能範囲")]
    float CanMoveSpace = 1;

    // 初期座標
    Vector2 InitialPosition;

    // 待ちなどのカウント
    [SerializeField]
    float FlameCount = 0;

    // どれだけ待つか
    [SerializeField]
    float setFlameCount = 300;


    // Use this for initialization
	void Start () {
        // 初期座標代入
        pos = transform.position;
        InitialPosition = pos;
        if (player == null) GameObject.Find("player");

        // エネミーの状態を移動状態にする
        EnemyState = Status.Move;
	}

    // Update
    private void FixedUpdate()
    {
        CharacterStatus(EnemyState);

        // アニメーション速度の設定
        GetComponent<Animator>().speed = AnimationSpeed;
    }

    // キャラクターのステータス
    public override void CharacterStatus(Status status)
    {
        switch (status)
        {
           // 移動
            case Status.Move:
                pos += CharacterMove(CharMoveDirection) * MoveSpeed;

                // 移動範囲を超えたら
                if ((pos - InitialPosition).magnitude > CanMoveSpace)
                {
                    CharMoveDirection = (CharMoveDirection == CharacterMoveDirection.Left) ? CharacterMoveDirection.Right : CharacterMoveDirection.Left;
                    EnemyState = Status.Wait;
                    AnimationSpeed = 0;
                }
                transform.position = pos;
                break;

            // 待ち
            case Status.Wait:
                FlameCount++;
                // 一定フレームを超えたら
                if(FlameCount > setFlameCount)
                {
                    EnemyState = Status.Move;
                    FlameCount = 0;
                    transform.localScale = new Vector3(-transform.localScale.x, 0.5f, 1f);
                    AnimationSpeed = 1;
                }
                break;
        }
    }
}
