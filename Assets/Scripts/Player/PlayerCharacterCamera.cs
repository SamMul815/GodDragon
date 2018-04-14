using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlayerCharacter
{
    partial class PlayerCharacterController
    {    
        //카메라 회전
        private void CameraSetRot()
        {
            float x = Input.GetAxis("Mouse Y");
            float y = Input.GetAxis("Mouse X");

            CameraRot.x -= x * m_TurnSpeed * Time.deltaTime;
            PlayerRot.y += y * m_TurnSpeed * Time.deltaTime;
            CameraRot.y = PlayerRot.y;
            if (CameraRot.x > _CameraDownAngleX)
                CameraRot.x = _CameraDownAngleX;

            if (CameraRot.x < _CameraUpAngleX)
                CameraRot.x = _CameraUpAngleX;

            this.transform.rotation = Quaternion.Euler(PlayerRot);
            headCamera.transform.rotation = Quaternion.Euler(CameraRot + _CameraEventAngle);
        }

        //카메라 흔들리는거
        private IEnumerator CorCameraShake()
        {
            float time = ShakingPlayTime;
            while (time > 0)
            {
                Vector3 shakingPos = Random.insideUnitSphere * ShakingRadius;
                _CameraEventAngle.x = shakingPos.x * time / ShakingPlayTime;
                _CameraEventAngle.y = shakingPos.y * time / ShakingPlayTime;
                time -= ShakingWaitTime;
                yield return new WaitForSeconds(ShakingWaitTime);
            }
            _CameraEventAngle = Vector3.zero;
            yield return null;
        }

        //걸을때 위아래 흔들리는거 쭈욱~
        private IEnumerator CorWalkShaking()
        {
            float vertical = 0.1f;
            float moveSpeed = 1.0f;
            float verticalValue = 0.0f;
            bool cameraUP = true;
            Vector3 basePos = headCamera.transform.localPosition;

            while (true)
            {
                if (_PlayerState == PLAYERSTATE.MOVE)
                {
                    if (cameraUP)
                    {
                        verticalValue += Time.fixedDeltaTime * moveSpeed;
                        headCamera.transform.localPosition = basePos + Vector3.up * verticalValue;

                        if (verticalValue > vertical)
                        {
                            cameraUP = false;
                        }
                    }
                    else
                    {
                        verticalValue -= Time.fixedDeltaTime * moveSpeed;
                        headCamera.transform.localPosition = basePos + Vector3.up * verticalValue;
                        if (verticalValue < -vertical)
                        {
                            cameraUP = true;
                        }
                    }
                }
                else
                {
                    if (Mathf.Abs(verticalValue) < Time.fixedDeltaTime * moveSpeed)
                    {
                        verticalValue = 0;
                        cameraUP = true;
                    }
                    else if (verticalValue > 0 && Mathf.Abs(verticalValue) > Time.fixedDeltaTime * moveSpeed)
                    {
                        verticalValue -= Time.fixedDeltaTime * moveSpeed;
                        headCamera.transform.localPosition = basePos + Vector3.up * verticalValue;
                    }
                    else if (verticalValue < 0 && Mathf.Abs(verticalValue) > Time.fixedDeltaTime * moveSpeed)
                    {
                        verticalValue += Time.fixedDeltaTime * moveSpeed;
                        headCamera.transform.localPosition = basePos + Vector3.up * verticalValue;
                    }
                }
                yield return new WaitForEndOfFrame();
            }
            yield return null;
        }

        private IEnumerator CorCameraShakeVertical()
        {
            float time = 0;
            while (time < ShakingPlayTime)
            {
                _CameraEventAngle.x = Mathf.Sin(time * Mathf.Deg2Rad * 2000) * 1;
                //Vector3 shakingPos = Random.insideUnitSphere * ShakingRadius;
                //_CameraEventAngle.x -= shakingPos.x;
                time += Time.fixedDeltaTime;
                yield return new WaitForEndOfFrame();
                //yield return new WaitForSeconds(ShakingWaitTime);
            }

            for (int i = 0; i < 10; i++)
            {
                _CameraEventAngle = Vector3.Lerp(_CameraEventAngle, Vector3.zero, (float)i / 10);
                yield return new WaitForEndOfFrame();
            }

            _CameraEventAngle = Vector3.zero;
            yield return null;
        }
    }
}