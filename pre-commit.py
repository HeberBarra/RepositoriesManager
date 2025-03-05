#!/usr/bin/env python3

import subprocess


format_command = ['dotnet', 'csharpier', '.']
list_files_command = 'git diff --staged --name-only'
modified_files = subprocess.check_output(list_files_command, shell=True)
modified_files = modified_files.decode('utf-8').split('\n')[:-1]

if any('cs' in file for file in modified_files):
    subprocess.run(format_command)

for file in modified_files:
    subprocess.run(['git', 'add', file])
