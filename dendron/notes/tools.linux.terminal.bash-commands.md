---
id: aa22op8u7zqi7c9rhlorgdq
title: Bash Commands
desc: ''
updated: 1759435874080
created: 1759435846528
---

## Basic Navigation

```bash
pwd                    # Print working directory (where you are)
ls                     # List files and folders
ls -l                  # List with details (permissions, size, date)
ls -la                 # List all files including hidden ones
ls -lh                 # List with human-readable file sizes

cd /path/to/folder     # Change directory
cd ~                   # Go to home directory
cd ..                  # Go up one level
cd -                   # Go to previous directory
```

## File and Directory Operations

### Create

```bash
mkdir foldername       # Create directory
mkdir -p path/to/deep/folder  # Create nested directories
touch filename.txt     # Create empty file
```

### Copy, Move, Delete

```bash
cp file.txt backup.txt           # Copy file
cp -r folder/ backup/            # Copy directory recursively
mv oldname.txt newname.txt       # Rename/move file
mv file.txt /path/to/destination/ # Move file
rm file.txt                      # Delete file
rm -r folder/                    # Delete directory recursively
rm -rf folder/                   # Force delete (be careful!)
```

### View Files

```bash
cat file.txt           # Display entire file
less file.txt          # View file page by page (q to quit)
head file.txt          # Show first 10 lines
tail file.txt          # Show last 10 lines
tail -f logfile.log    # Follow file in real-time (great for logs)
```

## File Permissions

```bash
chmod +x script.sh     # Make file executable
chmod 755 file.txt     # Set specific permissions (rwxr-xr-x)
chmod -R 755 folder/   # Change permissions recursively
chown user:group file  # Change file owner
```

## Searching and Finding

### Find Files

```bash
find . -name "*.txt"              # Find all .txt files
find /path -type f -name "file"   # Find file by name
find . -type d -name "folder"     # Find directory
```

### Search File Contents

```bash
grep "searchtext" file.txt        # Search in file
grep -r "searchtext" .            # Search recursively in all files
grep -i "text" file.txt           # Case-insensitive search
```

## Process Management

```bash
ps aux                 # List all running processes
top                    # Interactive process viewer
htop                   # Better process viewer (may need to install)
kill PID               # Kill process by ID
killall processname    # Kill all processes by name
ctrl+c                 # Stop current command
ctrl+z                 # Suspend current command
bg                     # Resume in background
fg                     # Resume in foreground
```

## System Information

```bash
df -h                  # Disk space usage
du -sh folder/         # Size of folder
free -h                # Memory usage
uname -a               # System information
whoami                 # Current username
which command          # Location of command
```

## Package Management

### Ubuntu/Debian

```bash
sudo apt update                  # Update package list
sudo apt upgrade                 # Upgrade installed packages
sudo apt install package-name    # Install package
sudo apt remove package-name     # Remove package
sudo apt search package-name     # Search for package
```

### Fedora/RHEL/CentOS

```bash
sudo dnf update
sudo dnf install package-name
```

## File Editing

```bash
nano file.txt          # Simple text editor (ctrl+x to exit)
vim file.txt           # Advanced editor (press i to edit, :wq to save and quit)
code file.txt          # Open in VS Code (if installed)
```

## Redirection and Pipes

```bash
command > file.txt     # Redirect output to file (overwrite)
command >> file.txt    # Append output to file
command < file.txt     # Use file as input
command1 | command2    # Pipe output of command1 to command2
```

### Examples

```bash
ls -la > files.txt
cat file.txt | grep "error"
echo "text" >> log.txt
```

## Networking

```bash
ping google.com        # Test connectivity
curl url               # Fetch URL content
wget url               # Download file from URL
ifconfig               # Network interfaces (may need net-tools)
ip addr                # Show IP addresses
netstat -tuln          # Show listening ports
```

## Useful Shortcuts

```bash
ctrl+c                 # Cancel current command
ctrl+d                 # Exit terminal/logout
ctrl+l                 # Clear screen (or type 'clear')
ctrl+a                 # Move to beginning of line
ctrl+e                 # Move to end of line
ctrl+r                 # Search command history
!!                     # Repeat last command
sudo !!                # Repeat last command with sudo
tab                    # Auto-complete
up/down arrows         # Navigate command history
```

## History and Aliases

```bash
history                # Show command history
!123                   # Run command number 123 from history
history | grep "search" # Search command history
```

### Create Aliases

Add to `~/.bashrc` for permanent aliases:

```bash
alias ll='ls -la'
alias ..='cd ..'
alias update='sudo apt update && sudo apt upgrade'
```

## Compression

### Tar Archives

```bash
tar -czf archive.tar.gz folder/    # Compress
tar -xzf archive.tar.gz            # Extract
```

### Zip

```bash
zip -r archive.zip folder/         # Compress
unzip archive.zip                  # Extract
```

## Environment Variables

```bash
echo $HOME             # Display variable
export VAR=value       # Set variable for session
printenv               # Show all environment variables
```

### Add Permanent Variables

Add to `~/.bashrc`:

```bash
echo 'export PATH=$PATH:/new/path' >> ~/.bashrc
source ~/.bashrc       # Reload configuration
```

## Tips for Beginners

1. **Use Tab completion** - saves typing and prevents errors
2. **Read error messages** - they usually tell you what's wrong
3. **Use `man command`** - shows manual for any command
4. **Use `command --help`** - quick help reference
5. **Be careful with `rm -rf`** - it deletes permanently
6. **Practice in safe directories** - don't experiment in system folders
7. **Use `sudo` carefully** - only when you need admin rights

Start with basic navigation and file operations, then gradually add more commands as you need them!