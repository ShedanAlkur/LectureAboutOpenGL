#version 330 core
uniform vec3 objectColor;
uniform vec3 lightColor;
uniform vec3 lightPos;

in vec3 normal;
in vec3 fragPos;

out vec4 FragColor;

void main()
{
	// ambient
	float ambientStrength = 0.1;
	vec3 ambient = ambientStrength * lightColor;

	// diffuse
	float diffuseFactor = dot(normalize(normal), normalize(lightPos - fragPos));
	vec3 diffuse = max(diffuseFactor, 0) * lightColor;

	vec3 result = (ambient + diffuse) * objectColor;
	FragColor = vec4(result, 1.0f);
}