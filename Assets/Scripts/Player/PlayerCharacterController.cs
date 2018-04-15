using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCharacter
{
    partial class PlayerCharacterController : MonoBehaviour
    {
        enum INPUTKEY
        {
            KEY_W = 1,
            KEY_S = 2,
            KEY_A = 4,
            KEY_D = 8,
            KEY_LSHIFT = 16,
            KEY_SPACE = 32,
            MOUSE_LEFT = 64,
            MOUSE_RIGHT = 128
        }
        enum PLAYERSTATE
        {
            STAND,
            MOVE,
            ROLL,
            FALLING,
            HIT,
            DEAD
        }
        enum PLAYERUPDATESTATE
        {
            NONE,
            FLASH,
            ATTACK,
            JUMP
        }

        //이동 관련
        public float m_TurnSpeed = 360;
        public float _AccelSpeed = 100;
        public float _BreakSped = 100;
        public float _MaxSpeed = 15;

        //카메라 회전
        private Vector3 CameraRot;
        private Vector3 PlayerRot;
        public float _CameraDownAngleX = 60;
        public float _CameraUpAngleX = -80;
        Vector3 _CameraEventAngle;

        //순간이동 관련 값
        public float _FlashDistance = 10;
        public float _FlashTime = 0.1f;
        public float _FlashDelay = 3.0f;
        private float _FlashRotAngle = 0.0f;
        private float _FlashCoolTime = 0.0f;

        //키 입력값
        //int m_KeyBit = 0;
        float _PlayerRotAngle = 0;
        PLAYERSTATE _PlayerState = PLAYERSTATE.STAND;
        PLAYERUPDATESTATE _PlayerUpdateState = PLAYERUPDATESTATE.NONE;

        //머리 카메라
        public GameObject headCamera;

        //카메라 진동값
        //추후 설정 저장해서 3~4개정도 만들것
        public float ShakingPlayTime;
        public float ShakingWaitTime;
        public float ShakingRadius;

        //플레이어 물리
        Rigidbody rigid;

        //코루틴 확인
        bool corFlash = false;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //m_BulletReroadCount = m_BulletCurrentCount;
            rigid = this.GetComponent<Rigidbody>();
            StartCoroutine("CorWalkShaking");
        }

        // Update is called once per frame
        void Update()
        {
            //캐릭터 회전값 및 키입력 상태체크
            CheckPlayerState();

            //캐릭터 머리 회전
            CameraSetRot();

            //쿨타임 체크
            CoolTimeCheck();
            //if (Input.GetMouseButtonDown(0))
            //{
            //    StartCoroutine("CorCameraShakeVertical");
            //}
            //if (Input.GetMouseButtonDown(1))
            //{
            //    StartCoroutine("CorCameraShake");
            //}
        }
        private void LateUpdate()
        {
            PlayerStateMoveLU();   
        }
        private void FixedUpdate()
        {
            PlayerStateMoveFU();
        }

        private void CoolTimeCheck()
        {
            if (_FlashCoolTime > 0) _FlashCoolTime -= Time.fixedDeltaTime;
        }
    }
}