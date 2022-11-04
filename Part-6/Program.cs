using System;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

namespace Part_6
{
    class Program
    {
        static void Main(string[] args)
        {
            GameWindowSettings gameWindowSettings = GameWindowSettings.Default;
            NativeWindowSettings nativeWindowSetting = new NativeWindowSettings()
            {
                Size = new Vector2i(800, 600),
            };

            Window window = new Window(gameWindowSettings, nativeWindowSetting);
            window.Run();
        }
    }
}
