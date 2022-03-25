using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Eccentric {
    public class PlayerMove : MonoBehaviour {

        public Transform CameraTransform;
        public float RotationSencitivity = 3f;
        public Rigidbody Rigidbody;
        public float JumpForce;
        public float Speed;

        private float _xRoation;
        private bool _grounded;

        void Update() {
            _xRoation -= Input.GetAxis("Mouse Y") * RotationSencitivity;
            _xRoation = Mathf.Clamp(_xRoation, -70f, 70f);
            CameraTransform.localEulerAngles = new Vector3(_xRoation, 0f, 0f);

            Vector3 resultEuler = transform.eulerAngles + new Vector3(0, Input.GetAxis("Mouse X") * RotationSencitivity, 0);
            Rigidbody.MoveRotation(Quaternion.Euler(resultEuler));
            //transform.Rotate(0, Input.GetAxis("Mouse X") * RotationSencitivity, 0);

            if (Input.GetKeyDown(KeyCode.Space)) {
                if (_grounded) {
                    Rigidbody.AddForce(0, JumpForce, 0, ForceMode.Impulse);
                }
            }
        }

        private void FixedUpdate() {
            Vector3 velicity = transform.TransformVector(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * Speed;
            Rigidbody.velocity = new Vector3(velicity.x, Rigidbody.velocity.y, velicity.z);

            // обнуляем _grounded в конце каждого FixedUpdate()
            // перед каждым FixedUpdate() выполняется OnCollisionStay(),
            // который делает _grounded = true, если игрок касается замли
            
            _grounded = false;

        }

        private void OnCollisionStay(Collision collision) {
            for (int i = 0; i < collision.contactCount; i++) {
                float dot = Vector3.Dot(Vector3.up, collision.contacts[i].normal);
                if (dot > 0.3f) {
                    _grounded = true;
                }
            }
        }
    }
}

