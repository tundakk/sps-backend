import os
import sys

def replace_in_file(file_path, old_str="sps", new_str="sps"):
    try:
        with open(file_path, "r", encoding="utf-8") as file:
            content = file.read()
    except UnicodeDecodeError:
        # Skipping non-text files
        return False

    if old_str not in content:
        return False

    new_content = content.replace(old_str, new_str)
    with open(file_path, "w", encoding="utf-8") as file:
        file.write(new_content)
    return True

def traverse_and_replace(directory):
    modified_count = 0
    for root, _, files in os.walk(directory):
        for file in files:
            file_path = os.path.join(root, file)
            if replace_in_file(file_path):
                print(f"Modified: {file_path}")
                modified_count += 1
    print(f"Total files modified: {modified_count}")

if __name__ == "__main__":
    if len(sys.argv) != 2:
        print("Usage: python rename_script.py <directoryPath>")
        sys.exit(1)
    
    directory = sys.argv[1]
    if not os.path.isdir(directory):
        print(f"Error: '{directory}' is not a valid directory.")
        sys.exit(1)
    
    traverse_and_replace(directory)