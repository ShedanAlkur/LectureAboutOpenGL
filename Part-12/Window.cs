using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

namespace Part_12
{
    class Window : GameWindow
    {
        private float accumulatedTime;
        private float targetUpdatePeriod;

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

        private int EBO, VBO, VAO;
        private int EBO2, VBO2, VAO2;
        private Shader screenShader, lightingShader, lampShader;
        private Texture texture1;
        private Texture texture2;
        private FrameBuffer frameBuffer;

        private Camera camera;
        private int renderMode = 0;

        public Window(GameWindowSettings gameWindowSetting, NativeWindowSettings nativeWindowSetting) :
            base(gameWindowSetting, nativeWindowSetting)
        {
            const int updatesPerSecond = 200;
            const int framesPerSecond = 60;
            targetUpdatePeriod = 1f / updatesPerSecond;
            accumulatedTime = 0;
            this.RenderFrequency = framesPerSecond;
            //this.UpdateFrequency = updatesPerSecond;

            camera = new Camera(new Vector3(0.0f, 0.0f, 3.0f), Vector3.UnitY, (float)Size.X / Size.Y);
            camera.Fov = MathHelper.PiOver4;
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            accumulatedTime += (float)UpdateTime;
            while (accumulatedTime >= targetUpdatePeriod)
            {
                accumulatedTime -= targetUpdatePeriod;
                // update
            }

            ProcessInput((float)args.Time);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            frameBuffer.Bind();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // z-buffer
            GL.Enable(EnableCap.DepthTest);

            // Transparency
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            float time = (float)GLFW.GetTime();
            // lightPos = new Vector3(1.0f * MathF.Cos(MathHelper.DegreesToRadians(time * 60)), 1.0f, 1.0f * MathF.Sin(MathHelper.DegreesToRadians(time * 60)));
            Matrix4 model;

            GL.BindVertexArray(VAO2);
            texture1.Use(TextureUnit.Texture0);
            texture2.Use(TextureUnit.Texture1);

            lightingShader.Use();
            lightingShader.SetMat4("view", camera.GetViewMatrix());
            lightingShader.SetMat4("projection", camera.GetProjectionMatrix());
            lightingShader.SetVec3("viewPos", camera.Position);

            lightingShader.SetInt("material.diffuse", 0);
            lightingShader.SetInt("material.specular", 1);
            lightingShader.SetFloat("material.shininess", 32f);

            Vector3 lightColor = new Vector3(1.0f, 1.0f, 1.0f);

            lightingShader.SetVec3("light.position", lightPos);
            lightingShader.SetVec3("light.ambient", lightColor * 0.2f);
            lightingShader.SetVec3("light.diffuse", lightColor * 0.5f);
            lightingShader.SetVec3("light.specular", new Vector3(1.0f));

            model = Matrix4.Identity;
            lightingShader.SetMat4("model", model);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

            GL.BindVertexArray(VAO2);
            lampShader.Use();
            lampShader.SetMat4("view", camera.GetViewMatrix());
            lampShader.SetMat4("projection", camera.GetProjectionMatrix());
            model = Matrix4.Identity;
            model = Matrix4.CreateTranslation(lightPos) * model;
            model = Matrix4.CreateScale(0.2f) * model;
            lampShader.SetMat4("model", model);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

            FrameBuffer.Unbind();

            GL.ClearColor(0.1f, 0.3f, 0.3f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            screenShader.Use();
            GL.BindVertexArray(VAO);
            GL.Disable(EnableCap.DepthTest);
            frameBuffer.Texture.Use(TextureUnit.Texture2); 
            screenShader.SetInt("texture1", 2);
            screenShader.SetInt("renderMode", renderMode);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            Context.SwapBuffers();

            Title = "UPS: " + (1 / UpdateTime).ToString("0000") + " FPS: " + (1 / RenderTime).ToString("0000");
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            LoadQuadObjects();
            LoadCubeObject();
            //
            screenShader = new Shader(@"shaders\screenShader.vert", @"shaders\screenShader.frag");
            lightingShader = new Shader(@"shaders\lightingShader.vert", @"shaders\lightingShader.frag");
            lampShader = new Shader(@"shaders\lampShader.vert", @"shaders\lampShader.frag");
            //shader.Use();

            texture1 = Texture.LoadFromFile(@"textures\container.png");
            texture2 = Texture.LoadFromFile(@"textures\container_specular.png");
            frameBuffer = new FrameBuffer(Size.X, Size.Y);

            CursorGrabbed = true;
        }

        protected override void OnUnload()
        {
            LoadQuadObjects();

            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.DeleteBuffer(VBO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.DeleteBuffer(VBO2);

            base.OnUnload();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, Size.X, Size.Y);
            frameBuffer.Resize(Size.X, Size.Y);

            base.OnResize(e);
        }

        private void LoadQuadObjects()
        {
            // 1. bind VAO
            // VAO - vertex array objecy
            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);

            // 2. copy our vertices array in a vertices buffer for openGL to use
            // VBO - vertex buffer object
            VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, quadVertices.Length * sizeof(float), quadVertices, BufferUsageHint.StaticDraw);

            // 3. set out vertex attributes pointers for vertex position
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0); // aPosition attribute
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float)); // aTexCoord attribute
            GL.EnableVertexAttribArray(1);

            // 4. copy our indices array in a element buffer for openGL to use
            // EBO - element buffer object
            EBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Size(), indices, BufferUsageHint.StaticDraw);

            GL.BindVertexArray(0);
        }

        private void LoadCubeObject()
        {
            VAO2 = GL.GenVertexArray();
            GL.BindVertexArray(VAO2);

            VBO2 = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO2);
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

            if (KeyboardState.IsKeyPressed(Keys.E)) renderMode = (--renderMode < 0) ? 7 : renderMode;
            if (KeyboardState.IsKeyPressed(Keys.R)) renderMode = (renderMode + 1) % 8;

            var mouse = MouseState;
            camera.ProcessMouseMovement(mouse.X - mouse.PreviousX,
                mouse.PreviousY - mouse.Y, true);
        }
    }
}
