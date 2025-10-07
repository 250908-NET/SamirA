#!/usr/bin/env bash
set -euo pipefail

# ---- Config ----
SOLUTION_NAME="FrenchTutor"

echo "🌱 Creating solution '${SOLUTION_NAME}'..."

# Create solution folder and enter it
mkdir -p "${SOLUTION_NAME}"
cd "${SOLUTION_NAME}"

# Create solution file
dotnet new sln -n "${SOLUTION_NAME}"

echo "📦 Creating projects..."

# Projects
dotnet new webapi        -n Api            -o src/Api              --framework net9.0 --use-controllers false
dotnet new classlib      -n Domain         -o src/Domain           --framework net9.0
dotnet new classlib      -n Infrastructure -o src/Infrastructure   --framework net9.0
dotnet new xunit         -n Tests          -o tests/Tests          --framework net9.0

echo "🧷 Wiring projects into the solution..."

# Add projects to solution
dotnet sln add src/Api src/Domain src/Infrastructure tests/Tests

# Set project references
dotnet add src/Api reference src/Domain src/Infrastructure
dotnet add src/Infrastructure reference src/Domain
dotnet add tests/Tests reference src/Domain

echo "📥 Restoring packages and building..."
dotnet restore
dotnet build

echo
echo "✅ Done!"
echo "➡️  Next steps:"
echo "   1) cd ${SOLUTION_NAME}"
echo "   2) Run the API: dotnet run --project src/Api"
echo "   3) Open http://localhost:5084/swagger (port may vary; check console output)"
