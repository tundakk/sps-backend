#!/usr/bin/env python3
"""
SPS Backend - Controller Generator Script

This script generates REST API controllers for the SPS Backend solution.
It creates boilerplate controller code following the project's patterns.

Author: SPS Backend Development Team
License: MIT
Created: 2025
Last Modified: June 16, 2025

WARNING: This script will overwrite existing files. Use with caution!
"""

import os
from typing import List

# Define the models for which you want to create controllers
MODELS: List[str] = [
    'Student',
    'Education',
    'SpsaCase',
    'Period',
    'Place',
    'Diagnosis',
    'EduCategory',
    'StudentPayment',
    'SupportingTeacher',
    'OpkvalSupervision',
    'EduStatus',
    'EducationPeriodRate',
    'TeacherPayment',
    'SupportType',
]

# Directory to place the generated controller files (relative to project root)
CONTROLLERS_DIR = './sps.API/Controllers/Implementations'


def create_controller_content(model: str) -> str:
    """
    Creates the C# controller code content for a given model.
    
    Args:
        model: The model name (e.g., 'Student')
        
    Returns:
        Complete C# controller code as string
    """
    service_var = model[0].lower() + model[1:]  # camelCase version
    
    return f"""using sps.API.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace sps.API.Controllers.Implementations
{{
    /// <summary>
    /// Controller for managing {model} entities
    /// Provides CRUD operations for {model} resources
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class {model}sController : BaseController<{model}sController>
    {{
        private readonly I{model}Service _{service_var}Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="{model}sController"/> class.
        /// </summary>
        /// <param name="{service_var}Service">The {model} service for business logic operations.</param>
        /// <param name="logger">The logger instance for this controller.</param>
        public {model}sController(I{model}Service {service_var}Service, ILogger<{model}sController> logger)
            : base(logger)
        {{
            _{service_var}Service = {service_var}Service;
        }}

        /// <summary>
        /// Gets all {model} entities
        /// </summary>
        /// <returns>List of all {model} entities</returns>
        [HttpGet]
        public IActionResult GetAll()
        {{
            try
            {{
                var response = _{service_var}Service.GetAll();
                if (!response.Success)
                {{
                    return BadRequest(response.Message);
                }}
                return Ok(response.Data);
            }}
            catch (Exception ex)
            {{
                return HandleError(ex);
            }}
        }}

        /// <summary>
        /// Gets a specific {model} by ID
        /// </summary>
        /// <param name="id">The unique identifier of the {model}</param>
        /// <returns>The {model} entity if found</returns>
        [HttpGet("{{id}}")]
        public IActionResult GetById(Guid id)
        {{
            try
            {{
                var response = _{service_var}Service.GetById(id);
                if (!response.Success)
                {{
                    return BadRequest(response.Message);
                }}
                if (response.Data == null)
                {{
                    return NotFound($"{model} with ID {{id}} not found");
                }}
                return Ok(response.Data);
            }}
            catch (Exception ex)
            {{
                return HandleError(ex);
            }}
        }}

        /// <summary>
        /// Creates a new {model} entity
        /// </summary>
        /// <param name="{service_var}Model">The {model} data to create</param>
        /// <returns>The created {model} entity</returns>
        [HttpPost]
        public IActionResult Create([FromBody] {model}Model {service_var}Model)
        {{
            try
            {{
                var response = _{service_var}Service.Insert({service_var}Model);
                if (!response.Success)
                {{
                    return BadRequest(response.Message);
                }}
                if (response.Data == null)
                {{
                    return BadRequest("Failed to create the {model}");
                }}
                return CreatedAtAction(nameof(GetById), new {{ id = response.Data.Id }}, response.Data);
            }}
            catch (Exception ex)
            {{
                return HandleError(ex);
            }}
        }}
        
        /// <summary>
        /// Updates an existing {model} entity
        /// </summary>
        /// <param name="id">The unique identifier of the {model} to update</param>
        /// <param name="{service_var}Model">The updated {model} data</param>
        /// <returns>The updated {model} entity</returns>
        [HttpPut("{{id}}")]
        public IActionResult Update(Guid id, [FromBody] {model}Model {service_var}Model)
        {{
            try
            {{
                {service_var}Model.Id = id;
                var response = _{service_var}Service.Update({service_var}Model);
                if (!response.Success)
                {{
                    return BadRequest(response.Message);
                }}
                return Ok(response.Data);
            }}
            catch (Exception ex)
            {{
                return HandleError(ex);
            }}
        }}

        /// <summary>
        /// Deletes a {model} entity
        /// </summary>
        /// <param name="id">The unique identifier of the {model} to delete</param>
        /// <returns>Confirmation of deletion</returns>
        [HttpDelete("{{id}}")]
        public IActionResult Delete(Guid id)
        {{
            try
            {{
                var response = _{service_var}Service.Delete(id);
                if (!response.Success)
                {{
                    return BadRequest(response.Message);
                }}
                return Ok(response.Data);
            }}
            catch (Exception ex)
            {{
                return HandleError(ex);
            }}
        }}
    }}
}}
"""


def generate_controllers() -> None:
    """
    Generates controller files for all models defined in MODELS list.
    Creates the output directory if it doesn't exist.
    """
    # Ensure the directory exists
    os.makedirs(CONTROLLERS_DIR, exist_ok=True)
    
    print(f"Generating controllers in: {CONTROLLERS_DIR}")
    print(f"Models to process: {len(MODELS)}")
    
    # Generate the controller files for each model
    for model in MODELS:
        controller_file_path = os.path.join(CONTROLLERS_DIR, f'{model}sController.cs')
        
        with open(controller_file_path, 'w', encoding='utf-8') as f:
            f.write(create_controller_content(model))
        
        print(f"✓ Created: {controller_file_path}")
    
    print(f"\n🎉 Successfully generated {len(MODELS)} controller files!")


if __name__ == "__main__":
    print("SPS Backend - Controller Generator")
    print("=" * 40)
    print("⚠️  WARNING: This will overwrite existing controller files!")
    
    # Uncomment the following lines to run the generation
    # confirmation = input("Are you sure you want to continue? (y/N): ")
    # if confirmation.lower() == 'y':
    #     generate_controllers()
    # else:
    #     print("Operation cancelled.")
    
    print("\n💡 To run this script, uncomment the confirmation lines in __main__")
    print("   This safety measure prevents accidental overwrites.")
