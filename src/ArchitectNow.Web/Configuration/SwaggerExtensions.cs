﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ArchitectNow.Models.Options;
using ArchitectNow.Web.Models;
using Microsoft.AspNetCore.Builder;
using NJsonSchema;
using NJsonSchema.Generation;
using NSwag;
using NSwag.AspNetCore;
using NSwag.SwaggerGeneration;
using NSwag.SwaggerGeneration.WebApi.Processors.Security;

namespace ArchitectNow.Web.Configuration
{
    public static class SwaggerExtensions
    {
	    public static void ConfigureSwagger(this IApplicationBuilder app, Assembly assembly, IEnumerable<SwaggerOptions> options)
	    {
		    foreach (var option in options)
		    {
			    var action = option.Configure;

			    var swaggerUiOwinSettings = new SwaggerUiSettings
			    {
				    DefaultPropertyNameHandling = PropertyNameHandling.CamelCase,
				    Title = option.Title,
				    SwaggerRoute = option.SwaggerRoute,
				    SwaggerUiRoute = option.SwaggerUiRoute,
				    UseJsonEditor = true,
				    FlattenInheritanceHierarchy = true,
				    IsAspNetCore = true
			    };
			    
			    foreach (var optionDocumentProcessor in option.DocumentProcessors)
			    {
				    swaggerUiOwinSettings.DocumentProcessors.Add(optionDocumentProcessor);
			    }
			    
			    action?.Invoke(swaggerUiOwinSettings);
			    if (option.Controllers?.Any() == true)
			    {
				    app.UseSwaggerUi(option.Controllers, swaggerUiOwinSettings,
					    new SwaggerJsonSchemaGenerator((JsonSchemaGeneratorSettings) swaggerUiOwinSettings));
			    }
			    else
			    {
				    app.UseSwaggerUi(assembly, swaggerUiOwinSettings);
			    }
		    }
	    }
	}
}
