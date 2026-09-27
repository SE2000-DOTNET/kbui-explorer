# Preferred layout: Cursor classic IDE (chat/agent on the right, not Glass).
# Use on startup preference, after layout resets, or when asked to
# "restore preferred layout". Alias: Restore-Preferred-Layout.ps1
$repo = Split-Path -Parent $MyInvocation.MyCommand.Path
$cursor = Join-Path $env:LOCALAPPDATA "Programs\cursor\resources\app\bin\cursor.cmd"
if (-not (Test-Path $cursor)) {
    Write-Error "Cursor CLI not found at $cursor"
    exit 1
}
& $cursor -r --classic $repo
