using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Part_1
{
    class Window : GameWindow
    {
        private float accumulatedUpdateTime;
        private float targetUpdatePeriod;

        public Window(GameWindowSettings gameWindowSetting, NativeWindowSettings nativeWindowSetting) :
            base(gameWindowSetting, nativeWindowSetting)
        {
            const int updatesPerSecond = 200;
            const int framesPerSecond = 60;
            this.RenderFrequency = framesPerSecond;
            this.UpdateFrequency = updatesPerSecond;
            targetUpdatePeriod = 1f / updatesPerSecond;
            accumulatedUpdateTime = 0;
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            accumulatedUpdateTime += (float)UpdateTime;
            while (accumulatedUpdateTime >= targetUpdatePeriod)
            {
                accumulatedUpdateTime -= targetUpdatePeriod;
                // game logic update
            }

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

            Context.SwapBuffers();
            Title = "UPS: " + (1 / UpdateTime).ToString("0000") + " FPS: " + (1 / RenderTime).ToString("0000");
        }

        protected override void OnLoad()
        {
            base.OnLoad();
        }

        protected override void OnUnload()
        {
            base.OnUnload();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
        }
    }
}
