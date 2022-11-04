#version 330 core

struct Material{
	sampler2D diffuse;
	sampler2D specular;
	float shininess;
};

struct Light {
	vec3 position;

	vec3 ambient;
	vec3 diffuse;
	vec3 specular;
};

uniform Material material;
uniform Light light;
uniform vec3 viewPos;

in vec3 normal;
in vec3 fragPos;
in vec2 texCoord;

out vec4 FragColor;

void main()
{
	// ambient
	vec3 ambient = light.ambient * vec3(texture(material.diffuse, texCoord));

	// diffuse
	vec3 norm = normalize(normal);
	vec3 lightDir = normalize(light.position - fragPos);
	float diff = max(dot(norm, lightDir), 0);
	vec3 diffuse = diff * light.diffuse * vec3(texture(material.diffuse, texCoord));

	// specular
	vec3 viewDir = normalize(viewPos - fragPos);
	vec3 reflectDir = reflect(-lightDir, norm);
	float spec = pow(max(dot(viewDir, reflectDir), 0), material.shininess);
	vec3 specular = spec * light.specular * vec3(texture(material.specular, texCoord));

	vec3 result = ambient + diffuse + specular;
	FragColor = vec4(result, 1.0f);
}