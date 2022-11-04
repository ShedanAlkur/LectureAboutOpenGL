#version 330 core
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec2 aTexCoord;
layout (location = 2) in vec3 aNormal;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
uniform mat3 normalMatrix;
uniform vec3 lightPos;

out vec3 normal;
out vec3 fragPos;
out vec2 texCoord;

void main()
{
	gl_Position = projection * view * model * vec4(aPosition, 1.0);
	fragPos = vec3(model * vec4(aPosition, 1.0));
	mat3 normalMatrix = mat3(transpose(inverse(model)));
	normal = normalMatrix * aNormal;
	texCoord = aTexCoord;
}