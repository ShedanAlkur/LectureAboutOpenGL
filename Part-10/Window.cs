using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

namespace Part_10
{
    class Window : GameWindow
    {

        float[] quadVertices =
        {
            // positions         // texCoords
            0.5f, 0.5f, 0.0f,    1.0f, 1.0f, // 1
            0.5f, -0.5f, 0.0f,   1.0f, 0.0f, // 2
            -0.5f, -0.5f, 0.0f,  0.0f, 0.0f, // 3
            -0.5f, 0.5f, 0.0f,   0.0f, 1.0f  // 4
        };

        /*  4---1
            |   |
            3---2  */

        float[] cubeVertices = {
            // vertex             // tex       // normal
            -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,  0.0f, 0.0f, -1.0f,
             0.5f, -0.5f, -0.5f,  1.0f, 0.0f,  0.0f, 0.0f, -1.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f,  0.0f, 0.0f, -1.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f,  0.0f, 0.0f, -1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,  0.0f, 0.0f, -1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,  0.0f, 0.0f, -1.0f,

            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,  0.0f, 0.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  1.0f, 0.0f,  0.0f, 0.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 1.0f,  0.0f, 0.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 1.0f,  0.0f, 0.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f, 1.0f,  0.0f, 0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,  0.0f, 0.0f, 1.0f,

            -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,  -1.0f, 0.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  1.0f, 1.0f,  -1.0f, 0.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,  -1.0f, 0.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,  -1.0f, 0.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,  -1.0f, 0.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,  -1.0f, 0.0f, 0.0f,

             0.5f,  0.5f,  0.5f,  1.0f, 0.0f,  1.0f, 0.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f,  1.0f, 0.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  0.0f, 1.0f,  1.0f, 0.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  0.0f, 1.0f,  1.0f, 0.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  0.0f, 0.0f,  1.0f, 0.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 0.0f,  1.0f, 0.0f, 0.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,  0.0f, -1.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  1.0f, 1.0f,  0.0f, -1.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  1.0f, 0.0f,  0.0f, -1.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  1.0f, 0.0f,  0.0f, -1.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,  0.0f, -1.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,  0.0f, -1.0f, 0.0f,

            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,  0.0f, 1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f,  0.0f, 1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 0.0f,  0.0f, 1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 0.0f,  0.0f, 1.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f, 0.0f,  0.0f, 1.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,  0.0f, 1.0f, 0.0f
        };

        uint[] indices =
        {
            0, 1, 3, // first triangle
            1, 2, 3, // second triangle
        };

        Vector3 lightPos = new Vector3(1.2f, 1.0f, 2.0f);

        private int qVAO, qVBO, qEBO;
        private int cVAO, cVBO;
        private Shader lightingShader, lampShader;


        private Camera camera;
        private bool doColorRotation = false;

        public Window(GameWindowSettings gameWindowSetting, NativeWindowSettings nativeWindowSetting) :
                base(gameWindowSetting, nativeWindowSetting)
        {
            camera = new Camera(new Vector3(0.0f, 0.0f, 3.0f), Vector3.UnitY, (float)Size.X / Size.Y);
            camera.Fov = MathHelper.PiOver4;
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            ProcessInput((float)args.Time);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // z-buffer
            GL.Enable(EnableCap.DepthTest);

            // Transparency
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            float time = (float)GLFW.GetTime();
            Vector3 color = (doColorRotation) ?
                (new Vector3((float)Math.Sin(time), (float)Math.Sin(time + MathHelper.ThreePiOver2), (float)Math.Sin(time + MathHelper.PiOver4))) / 2 + new Vector3(0.5f) :
                Vector3.One; lightPos = new Vector3(1.0f * MathF.Cos(MathHelper.DegreesToRadians(time * 60)), 1.0f, 1.0f * MathF.Sin(MathHelper.DegreesToRadians(time * 60)));
            Matrix4 model;

            GL.BindVertexArray(cVAO);

            lightingShader.Use();
            lightingShader.SetMat4("view", camera.GetViewMatrix());
            lightingShader.SetMat4("projection", camera.GetProjectionMatrix());
            lightingShader.SetVec3("objectColor", 1.0f, 0.5f, 0.31f);
            lightingShader.SetVec3("lightColor", new Vector3(color));
            lightingShader.SetVec3("lightPos", lightPos);
            model = Matrix4.Identity;
            //model = Matrix4.CreateTranslation(1.0f, 0.0f, 1.0f) * model;
            model = Matrix4.CreateRotationX(MathF.Cos(MathHelper.DegreesToRadians(time * 20))) * model;
            model = Matrix4.CreateRotationY(MathF.Sin(MathHelper.DegreesToRadians(time * 20))) * model;
            model = Matrix4.CreateRotationZ(-MathF.Cos(MathHelper.DegreesToRadians(time * 20))) * model;
            lightingShader.SetMat4("model", model);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

            GL.BindVertexArray(cVAO);
            lampShader.Use();
            lampShader.SetMat4("view", camera.GetViewMatrix());
            lampShader.SetMat4("projection", camera.GetProjectionMatrix());
            lampShader.SetVec3("lightColor", color);
            model = Matrix4.Identity;
            model = Matrix4.CreateTranslation(lightPos) * model;
            model = Matrix4.CreateScale(0.2f) * model;
            lampShader.SetMat4("model", model);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

            Context.SwapBuffers();

            Title = "UPS: " + (1 / UpdateTime).ToString("0000") + " FPS: " + (1 / RenderTime).ToString("0000");
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            LoadQuadObjects();
            LoadCubeObject();
            //
            lightingShader = new Shader(@"shaders\lightingShader.vert", @"shaders\lightingShader.frag");
            lampShader = new Shader(@"shaders\lampShader.vert", @"shaders\lampShader.frag");
            //shader.Use();

            CursorGrabbed = true;
        }

        protected override void OnUnload()
        {
            LoadQuadObjects();

            GL.BindBuffer(BufferTarget.ArrayBuffer, qVBO);
            GL.DeleteBuffer(qVBO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, qVBO);
            GL.DeleteBuffer(cVBO);

            base.OnUnload();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, Size.X, Size.Y);

            base.OnResize(e);
        }

        private void LoadQuadObjects()
        {
            // 1. bind VAO
            // VAO - vertex array objecy
            qVAO = GL.GenVertexArray();
            GL.BindVertexArray(qVAO);

            // 2. copy our vertices array in a vertices buffer for openGL to use
            // VBO - vertex buffer object
            qVBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, qVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, quadVertices.Length * sizeof(float), quadVertices, BufferUsageHint.StaticDraw);

            // 3. set out vertex attributes pointers for vertex position
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0); // aPosition attribute
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float)); // aTexCoord attribute
            GL.EnableVertexAttribArray(1);

            // 4. copy our indices array in a element buffer for openGL to use
            // EBO - element buffer object
            qEBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, qEBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Size(), indices, BufferUsageHint.StaticDraw);

            GL.BindVertexArray(0);
        }

        private void LoadCubeObject()
        {
            cVAO = GL.GenVertexArray();
            GL.BindVertexArray(cVAO);

            cVBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, cVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, cubeVertices.Size(), cubeVertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 5 * sizeof(float));
            GL.EnableVertexAttribArray(2);

            GL.BindVertexArray(0);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            camera.Fov -= MathHelper.DegreesToRadians(e.OffsetY);
        }

        private void ProcessInput(float deltaTime)
        {
            if (KeyboardState.IsKeyPressed(Keys.Escape))
            {
                Close();
            }

            Direction direction = Direction.None;
            if (KeyboardState.IsKeyDown(Keys.W)) direction = direction | Direction.Forward;
            if (KeyboardState.IsKeyDown(Keys.S)) direction = direction | Direction.Backward;
            if (KeyboardState.IsKeyDown(Keys.A)) direction = direction | Direction.Left;
            if (KeyboardState.IsKeyDown(Keys.D)) direction = direction | Direction.Right;
            if (KeyboardState.IsKeyDown(Keys.Space)) direction = direction | Direction.Up;
            if (KeyboardState.IsKeyDown(Keys.LeftShift)) direction = direction | Direction.Down;
            if (direction != Direction.None) camera.ProcessKeyboard(direction, deltaTime);

            if (KeyboardState.IsKeyPressed(Keys.R)) doColorRotation = !doColorRotation;

            var mouse = MouseState;
            camera.ProcessMouseMovement(mouse.X - mouse.PreviousX,
                mouse.PreviousY - mouse.Y, true);
        }
    }
}
