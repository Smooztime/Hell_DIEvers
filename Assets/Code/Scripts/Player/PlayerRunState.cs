﻿using UnityEngine;

namespace Code.Scripts.Player
{
    public class PlayerRunState: PlayerBaseState
    {
        
        public PlayerRunState(PlayerController player) : base(player){}
        public override void Enter()
        {
            _player.Data.IsJumping = false;
            _player.PlayerAnim.PlayAnimation(PlayerAnimationConstants.RUN);
            _player.GetComponent<ActiveRagDoll>().SetActiveRagDoll(false);
            /*if (_player.Data.JumpBuffered)
            {
                base.HandleJump();
            }*/
            _player.EventData.HandlePlayerRuns(_player);
                
        }
        public override void Update()
        {
            base.Update();
        }
        /*public override void HandleJump()
        {
            base.HandleJump();
        }*/
        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (_player.RB.velocity.y < -15f || _player.GetComponent<ActiveRagDoll>().IsRagDoll == true && !_player.IsGrounded)
            {
                _player.ChangeState(PlayerStates.InAir);
                return;
            }
            //if(_player.Data.IsInAir)
            //{
            //    if (_player.RB.velocity.magnitude <= 0.0001f)
            //        _player.ChangeState(PlayerStates.Idle);
            //}
            
        }
    }
}