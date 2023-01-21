#!/bin/sh

#  appcenter-post-build.sh

echo "Starting post build script"

filename="android-buildscripts.sh"

source "${PWD}/${filename}"

postbuild

echo "Finished post build script"
