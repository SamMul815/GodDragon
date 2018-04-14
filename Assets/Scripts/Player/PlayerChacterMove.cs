using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCharacter
{
    partial class PlayerCharacterController
    {
        //플레이어 움직일 키입력값 전부 확인
        private int KeyInput()
        {
            int _keyBit = 0;

            if (Input.GetKey(KeyCode.W)) { _keyBit |= (int)INPUTKEY.KEY_W; }
            else if (Input.GetKey(KeyCode.S)) { _keyBit |= (int)INPUTKEY.KEY_S; }
            if (Input.GetKey(KeyCode.A)) { _keyBit |= (int)INPUTKEY.KEY_A; }
            else if (Input.GetKey(KeyCode.D)) { _keyBit |= (int)INPUTKEY.KEY_D; }

            if (Input.GetKeyDown(KeyCode.Space)) { _keyBit |= (int)INPUTKEY.KEY_SPACE; }
            if (Input.GetMouseButtonDown(1)) { _keyBit |= (int)INPUTKEY.MOUSE_RIGHT; }


            return _keyBit;
        }

        //키값 확인후 캐릭터 회전값 이동값 설정
        private void CheckPlayerState()
        {
            int _keyBit = KeyInput();

            if(IsFalling())
            {
                _PlayerState = PLAYERSTATE.FALLING;
                return;
            }
            if ((_keyBit & (int)INPUTKEY.KEY_SPACE) == (int)INPUTKEY.KEY_SPACE)
            {
                _PlayerUpdateState = PLAYERUPDATESTATE.JUMP;
                _keyBit -= (int)INPUTKEY.KEY_SPACE;
            }

            if ((_keyBit & (int)INPUTKEY.MOUSE_RIGHT) == (int)INPUTKEY.MOUSE_RIGHT)
            {
                _PlayerUpdateState = PLAYERUPDATESTATE.FLASH;
                _keyBit -= (int)INPUTKEY.MOUSE_RIGHT;
            }
            switch (_keyBit)
            {
                case (int)INPUTKEY.KEY_W: _PlayerRotAngle = 0; _PlayerState = PLAYERSTATE.MOVE; break;
                case (int)INPUTKEY.KEY_A: _PlayerRotAngle = -90; _PlayerState = PLAYERSTATE.MOVE; break;
                case (int)INPUTKEY.KEY_S: _PlayerRotAngle = 180; _PlayerState = PLAYERSTATE.MOVE; break;
                case (int)INPUTKEY.KEY_D: _PlayerRotAngle = 90; _PlayerState = PLAYERSTATE.MOVE; break;

                case (int)INPUTKEY.KEY_W | (int)INPUTKEY.KEY_A: _PlayerRotAngle = -45; _PlayerState = PLAYERSTATE.MOVE; break;
                case (int)INPUTKEY.KEY_W | (int)INPUTKEY.KEY_D: _PlayerRotAngle = 45; _PlayerState = PLAYERSTATE.MOVE; break;
                case (int)INPUTKEY.KEY_S | (int)INPUTKEY.KEY_A: _PlayerRotAngle = -135; _PlayerState = PLAYERSTATE.MOVE; break;
                case (int)INPUTKEY.KEY_S | (int)INPUTKEY.KEY_D: _PlayerRotAngle = 135; _PlayerState = PLAYERSTATE.MOVE; break;

                default: _PlayerState = PLAYERSTATE.STAND; break;
            }
        }

        private void PlayerStateMoveLU()
        {
            if ((_PlayerUpdateState == PLAYERUPDATESTATE.FLASH) && corFlash == false &&  _FlashCoolTime <= 0)
            {
                StartCoroutine("CorFlash");
                _PlayerUpdateState = PLAYERUPDATESTATE.NONE;
            }

            if(_PlayerUpdateState == PLAYERUPDATESTATE.JUMP && _PlayerState != PLAYERSTATE.FALLING )
            {
                rigid.AddForce(Vector3.up * 20, ForceMode.Impulse);
                _PlayerUpdateState = PLAYERUPDATESTATE.NONE;
            }
            _PlayerUpdateState = PLAYERUPDATESTATE.NONE;
        }
        private void PlayerStateMoveFU()
        {
            if (_PlayerState == PLAYERSTATE.FALLING)
            {
                Debug.Log("Falling");
            }
            else if (_PlayerState == PLAYERSTATE.MOVE)
            {
                Vector3 v = Quaternion.AngleAxis(_PlayerRotAngle, Vector3.up) * this.transform.forward * Time.fixedDeltaTime * _AccelSpeed;
                rigid.velocity += v * Time.timeScale;
                if (rigid.velocity.sqrMagnitude > _MaxSpeed * _MaxSpeed)
                {
                    rigid.velocity = rigid.velocity.normalized * _MaxSpeed;
                }
            }
            else if(_PlayerState == PLAYERSTATE.STAND)
            {
                if (rigid.velocity.sqrMagnitude > _BreakSped * _BreakSped * Time.fixedDeltaTime * Time.fixedDeltaTime)
                {
                    rigid.velocity -= rigid.velocity.normalized * _BreakSped * Time.fixedDeltaTime;
                }
            }
        }
        private bool IsFalling()
        {
            CapsuleCollider capCol = GetComponent<CapsuleCollider>();
            Ray ray = new Ray(this.transform.position +capCol.center, Vector3.down);
            if(Physics.SphereCast(ray,capCol.radius * 0.95f,capCol.height * 0.7f ))
            {
                return false;
            }
            else
            {
                Debug.Log("Falling");
                return true;                
            }
        }

        IEnumerator CorFlash()
        {
            corFlash = true;
            _FlashCoolTime = _FlashDelay;

            CapsuleCollider capCol = GetComponent<CapsuleCollider>();

            Vector3 dir = Quaternion.AngleAxis(_PlayerRotAngle, Vector3.up) * this.transform.forward;
            float distance = 0;
            Vector3 savePosition = this.transform.position;
            float saveView = headCamera.GetComponent<Camera>().fieldOfView;

            while (distance < _FlashDistance)
            {
                headCamera.GetComponent<Camera>().fieldOfView += 1;
                distance += _FlashDistance / _FlashTime * Time.fixedDeltaTime;
                Vector3 p1 = transform.position - (dir * 0.5f) + capCol.center + Vector3.up * -capCol.height * 0.5f;
                Vector3 p2 = p1 + Vector3.up * capCol.height;

                RaycastHit hit;
                if (Physics.CapsuleCast(p1, p2, capCol.radius, dir, out hit, distance))
                {
                    this.transform.position = new Vector3(hit.point.x, this.transform.position.y, hit.point.z);
                    break;
                }
                else
                {
                    this.transform.position = savePosition + dir * distance;
                }
                yield return new WaitForEndOfFrame();
            }
            headCamera.GetComponent<Camera>().fieldOfView = saveView;
            corFlash = false;
            yield return null;
        }
    }
}