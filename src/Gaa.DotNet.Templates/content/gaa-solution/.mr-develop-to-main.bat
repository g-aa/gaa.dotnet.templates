@echo off
git checkout main --
git pull -v --progress "origin"
git merge --ff-only remotes/origin/develop
git push -v --progress "origin" main:main
git checkout develop --
git pull -v --progress "origin"