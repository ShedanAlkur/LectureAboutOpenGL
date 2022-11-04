#version 330 core
uniform sampler2D texture0;
uniform sampler2D texture1;

in vec2 TexCoord;

out vec4 FragColor;

void main()
{
    vec4 color1 = texture(texture0, TexCoord);
    vec4 color2 = texture(texture1, TexCoord);
	FragColor = mix(color1, color2, 0.2);
}