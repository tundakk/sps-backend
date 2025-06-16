# Development Tools and Scripts

This folder contains development utilities, scripts, and tools used during the development of the SPS Backend solution.

## 📁 Folder Structure

- `scripts/` - Python scripts for code generation and utilities
- `tools/` - Additional development tools (future)
- `templates/` - Code templates and scaffolding (future)

## 🐍 Python Scripts

### Code Generation Scripts
Located in `scripts/` folder, these scripts automate the creation of boilerplate code:

1. **`generate_controllers.py`** - Generates REST API controllers
2. **`generate_bll_services.py`** - Generates business logic layer services
3. **`generate_dal_repositories.py`** - Generates data access layer repositories
4. **`rename_script.py`** - Utility for bulk text replacement in files

### Prerequisites
- Python 3.7+
- No external dependencies required (uses only standard library)

### Usage
Run scripts from the project root directory:

```bash
cd c:\Repositories\sps-backend
python dev\scripts\generate_controllers.py
python dev\scripts\generate_bll_services.py
python dev\scripts\generate_dal_repositories.py
```

## ⚠️ Important Notes

- These scripts were used during initial project scaffolding
- **DO NOT run these scripts on existing code** - they will overwrite files
- Scripts are preserved for reference and potential future use
- Always backup your work before running any generation scripts

## 🔧 Maintenance

- Scripts may need updates if project structure changes
- Entity models list may need to be updated for new entities
- Namespace references should be verified before use

## 📝 License

These development tools are covered under the same MIT license as the main project.
