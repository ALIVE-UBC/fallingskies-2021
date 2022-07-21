## Git for Windows Survival Guide

1. Download and install the latest [Git for Windows](https://git-scm.com/download/win) and [Git LFS](https://git-lfs.github.com/).

2. Launch Git for Windows, which is a black terminal window. Don't panic.

3. Basic commands for navigate around:
```
# create a new directory named "foo"
mkdir foo
# enter the directory named "foo" (use Tab to auto-complete long names)
cd foo
# return to the parent directory
cd ..
```
... or just navigate in file explorer and right-click -> "Git Bash Here".

4. Set up Git LFS and retore autocrlf sanity
```
git lfs install
git config --global core.autocrlf false
```

5. Clone the repo with your CDM GitLab credentials and enter the directory:
```
git clone https://gitlab.thecdm.ca/skyfall/fallingskies-2021.git
cd fallingskies-2021
```

6. Open the project in Unity and start hacking!


## Basic Git Commands

First, [get yourself familiar with Vim](https://www.howtoforge.com/vim-basics), as it's the default editor for Git.

```
# view current status (idempotent; use it whenever you want)
git status

# switch to a new branch for working on
git switch -c fix-player-death

# add "foo.cs" to the staging area (read to be commmited)
git add foo.cs

# add everything to the staging area ("." stands for current directory; use with caution!)
git add .

# commit and summerize your changes in the staging area
# (the default editor is vim. don't panic)
git commit

# commit and summerize your changes without an editor
git commit -m "fix a bug where player randomly dies"

# push your changes to CDM GitLab
git push -u origin fix-player-death
```

When in trouble, contact Zhuoyun Wei <zhuoyun_wei@thecdm.ca> with your `git status` screenshot.
