#!/usr/bin/env python3
"""
SPS Backend - Business Logic Layer (BLL) Services Generator

This script generates service interfaces and implementations for the Business Logic Layer.
It creates boilerplate service code following the project's repository pattern.

Author: SPS Backend Development Team
License: MIT
Created: 2025
Last Modified: June 16, 2025

WARNING: This script will overwrite existing files. Use with caution!
"""

import os
from typing import List

# Define the models for which you want to create services
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

# Directories to place the generated files (relative to project root)
BASE_DIR = './sps.BLL/Services'
INTERFACES_DIR = os.path.join(BASE_DIR, 'Interfaces')
IMPLEMENTATIONS_DIR = os.path.join(BASE_DIR, 'Implementations')


def create_interface_content(model: str) -> str:
    """
    Creates the C# service interface code for a given model.
    
    Args:
        model: The model name (e.g., 'Student')
        
    Returns:
        Complete C# interface code as string
    """
    return f"""using sps.Domain.Model.Models;

namespace sps.BLL.Services.Interfaces
{{
    /// <summary>
    /// Service interface for {model} business logic operations
    /// Extends the base service with {model}-specific operations
    /// </summary>
    public interface I{model}Service : IBaseService<{model}Model>
    {{
        // Add any additional methods specific to {model} business logic here
        // Example:
        // Task<ServiceResponse<{model}Model>> GetBy{model}SpecificPropertyAsync(string property);
        // Task<ServiceResponse<IEnumerable<{model}Model>>> GetActive{model}sAsync();
    }}
}}
"""


def create_service_content(model: str) -> str:
    """
    Creates the C# service implementation code for a given model.
    
    Args:
        model: The model name (e.g., 'Student')
        
    Returns:
        Complete C# service implementation code as string
    """
    service_var = model[0].lower() + model[1:]  # camelCase version
    repo_var = service_var + "Repo"
    
    return f"""using sps.BLL.Services.Interfaces;
using sps.DAL.Entities;
using sps.DAL.Repos.Interfaces;
using sps.Domain.Model.Models;
using Mapster;
using Microsoft.Extensions.Logging;

namespace sps.BLL.Services.Implementations
{{
    /// <summary>
    /// Service implementation for {model} business logic operations
    /// Handles data transformation, validation, and business rules
    /// </summary>
    public class {model}Service : BaseService<{model}Model, {model}, I{model}Repo>, I{model}Service
    {{
        /// <summary>
        /// Initializes a new instance of the <see cref="{model}Service"/> class
        /// </summary>
        /// <param name="{repo_var}">The {model} repository for data access</param>
        /// <param name="logger">The logger instance for this service</param>
        public {model}Service(I{model}Repo {repo_var}, ILogger<{model}Service> logger)
            : base({repo_var}, logger)
        {{
        }}

        // Implement any additional business logic methods specific to {model} here
        
        /// <summary>
        /// Example of a custom business logic method
        /// </summary>
        /// <param name="id">The {model} identifier</param>
        /// <returns>Service response with {model} data</returns>
        // public async Task<ServiceResponse<{model}Model>> CustomBusinessMethodAsync(Guid id)
        // {{
        //     try
        //     {{
        //         // Custom business logic implementation
        //         var entity = await _repository.GetByIdAsync(id);
        //         if (entity == null)
        //         {{
        //             return ServiceResponse<{model}Model>.Failure("Resource not found");
        //         }}
        //         
        //         // Apply business rules, transformations, etc.
        //         var model = entity.Adapt<{model}Model>();
        //         
        //         return ServiceResponse<{model}Model>.Success(model);
        //     }}
        //     catch (Exception ex)
        //     {{
        //         _logger.LogError(ex, "Error in custom business method for {model}");
        //         return ServiceResponse<{model}Model>.Failure("An error occurred while processing the request");
        //     }}
        // }}
    }}
}}
"""


def generate_services() -> None:
    """
    Generates service interface and implementation files for all models.
    Creates the output directories if they don't exist.
    """
    # Ensure directories exist
    os.makedirs(INTERFACES_DIR, exist_ok=True)
    os.makedirs(IMPLEMENTATIONS_DIR, exist_ok=True)
    
    print(f"Generating services in:")
    print(f"  Interfaces: {INTERFACES_DIR}")
    print(f"  Implementations: {IMPLEMENTATIONS_DIR}")
    print(f"Models to process: {len(MODELS)}")
    
    # Generate the interface and service files for each model
    for model in MODELS:
        # Interface file
        interface_file_path = os.path.join(INTERFACES_DIR, f'I{model}Service.cs')
        with open(interface_file_path, 'w', encoding='utf-8') as f:
            f.write(create_interface_content(model))
        print(f"✓ Created interface: {interface_file_path}")

        # Service implementation file
        service_file_path = os.path.join(IMPLEMENTATIONS_DIR, f'{model}Service.cs')
        with open(service_file_path, 'w', encoding='utf-8') as f:
            f.write(create_service_content(model))
        print(f"✓ Created service: {service_file_path}")

    print(f"\n🎉 Successfully generated {len(MODELS) * 2} service files!")
    print(f"   ({len(MODELS)} interfaces + {len(MODELS)} implementations)")


if __name__ == "__main__":
    print("SPS Backend - BLL Services Generator")
    print("=" * 40)
    print("⚠️  WARNING: This will overwrite existing service files!")
    
    # Uncomment the following lines to run the generation
    # confirmation = input("Are you sure you want to continue? (y/N): ")
    # if confirmation.lower() == 'y':
    #     generate_services()
    # else:
    #     print("Operation cancelled.")
    
    print("\n💡 To run this script, uncomment the confirmation lines in __main__")
    print("   This safety measure prevents accidental overwrites.")
