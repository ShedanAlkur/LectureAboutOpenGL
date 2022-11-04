﻿#version 330 core
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec2 aTexCoord;

uniform mat4 transform;

out vec2 TexCoord;

void main()
{
	gl_Position = transform * vec4(aPosition, 1.0);
	TexCoord = aTexCoord;
}