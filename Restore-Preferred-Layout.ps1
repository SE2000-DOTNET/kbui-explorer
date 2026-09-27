# Restores this repo's preferred layout: Cursor classic IDE
# (editor + explorer left, AI chat/agent on the right — not Glass/Agents Window).
# Same behavior as Open-Cursor-Classic-IDE.ps1.
$repo = Split-Path -Parent $MyInvocation.MyCommand.Path
& (Join-Path $repo "Open-Cursor-Classic-IDE.ps1")
