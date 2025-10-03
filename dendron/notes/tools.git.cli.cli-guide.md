---
id: vnak459f2sczn29muy57qr0
title: CLI Guide
desc: ''
updated: 1759436701420
created: 1759436675948
---

## Installation

### Windows

```bash
# Using winget
winget install --id GitHub.cli

# Using Chocolatey
choco install gh

# Using Scoop
scoop install gh
```

### macOS

```bash
# Using Homebrew
brew install gh
```

### Linux

```bash
# Debian/Ubuntu
sudo apt install gh

# Fedora/RHEL
sudo dnf install gh

# Arch Linux
sudo pacman -S github-cli
```

### Verify Installation

```bash
gh --version
```

## Authentication

### Login to GitHub

```bash
# Interactive login (recommended)
gh auth login

# Login with token
gh auth login --with-token < token.txt

# Check authentication status
gh auth status

# Logout
gh auth logout
```

### Setup SSH Keys

```bash
# List SSH keys
gh ssh-key list

# Add new SSH key
gh ssh-key add ~/.ssh/id_rsa.pub -t "My Key"
```

## Repository Management

### Creating Repositories

```bash
# Create a new repository (interactive)
gh repo create

# Create repository with name
gh repo create my-repo

# Create and clone
gh repo create my-repo --clone

# Create with options
gh repo create my-repo --public --description "My project"
gh repo create my-repo --private --gitignore Node

# Create from template
gh repo create my-repo --template owner/template-repo
```

### Cloning Repositories

```bash
# Clone your repository
gh repo clone my-repo

# Clone someone else's repository
gh repo clone owner/repo

# Clone with specific directory
gh repo clone owner/repo target-directory
```

### Repository Information

```bash
# View repository
gh repo view

# View specific repository
gh repo view owner/repo

# View in browser
gh repo view --web
gh browse

# View README
gh repo view owner/repo --readme
```

### Repository Operations

```bash
# Fork a repository
gh repo fork owner/repo

# Fork and clone
gh repo fork owner/repo --clone

# List your repositories
gh repo list

# List organization repositories
gh repo list organization-name

# Archive repository
gh repo archive owner/repo

# Delete repository
gh repo delete owner/repo

# Rename repository
gh repo rename new-name

# Edit repository settings
gh repo edit --description "New description"
gh repo edit --visibility private
gh repo edit --enable-wiki=false
```

## Working with Issues

### Creating Issues

```bash
# Create issue (interactive)
gh issue create

# Create with title and body
gh issue create --title "Bug: Login fails" --body "Description here"

# Create from file
gh issue create --title "Bug" --body-file issue.md

# Create with labels and assignees
gh issue create --title "Bug" --label bug --assignee username
```

### Viewing Issues

```bash
# List issues
gh issue list

# List with filters
gh issue list --state open
gh issue list --label bug
gh issue list --assignee @me
gh issue list --author username

# View specific issue
gh issue view 123

# View issue in browser
gh issue view 123 --web
```

### Managing Issues

```bash
# Close issue
gh issue close 123

# Reopen issue
gh issue reopen 123

# Edit issue
gh issue edit 123 --title "New title"
gh issue edit 123 --add-label enhancement
gh issue edit 123 --remove-label bug

# Comment on issue
gh issue comment 123 --body "Thanks for reporting!"

# Pin issue
gh issue pin 123

# Transfer issue to another repo
gh issue transfer 123 owner/other-repo

# Delete issue
gh issue delete 123
```

## Working with Pull Requests

### Creating Pull Requests

```bash
# Create PR (interactive)
gh pr create

# Create with title and body
gh pr create --title "Add feature" --body "Description"

# Create from file
gh pr create --title "Feature" --body-file pr.md

# Create as draft
gh pr create --draft

# Create with reviewers and assignees
gh pr create --reviewer user1,user2 --assignee user3

# Create to specific branch
gh pr create --base main --head feature-branch
```

### Viewing Pull Requests

```bash
# List PRs
gh pr list

# List with filters
gh pr list --state open
gh pr list --author @me
gh pr list --label bug

# View specific PR
gh pr view 456

# View PR in browser
gh pr view 456 --web

# View PR diff
gh pr diff 456

# View PR checks
gh pr checks 456
```

### Managing Pull Requests

```bash
# Checkout PR locally
gh pr checkout 456

# Review PR
gh pr review 456
gh pr review 456 --approve
gh pr review 456 --request-changes --body "Please fix..."
gh pr review 456 --comment --body "Looks good"

# Merge PR
gh pr merge 456
gh pr merge 456 --squash
gh pr merge 456 --rebase
gh pr merge 456 --merge

# Close PR
gh pr close 456

# Reopen PR
gh pr reopen 456

# Mark as ready for review
gh pr ready 456

# Edit PR
gh pr edit 456 --title "New title"
gh pr edit 456 --add-reviewer username

# Comment on PR
gh pr comment 456 --body "Comment text"
```

## Working with Releases

### Creating Releases

```bash
# Create release (interactive)
gh release create

# Create with tag
gh release create v1.0.0

# Create with notes
gh release create v1.0.0 --notes "Release notes here"

# Create from file
gh release create v1.0.0 --notes-file CHANGELOG.md

# Create with assets
gh release create v1.0.0 ./dist/*.zip

# Create as prerelease
gh release create v1.0.0-beta --prerelease

# Create as draft
gh release create v1.0.0 --draft

# Auto-generate release notes
gh release create v1.0.0 --generate-notes
```

### Viewing Releases

```bash
# List releases
gh release list

# View specific release
gh release view v1.0.0

# View latest release
gh release view --latest

# View in browser
gh release view v1.0.0 --web
```

### Managing Releases

```bash
# Download release assets
gh release download v1.0.0

# Download specific asset
gh release download v1.0.0 --pattern "*.zip"

# Upload assets to existing release
gh release upload v1.0.0 ./file.zip

# Edit release
gh release edit v1.0.0 --notes "Updated notes"

# Delete release
gh release delete v1.0.0

# Delete tag only
gh release delete v1.0.0 --yes --cleanup-tag
```

## GitHub Actions (Workflows)

### Viewing Workflows

```bash
# List workflows
gh workflow list

# View workflow runs
gh run list

# View specific workflow runs
gh run list --workflow=ci.yml

# View run details
gh run view 123456

# View run logs
gh run view 123456 --log

# Watch a run in real-time
gh run watch 123456
```

### Managing Workflows

```bash
# Trigger workflow
gh workflow run ci.yml

# Trigger with inputs
gh workflow run ci.yml --field version=1.0.0

# Enable workflow
gh workflow enable ci.yml

# Disable workflow
gh workflow disable ci.yml

# Rerun failed jobs
gh run rerun 123456

# Rerun all jobs
gh run rerun 123456 --all

# Cancel run
gh run cancel 123456

# Download run artifacts
gh run download 123456
```

## Gists

### Creating Gists

```bash
# Create gist from file
gh gist create file.txt

# Create public gist
gh gist create file.txt --public

# Create with description
gh gist create file.txt --desc "My gist"

# Create from multiple files
gh gist create file1.txt file2.txt

# Create from stdin
echo "content" | gh gist create -
```

### Managing Gists

```bash
# List your gists
gh gist list

# View gist
gh gist view <gist-id>

# Edit gist
gh gist edit <gist-id>

# Clone gist
gh gist clone <gist-id>

# Delete gist
gh gist delete <gist-id>
```

## GitHub Projects

### Working with Projects

```bash
# List projects
gh project list

# View project
gh project view 1

# Create project item
gh project item-create 1 --title "New task"

# List project items
gh project item-list 1

# Close project
gh project close 1
```

## Aliases

### Creating Command Aliases

```bash
# List aliases
gh alias list

# Create alias
gh alias set pv 'pr view'
gh alias set co 'pr checkout'
gh alias set issues 'issue list --assignee @me'

# Use alias
gh pv 123

# Delete alias
gh alias delete pv
```

### Useful Aliases

```bash
# My open PRs
gh alias set myprs 'pr list --author @me'

# My issues
gh alias set myissues 'issue list --assignee @me'

# Quick PR create
gh alias set prc 'pr create --fill'

# View PR checks
gh alias set checks 'pr checks'
```

## Search

### Search Repositories

```bash
# Search repositories
gh search repos "machine learning"

# Search with filters
gh search repos "language:python stars:>1000"

# Limit results
gh search repos "react" --limit 10
```

### Search Issues and PRs

```bash
# Search issues
gh search issues "is:open is:issue label:bug"

# Search PRs
gh search prs "is:open is:pr author:username"

# Search code
gh search code "function main"
```

## Configuration

### View Configuration

```bash
# Show config
gh config get editor
gh config get git_protocol

# List all config
gh config list
```

### Set Configuration

```bash
# Set default editor
gh config set editor vim
gh config set editor "code --wait"

# Set git protocol
gh config set git_protocol ssh
gh config set git_protocol https

# Set default browser
gh config set browser chrome
```

## Extensions

### Managing Extensions

```bash
# List installed extensions
gh extension list

# Install extension
gh extension install owner/gh-extension

# Upgrade extension
gh extension upgrade extension-name
gh extension upgrade --all

# Remove extension
gh extension remove extension-name

# Browse extensions
gh extension browse
```

### Popular Extensions

```bash
# GitHub Copilot CLI
gh extension install github/gh-copilot

# Dashboard
gh extension install dlvhdr/gh-dash

# Project management
gh extension install github/gh-projects
```

## Common Workflows

### Quick Repository Setup

```bash
# Initialize and push new repo
git init
git add .
git commit -m "Initial commit"
gh repo create my-repo --source=. --public --push
```

### Create Feature Branch and PR

```bash
# Create branch
git checkout -b feature-branch

# Make changes and commit
git add .
git commit -m "Add feature"

# Push and create PR
git push -u origin feature-branch
gh pr create --fill
```

### Review and Merge PR

```bash
# View PR
gh pr view 123

# Check out PR locally
gh pr checkout 123

# Test changes
dotnet test

# Approve and merge
gh pr review 123 --approve
gh pr merge 123 --squash
```

### Create and Publish Release

```bash
# Create tag
git tag v1.0.0
git push origin v1.0.0

# Create release with assets
gh release create v1.0.0 ./dist/* --notes "Release v1.0.0"
```

### Clone and Setup Forked Repository

```bash
# Fork and clone
gh repo fork owner/repo --clone

# Add upstream remote (done automatically)
git remote -v

# Sync with upstream
git fetch upstream
git checkout main
git merge upstream/main
```

## Advanced Features

### Using with jq for JSON Processing

```bash
# Get PR data as JSON
gh pr list --json number,title,author

# Filter with jq
gh pr list --json number,title,state | jq '.[] | select(.state=="OPEN")'

# Get issue count
gh issue list --json number | jq 'length'
```

### Bulk Operations

```bash
# Close all PRs with specific label
gh pr list --label "wontfix" --json number --jq '.[].number' | \
  xargs -I {} gh pr close {}

# Assign all open issues to yourself
gh issue list --state open --json number --jq '.[].number' | \
  xargs -I {} gh issue edit {} --add-assignee @me
```

## Tips and Best Practices

1. **Use `--fill` flag** - Auto-fills PR/issue from commits and branch name
2. **Set up aliases** - Create shortcuts for frequently used commands
3. **Use `--web` flag** - Quickly open items in browser
4. **Leverage JSON output** - Combine with `jq` for powerful scripting
5. **Use interactive mode** - Run commands without flags for guided experience
6. **Check status first** - Use `gh auth status` if commands fail
7. **Use templates** - Create issue/PR templates for consistency
8. **Explore extensions** - Enhance gh with community extensions
9. **Use autocomplete** - Enable shell completion for faster typing
10. **Read help** - Use `gh <command> --help` for detailed information

## Troubleshooting

### Common Issues

```bash
# Authentication issues
gh auth login
gh auth refresh

# Clear cache
rm -rf ~/.config/gh/hosts.yml

# Check version
gh version

# Update gh
gh upgrade

# Verbose output for debugging
gh <command> --verbose
```

### Get Help

```bash
# General help
gh help

# Command help
gh repo --help
gh pr create --help

# Show manual
man gh
man gh-pr-create
```

## Resources

- [GitHub CLI Documentation](https://cli.github.com/manual/)
- [GitHub CLI Repository](https://github.com/cli/cli)
- [GitHub CLI Extensions](https://github.com/topics/gh-extension)
- [GitHub REST API](https://docs.github.com/en/rest)