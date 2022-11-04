#version 330 core

uniform sampler2D texture1;

in vec2 TexCoord;

out vec4 FragColor;

uniform int renderMode;

//const float offsetX = 1.0/800.0;
//const float offsetY = 1.0/600.0;

const float offsetX = 1.0/300.0;
const float offsetY = offsetX;

const vec2 offsets[9] = vec2[](
    vec2(-offsetX,  offsetY),
    vec2( 0.0f,    offsetY),
    vec2( offsetX,  offsetY),
    vec2(-offsetX,  0.0f),  
    vec2( 0.0f,    0.0f),  
    vec2( offsetX,  0.0f),  
    vec2(-offsetX, -offsetY),
    vec2( 0.0f,   -offsetY),
    vec2( offsetX, -offsetY) 
);

const float kernel1[9] = float[](
    -1, -1, -1,
    -1,  9, -1,
    -1, -1, -1
);

const float kernel2[9] = float[](
    1.0 / 16, 2.0 / 16, 1.0 / 16,
    2.0 / 16, 4.0 / 16, 2.0 / 16,
    1.0 / 16, 2.0 / 16, 1.0 / 16  
);

const float kernel3[9] = float[](
    1., 1.0, 1.,
    1., -8., 1.,
    1., 1.0, 1.  
);

const float kernel4[9] = float[](
    0., 1.0, 0.,
    1., -4., 1.,
    0., 1.0, 0.  
);

const float kernel5[9] = float[](
    1., -2., 1.,
    -2., 4., -2.,
    1., -2., 1.  
);

void main()
{
	vec3 color = vec3(texture(texture1, TexCoord));

    vec3 sampleTex[9];
    for(int i = 0; i < 9; i++)
    {
        sampleTex[i] = vec3(texture(texture1, TexCoord.st + offsets[i]));
    }

    switch (renderMode)
    {
        case 1:            
	        // negative
	        color = 1. - color;
            break;
        case 2:        
	        // gray
	        float average = 0.2126 * color.r + 0.7152 * color.g + 0.0722 * color.b;
            color = vec3(average);
            break;
        case 3:
            // filter
            color = vec3(0.0);
            for(int i = 0; i < 9; i++)
                color += sampleTex[i] * kernel1[i];
            break;
        case 4:
            // filter
            color = vec3(0.0);
            for(int i = 0; i < 9; i++)
                color += sampleTex[i] * kernel2[i];
            break;
        case 5:
            // filter
            color = vec3(0.0);
            for(int i = 0; i < 9; i++)
                color += sampleTex[i] * kernel3[i];
	        average = 0.2126 * color.r + 0.7152 * color.g + 0.0722 * color.b;
            color = vec3(average);       
            break;
        case 6:
            // filter
            color = vec3(0.0);
            for(int i = 0; i < 9; i++)
                color += sampleTex[i] * kernel4[i];
	        average = 0.2126 * color.r + 0.7152 * color.g + 0.0722 * color.b;
            color = vec3(average);       
            break;
        case 7:
            // filter
            color = vec3(0.0);
            for(int i = 0; i < 9; i++)
                color += sampleTex[i] * kernel5[i];
	        average = 0.2126 * color.r + 0.7152 * color.g + 0.0722 * color.b;
            color = vec3(average);       
            break;
        default: break;
    }
    
    FragColor = vec4(color, 1.0);
}