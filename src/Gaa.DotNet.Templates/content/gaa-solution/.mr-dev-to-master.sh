#!/usr/bin/env bash

git checkout master --
git pull -v --progress "origin"
git merge --ff-only remotes/origin/develop
git push -v --progress "origin"master:master
git checkout develop --
git pull -v --progress "origin"