using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Part_12
{
    class FrameBuffer
    {
        public Texture Texture;
        public int FramebufferID;
        public int RenderbufferID;

        int width;
        int height;

        public FrameBuffer(int width, int height)
        {
            this.width = width;
            this.height = height;

            this.Texture = new Texture(width, height);
            FramebufferID = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, FramebufferID);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0,
                TextureTarget.Texture2D, Texture.Handle, 0);

            RenderbufferID = GL.GenRenderbuffer();
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, RenderbufferID);
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.Depth24Stencil8,
                width, height);
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthStencilAttachment,
                RenderbufferTarget.Renderbuffer, RenderbufferID);

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer,
                FramebufferAttachment.DepthStencilAttachment,
                RenderbufferTarget.Renderbuffer,
                RenderbufferID);
        }

        public void Resize(int width, int height)
        {
            if (this.width == width && this.height == height) return;
            this.width = width;
            this.height = height;

            Texture.Resize(width, height);

            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, RenderbufferID);
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.Depth24Stencil8,
                width, height);
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, FramebufferID);
        }
        public static void Unbind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }
    }
}
