using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

namespace Part_6
{
    class Window : GameWindow
    {
        private float accumulatedTime;
        private float targetUpdatePeriod;

        float[] vertices =
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

        uint[] indices =
        {
            0, 1, 3, // first triangle
            1, 2, 3, // second triangle
        };

        private int EBO;
        private int VBO;
        private int VAO;
        private Shader shader;
        private Texture texture;

        private Vector2 tileRelSize = new Vector2(1f / 5, 1f / 2);
        private Vector2 tileOffset = new Vector2(0f, 0f);
        private float animationFramePeriod = 0.08f;
        private float animationTimeCounter = 0f;

        public Window(GameWindowSettings gameWindowSetting, NativeWindowSettings nativeWindowSetting) :
            base(gameWindowSetting, nativeWindowSetting)
        {

        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (KeyboardState.IsKeyPressed(Keys.Escape))
            {
                Close();
            }
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            shader.Use();
            shader.SetInt("texSheet", 0);
            shader.SetVec2("tileRelSize", tileRelSize);

            animationTimeCounter += (float)RenderTime;
            while (animationTimeCounter >= animationFramePeriod)
            {
                animationTimeCounter -= animationFramePeriod;
                // update
                tileOffset.Y = (tileOffset.Y + (int)(++tileOffset.X / 5)) % 2;
                tileOffset.X = tileOffset.X % 5;
            }
            shader.SetVec2("tileOffset", tileOffset);

            GL.BindVertexArray(VAO);
            shader.SetInt("texSheet", 0);
            texture.Use(TextureUnit.Texture0);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            Context.SwapBuffers();

            Title = "UPS: " + (1 / UpdateTime).ToString("0000") + " FPS: " + (1 / RenderTime).ToString("0000");
        }

        protected override void OnLoad()
        {
            base.OnLoad();


            // 1. bind VAO
            // VAO - vertex array objecy
            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);

            // 2. copy our vertices array in a vertices buffer for openGL to use
            // VBO - vertex buffer object
            VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

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
            //
            shader = new Shader("baseShader.vert", "baseShader.frag");
            texture = Texture.LoadFromFile(@"textures\ame_sheet.png");
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.DeleteBuffer(VBO);

            shader.Dispose();

            base.OnUnload();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, Size.X, Size.Y);

            base.OnResize(e);
        }
    }
}
