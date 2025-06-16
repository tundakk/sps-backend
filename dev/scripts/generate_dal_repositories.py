#!/usr/bin/env python3
"""
SPS Backend - Data Access Layer (DAL) Repositories Generator

This script generates repository interfaces and implementations for the Data Access Layer.
It creates boilerplate repository code following the project's repository pattern.

Author: SPS Backend Development Team
License: MIT
Created: 2025
Last Modified: June 16, 2025

WARNING: This script will overwrite existing files. Use with caution!
"""

import os
from typing import List

# Define the models for which you want to create repositories
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
INTERFACES_DIR = './sps.DAL/Repos/Interfaces'
REPOS_DIR = './sps.DAL/Repos/Implementations'


def create_interface_content(model: str) -> str:
    """
    Creates the C# repository interface code for a given model.
    
    Args:
        model: The model name (e.g., 'Student')
        
    Returns:
        Complete C# interface code as string
    """
    return f"""using sps.DAL.Repos.Base;
using sps.Domain.Model.Entities;

namespace sps.DAL.Repos.Interfaces
{{
    /// <summary>
    /// Repository interface for {model} data access operations
    /// Extends the base repository with {model}-specific data operations
    /// </summary>
    public interface I{model}Repo : IBaseRepo<{model}>
    {{
        // Add any additional data access methods specific to {model} here
        // Example:
        // Task<{model}?> GetBy{model}SpecificPropertyAsync(string property);
        // Task<IEnumerable<{model}>> GetActive{model}sAsync();
        // Task<IEnumerable<{model}>> GetByDateRangeAsync(DateTime from, DateTime to);
    }}
}}
"""


def create_repo_content(model: str) -> str:
    """
    Creates the C# repository implementation code for a given model.
    
    Args:
        model: The model name (e.g., 'Student')
        
    Returns:
        Complete C# repository implementation code as string
    """
    return f"""using Microsoft.EntityFrameworkCore;
using sps.DAL.DataModel;
using sps.DAL.Repos.Base;
using sps.DAL.Repos.Interfaces;
using sps.Domain.Model.Entities;

namespace sps.DAL.Repos.Implementations
{{
    /// <summary>
    /// Repository implementation for {model} data access operations
    /// Provides data access methods with Entity Framework Core
    /// </summary>
    public class {model}Repo : BaseRepo<{model}>, I{model}Repo
    {{
        /// <summary>
        /// Initializes a new instance of the <see cref="{model}Repo"/> class
        /// </summary>
        /// <param name="context">The database context for data operations</param>
        public {model}Repo(SpsDbContext context) : base(context)
        {{
        }}

        // Implement any additional data access methods specific to {model} here
        
        /// <summary>
        /// Example of a custom data access method
        /// </summary>
        /// <param name="property">Search parameter</param>
        /// <returns>Matching {model} entity or null</returns>
        // public async Task<{model}?> GetBy{model}SpecificPropertyAsync(string property)
        // {{
        //     return await _context.Set<{model}>()
        //         .FirstOrDefaultAsync(x => x.SomeProperty == property);
        // }}
        
        /// <summary>
        /// Example of getting active entities
        /// </summary>
        /// <returns>Collection of active {model} entities</returns>
        // public async Task<IEnumerable<{model}>> GetActive{model}sAsync()
        // {{
        //     return await _context.Set<{model}>()
        //         .Where(x => x.IsActive == true)
        //         .ToListAsync();
        // }}
        
        /// <summary>
        /// Example of date range query
        /// </summary>
        /// <param name="from">Start date</param>
        /// <param name="to">End date</param>
        /// <returns>Collection of {model} entities within date range</returns>
        // public async Task<IEnumerable<{model}>> GetByDateRangeAsync(DateTime from, DateTime to)
        // {{
        //     return await _context.Set<{model}>()
        //         .Where(x => x.CreatedAt >= from && x.CreatedAt <= to)
        //         .ToListAsync();
        // }}
    }}
}}
"""


def generate_repositories() -> None:
    """
    Generates repository interface and implementation files for all models.
    Creates the output directories if they don't exist.
    """
    # Ensure directories exist
    os.makedirs(INTERFACES_DIR, exist_ok=True)
    os.makedirs(REPOS_DIR, exist_ok=True)
    
    print(f"Generating repositories in:")
    print(f"  Interfaces: {INTERFACES_DIR}")
    print(f"  Implementations: {REPOS_DIR}")
    print(f"Models to process: {len(MODELS)}")

    # Generate the repository interface and implementation files for each model
    for model in MODELS:
        # Interface file
        interface_file_path = os.path.join(INTERFACES_DIR, f'I{model}Repo.cs')
        with open(interface_file_path, 'w', encoding='utf-8') as f:
            f.write(create_interface_content(model))
        print(f"✓ Created interface: {interface_file_path}")

        # Repository implementation file
        repo_file_path = os.path.join(REPOS_DIR, f'{model}Repo.cs')
        with open(repo_file_path, 'w', encoding='utf-8') as f:
            f.write(create_repo_content(model))
        print(f"✓ Created repository: {repo_file_path}")

    print(f"\n🎉 Successfully generated {len(MODELS) * 2} repository files!")
    print(f"   ({len(MODELS)} interfaces + {len(MODELS)} implementations)")


if __name__ == "__main__":
    print("SPS Backend - DAL Repositories Generator")
    print("=" * 40)
    print("⚠️  WARNING: This will overwrite existing repository files!")
    
    # Uncomment the following lines to run the generation
    # confirmation = input("Are you sure you want to continue? (y/N): ")
    # if confirmation.lower() == 'y':
    #     generate_repositories()
    # else:
    #     print("Operation cancelled.")
    
    print("\n💡 To run this script, uncomment the confirmation lines in __main__")
    print("   This safety measure prevents accidental overwrites.")
