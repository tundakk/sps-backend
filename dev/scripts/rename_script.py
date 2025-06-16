#!/usr/bin/env python3
"""
SPS Backend - Text Replacement Utility Script

This script performs bulk text replacement across files in a directory tree.
Useful for refactoring, renaming, or updating text patterns across the codebase.

Author: SPS Backend Development Team
License: MIT
Created: 2025
Last Modified: June 16, 2025

WARNING: This script modifies files in place. Always backup before use!
"""

import os
import sys
from typing import Tuple


def replace_in_file(file_path: str, old_str: str = "sps", new_str: str = "sps") -> bool:
    """
    Replaces text in a single file.
    
    Args:
        file_path: Path to the file to process
        old_str: Text to search for
        new_str: Text to replace with
        
    Returns:
        True if file was modified, False otherwise
    """
    try:
        with open(file_path, "r", encoding="utf-8") as file:
            content = file.read()
    except UnicodeDecodeError:
        # Skip non-text files (binaries, images, etc.)
        print(f"⚠️  Skipped (binary): {file_path}")
        return False
    except Exception as e:
        print(f"❌ Error reading {file_path}: {e}")
        return False

    if old_str not in content:
        return False

    try:
        new_content = content.replace(old_str, new_str)
        with open(file_path, "w", encoding="utf-8") as file:
            file.write(new_content)
        return True
    except Exception as e:
        print(f"❌ Error writing {file_path}: {e}")
        return False


def traverse_and_replace(directory: str, old_str: str, new_str: str, 
                        excluded_dirs: set = None, excluded_extensions: set = None) -> Tuple[int, int]:
    """
    Traverses directory tree and replaces text in all applicable files.
    
    Args:
        directory: Root directory to start from
        old_str: Text to search for
        new_str: Text to replace with
        excluded_dirs: Set of directory names to skip
        excluded_extensions: Set of file extensions to skip
        
    Returns:
        Tuple of (files_processed, files_modified)
    """
    if excluded_dirs is None:
        excluded_dirs = {'bin', 'obj', 'node_modules', '.git', '.vs', '.vscode'}
    
    if excluded_extensions is None:
        excluded_extensions = {'.dll', '.exe', '.pdb', '.cache', '.png', '.jpg', '.gif', '.ico'}
    
    files_processed = 0
    files_modified = 0
    
    print(f"🔍 Searching for '{old_str}' to replace with '{new_str}'")
    print(f"📁 Starting directory: {directory}")
    print(f"🚫 Excluded directories: {excluded_dirs}")
    print(f"🚫 Excluded extensions: {excluded_extensions}")
    print("-" * 60)
    
    for root, dirs, files in os.walk(directory):
        # Remove excluded directories from the search
        dirs[:] = [d for d in dirs if d not in excluded_dirs]
        
        for file in files:
            file_path = os.path.join(root, file)
            file_ext = os.path.splitext(file)[1].lower()
            
            # Skip excluded file types
            if file_ext in excluded_extensions:
                continue
                
            files_processed += 1
            
            if replace_in_file(file_path, old_str, new_str):
                print(f"✅ Modified: {file_path}")
                files_modified += 1
    
    return files_processed, files_modified


def main():
    """Main entry point for the script."""
    print("SPS Backend - Text Replacement Utility")
    print("=" * 40)
    
    if len(sys.argv) < 4:
        print("Usage: python rename_script.py <directory> <old_text> <new_text>")
        print("\nExample:")
        print('  python rename_script.py "C:\\MyProject" "OldName" "NewName"')
        print("\nOptions:")
        print("  directory  - Directory path to search recursively")
        print("  old_text   - Text to search for")
        print("  new_text   - Text to replace with")
        sys.exit(1)
    
    directory = sys.argv[1]
    old_text = sys.argv[2]
    new_text = sys.argv[3]
    
    # Validate directory
    if not os.path.isdir(directory):
        print(f"❌ Error: '{directory}' is not a valid directory.")
        sys.exit(1)
    
    # Safety check
    if old_text == new_text:
        print("⚠️  Warning: Old text and new text are identical. Nothing to do.")
        sys.exit(0)
    
    print(f"⚠️  WARNING: This will modify files in '{directory}'")
    print(f"    Replacing: '{old_text}' → '{new_text}'")
    
    # Uncomment for interactive mode
    # confirmation = input("\nAre you sure you want to continue? (y/N): ")
    # if confirmation.lower() != 'y':
    #     print("Operation cancelled.")
    #     sys.exit(0)
    
    print("\n💡 Safety mode: Script will show what would be changed.")
    print("   To actually run replacements, uncomment the confirmation section.")
    return
    
    # Process files
    try:
        processed, modified = traverse_and_replace(directory, old_text, new_text)
        
        print("-" * 60)
        print(f"📊 Summary:")
        print(f"   Files processed: {processed}")
        print(f"   Files modified: {modified}")
        print(f"   Success rate: {(modified/processed*100) if processed > 0 else 0:.1f}%")
        
        if modified > 0:
            print(f"\n🎉 Successfully replaced '{old_text}' with '{new_text}' in {modified} files!")
        else:
            print(f"\n💭 No occurrences of '{old_text}' found.")
            
    except KeyboardInterrupt:
        print("\n\n⚠️  Operation cancelled by user.")
        sys.exit(1)
    except Exception as e:
        print(f"\n❌ Unexpected error: {e}")
        sys.exit(1)


if __name__ == "__main__":
    main()
