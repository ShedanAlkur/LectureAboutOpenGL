using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL4;

namespace Part_3
{
    class Window : GameWindow
    {
        float[] vertices =
        {
             0.5f,  0.5f, 0.0f, // 1
             0.5f, -0.5f, 0.0f, // 2
            -0.5f, -0.5f, 0.0f, // 3
            -0.5f,  0.5f, 0.0f, // 4
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

        private bool isLineMode = false;

        public Window(GameWindowSettings gameWindowSetting, NativeWindowSettings nativeWindowSetting) :
            base(gameWindowSetting, nativeWindowSetting)
        {

        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (KeyboardState.IsKeyPressed(Keys.R)) isLineMode = !isLineMode;

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

            if (isLineMode)
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            else
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            shader.Use();
            GL.BindVertexArray(VAO);
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

            // 3. set out vertex attribures pointers for vertex position
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // 4. copy our indices array in a element buffer for openGL to use
            // EBO - element buffer object
            EBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Size(), indices, BufferUsageHint.StaticDraw);

            GL.BindVertexArray(0);
            //
            shader = new Shader("baseShader.vert", "baseShader.frag");
        }

        protected override void OnUnload()
        {
            GL.DeleteBuffer(EBO);
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
