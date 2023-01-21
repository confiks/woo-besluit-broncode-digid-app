#!/usr/bin/env bash

blobstorageurl="SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS"
filename="ios-buildscripts.sh"

echo "Start executing pre-build script"
echo "Agent job status: "$AGENT_JOBSTATUS

echo "Downloading buildscript from ${blobstorageurl}${filename}"

if curl --silent -o "${PWD}/${filename}" "${blobstorageurl}${filename}"; then

    echo "Download completed"
    if [[ -f "${PWD}/${filename}" ]]; then

        source "${PWD}/${filename}"
        
        prebuild
        
    fi
else
    echo "Something went wrong"
    exit 1
fi

echo "Completed pre-build script"
