using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Part_2
{
    class Window : GameWindow
    {
        float[] vertices =
{
            -0.5f, -0.5f, 0.0f, // 1
             0.5f, -0.5f, 0.0f, // 2
             0.0f,  0.5f, 0.0f, // 3
        };
        /*  3
           / \
          1---2  */

        private int VBO;
        private int VAO;
        private Shader shader;

        public Window(GameWindowSettings gameWindowSetting, NativeWindowSettings nativeWindowSetting) :
            base(gameWindowSetting, nativeWindowSetting)
        {
            const int updatesPerSecond = 200;
            const int framesPerSecond = 60;
            this.RenderFrequency = framesPerSecond;
            this.UpdateFrequency = updatesPerSecond;
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

            GL.ClearColor(0.9f, 0.9f, 1.0f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            shader.Use();
            GL.BindVertexArray(VAO);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
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

            // 2. copy our vertices array in a buffer for openGL to use
            // VBO - vertex buffer object
            VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            // 3. set out vertex attribures pointers
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.BindVertexArray(0);
            //
            shader = new Shader("baseShader.vert", "baseShader.frag");
        }

        protected override void OnUnload()
        {
            GL.DeleteBuffer(VBO);
            GL.DeleteBuffer(VAO);

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
