using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Part_7
{
    class Shader : IDisposable
    {
        private const string shaderFolder = @"shaders\";
        public readonly int Handle;
        private Dictionary<string, int> uniformLocations;

        public Shader(string vertexPath, string fragmentPath)
        {
            // ver1
            string vertexShaderSource;
            using (StreamReader reader = new StreamReader(shaderFolder + vertexPath, Encoding.UTF8))
                vertexShaderSource = reader.ReadToEnd();

            // ver2
            string fragmentShaderSource = File.ReadAllText(shaderFolder + fragmentPath, Encoding.UTF8);

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexShaderSource);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentShaderSource);

            CompileShader(vertexShader);
            CompileShader(fragmentShader);

            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);

            LinkProgram(Handle);

            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);
            uniformLocations = new Dictionary<string, int>();
            for (int i = 0; i < numberOfUniforms; i++)
            {
                string key = GL.GetActiveUniform(Handle, i, out _, out _);
                int location = GL.GetUniformLocation(Handle, key);
                uniformLocations.Add(key, location);
            }
        }

        private static void CompileShader(int shader)
        {
            GL.CompileShader(shader);

            GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
            if (code != (int)All.True)
            {
                string infoLog = GL.GetShaderInfoLog(shader);
                throw new Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
            }
        }

        private static void LinkProgram(int program)
        {
            GL.LinkProgram(program);

            //GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
            //if (code != (int)All.True)) throw new Exception($"Error occurred whilst linking Program({program})");

            string infoLog = GL.GetProgramInfoLog(program);
            if (infoLog != string.Empty)
                throw new Exception($"Error occurred whilst linking Program({program}).\n\n{infoLog}");

        }

        public void Use()
        {
            GL.UseProgram(Handle);
        }

        public void SetInt(string name, int data)
        {
            GL.Uniform1(uniformLocations[name], data);
        }
        public void SetFloat(string name, float data)
        {
            GL.Uniform1(uniformLocations[name], data);
        }
        public void SetVec2(string name, Vector2 data)
        {
            GL.Uniform2(uniformLocations[name], ref data);
        }
        public void SetVec3(string name, Vector3 data)
        {
            GL.Uniform3(uniformLocations[name], ref data);
        }
        public void SetVec4(string name, Vector4 data)
        {
            GL.Uniform4(uniformLocations[name], ref data);
        }
        public void SetMat3(string name, Matrix3 data, bool transpose = false)
        {
            GL.UniformMatrix3(uniformLocations[name], transpose, ref data);
        }
        public void SetMat4(string name, Matrix4 data, bool transpose = false)
        {
            GL.UniformMatrix4(uniformLocations[name], transpose, ref data);
        }

        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(Handle, attribName);
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(Handle);

                disposedValue = true;
            }
        }

        ~Shader()
        {
            GL.DeleteProgram(Handle);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }

}
