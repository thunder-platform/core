@ECHO OFF
SET RepoRoot=%~dp0..\..\
%RepoRoot%build.cmd -projects %RepoRoot%platform\Platform\**\*.csproj %*