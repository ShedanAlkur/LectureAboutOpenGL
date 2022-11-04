#version 330 core

uniform sampler2D texSheet;
uniform vec2 tileRelSize;
uniform vec2 tileOffset;

in vec2 TexCoord;

out vec4 FragColor;

void main()
{
	vec2 tileTexCoord = (TexCoord + tileOffset) * tileRelSize;
	FragColor = texture(texSheet, tileTexCoord);
}