#version 330 core

uniform float time;

out vec4 FragColor;

void main()
{
	float factor = cos(time) / 2 + 0.5f;
	vec4 color1 = vec4(1.0f, 0.5f, 0.2f, 1.0f);
	vec4 color2 = vec4(.0f, .9f, .2f, 1.0f);
	FragColor = mix(color1, color2, factor);
}