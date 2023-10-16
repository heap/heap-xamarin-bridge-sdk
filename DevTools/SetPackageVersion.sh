#!/bin/bash
# DevTools/SetPackageVersion.sh - Updates the PackageReference version in a csproj file.

set -o errexit

function realpath {
    [[ $1 = /* ]] && echo "$1" || echo "${PWD}/${1#./}"
}

SCRIPT_DIR="$(dirname "$(realpath "$0")")"

PACKAGE=$1
VERSION=$2
CSPROJ_FILE=$3

QUERY="//PackageReference[@Include='$PACKAGE']/Version/text()"
CURRENT_VERSION=$(xpath -q -e "${QUERY}" "${CSPROJ_FILE}")
XSLT_FILE=${SCRIPT_DIR}/Resources/set-version.xslt

if [[ "${VERSION}" != "${CURRENT_VERSION}" ]];
then
    xsltproc --stringparam package "${PACKAGE}" --stringparam version "${VERSION}" --output "${CSPROJ_FILE}" "${XSLT_FILE}" "${CSPROJ_FILE}"
    sed -i '' 's#"/>#" />#' "${CSPROJ_FILE}"
fi
