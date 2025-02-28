import os

# Define the models you want to create interfaces and services for
models = [
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

# Directories to place the generated files
base_dir = './Services'
interfaces_dir = os.path.join(base_dir, 'Interfaces')
implementations_dir = os.path.join(base_dir, 'Implementations')

# Ensure directories exist
os.makedirs(interfaces_dir, exist_ok=True)
os.makedirs(implementations_dir, exist_ok=True)

# Function to create interface content
def create_interface_content(model):
    return f"""namespace sps.BLL.Infrastructure.Interfaces
{{
    using sps.Domain.Model.Models;
    using sps.Domain.Model.Responses;
    using System.Collections.Generic;

    public interface I{model}Service : IBaseService<{model}Model>
    {{
        // Add any additional methods specific to {model} if needed
    }}
}}
"""

# Function to create service content (implementations)
def create_service_content(model):
    return f"""namespace sps.BLL.Infrastructure.Services.Implementations
{{
    using sps.BLL.Infrastructure.Interfaces;
    using sps.DAL.Entities;
    using sps.DAL.Repos.Interfaces;
    using sps.Domain.Model.Models;
    using sps.Domain.Model.Responses;
    using Mapster;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;

    public class {model}Service : BaseService<{model}Model, {model}, I{model}Repo>, I{model}Service
    {{
        public {model}Service(I{model}Repo {model[0].lower() + model[1:]}Repo, ILogger<{model}Service> logger)
            : base({model[0].lower() + model[1:]}, logger)
        {{
        }}

        // Implement any additional methods specific to {model} here
    }}
}}
"""

# Generate the interface and service files for each model
for model in models:
    # Interface file
    interface_file_path = os.path.join(interfaces_dir, f'I{model}Service.cs')
    with open(interface_file_path, 'w') as f:
        f.write(create_interface_content(model))

    # Service implementation file
    service_file_path = os.path.join(implementations_dir, f'{model}Service.cs')
    with open(service_file_path, 'w') as f:
        f.write(create_service_content(model))

print("Interface and implementation files generated successfully.")
