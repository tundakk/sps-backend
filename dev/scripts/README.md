# Development Scripts Index

This directory contains Python scripts used for code generation and development utilities in the SPS Backend project.

## 📋 Script Inventory

| Script Name | Purpose | Status | Last Updated |
|-------------|---------|--------|--------------|
| `generate_controllers.py` | Generates REST API controllers | ✅ Ready | June 16, 2025 |
| `generate_bll_services.py` | Generates BLL service interfaces and implementations | ✅ Ready | June 16, 2025 |
| `generate_dal_repositories.py` | Generates DAL repository interfaces and implementations | ✅ Ready | June 16, 2025 |
| `rename_script.py` | Bulk text replacement utility | ✅ Ready | June 16, 2025 |

## 🚀 Quick Start

### Prerequisites
- Python 3.7 or higher
- Run from project root directory: `c:\Repositories\sps-backend`

### Usage Examples

```powershell
# Navigate to project root
cd c:\Repositories\sps-backend

# Generate controllers (CAUTION: Will overwrite existing files)
python dev\scripts\generate_controllers.py

# Generate BLL services
python dev\scripts\generate_bll_services.py

# Generate DAL repositories  
python dev\scripts\generate_dal_repositories.py

# Bulk text replacement
python dev\scripts\rename_script.py "C:\MyProject" "OldText" "NewText"
```

## ⚠️ Safety Features

All scripts include safety measures:
- **Confirmation prompts** before executing destructive operations
- **Dry-run mode** to preview changes
- **File backup recommendations**
- **Detailed logging** of operations

## 🔧 Customization

### Adding New Models
To generate code for new entities, edit the `MODELS` list in each script:

```python
MODELS: List[str] = [
    'Student',
    'YourNewModel',  # Add here
    # ... existing models
]
```

### Modifying Templates
Each script contains template functions that can be customized:
- `create_controller_content()` - Controller templates
- `create_interface_content()` - Interface templates  
- `create_service_content()` - Service implementation templates

## 📁 Output Locations

| Script | Output Directory |
|--------|------------------|
| Controllers | `./sps.API/Controllers/Implementations/` |
| BLL Interfaces | `./sps.BLL/Services/Interfaces/` |
| BLL Implementations | `./sps.BLL/Services/Implementations/` |
| DAL Interfaces | `./sps.DAL/Repos/Interfaces/` |
| DAL Implementations | `./sps.DAL/Repos/Implementations/` |

## 🔍 Troubleshooting

### Common Issues

1. **"Permission denied" errors**
   - Ensure files are not open in Visual Studio
   - Run PowerShell as Administrator if needed

2. **"Module not found" errors**
   - Verify Python is installed and in PATH
   - Scripts use only standard library (no pip install needed)

3. **Generated code doesn't compile**
   - Check namespace references in templates
   - Verify entity models exist in Domain.Model project
   - Update dependency injection registrations

### Getting Help

1. Check the main documentation in `docs/`
2. Review the project's GitHub Copilot instructions
3. Examine existing generated files for patterns

## 📝 Development Notes

- Scripts were created during initial project scaffolding
- Templates follow the established project architecture patterns
- All generated code follows project coding standards
- Regular maintenance may be needed as project evolves

## 🔒 Security Considerations

- Scripts modify source code files
- Always review generated code before committing
- Use version control to track changes
- Test generated code thoroughly before deployment
