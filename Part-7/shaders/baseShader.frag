#version 330 core

uniform float time;
uniform sampler2D texture0;

in vec2 TexCoord;

out vec4 FragColor;

void main()
{
	FragColor = texture(texture0, TexCoord);
}