# Before going to any detail of this project, please REMEMBER:
* I, Toan Nguyen defined rules, so please don't argue if you're new to this project. **Our place, our rules!**
* We care to every detail of the source code like **spacing, comma, naming conventions and coding standards** so YOU (developers) should follow it strictly.

# Guidelines

* [Coding Best Practices](./docs/BestPractices/BestPractices.md)
* [Report Guideline](./docs/Report/Report.md)
* [How to build project from source](./docs/BestPractices/BuildFromSource.md)
* [Principle of reference resolution](./docs/BestPractices/BuildFromSource.md)

# Common build tasks

There are serveral tasks that we usually use:

```
.\build.cmd /t:TASKNAME
```

The TASKNAME is described as below:

**Task**                            | **Description**                                                                    | **Sample**
------------------------------------|------------------------------------------------------------------------------------|-----------------------------------------
Cow                                 | Everyone should try it to see the gift!                                            | ``` .\build.cmd /t:Cow ```
RepoTasksNoop                       | Just a noop to test custom tasks                                                   | ``` .\build.cmd /t:RepoTasksNoop ```
GenerateProjectList                 | Generate ProjectReferences.props                                                   | ``` .\build.cmd /t:GenerateProjectList ```
GenerateDependenciesPropsFile       | Generate dependencies.props                                                        | ``` .\build.cmd /t:GenerateDependenciesPropsFile ```
CheckCsProjectFormat                | Validate .csproj format to ensure no PackageReference                              | ``` .\build.cmd /t:CheckCsProjectFormat ```

Sample:
```
.\build.cmd /t:Cow
```

# Common build parameters:

**Parameter**                       | **Description**
------------------------------------|------------------------------------------------------------------------------------
/bl                                 | Generate binlog file for debugging purpose. ``` .\build.cmd /t:Cow /bl```
-Verbose                            | Write verbose log ``` .\build.cmd /t:Cow -Verbose```
/verbosity:diagnostic               | Set diagnostic level for logging. ``` .\build.cmd /t:Cow /verbosity:diagnostic```

# Install and build Fas Service
* If you have an error for build Thunder.Timesheet.Connector.Fas like this
"C:\Program Files\dotnet\sdk\3.0.100\Microsoft.Common.CurrentVersion.targets(2726,5): error MSB4216: Could not run the "ResolveComReference"
task because MSBuild could not create or connect to a task host with runtime "CLR4" and architecture "x86".  Please ensure that (1) the requested runtime and/or architecture are available on the machine,
and (2) that the required executable "C:\Program Files\dotnet\sdk\3.0.100\MSBuild.exe" exists and can be run. [*\ThunderPlatform\platform\Services\Timesheet.Connector.Fas\Thunder.Timesheet.Connector.Fas.csproj]"

* Go to "*\Timesheet.Connector.Fas\ExternalLibs"
* Copy file "standalone+sdk-6.3.1.37.zip" to another location and upzip it
* Run "Register_SDK_x86.bat" in 32bit and 64bit folder as Administration
* Rebuild solution using cmd: "build.cmd -r -rebuild"

