# cuddly-crustacean
Simple Search API Example

# Running The code
Code Can be run and debugged in numerous ways.  It was setup using the VS .NETCore WebApplication template with Docker tooling enabled,
in order to allow for debugging of the hosted runtime within a container.

.NETCore CLI
1. Navigate to the cloned project Directory.
2. change directory to the `\ProductApi\` directory
3. Run `dotnet run`

Tests can also be done in the same way by running `dotnet test` from the root level directory

Visual Studio 
* Build and Debug Targeting IISExpress
OR
* Build and Debug Targeting Docker

Run against Docker or IIS express, then use your httpclient of choice to prod the project.
