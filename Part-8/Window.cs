using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

namespace Part_8
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
     -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
      0.5f, -0.5f, -0.5f,  1.0f, 0.0f,
      0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
      0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
     -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
     -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,

     -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
      0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
      0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
      0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
     -0.5f,  0.5f,  0.5f,  0.0f, 1.0f,
     -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,

     -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
     -0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
     -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
     -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
     -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
     -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

      0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
      0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
      0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
      0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
      0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
      0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

     -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
      0.5f, -0.5f, -0.5f,  1.0f, 1.0f,
      0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
      0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
     -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
     -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,

     -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
      0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
      0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
      0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
     -0.5f,  0.5f,  0.5f,  0.0f, 0.0f,
     -0.5f,  0.5f, -0.5f,  0.0f, 1.0f
};

        uint[] indices =
        {
            0, 1, 3, // first triangle
            1, 2, 3, // second triangle
        };

        private int qVAO, qVBO, qEBO;
        private int cVAO, cVBO;
        private Shader shader;
        private Texture texture1;
        private Texture texture2;

        private bool doDepthTest = false;
        private bool usePerspectiveProjection = true;

        public Window(GameWindowSettings gameWindowSetting, NativeWindowSettings nativeWindowSetting) :
            base(gameWindowSetting, nativeWindowSetting)
        {

        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (KeyboardState.IsKeyPressed(Keys.R)) doDepthTest = !doDepthTest;
            if (KeyboardState.IsKeyPressed(Keys.E)) usePerspectiveProjection = !usePerspectiveProjection;

            if (KeyboardState.IsKeyPressed(Keys.Escape))
            {
                Close();
            }
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            if (doDepthTest) GL.Enable(EnableCap.DepthTest);
            else GL.Disable(EnableCap.DepthTest);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            float time = (float)GLFW.GetTime();
            Matrix4 model = Matrix4.Identity;
            model = Matrix4.CreateTranslation(0f, 0f, 0f) * model;
            model = Matrix4.CreateRotationX(MathHelper.DegreesToRadians((float)Math.Sin(time) * 55) + 45) * model;
            model = Matrix4.CreateRotationY(MathHelper.DegreesToRadians((float)Math.Cos(time) * 55)) * model;
            model = Matrix4.CreateScale(0.5f, 1.75f, 1f) * model;
            var view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);


            Matrix4 projection;
            if (usePerspectiveProjection)
                projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), (float)Size.X / Size.Y, 0.1f, 100.0f);
            else
                projection = Matrix4.CreateOrthographic(2, 2, 0.1f, 100f);
            //var projection = Matrix4.CreateOrthographicOffCenter(0, 800, 0, 600, 0.1f, 100.0f);

            shader.Use();
            shader.SetInt("texture0", 0);
            shader.SetInt("texture1", 1);

            shader.SetMat4("model", model);
            shader.SetMat4("view", view);
            shader.SetMat4("projection", projection);

            texture1.Use(TextureUnit.Texture0);
            texture2.Use(TextureUnit.Texture1);

            //DrawQuad();
            DrawCube();

            Context.SwapBuffers();

            Title = "UPS: " + (1 / UpdateTime).ToString("0000") + " FPS: " + (1 / RenderTime).ToString("0000");
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            LoadQuadObjects();
            LoadCubeObject();

            shader = new Shader("baseShader.vert", "baseShader.frag");

            texture1 = Texture.LoadFromFile(@"textures\wooden_container.jpg");
            texture2 = Texture.LoadFromFile(@"textures\yoba_face.png");
        }

        protected override void OnUnload()
        {
            shader.Dispose();

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

        private void DrawQuad()
        {
            GL.BindVertexArray(qVAO);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
        }
        private void LoadCubeObject()
        {
            cVAO = GL.GenVertexArray();
            GL.BindVertexArray(cVAO);

            cVBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, cVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, cubeVertices.Size(), cubeVertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            GL.BindVertexArray(0);
        }

        private void DrawCube()
        {
            GL.BindVertexArray(cVAO);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3 * 2 * 6);
        }
    }
}
