using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Part_12
{
    [Flags]
    enum Direction
    {
        None = 0,
        Forward = 1 << 1,
        Backward = 1 << 2,
        Left = 1 << 3,
        Right = 1 << 4,
        Up = 1 << 5,
        Down = 1 << 6,
    }

    class Camera
    {
        const float YAW = -90.0f;
        const float PITCH = 0.0f;
        const float SPEED = 2.5f;
        const float SENSITIVITY = 0.1f;
        const float ZOOM = 45.0f;
        const float FOV = MathHelper.PiOver4;

        static readonly float MIN_FOV = MathHelper.DegreesToRadians(1.0f);
        static readonly float MAX_FOV = MathHelper.DegreesToRadians(179.0f);

        public Vector3 Position;
        public Vector3 Front;
        public Vector3 Up;
        public Vector3 Right;
        public Vector3 WorldUp;

        public float Yaw;
        public float Pitch;

        public float MovementSpeed;
        public float MouseSensitivity;
        public float Zoom;

        private float fov;
        public float Fov { get => fov;
            set
            {
                fov = MathHelper.Clamp(value, MIN_FOV, MAX_FOV);
            }
        }
        public float AspectRatio;

        public Camera(Vector3 position, Vector3 worldUp, float aspectRatio, float yaw = YAW, float pitch = PITCH)
        {
            MovementSpeed = SPEED;
            MouseSensitivity = SENSITIVITY;
            Zoom = ZOOM;
            this.AspectRatio = aspectRatio;
            this.Fov = FOV;
            this.Position = position;
            this.WorldUp = worldUp;
            this.Yaw = yaw;
            this.Pitch = pitch;
            UpdateCameraVectors();
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + Front, Up);
        }

        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(Fov, AspectRatio, 0.01f, 100f);
        }

        private void UpdateCameraVectors()
        {
            Vector3 front;
            front.X = MathF.Cos(MathHelper.DegreesToRadians(Yaw)) * MathF.Cos(MathHelper.DegreesToRadians(Pitch));
            front.Y = MathF.Sin(MathHelper.DegreesToRadians(Pitch));
            front.Z = MathF.Sin(MathHelper.DegreesToRadians(Yaw)) * MathF.Cos(MathHelper.DegreesToRadians(Pitch));
            Front = Vector3.Normalize(front);

            Right = Vector3.Normalize(Vector3.Cross(Front, WorldUp));
            Up = Vector3.Normalize(Vector3.Cross(Right, Front));
        }

        public void ProcessKeyboard(Direction direction, float deltaTime)
        {            
            float velocity = MovementSpeed * deltaTime;
            if (direction.HasFlag(Direction.Forward))
                Position += Front * velocity;
            if (direction.HasFlag(Direction.Backward))
                Position -= Front * velocity;

            if (direction.HasFlag(Direction.Right))
                Position += Right * velocity;
            if (direction.HasFlag(Direction.Left))
                Position -= Right * velocity;

            if (direction.HasFlag(Direction.Up))
                Position += Up * velocity;
            if (direction.HasFlag(Direction.Down))
                Position -= Up * velocity;
        }

        public void ProcessMouseMovement(float xoffset, float yoffset, bool constrainPitch = true)
        {
            xoffset *= MouseSensitivity;
            yoffset *= MouseSensitivity;

            Yaw += xoffset;
            Pitch += yoffset;

            if (constrainPitch)
            {
                Pitch = MathHelper.Clamp(Pitch, -89f, 89f);
            }

            UpdateCameraVectors();
        }
    }
}
