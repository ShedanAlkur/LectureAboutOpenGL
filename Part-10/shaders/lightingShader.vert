#version 330 core
layout (location = 0) in vec3 aPosition;
layout (location = 2) in vec3 aNormal;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec3 normal;
out vec3 fragPos;

void main()
{
	gl_Position = projection * view * model * vec4(aPosition, 1.0);
	fragPos = vec3(model * vec4(aPosition, 1.0));
	mat3 normalMatrix = mat3(transpose(inverse(model)));
	normal = normalMatrix * aNormal;
}